using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;

namespace DCEStudyTools.ViewDuplicate
{
    class StructuralPlanSelectionFilter : ISelectionFilter
    {

        public StructuralPlanSelectionFilter()
        {
        }

        public bool AllowElement(Element elem)
        {
            if (Properties.Settings.Default.CATEGORY_NAME_VIEW.Equals(elem.Category.Name) && 
                Properties.Settings.Default.FAMILY_TYPE_NAME_STR_PLAN.Equals(elem.GetType().Name))
            {
                return true;
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    }
}
