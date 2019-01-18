using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using DCEStudyTools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCEStudyTools.Design.Wall
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class WallMarking : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication _uiapp = commandData.Application;
            UIDocument _uidoc = _uiapp.ActiveUIDocument;
            Document _doc = _uidoc.Document;
            
            View activeV = _doc.ActiveView;

            SelectionFilterElement filterElement = new FilteredElementCollector(_doc)
                        .OfClass(typeof(SelectionFilterElement)).Cast<SelectionFilterElement>()
                        .Where(sf => sf.Name.Equals("Voiles Porteurs"))
                        .FirstOrDefault();

            Transaction t = new Transaction(_doc);
            t.Start("Creat filter");

            // Dont find the filter in the doc, create a new filter
            if (filterElement == null)
            {
                try
                {
                    filterElement = SelectionFilterElement.Create(_doc, "Voiles Porteurs");
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    return Result.Failed;
                }
            }
            t.Commit();
                
            // The filter is not added to the active view, add it to the view
            //bool filterExistsInActiveView = 
            //    activeV.GetFilters().Any(id => _doc.GetElement(id).Name.Equals("Voiles Porteurs"));
            //if (!filterExistsInActiveView)
            //{
            //    ViewTemplate.AddFilterToView(
            //        _doc, activeV, filterElement,
            //        new Color(000, 128, 255), new string[] { "Uni", "Solid fill" },
            //        new Color(000, 128, 255), new string[] { "Uni", "Solid fill" });
            //}

            // Add selected walls to the filter
            IList<Reference> refIds;
            try
            {
                refIds = _uidoc.Selection.PickObjects(ObjectType.Element, 
                    new WallSelectionFilter(activeV.GenLevel.Id));
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }

            t.Start("Add walls to filter");
            foreach (Reference refe in refIds)
            {
                filterElement.AddSingle(refe.ElementId);
            }
            t.Commit();

            ViewTemplate.RemoveFilterFromViewOrTemplate(_doc, activeV, filterElement);

            ViewTemplate.AddFilterToViewOrTemplate(
                _doc, activeV, filterElement,
                new Color(000, 128, 255), new string[] { "Uni", "Solid fill" },
                new Color(000, 128, 255), new string[] { "Uni", "Solid fill" });

            _uidoc.RefreshActiveView();
            

            return Result.Succeeded;
        }
    }
}
