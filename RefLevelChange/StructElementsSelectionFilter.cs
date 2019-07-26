using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCEStudyTools.RefLevelChange
{
    class StructElementsSelectionFilter : ISelectionFilter
    {

        public StructElementsSelectionFilter()
        {
        }

        public bool AllowElement(Element elem)
        {
            if (Properties.Settings.Default.CATEGORY_NAME_FLOOR.Equals(elem.Category.Name)
                || Properties.Settings.Default.CATEGORY_NAME_BEAM.Equals(elem.Category.Name)
                || Properties.Settings.Default.CATEGORY_NAME_WALL.Equals(elem.Category.Name)
                || Properties.Settings.Default.CATEGORY_NAME_COLUMN.Equals(elem.Category.Name))
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
