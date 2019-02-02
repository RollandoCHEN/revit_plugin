using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                // TODO : Extract GetAllStructuralLevels the methode
                // Get list of all structural levels
                IList<Level> strLevels =
                    (from lev in new FilteredElementCollector(_doc)
                    .OfClass(typeof(Level))
                     where lev.GetEntitySchemaGuids().Count != 0
                     select lev)
                    .Cast<Level>()
                    .OrderBy(l => l.Elevation)
                    .ToList();

                if (strLevels.Count == 0)
                {
                    TaskDialog.Show("Revit", "Configurer les niveaux structuraux avant de lancer cette commande.");
                    return Result.Cancelled;
                }

                // Get list of all zone de définition
                List<Element> zoneList =
                    new FilteredElementCollector(_doc)
                    .OfCategory(BuiltInCategory.OST_VolumeOfInterest)
                     .Cast<Element>()
                     .ToList();

                if (zoneList.Count == 0)
                {
                    TaskDialog.Show("Revit", "Aucune zone de définition dans le projet");
                    return Result.Cancelled;
                }

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
                        TaskDialog.Show("Revit", "Les élément selectionnés doivent être des VUES EN PLAN");
                        return Result.Cancelled;
                    }

                    View view = ele as View;
                    if (!view.GetType().Name.Equals(Properties.Settings.Default.FAMILY_TYPE_NAME_STR_PLAN))
                    {
                        TaskDialog.Show("Revit", "Les élément selectionnés doivent être des VUES EN PLAN");
                        return Result.Cancelled;
                    }
                }

                foreach (ElementId refId in refIds)
                {
                    View viewToDuplicate = (View)_doc.GetElement(refId);
                    String levelName = viewToDuplicate.GenLevel.Name;
                    Transaction t = new Transaction(_doc, "Duplicate view");
                    t.Start();
                    for (int i = 0; i < zoneCount; i++)
                    {
                        ElementId viewId = viewToDuplicate.Duplicate(ViewDuplicateOption.AsDependent);
                        View duplicatedView = _doc.GetElement(viewId) as View;
                        duplicatedView.Name = $"{levelName} - {zoneList[i].Name}";
                        duplicatedView.get_Parameter(BuiltInParameter.VIEWER_VOLUME_OF_INTEREST_CROP).Set(zoneList[i].Id);
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
    }
}
