using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCEStudyTools.BeamTypeChange
{
    class BeamSelectionFilter : ISelectionFilter
    {
        private ElementId _levelId;
        private SelectionFilterElement _selectFilterElem;

        public BeamSelectionFilter(ElementId levelId)
        {
            _levelId = levelId;
        }

        public BeamSelectionFilter(ElementId levelId, SelectionFilterElement selectionFilterElement)
        {
            _levelId = levelId;
            _selectFilterElem = selectionFilterElement;
        }

        public bool AllowElement(Element elem)
        {
            FamilyInstance fi = elem as FamilyInstance;

            if ((_selectFilterElem == null || _selectFilterElem.Contains(elem.Id))
                && fi.Category.Name == Properties.Settings.Default.BEAM_CATEGORY_NAME && fi.Host.Id == _levelId)
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
