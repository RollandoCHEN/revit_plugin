using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCEStudyTools.Utils.Getter
{
    class PropertyValueGetter
    {
        public static string GetFamilySymbolStringValueByPropertyName(FamilySymbol familySymbol, string propertyName)
        {
            string value = 
                (from Parameter pr in familySymbol.Parameters
                 where pr.Definition.Name.Equals(propertyName)
                 select pr)
                     .First()
                     .AsValueString();
            return value;
        }

        public static double GetFamilySymbolDoubleValueByPropertyName(FamilySymbol familySymbol, string propertyName)
        {
            double value =
                (from Parameter pr in familySymbol.Parameters
                 where pr.Definition.Name.Equals(propertyName)
                 select pr)
                     .First()
                     .AsDouble();
            return value;
        }

    }
}
