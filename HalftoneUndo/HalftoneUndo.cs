using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

using static DCEStudyTools.Utils.Getter.RvtElementGetter;

namespace DCEStudyTools.HalftoneUndo
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class HalftoneUndo : IExternalCommand
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
                // Get list of all CAD files
                IList<ImportInstance> cadFileLinksList = GetAllCADFiles(_doc);
                if (cadFileLinksList.Count == 0){ return Result.Cancelled; }

                // Get list of all views
                IList<ViewPlan> viewPlanList = GetAllStructuralPlans(_doc, true);
                if (viewPlanList.Count == 0){ return Result.Cancelled; }

                foreach (ViewPlan view in viewPlanList) // Loop through each view
                {
                    foreach (ImportInstance cadFile in cadFileLinksList)    // On each view, loop through each CAD file
                    {
                        OverrideGraphicSettings ogs = view.GetElementOverrides(cadFile.Id);
                        //Set Halftone Element
                        using (Transaction tx = new Transaction(_doc))
                        {
                            tx.Start("Undo Halftone");
                            ogs.SetHalftone(false);
                            view.SetElementOverrides(cadFile.Id, ogs);
                            tx.Commit();
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
