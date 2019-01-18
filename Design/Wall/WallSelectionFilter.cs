using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace DCEStudyTools.Design.Wall
{
    class WallSelectionFilter : ISelectionFilter
    {
        private ElementId _levelId;
        private SelectionFilterElement _selectFilterElem;

        public WallSelectionFilter(ElementId levelId)
        {
            _levelId = levelId;
        }

        public WallSelectionFilter(ElementId levelId, SelectionFilterElement selectionFilterElement)
        {
            _levelId = levelId;
            _selectFilterElem = selectionFilterElement;
        }

        public bool AllowElement(Element elem)
        {
            if ((_selectFilterElem == null || _selectFilterElem.Contains(elem.Id))
                && elem.Category.Name == "Murs" && elem.LevelId == _levelId)
            {
                return true;
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
