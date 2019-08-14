using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;

using static DCEStudyTools.Utils.Getter.RvtElementGetter;

namespace DCEStudyTools.UnderlayUndo
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class UnderlayUndo : IExternalCommand
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
                IList<ViewPlan> viewPlanList = GetAllLinkedViewPlans(_doc);
                if (viewPlanList.Count == 0) { return Result.Cancelled; }

                foreach (ViewPlan view in viewPlanList)
                {
                    using (Transaction tx = new Transaction(_doc))
                    {
                        tx.Start("Reset Underlay");
                        ElementId id = new ElementId(-1);
                        view.SetUnderlayRange(id, id);
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
