using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DCEStudyTools.CADLink
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class CADLink : IExternalCommand
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
                // Get list of all structural view plans
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

                Dictionary<string, ViewPlan> viewDic = new Dictionary<string, ViewPlan>();
                foreach (ViewPlan vp in viewPlanList)
                {
                    // filter structural view plan
                    Regex regex = new Regex(@"r\+\d|rdc|ss\d|ss\-\d");
                    Match match = regex.Match(vp.Name.ToLower());
                    // TODO : to be improved
                    if (match.Success && vp.Name.Contains("PH"))
                    {
                        if (!viewDic.ContainsKey(match.Value))
                        {
                            viewDic.Add(match.Value.ToLower(), vp);
                        }
                    }
                }

                CADLinkForm form = new CADLinkForm(viewDic);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    DWGImportOptions opt = new DWGImportOptions
                    {
                        Placement = ImportPlacement.Origin,
                        AutoCorrectAlmostVHLines = true,
                        ThisViewOnly = false,
                        Unit = ImportUnit.Default,
                    };
                    
                    ElementId linkId = ElementId.InvalidElementId;
                    // add links
                    foreach (DataGridViewRow row in form.DataGridView.Rows)
                    {
                        string keyword = row.Cells[0].Value.ToString();
                        if (!string.IsNullOrEmpty(keyword))
                        {
                            ViewPlan view = viewDic[keyword];
                            string filePath = row.Cells[2].Value.ToString();
                            using (Transaction tran = new Transaction(_doc, "Quick Link"))
                            {
                                tran.Start();
                                _doc.Link(filePath, opt, view, out linkId);
                                tran.Commit();
                            }
                        }
                    }
                    return Result.Succeeded;
                }
                else
                {
                    return Result.Cancelled;
                }

            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }
        }
    }
}
