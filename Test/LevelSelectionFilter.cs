using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCEStudyTools.Test
{
    class LevelSelectionFilter : ISelectionFilter
    {

        public LevelSelectionFilter()
        {
        }

        public bool AllowElement(Element elem)
        {
            if (Properties.Settings.Default.CATEGORY_NAME_LEVEL.Equals(elem.Category.Name))
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
