using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

using static DCEStudyTools.Utils.Getter.RvtElementGetter;

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
                IList<Level> strLevels = GetAllLevels(_doc, true, true);
                if (strLevels.Count == 0) { return Result.Cancelled; }

                // Get list of all CAD files
                IList<ImportInstance> cadFileLinksList = GetAllCADFiles(_doc);
                if (cadFileLinksList.Count == 0) { return Result.Cancelled; }

                // Get list of all views
                IList<ViewPlan> viewPlanList = GetAllStructuralPlans(_doc, true);
                if (viewPlanList.Count == 0) { return Result.Cancelled; }

                // Set offset to -0.1m for each CAD file
                foreach (ImportInstance cad in cadFileLinksList)
                {
                    using (Transaction tx = new Transaction(_doc, "Set offset for each CAD file"))
                    {
                        tx.Start();
                        cad.get_Parameter(BuiltInParameter.IMPORT_BASE_LEVEL_OFFSET).Set(
                        UnitUtils.Convert(-0.1, DisplayUnitType.DUT_METERS, DisplayUnitType.DUT_DECIMAL_FEET));
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
