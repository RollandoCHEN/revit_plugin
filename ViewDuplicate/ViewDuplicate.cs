using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using DCEStudyTools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

using static DCEStudyTools.Utils.Getter.RvtElementGetter;

namespace DCEStudyTools.ViewDuplicate
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class ViewDuplicate : IExternalCommand
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
                // Get list of all structural levels
                IList<Level> strLevels = GetAllLevels(_doc, true, true);
                if (strLevels.Count == 0){ return Result.Cancelled; }

                // Get list of all zone de définition
                IList<Element> zoneList = GetAllBuildingZones(_doc);
                if (zoneList.Count == 0){ return Result.Cancelled; }

                int zoneCount = zoneList.Count;  // Num of duplicata

                // Get the selected view
                ICollection<ElementId> refIds = _uidoc.Selection.GetElementIds();

                if (refIds.Count == 0)
                {
                    TaskDialog.Show("Revit", "Selecionner au moins d'une vue à dupliquer");
                    return Result.Cancelled;
                }


                foreach (ElementId refId in refIds)
                {
                    Element ele = _doc.GetElement(refId);
                    if (ele.Category == null || !ele.Category.Name.Equals(Properties.Settings.Default.CATEGORY_NAME_VIEW))
                    {
                        TaskDialog.Show("Revit", "Les élément selectionnés doivent être des VUES");
                        return Result.Cancelled;
                    }

                    View view = ele as View;
                    ViewFamilyType viewType = _doc.GetElement(view.GetTypeId()) as ViewFamilyType;
                    if (!viewType.FamilyName.Equals(Properties.Settings.Default.FAMILY_TYPE_NAME_STR_PLAN))
                    {
                        TaskDialog.Show("Revit", $"Les élément selectionnés doivent être des VUES EN PLAN");
                        return Result.Cancelled;
                    }
                }

                foreach (ElementId refId in refIds)
                {
                    ViewPlan viewToDuplicate = _doc.GetElement(refId) as ViewPlan;
                    Level level = viewToDuplicate.GenLevel;
                    String levelName = level.Name;
                    Transaction t = new Transaction(_doc, "Duplicate view");
                    t.Start();
                    for (int i = 0; i < zoneCount; i++)
                    {
                        ElementId viewId = viewToDuplicate.Duplicate(ViewDuplicateOption.AsDependent);
                        ViewPlan duplicatedView = _doc.GetElement(viewId) as ViewPlan;
                        duplicatedView.Name = $"{levelName} - {zoneList[i].Name}";
                        duplicatedView.get_Parameter(BuiltInParameter.VIEWER_VOLUME_OF_INTEREST_CROP).Set(zoneList[i].Id);

                        // Associate level to the new viewplan
                        AssociateLevelToNewViewPlan(level, duplicatedView);
                    }
                    t.Commit();
                }

                return Result.Succeeded;
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }
        }

        private void AssociateLevelToNewViewPlan(Level level, ViewPlan view)
        {
            SchemaBuilder builder = new SchemaBuilder(Guids.VIEW_PLAN_SHEMA_GUID);

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
    }
}
