using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

using static DCEStudyTools.Utils.Getter.RvtElementGetter;

namespace DCEStudyTools.HalftoneSetting
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class HalftoneSetting : IExternalCommand
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
                IList<Level> strLevels = GetAllLevels(_doc, true);
                if (strLevels.Count == 0){ return Result.Cancelled; }

                // Get list of all CAD files
                IList<ImportInstance> cadFileLinksList = GetAllCADFiles(_doc);
                if (cadFileLinksList.Count == 0){ return Result.Cancelled; }

                // Get list of all views
                IList<ViewPlan> viewPlanList = GetAllLinkedViewPlans(_doc);
                if (viewPlanList.Count == 0){return Result.Cancelled; }

                foreach (ViewPlan view in viewPlanList) // Loop through each view
                {
                    Level viewLvl = view.GenLevel;
                    Level supLvl = strLevels.Where(lvl => lvl.Elevation > viewLvl.Elevation).OrderBy(l => l.Elevation).FirstOrDefault();

                    foreach (ImportInstance cadFile in cadFileLinksList)    // On each view, loop through each CAD file
                    {
                        if (supLvl != null && cadFile.LevelId == supLvl.Id) // super level exists and CAD file is on super level
                        {
                            OverrideGraphicSettings ogs = view.GetElementOverrides(cadFile.Id);
                            //Set Halftone Element
                            using (Transaction tx = new Transaction(_doc))
                            {
                                tx.Start("Halftone");
                                ogs.SetHalftone(true);
                                view.SetElementOverrides(cadFile.Id, ogs);
                                tx.Commit();
                            }
                        }
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
