using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DCEStudyTools.Utils;
using System.Collections.Generic;
using System.Linq;

namespace DCEStudyTools.BeamTypeDetect
{
    public class ChangeBeamFamilyTypeEvent : IExternalEventHandler
    {
        private Document _doc;
        private UIDocument _uidoc;
        private BeamFamily _beamFamily;

        public IList<Element> BeamsToBeNormal { set; get; } = new List<Element>();

        public IList<Element> BeamsToBeGoundBeam { set; get; } = new List<Element>();

        public void Execute(UIApplication app)
        {
            _uidoc = app.ActiveUIDocument;
            _doc = _uidoc.Document;

            _beamFamily = new BeamFamily(_doc);

            _beamFamily.AdjustBeamFamilyTypeName();

            ChangeBeamFamilyType("", BeamsToBeNormal);
 
            ChangeBeamFamilyType("L", BeamsToBeGoundBeam);
        }

        public string GetName()
        {
            return "Change beam family type";
        }

        private void ChangeBeamFamilyType(string targetTypeSign, IList<Element> elemCol)
        {
            if (elemCol.Count != 0)
            {
                foreach (Element elem in elemCol)
                {
                    FamilyInstance beam = elem as FamilyInstance;
                    double beamHeight =
                        UnitUtils.Convert(
                            (from Parameter pr in beam.Symbol.Parameters
                             where pr.Definition.Name.Equals("Hauteur")
                             select pr)
                             .First()
                             .AsDouble(),
                            DisplayUnitType.DUT_DECIMAL_FEET,
                            DisplayUnitType.DUT_CENTIMETERS);

                    double beamWidth =
                        UnitUtils.Convert(
                            (from Parameter pr in beam.Symbol.Parameters
                             where pr.Definition.Name.Equals("Largeur")
                             select pr)
                             .First()
                             .AsDouble(),
                            DisplayUnitType.DUT_DECIMAL_FEET,
                            DisplayUnitType.DUT_CENTIMETERS);

                    string beamSign =
                        (from Parameter pr in beam.Symbol.Parameters
                         where pr.Definition.Name.Equals("Poutre type")
                         select pr)
                         .First()
                         .AsString();

                    if (!beamSign.Equals(targetTypeSign))
                    {
                        FamilySymbol beamType =
                            _beamFamily.GetBeamFamilyTypeOrCreateNew(
                                targetTypeSign,
                                beamHeight,
                                beamWidth);
                        Transaction t = new Transaction(_doc);
                        t.Start("Change beam type");
                        beam.Symbol = beamType;
                        t.Commit();
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }
}