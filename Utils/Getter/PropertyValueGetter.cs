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
        // On a 2 methodes GetString et GetValueString car dans Revit 
        //  -   pour tous les paramètres en Texte, on utilise asString à choper sa valeur
        //  -   pour tous les paramètres ne pas en Texte, on utilise plutôt asValueString à choper sa valeur

        public static string GetFamilySymbolStringByPropertyName(FamilySymbol familySymbol, string propertyName)
        {
            string value = 
                (from Parameter pr in familySymbol.Parameters
                 where pr.Definition.Name.Equals(propertyName)
                 select pr)
                     .First()
                     .AsString();
            return value;
        }

        public static string GetFamilySymbolValueStringByPropertyName(FamilySymbol familySymbol, string propertyName)
        {
            string value =
                (from Parameter pr in familySymbol.Parameters
                 where pr.Definition.Name.Equals(propertyName)
                 select pr)
                     .First()
                     .AsValueString();
            return value;
        }

        public static double GetFamilySymbolDoubleByPropertyName(FamilySymbol familySymbol, string propertyName)
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
