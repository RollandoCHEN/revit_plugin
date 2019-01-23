using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DCEStudyTools.Utils;
using System.Collections.Generic;
using System.Linq;

namespace DCEStudyTools.BeamTypeCorrect
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

            ChangeBeamFamilyType(Properties.Settings.Default.BEAM_TYPE_SIGN_POU, BeamsToBeNormal);
 
            ChangeBeamFamilyType(Properties.Settings.Default.BEAM_TYPE_SIGN_LON, BeamsToBeGoundBeam);
        }

        public string GetName()
        {
            return "Change beam family type";
        }

        // TODO : Extraire method
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
                             where pr.Definition.Name.Equals(Properties.Settings.Default.PARA_NAME_BEAM_HEIGHT)
                             select pr)
                             .First()
                             .AsDouble(),
                            DisplayUnitType.DUT_DECIMAL_FEET,
                            DisplayUnitType.DUT_CENTIMETERS);

                    double beamWidth =
                        UnitUtils.Convert(
                            (from Parameter pr in beam.Symbol.Parameters
                             where pr.Definition.Name.Equals(Properties.Settings.Default.PARA_NAME_BEAM_WIDTH)
                             select pr)
                             .First()
                             .AsDouble(),
                            DisplayUnitType.DUT_DECIMAL_FEET,
                            DisplayUnitType.DUT_CENTIMETERS);

                    string beamSign =
                        (from Parameter pr in beam.Symbol.Parameters
                         where pr.Definition.Name.Equals(Properties.Settings.Default.PARA_NAME_BEAM_TYPE)
                         select pr)
                         .First()
                         .AsString();

                    string beamMat =
                    (from Parameter pr in beam.Symbol.Parameters
                     where pr.Definition.Name.Equals(Properties.Settings.Default.PARA_NAME_BEAM_MATERIAL)
                     select pr)
                     .First()
                     .AsString();

                    if (!beamSign.Equals(targetTypeSign))
                    {
                        FamilySymbol beamType =
                            _beamFamily.GetBeamFamilyTypeOrCreateNew(
                                targetTypeSign,
                                beamMat,
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