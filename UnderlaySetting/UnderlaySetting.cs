using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCEStudyTools.UnderlaySetting
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class UnderlaySetting : IExternalCommand
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

                // Get list of all CAD files
                IList<ImportInstance> cadFileLinksList =
                    new FilteredElementCollector(_doc)
                    .OfClass(typeof(ImportInstance))
                    .Cast<ImportInstance>()
                    .ToList();

                if (cadFileLinksList.Count == 0)
                {
                    TaskDialog.Show("Revit", "No dwg file is found in the document.");
                    return Result.Cancelled;
                }

                // Get list of all views
                IList<ViewPlan> viewPlanList =
                            (from ViewPlan view in new FilteredElementCollector(_doc)
                            .OfClass(typeof(ViewPlan))
                             where view.ViewType == ViewType.CeilingPlan && !view.IsTemplate
                             select view)
                            .Cast<ViewPlan>()
                            .ToList();

                if (viewPlanList.Count == 0)
                {
                    TaskDialog.Show("Revit", "No view plan is found in the document.");
                    return Result.Cancelled;
                }

                // Set offset to -0.1m for each CAD file
                foreach (ImportInstance cad in cadFileLinksList)
                {
                    using (Transaction tx = new Transaction(_doc, "Set offset for each CAD file"))
                    {
                        tx.Start();
                        cad.get_Parameter(BuiltInParameter.IMPORT_BASE_LEVEL_OFFSET).Set(
                        UnitUtils.Convert(-1, DisplayUnitType.DUT_DECIMETERS, DisplayUnitType.DUT_DECIMAL_FEET));
                        tx.Commit();
                    }
                }

                foreach (ViewPlan view in viewPlanList)
                {
                    Level viewLvl = view.GenLevel;
                    Level supLvl = strLevels.Where(lvl => lvl.Elevation > viewLvl.Elevation).OrderBy(l => l.Elevation).FirstOrDefault();
                    using (Transaction tx = new Transaction(_doc))
                    {
                        tx.Start("Underlay Range");
                        if (supLvl != null)
                        {
                            view.SetUnderlayRange(viewLvl.Id, supLvl.Id);
                        }
                        else
                        {
                            view.SetUnderlayBaseLevel(viewLvl.Id);
                        }
                        tx.Commit();
                    }
                }
                    
                return Result.Succeeded;
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }
        }
    }
}
