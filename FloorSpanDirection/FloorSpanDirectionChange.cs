using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCEStudyTools.FloorSpanDirection
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class FloorSpanDirectionChange : IExternalCommand
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;
            
            View activeView = _doc.ActiveView;

            FloorDirectionForm form = new FloorDirectionForm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    IList<Reference> refIds = 
                        _uidoc.Selection.PickObjects(
                            ObjectType.Element,
                            new FloorSelectionFilter(activeView.GenLevel.Id));

                    foreach (var refId in refIds)
                    {
                        Floor floor = _doc.GetElement(refId) as Floor;
                        Transaction t = new Transaction(_doc);
                        t.Start("Change span direction");
                        floor.SpanDirectionAngle = form.SelectedSpanDirectionAngle;
                        t.Commit();
                    }
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                {
                    return Result.Cancelled;
                }
                return Result.Succeeded;
            }
            else
            {
                return Result.Cancelled;
            }
            
        }


        public bool IsElementVisibleInView(
          View view,
          Element el)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            if (el == null)
            {
                throw new ArgumentNullException(nameof(el));
            }

            // Obtain the element's document.

            Document doc = el.Document;

            ElementId elId = el.Id;

            // Create a FilterRule that searches 
            // for an element matching the given Id.

            FilterRule idRule = ParameterFilterRuleFactory
              .CreateEqualsRule(
                new ElementId(BuiltInParameter.ID_PARAM),
                elId);

            var idFilter = new ElementParameterFilter(idRule);

            // Use an ElementCategoryFilter to speed up the 
            // search, as ElementParameterFilter is a slow filter.

            Category cat = el.Category;
            var catFilter = new ElementCategoryFilter(cat.Id);

            // Use the constructor of FilteredElementCollector 
            // that accepts a view id as a parameter to only 
            // search that view.
            // Also use the WhereElementIsNotElementType filter 
            // to eliminate element types.

            FilteredElementCollector collector =
                new FilteredElementCollector(doc, view.Id)
                  .WhereElementIsNotElementType()
                  .WherePasses(catFilter)
                  .WherePasses(idFilter);

            // If the collector contains any items, then 
            // we know that the element is visible in the
            // given view.

            return collector.Any();
        }
    }
}
