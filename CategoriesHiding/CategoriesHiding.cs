using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace DCEStudyTools.CategoriesHiding
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class CategoriesHiding : IExternalCommand
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            Autodesk.Revit.DB.View actView = _doc.ActiveView;

            List<Element> elemList = new List<Element>();
            Autodesk.Revit.DB.View template = null;

            if (actView.ViewTemplateId != ElementId.InvalidElementId)
            {
                template = _doc.GetElement(actView.ViewTemplateId) as Autodesk.Revit.DB.View;
            }

            bool VGCtrlByTmp = false;
            if (template != null)    // The view is controlled by a view template
            {
                List<ElementId> nonControledParamsIds = template.GetNonControlledTemplateParameterIds().ToList();
                // graphics visibility param is included in the view template
                VGCtrlByTmp = !nonControledParamsIds.Any(
                    id => id.IntegerValue == (int)BuiltInParameter.VIS_GRAPHICS_MODEL
                    );
            }

            IList<Reference> select = new List<Reference>();
            try
            {
                select = _uidoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element);
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }

            using (Transaction t = new Transaction(_doc, "Hide categories"))
            {
                t.Start();
                foreach (Reference elemRef in select)
                {
                    Element elem = _doc.GetElement(elemRef);
                    if (VGCtrlByTmp)
                    {
                        if(template.CanCategoryBeHidden(elem.Category.Id))
                        {
                            template.SetCategoryHidden(elem.Category.Id, true);
                        }
                        else
                        {
                            MessageBox.Show(
                                $"The category \"{elem.Category.Name}\" of \"{elem.Name}\" can not be hidden!", 
                                "Categories can't be hidden"
                                );
                        }
                    }
                    else
                    {
                        actView.SetCategoryHidden(_doc.GetElement(elemRef).Category.Id, true);
                    }  
                }
                t.Commit();
            }
            return Result.Succeeded;
        }
    }
}
