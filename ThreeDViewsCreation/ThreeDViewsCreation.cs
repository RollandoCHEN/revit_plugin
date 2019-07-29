using System.Collections.Generic;
using System.Linq;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.ObjectModel;
using DCEStudyTools.Utils;
using Autodesk.Revit.DB.ExtensibleStorage;

using static DCEStudyTools.Utils.RvtElementGetter;

namespace DCEStudyTools.ThreeDViewsCreation
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class ThreeDViewsCreation : IExternalCommand
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;

        private const double SECTION_BOX_XY_MARGIN = 10;                     // Feets
        private const double BOTTOM_OFFSET = 16.5;                           // Feets

        private const string THREED_VIEW_TEMPLATE_NAME = "DCE Structure 3D";

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            // Get list of all levels for structural elements
            IList<Level> strLevels = GetAllStructLevels(_doc);
            if (strLevels.Count == 0) { return Result.Cancelled; }

            // Get a ViewFamilyType for a 3D view
            ViewFamilyType viewFamilyType =
                (from v in new FilteredElementCollector(_doc)
                 .OfClass(typeof(ViewFamilyType))
                 .Cast<ViewFamilyType>()
                 where v.ViewFamily == ViewFamily.ThreeDimensional
                 select v).First();

            Autodesk.Revit.DB.View viewTemplate = ViewTemplate.FindViewTemplateOrDefault(_doc,
                ViewType.ThreeD,
                THREED_VIEW_TEMPLATE_NAME, 
                out bool templateIsFound);
            
            if (viewTemplate != null && !templateIsFound)     // Dont find the view template named as "DCE Structure 3D"
            {
                viewTemplate = CreateTemplateFor3DView(viewTemplate);
            }

            // Get all the 3D view created by the plugin
            IList<View3D> threeDViewList =
                (from v in new FilteredElementCollector(_doc)
                 .OfClass(typeof(View3D))
                 .Cast<View3D>()
                 where v.GetEntitySchemaGuids().Count != 0
                 select v)
                 .ToList();

            Transaction t = new Transaction(_doc);
            // loop through all levels, except the first one
            for (int highFloorLevelInd = 1; highFloorLevelInd < strLevels.Count; highFloorLevelInd++)
            {
                Level highFloorLevel = strLevels[highFloorLevelInd];

                t.Start("Create or update 3D view");

                View3D view;
                if (threeDViewList.Any(v => GetAssociateLevelOf3DView(v).Equals(highFloorLevel.Id)))
                {
                    view = (from v in threeDViewList
                            where GetAssociateLevelOf3DView(v).Equals(highFloorLevel.Id)
                            select v)
                            .First();
                }
                else
                {
                    // Create the 3d view
                    view = View3D.CreateIsometric(_doc, viewFamilyType.Id);
                    // Asociate the high-floor level to the 3d view
                    AssociateLevelTo3DView(highFloorLevel, view);
                }

                if (viewTemplate != null)
                {
                    // Apply the "DCE Structure 3D" view template to the created view
                    view.ViewTemplateId = viewTemplate.Id;
                }

                // Set the name of the view
                if (view.Name != highFloorLevel.Name)
                {
                    view.Name = highFloorLevel.Name;
                }

                // Get the bounding box of space between the high-floor and low-floor
                BoundingBoxXYZ boundingBoxXYZ = GetBoundingBoxOfLevel(strLevels, highFloorLevelInd, view);

                // Apply this bounding box to the view's section box
                view.SetSectionBox(boundingBoxXYZ);

                // Save orientation and lock the view
                view.SaveOrientationAndLock();

                t.Commit();

                // Open the just-created view
                // There cannot be an open transaction when the active view is set
                _uidoc.ActiveView = view;

            }
                
            return Result.Succeeded;
        }

        private BoundingBoxXYZ GetBoundingBoxOfLevel(IList<Level> strLevels, int highFloorLevelInd, View3D view)
        {
            Level highFloorLevel = strLevels[highFloorLevelInd];
            Level lowFloorLevel = strLevels[highFloorLevelInd - 1];

            // Get the outline of the entire 3D model 
            FilteredElementCollector fec = new FilteredElementCollector(_doc).OfClass(typeof(Wall));
            BoundingBoxXYZ ibb = fec.Cast<Wall>().First().get_BoundingBox(view);
            Outline outline = new Outline(ibb.Min, ibb.Max);
            foreach (Wall wall in fec)
            {
                BoundingBoxXYZ boxXYZ = wall.get_BoundingBox(view);
                outline.AddPoint(boxXYZ.Min);
                outline.AddPoint(boxXYZ.Max);
            }

            // Create a new BoundingBoxXYZ to define a 3D rectangular space
            BoundingBoxXYZ boundingBoxXYZ = new BoundingBoxXYZ();

            if (highFloorLevelInd == 1)
            {
                // Set the lower left botton corner of the box
                // To show the fondation, use the Z of the current level - BOTTOMOFFSET
                boundingBoxXYZ.Min = new XYZ(
                    outline.MinimumPoint.X - SECTION_BOX_XY_MARGIN,
                    outline.MinimumPoint.Y - SECTION_BOX_XY_MARGIN,
                    lowFloorLevel.ProjectElevation - BOTTOM_OFFSET);
            }
            else
            {
                // Use the Z of the current level.
                boundingBoxXYZ.Min = new XYZ(
                    outline.MinimumPoint.X - SECTION_BOX_XY_MARGIN,
                    outline.MinimumPoint.Y - SECTION_BOX_XY_MARGIN,
                    lowFloorLevel.ProjectElevation);
            }

            boundingBoxXYZ.Max = new XYZ(
                outline.MaximumPoint.X + SECTION_BOX_XY_MARGIN,
                outline.MaximumPoint.Y + SECTION_BOX_XY_MARGIN,
                highFloorLevel.ProjectElevation);

            return boundingBoxXYZ;
        }

        private Autodesk.Revit.DB.View CreateTemplateFor3DView(Autodesk.Revit.DB.View viewTemplate)
        {
            Transaction t = new Transaction(_doc);
            t.Start("Create view template 'DCE Structure 3D'");

            // Copy an existing view template
            ICollection<ElementId> copyIds = new Collection<ElementId>
                    {
                        viewTemplate.Id
                    };

            // Create a default CopyPasteOptions
            CopyPasteOptions cpOpts = new CopyPasteOptions();
            ElementId tmpId = ElementTransformUtils.CopyElements(
                _doc, copyIds, _doc, Transform.Identity, cpOpts)
                .First();

            viewTemplate = _doc.GetElement(tmpId) as Autodesk.Revit.DB.View;
            viewTemplate.Name = THREED_VIEW_TEMPLATE_NAME;

            t.Commit();

            // Ensure that the parameters are included in "DCE Structure 3D" view template
            List<BuiltInParameter> bipList = new List<BuiltInParameter>()
                    {
                        BuiltInParameter.VIEW_SCALE_PULLDOWN_METRIC,
                        BuiltInParameter.VIEW_DETAIL_LEVEL,
                        // BuiltInParameter.VIEW_PARTS_VISIBILITY,
                        BuiltInParameter.VIS_GRAPHICS_MODEL,
                        BuiltInParameter.VIS_GRAPHICS_ANNOTATION,
                        // BuiltInParameter.VIS_GRAPHICS_ANALYTICAL_MODEL,
                        // BuiltInParameter.VIS_GRAPHICS_IMPORT,
                        BuiltInParameter.VIS_GRAPHICS_FILTERS,
                        BuiltInParameter.VIS_GRAPHICS_RVT_LINKS,
                        BuiltInParameter.GRAPHIC_DISPLAY_OPTIONS_MODEL,
                        //BuiltInParameter.GRAPHIC_DISPLAY_OPTIONS_SHADOWS,
                        //BuiltInParameter.GRAPHIC_DISPLAY_OPTIONS_SKETCHY_LINES,
                        //BuiltInParameter.GRAPHIC_DISPLAY_OPTIONS_LIGHTING,
                        //BuiltInParameter.GRAPHIC_DISPLAY_OPTIONS_PHOTO_EXPOSURE,
                        //BuiltInParameter.GRAPHIC_DISPLAY_OPTIONS_BACKGROUND,
                        //BuiltInParameter.VIEW_PHASE_FILTER,
                        BuiltInParameter.VIEW_DISCIPLINE,
                        BuiltInParameter.VIEW_SHOW_HIDDEN_LINES,
                        //BuiltInParameter.VIEWER3D_RENDER_SETTINGS
                    };
            ViewTemplate.IncludParamToViewTemplate(_doc, viewTemplate, bipList);


            List<BuiltInCategory> bicList = new List<BuiltInCategory>
                    {
                        BuiltInCategory.OST_Walls,
                        BuiltInCategory.OST_StructuralColumns,
                        BuiltInCategory.OST_StructuralFraming,
                        BuiltInCategory.OST_Floors,
                        BuiltInCategory.OST_StructuralFoundation
                    };
            ViewTemplate.SetOnlyCategoriesVisible(_doc, viewTemplate, bicList);

            t.Start("Set view display paramters");
            // Set the scale value
            viewTemplate.Scale = 400;
            // Set the detail level
            viewTemplate.DetailLevel = ViewDetailLevel.Fine;
            // Set the display style
            viewTemplate.DisplayStyle = DisplayStyle.ShadingWithEdges;
            // Set the discipline
            viewTemplate.Discipline = ViewDiscipline.Structural;

            OverrideGraphicSettings ogs = new OverrideGraphicSettings();
            ogs.SetSurfaceTransparency(50);
            ogs.SetCutFillPatternVisible(false);
            viewTemplate.SetCategoryOverrides(
                _doc.Settings.Categories.get_Item(BuiltInCategory.OST_Floors).Id,
                ogs);

            // Remove all the filter in the view template
            foreach (ElementId id in viewTemplate.GetFilters())
            {
                viewTemplate.RemoveFilter(id);
            }
            t.Commit();
            return viewTemplate;
        }

        private void AssociateLevelTo3DView(Level level, View3D view)
        {
            SchemaBuilder builder = new SchemaBuilder(Guids.THREE_D_VIEW_SHEMA_GUID);

            builder.SetReadAccessLevel(AccessLevel.Public);
            builder.SetWriteAccessLevel(AccessLevel.Public);

            builder.SetSchemaName("AssociatedLevel");

            builder.SetDocumentation("Associated level");

            // Create field1
            FieldBuilder fieldBuilder1 = builder.AddSimpleField("Level", typeof(ElementId));

            // Register the schema object
            Schema schema = builder.Finish();

            Field levelId = schema.GetField("Level");
                
            Entity ent = new Entity(schema);

            ent.Set(levelId, level.Id);

            view.SetEntity(ent);
        }

        private ElementId GetAssociateLevelOf3DView(View3D view)
        {
            Schema threeDViewSchema = Schema.Lookup(Guids.THREE_D_VIEW_SHEMA_GUID);
            Entity threeDViewSchemaEnt = view.GetEntity(threeDViewSchema);
            ElementId levelId = threeDViewSchemaEnt.Get<ElementId>(threeDViewSchema.GetField("Level"));
            return levelId;
        }
    }
}
