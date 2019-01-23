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
        public static readonly BeamType LON = new BeamType(Properties.Settings.Default.BEAM_TYPE_SIGN_LON, Properties.Settings.Default.BEAM_TYPE_SYNT_LON);

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

    public class BeamMat
    {
        public static Dictionary<string, string> MatDictionary { get; private set; }
        
        public static string GetMatSign(string beamMat)
        {
            MatDictionary.Add("Béton25", "BA25");
            MatDictionary.Add("Béton30", "BA30");
            MatDictionary.Add("Béton35", "BA35");
            MatDictionary.Add("Béton40", "BA40");
            MatDictionary.Add("Béton45", "BA45");

            foreach (var mat in MatDictionary)
            {
                if (beamMat.Contains(mat.Key))
                {
                    return mat.Value;
                }
            }
            return "BA25";
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

        private List<FamilySymbol> _beamTypesList = new List<FamilySymbol>();
        private Family _family;
        private Document _doc;

        public Family GetFamily
        {
            get
            {
                return _family;
            }
        }
        public List<FamilySymbol> BeamTypesList
        {
            get
            {
                return _beamTypesList;
            }
        }

        // Revit    |       Objet           |       FamilyInstance
        // Famille  =>      Family          ->      Name = "POU-BA"
        // Type     =>      FamilySymbol    ->      .Family
        // Poutre   =>      FamilyInstance  ->      .Symbol

        public BeamFamily(Document doc)
        {
            _doc = doc;
            // Retrieve the Beam family "POU-BA"
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

            // Retrieve all the Beam types of the family "POU-BA"
            List<FamilySymbol> beamTypesList =
                    (from beam in new FilteredElementCollector(_doc)
                     .OfClass(typeof(FamilySymbol))
                     .OfCategory(BuiltInCategory.OST_StructuralFraming)
                     .Cast<FamilySymbol>()
                     where beam.Family.Id==f.Id
                     select beam)
                     .ToList();

            _family = f;
            _beamTypesList = beamTypesList;
        }

        public void AdjustBeamFamilyTypeName()
        {
            foreach (FamilySymbol beamType in BeamTypesList)
            {
                string beamSign =
                    (from Parameter pr in beamType.Parameters
                     where pr.Definition.Name.Equals(Properties.Settings.Default.PARA_NAME_BEAM_TYPE)
                     select pr)
                     .First()
                     .AsString();

                string beamMat =
                    (from Parameter pr in beamType.Parameters
                     where pr.Definition.Name.Equals(Properties.Settings.Default.PARA_NAME_BEAM_MATERIAL)
                     select pr)
                     .First()
                     .AsString();

                double beamHeight =
                    UnitUtils.Convert(
                        (from Parameter pr in beamType.Parameters
                         where pr.Definition.Name.Equals(Properties.Settings.Default.PARA_NAME_BEAM_HEIGHT)
                         select pr)
                         .First()
                         .AsDouble(),
                        DisplayUnitType.DUT_DECIMAL_FEET,
                        DisplayUnitType.DUT_CENTIMETERS);

                double beamWidth =
                    UnitUtils.Convert(
                        (from Parameter pr in beamType.Parameters
                         where pr.Definition.Name.Equals(Properties.Settings.Default.PARA_NAME_BEAM_WIDTH)
                         select pr)
                         .First()
                         .AsDouble(),
                        DisplayUnitType.DUT_DECIMAL_FEET,
                        DisplayUnitType.DUT_CENTIMETERS);
                
                string synthaxe = BeamType.GetSyntaxe(beamSign);

                string matSign = BeamMat.GetMatSign(beamMat);

                string targetBeamTypeName = $"{synthaxe}-{matSign}-{beamWidth}x{beamHeight}";

                if (!beamType.Name.Equals(targetBeamTypeName))
                {
                    Transaction t = new Transaction(_doc);
                    t.Start("Change beam type name");
                    beamType.Name = targetBeamTypeName;
                    t.Commit();
                }
            }
        }

        public FamilySymbol GetBeamFamilyTypeOrCreateNew(string beamSign, string beamMat, double beamHeight, double beamWidth)
        {
            string synthaxe = BeamType.GetSyntaxe(beamSign);
            string matSign = BeamMat.GetMatSign(beamMat);

            string targetBeamTypeName = $"{synthaxe}-{matSign}-{beamWidth}x{beamHeight}";

            // find the family type for beam creation
            FamilySymbol beamType =
                (from sm in BeamTypesList
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

                    beamType = s.Duplicate("InitialName") as FamilySymbol;

                    SetLengthValueTo(
                        beamType, Properties.Settings.Default.PARA_NAME_BEAM_HEIGHT,
                        UnitUtils.Convert(beamHeight, DisplayUnitType.DUT_CENTIMETERS, DisplayUnitType.DUT_DECIMAL_FEET));

                    SetLengthValueTo(
                        beamType, Properties.Settings.Default.PARA_NAME_BEAM_WIDTH,
                        UnitUtils.Convert(beamWidth, DisplayUnitType.DUT_CENTIMETERS, DisplayUnitType.DUT_DECIMAL_FEET));

                    SetTextValueTo(beamType, Properties.Settings.Default.PARA_NAME_BEAM_TYPE, beamSign);

                    beamType.Name = targetBeamTypeName;
                    t.Commit();

                    BeamTypesList.Add(beamType);
                }
            }

            return beamType;
        }

        private void SetLengthValueTo(Element element, string paraName, double value)
        {
            foreach (Parameter pr in element.Parameters)
            {
                if (pr.Definition.Name.Equals(paraName) && pr.StorageType == StorageType.Double)
                {
                    pr.Set(value);
                }
            }
        }

        private void SetTextValueTo(Element element, string paraName, string value)
        {
            foreach (Parameter pr in element.Parameters)
            {
                if (pr.Definition.Name.Equals(paraName) && pr.StorageType == StorageType.String)
                {
                    pr.Set(value);
                }
            }
        }
    }
}
