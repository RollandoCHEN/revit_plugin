using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                foreach (ViewPlan view in viewPlanList)
                {
                    foreach (ImportInstance cadFile in cadFileLinksList)
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
