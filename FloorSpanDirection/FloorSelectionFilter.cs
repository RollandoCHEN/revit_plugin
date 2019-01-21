using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;

namespace DCEStudyTools.FloorSpanDirection
{
    internal class FloorSelectionFilter : ISelectionFilter
    {
        private ElementId _levelId;

        public FloorSelectionFilter(ElementId levelId)
        {
            _levelId = levelId;
        }

        public bool AllowElement(Element elem)
        {
            if (Properties.Settings.Default.CATEGORY_NAME_FLOOR.Equals(elem.Category.Name) && elem.LevelId == _levelId)
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