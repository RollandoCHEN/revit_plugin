using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using DCEStudyTools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

using static DCEStudyTools.Utils.Getter.RvtElementGetter;
using static DCEStudyTools.Properties.Settings;

namespace DCEStudyTools.FoundationLoadCreation
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class FoundationLoadCreation : IExternalCommand
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            try
            {
                View activeV = _doc.ActiveView;

                IList<FamilyInstance> foundations = GetAllPileFoundations(_doc);

                foreach (FamilyInstance foundation in foundations)
                {
                    BoundingBoxXYZ fBounding = foundation.get_BoundingBox(_doc.ActiveView);
                    XYZ fCenter = (fBounding.Max + fBounding.Min) / 2.0;
                    XYZ loadPosition = new XYZ(fCenter.X, fCenter.Y, fBounding.Max.Z);
                    XYZ force = new XYZ(0, 0, 1);
                    XYZ moment = new XYZ(0, 0, 0);
                    Level fLevel = _doc.GetElement(foundation.LevelId) as Level;

                    PointLoad pl;

                    if (foundation.GetEntitySchemaGuids().Count != 0)
                    {
                        Schema foundationSchema = Schema.Lookup(Guids.FOUNDATION_SHEMA_GUID);
                        Entity foundationSchemaEnt = foundation.GetEntity(foundationSchema);
                        ElementId loadId = foundationSchemaEnt.Get<ElementId>(foundationSchema.GetField("PointLoad"));
                        if (loadId == ElementId.InvalidElementId)
                        {
                            Transaction t = new Transaction(_doc);
                            t.Start("Delete entity of foundation");
                            foundation.DeleteEntity(foundationSchema);
                            t.Commit();
                            pl = CreateNewLoadForFoundation(foundation, loadPosition, force, moment);
                        }
                        else
                        {
                            pl = _doc.GetElement(loadId) as PointLoad;
                        }
                    }
                    else
                    {
                        pl = CreateNewLoadForFoundation(foundation, loadPosition, force, moment);
                    }

                    if (pl.GetEntitySchemaGuids().Count != 0)
                    {
                        Schema pointLoadSchema = Schema.Lookup(Guids.POINTLOAD_SHEMA_GUID);
                        Entity pointLoadSchemaEnt = pl.GetEntity(pointLoadSchema);
                        ElementId tagId = pointLoadSchemaEnt.Get<ElementId>(pointLoadSchema.GetField("Tag"));
                        if (tagId == ElementId.InvalidElementId)
                        {
                            Transaction t = new Transaction(_doc);
                            t.Start("Delete entity of point load");
                            pl.DeleteEntity(pointLoadSchema);
                            t.Commit();
                            CreateNewTagForPointLoad(activeV, loadPosition, pl);
                        }
                    }
                    else
                    {
                        CreateNewTagForPointLoad(activeV, loadPosition, pl);
                    }
                }
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }

            return Result.Succeeded;
        }

        private PointLoad CreateNewLoadForFoundation(FamilyInstance foundation, XYZ loadPosition, XYZ force, XYZ moment)
        {
            PointLoad pl;
            Transaction t = new Transaction(_doc);
            t.Start("Creat point load");
            pl = PointLoad.Create(_doc, loadPosition, force, moment, null, null);
            t.Commit();
            AssociateLoadToFoundation(foundation, pl);
            return pl;
        }

        private void CreateNewTagForPointLoad(View activeV, XYZ loadPosition, PointLoad pl)
        {
            Transaction t = new Transaction(_doc);
            t.Start("Create tag for point load");
            TagMode tagMode = TagMode.TM_ADDBY_CATEGORY;
            TagOrientation tagorn = TagOrientation.Horizontal;

            IndependentTag tag = _doc.Create.NewTag(activeV, pl, false, tagMode, tagorn, loadPosition);
            FamilySymbol tagType =
                (from fs in new FilteredElementCollector(_doc)
                    .OfCategory(BuiltInCategory.OST_PointLoadTags)
                    .OfClass(typeof(FamilySymbol))
                    .Cast<FamilySymbol>()
                 where fs.Name.Equals(Default.TYPE_NAME_POINT_LOAD)
                 select fs)
                    .FirstOrDefault();
            if (tagType != null)
            {
                tag.ChangeTypeId(tagType.Id);
            }
            t.Commit();
            AssociateTagToPointLoad(pl, tag);
        }

        private void AssociateTagToPointLoad(PointLoad pl, IndependentTag tag)
        {
            using (Transaction trans = new Transaction(_doc, "Create Extensible Store"))
            {
                trans.Start();

                SchemaBuilder builder = new SchemaBuilder(Guids.POINTLOAD_SHEMA_GUID);

                builder.SetReadAccessLevel(AccessLevel.Public);
                builder.SetWriteAccessLevel(AccessLevel.Public);

                builder.SetSchemaName("Tag");

                builder.SetDocumentation("The tag linked to the point load");

                // Create field1
                FieldBuilder fieldBuilder1 = builder.AddSimpleField("Tag", typeof(ElementId));

                // Register the schema object
                Schema schema = builder.Finish();

                Field tagField = schema.GetField("Tag");
                Entity ent = new Entity(schema);

                ent.Set(tagField, tag.Id);
                pl.SetEntity(ent);

                trans.Commit();
            }
        }

        public void AssociateLoadToFoundation(FamilyInstance foundation, PointLoad pointLoad)
        {
            using (Transaction trans = new Transaction(_doc, "Create Extensible Store"))
            {
                trans.Start();

                SchemaBuilder builder = new SchemaBuilder(Guids.FOUNDATION_SHEMA_GUID);

                builder.SetReadAccessLevel(AccessLevel.Public);
                builder.SetWriteAccessLevel(AccessLevel.Public);

                builder.SetSchemaName("PointLoad");

                builder.SetDocumentation("The point load linked to the foundation");

                // Create field1
                FieldBuilder fieldBuilder1 = builder.AddSimpleField("PointLoad", typeof(ElementId));

                // Register the schema object
                Schema schema = builder.Finish();

                Field pointLoadField = schema.GetField("PointLoad");
                Entity ent = new Entity(schema);

                ent.Set(pointLoadField, pointLoad.Id);

                foundation.SetEntity(ent);

                trans.Commit();
            }
        }
    }
}
