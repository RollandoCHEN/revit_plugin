using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using DCEStudyTools.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DCEStudyTools.Utils
{
    public class BeamType
    {
        public static readonly BeamType POU = new BeamType(Properties.Settings.Default.BEAM_TYPE_SIGN_POU, Properties.Settings.Default.BEAM_TYPE_SYNT_POU);
        public static readonly BeamType BN = new BeamType(Properties.Settings.Default.BEAM_TYPE_SIGN_BN, Properties.Settings.Default.BEAM_TYPE_SYNT_BN);
        public static readonly BeamType POUR = new BeamType(Properties.Settings.Default.BEAM_TYPE_SIGN_POUR, Properties.Settings.Default.BEAM_TYPE_SYNT_POUR);
        public static readonly BeamType TAL = new BeamType(Properties.Settings.Default.BEAM_TYPE_SIGN_TAL, Properties.Settings.Default.BEAM_TYPE_SYNT_TAL);
        public static readonly BeamType LINT = new BeamType(Properties.Settings.Default.BEAM_TYPE_SIGN_LINT, Properties.Settings.Default.BEAM_TYPE_SYNT_LINT);
        public static readonly BeamType LON = new BeamType(Properties.Settings.Default.BEAM_TYPE_SYNT_LON, Properties.Settings.Default.BEAM_TYPE_SIGN_LON);

        public static IEnumerable<BeamType> Values
        {
            get
            {
                yield return POU;
                yield return BN;
                yield return POUR;
                yield return TAL;
                yield return LINT;
                yield return LON;
            }
        }

        public string ParaSign { get; private set; }
        public string Syntaxe { get; private set; }

        BeamType(string sign, string syntaxe)
        {
            ParaSign = sign;
            Syntaxe = syntaxe;
        }

        public static string GetSyntaxe(string beamSign)
        {
            foreach (BeamType item in Values)
            {
                if (beamSign.Equals(item.ParaSign))
                {
                    return item.Syntaxe;
                } 
            }
            return String.Empty;
        }
    }

    class BeamFamily
    {
        readonly static string path =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
            Path.DirectorySeparatorChar +
            Properties.Settings.Default.FAMILY_NAME_BEAM + Properties.Settings.Default.RVT_FAMILY_EXTENSION;

        const StructuralType STBEAM
          = StructuralType.Beam;

        private List<FamilySymbol> _beamMaps = new List<FamilySymbol>();
        private Family _family;
        private Document _doc;

        public Family GetFamily
        {
            get
            {
                return _family;
            }
        }
        public List<FamilySymbol> BeamMaps
        {
            get
            {
                return _beamMaps;
            }
        }

        public BeamFamily(Document doc)
        {
            _doc = doc;
            // Retrieve the Beam family
            Family f =
                (from fa in new FilteredElementCollector(doc)
                .OfClass(typeof(Family)).Cast<Family>()
                    where fa.Name.Equals(Properties.Settings.Default.FAMILY_NAME_BEAM)
                    select fa)
                .FirstOrDefault();

            // if the family doesn't exist, then load family
            if (null == f)      
            {
                Transaction t = new Transaction(doc);
                t.Start("Load family");
                File.WriteAllBytes(path, Resources.concrete_beam);
                if (!doc.LoadFamily(path, out f))
                {
                    string message1 = "Unable to load '" + path + "'.";
                    TaskDialog.Show("Revit", message1);
                }
                TaskDialog.Show("Revit", $"The family is writen to {path}");
                //File.Delete(path);
                t.Commit();
            }

            foreach (ElementId elementId in f.GetFamilySymbolIds())
            {
                object symbol = doc.GetElement(elementId);
                FamilySymbol familyType = symbol as FamilySymbol;
                if (null == familyType)
                {
                    continue;
                }
                if (null == familyType.Category)
                {
                    continue;
                }

                //add symbols of beams to lists 
                string categoryName = familyType.Category.Name;
                if (Properties.Settings.Default.CATEGORY_NAME_BEAM.Equals(categoryName))
                {
                    BeamMaps.Add(familyType);
                }
            }

            _family = f;
        }

        public void AdjustBeamFamilyTypeName()
        {
            foreach (FamilySymbol beamType in BeamMaps)
            {
                string beamSign =
                    (from Parameter pr in beamType.Parameters
                     where pr.Definition.Name.Equals(Properties.Settings.Default.BEAM_TYPE_PARAMETER)
                     select pr)
                     .First()
                     .AsString();

                double beamHeight =
                    UnitUtils.Convert(
                        (from Parameter pr in beamType.Parameters
                         where pr.Definition.Name.Equals(Properties.Settings.Default.BEAM_HEIGHT_PARAMETER)
                         select pr)
                         .First()
                         .AsDouble(),
                        DisplayUnitType.DUT_DECIMAL_FEET,
                        DisplayUnitType.DUT_CENTIMETERS);

                double beamWidth =
                    UnitUtils.Convert(
                        (from Parameter pr in beamType.Parameters
                         where pr.Definition.Name.Equals(Properties.Settings.Default.BEAM_WIDTH_PARAMETER)
                         select pr)
                         .First()
                         .AsDouble(),
                        DisplayUnitType.DUT_DECIMAL_FEET,
                        DisplayUnitType.DUT_CENTIMETERS);

                string synthaxe = BeamType.GetSyntaxe(beamSign);

                string targetBeamTypeName = $"{synthaxe}-BA25-{beamWidth}x{beamHeight}";

                if (!beamType.Name.Equals(targetBeamTypeName))
                {
                    Transaction t = new Transaction(_doc);
                    t.Start("Change beam type name");
                    beamType.Name = targetBeamTypeName;
                    t.Commit();
                }
            }
        }

        public FamilySymbol GetBeamFamilyTypeOrCreateNew(string beamSign, double beamHeight, double beamWidth)
        {
            string synthaxe = BeamType.GetSyntaxe(beamSign);

            string targetBeamTypeName = $"{synthaxe}-BA25-{beamWidth}x{beamHeight}";

            // find the family type for beam creation
            FamilySymbol beamType =
                (from sm in BeamMaps
                 where sm.Name.Equals(targetBeamTypeName)
                 select sm)
                 .FirstOrDefault();

            // if family type doesn't exist in the project, create a new one
            if (null == beamType)
            {
                if (null != _family)
                {
                    System.Diagnostics.Debug.Print("Family name={0}", _family.Name);

                    FamilySymbol s = null;
                    foreach (ElementId id in _family.GetFamilySymbolIds())
                    {
                        s = _doc.GetElement(id) as FamilySymbol;
                        break;
                    }
                    System.Diagnostics.Debug.Assert(null != s,
                      "expected at least one symbol"
                      + " to be defined in family");
                    Transaction t = new Transaction(_doc);
                    t.Start("Create new family type");

                    beamType = s.Duplicate("Test") as FamilySymbol;

                    SetLengthValueTo(
                        beamType, "Hauteur",
                        UnitUtils.Convert(beamHeight, DisplayUnitType.DUT_CENTIMETERS, DisplayUnitType.DUT_DECIMAL_FEET));

                    SetLengthValueTo(
                        beamType, "Largeur",
                        UnitUtils.Convert(beamWidth, DisplayUnitType.DUT_CENTIMETERS, DisplayUnitType.DUT_DECIMAL_FEET));

                    SetTextValueTo(beamType, "Poutre type", beamSign);

                    beamType.Name = targetBeamTypeName;
                    t.Commit();

                    BeamMaps.Add(beamType);
                }
            }

            return beamType;
        }

        private  void SetLengthValueTo(Element element, string paraName, double value)
        {
            foreach (Parameter pr in element.Parameters)
            {
                if (pr.Definition.Name.Equals(paraName) && pr.Definition.ParameterType == ParameterType.Length)
                {
                    pr.Set(value);
                }
            }
        }

        private void SetTextValueTo(Element element, string paraName, string value)
        {
            foreach (Parameter pr in element.Parameters)
            {
                if (pr.Definition.Name.Equals(paraName) && pr.Definition.ParameterType == ParameterType.Text)
                {
                    pr.Set(value);
                }
            }
        }
    }
}
