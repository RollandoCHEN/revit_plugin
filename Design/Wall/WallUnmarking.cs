using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using DCEStudyTools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DCEStudyTools.Design.Wall
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class WallUnmarking : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication _uiapp = commandData.Application;
            UIDocument _uidoc = _uiapp.ActiveUIDocument;
            Document _doc = _uidoc.Document;
            
            Autodesk.Revit.DB.View activeV = _doc.ActiveView;

            SelectionFilterElement filterElement = new FilteredElementCollector(_doc)
                        .OfClass(typeof(SelectionFilterElement)).Cast<SelectionFilterElement>()
                        .Where(sf => sf.Name.Equals("Voiles Porteurs"))
                        .FirstOrDefault();

            using (Transaction t = new Transaction(_doc, "Remove walls from filter"))
            {
                t.Start();

                bool filterExistsInActiveView = false;
                if (activeV.GetFilters().Count != 0)
                {
                    foreach (ElementId id in activeV.GetFilters())
                    {
                        if (_doc.GetElement(id).Name.Equals("Voiles Porteurs"))
                        {
                            filterExistsInActiveView = true;
                        }
                    }
                }

                if (!filterExistsInActiveView)
                {
                    MessageBox.Show("No filter is added to the active view!");
                    return Result.Cancelled;
                }
                else
                {
                    IList<Reference> refIds;
                    try
                    {
                        refIds = _uidoc.Selection.PickObjects(ObjectType.Element,
                            new WallSelectionFilter(activeV.GenLevel.Id, filterElement));
                    }
                    catch (OperationCanceledException)
                    {
                        return Result.Cancelled;
                    }
                    catch (Exception)
                    {
                        return Result.Failed;
                    }

                    foreach (Reference refe in refIds)
                    {
                        filterElement.RemoveSingle(refe.ElementId);
                    }

                    activeV.RemoveFilter(filterElement.Id);
                    ViewTemplate.AddFilterToView(
                        _doc, activeV, filterElement,
                        new Color(000, 128, 255), new string[] { "Uni", "Solid fill" },
                        new Color(000, 128, 255), new string[] { "Uni", "Solid fill" });
                }
                t.Commit();
            }

            _uidoc.RefreshActiveView();

            return Result.Succeeded;
        }
    }
}
