using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

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

                // Set offset to 0 for each CAD file
                foreach (ImportInstance cad in cadFileLinksList)
                {
                    using (Transaction tx = new Transaction(_doc, "Set offset for each CAD file"))
                    {
                        tx.Start();
                        cad.get_Parameter(BuiltInParameter.IMPORT_BASE_LEVEL_OFFSET).Set(0);
                        tx.Commit();
                    }
                }

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
