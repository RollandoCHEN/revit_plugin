using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using DCEStudyTools.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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

        public static string GetBeamSign(string syntaxe)
        {
            foreach (BeamType item in Values)
            {
                if (syntaxe.Equals(item.Syntaxe))
                {
                    return item.ParaSign;
                }
            }
            return String.Empty;
        }
    }

    public class BeamMaterial
    {
        public static readonly BeamMaterial BA25 = new BeamMaterial(
            Properties.Settings.Default.BEAM_MAT_NAME_BA25, 
            Properties.Settings.Default.BEAM_MAT_SYNTAXE_BA25);

        public static readonly BeamMaterial BA30 = new BeamMaterial(
            Properties.Settings.Default.BEAM_MAT_NAME_BA30,
            Properties.Settings.Default.BEAM_MAT_SYNTAXE_BA30);

        public static readonly BeamMaterial BA35 = new BeamMaterial(
            Properties.Settings.Default.BEAM_MAT_NAME_BA35,
            Properties.Settings.Default.BEAM_MAT_SYNTAXE_BA35);

        public static readonly BeamMaterial BA40 = new BeamMaterial(
            Properties.Settings.Default.BEAM_MAT_NAME_BA40,
            Properties.Settings.Default.BEAM_MAT_SYNTAXE_BA40);

        public static readonly BeamMaterial BA45 = new BeamMaterial(
            Properties.Settings.Default.BEAM_MAT_NAME_BA45,
            Properties.Settings.Default.BEAM_MAT_SYNTAXE_BA45);

        public static IEnumerable<BeamMaterial> Values
        {
            get
            {
                yield return BA25;
                yield return BA30;
                yield return BA35;
                yield return BA40;
                yield return BA45;
            }
        }

        public string MatName { get; private set; }
        public string MatSyntaxe { get; private set; }

        public BeamMaterial(string rvtMatName, string syntaxeBA)
        {
            MatName = rvtMatName;
            MatSyntaxe = syntaxeBA;
        }

        public static string GetMatSign(string beamMat)
        {
            foreach (BeamMaterial mat in Values)
            {
                if (mat.MatName.Equals(beamMat))
                {
                    return mat.MatSyntaxe;
                }
            }
            return Properties.Settings.Default.BEAM_MAT_SYNTAXE_BA25;
        }

        public static string GetMatName(string matSyntaxe)
        {
            foreach (BeamMaterial mat in Values)
            {
                if (mat.MatSyntaxe.Equals(matSyntaxe))
                {
                    return mat.MatName;
                }
            }
            return Properties.Settings.Default.BEAM_MAT_NAME_BA25;
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

        public static void GetBeamSymbolProperties(FamilySymbol beamSymbol, out string beamSign, out string beamMat, out double beamHeight, out double beamWidth)
        {
            beamSign =
                (from Parameter pr in beamSymbol.Parameters
                 where pr.Definition.Name.Equals(Properties.Settings.Default.PARA_NAME_BEAM_TYPE)
                 select pr)
                 .First()
                 .AsString();

            beamMat =
                (from Parameter pr in beamSymbol.Parameters
                 where pr.Definition.Name.Equals(Properties.Settings.Default.PARA_NAME_BEAM_MATERIAL)
                 select pr)
                 .First()
                 .AsValueString();

            beamHeight = UnitUtils.Convert(
                    (from Parameter pr in beamSymbol.Parameters
                     where pr.Definition.Name.Equals(Properties.Settings.Default.PARA_NAME_BEAM_HEIGHT)
                     select pr)
                     .First()
                     .AsDouble(),
                    DisplayUnitType.DUT_DECIMAL_FEET,
                    DisplayUnitType.DUT_CENTIMETERS);
            beamWidth = UnitUtils.Convert(
                    (from Parameter pr in beamSymbol.Parameters
                     where pr.Definition.Name.Equals(Properties.Settings.Default.PARA_NAME_BEAM_WIDTH)
                     select pr)
                     .First()
                     .AsDouble(),
                    DisplayUnitType.DUT_DECIMAL_FEET,
                    DisplayUnitType.DUT_CENTIMETERS);
        }

        public void AdjustWholeBeamFamilyTypeName()
        {
            foreach (FamilySymbol beamSymbol in BeamTypesList)
            {
                AdjustBeamTypeName(beamSymbol);
            }
        }

        public void AdjustBeamTypeName(FamilySymbol beamSymbol)
        {
            double beamHeight, beamWidth;
            string beamSign, beamMat;
            GetBeamSymbolProperties(beamSymbol, out beamSign, out beamMat, out beamHeight, out beamWidth);

            string synthaxe = BeamType.GetSyntaxe(beamSign);
            string matSign = BeamMaterial.GetMatSign(beamMat);

            string targetBeamTypeName = $"{synthaxe}-{matSign}-{beamWidth}x{beamHeight}";

            if (!beamSymbol.Name.Equals(targetBeamTypeName))
            {
                Transaction t = new Transaction(_doc);
                t.Start("Change beam type name");
                beamSymbol.Name = targetBeamTypeName;
                t.Commit();
            }
        }

        public void AdjustWholeBeamFamilyProperties()
        {
            foreach (FamilySymbol beamSymbol in BeamTypesList)
            {
                // if the symbol name is in format of "ss+-BAdd-dd+xdd+", update the properties
                Regex regex = new Regex(@"[A-Z]{2,}-BA\d{2}-\d{2,}x\d{2,}");
                Match match = regex.Match(beamSymbol.Name);
                if (match.Success)
                {
                    AdjustBeamTypeProperties(beamSymbol);
                }
            }
        }

        public void AdjustBeamTypeProperties(FamilySymbol beamSymbol)
        {
            string actualTypeName = beamSymbol.Name;
            string targetSynthaxe = actualTypeName.Before("-");
            string targetBeamSign = BeamType.GetBeamSign(targetSynthaxe);

            string targetMatSign = actualTypeName.Between("-", "-");
            string targetBeamMat = BeamMaterial.GetMatName(targetMatSign);

            string dimensionString = actualTypeName.After("-");
            double targetWidth = Convert.ToDouble(dimensionString.Before("x"));
            double targetHeight = Convert.ToDouble(dimensionString.After("x"));

            double beamHeight, beamWidth;
            string beamSign, beamMat;
            GetBeamSymbolProperties(beamSymbol, out beamSign, out beamMat, out beamHeight, out beamWidth);
            
            if (!beamSign.Equals(targetBeamSign))
            {
                Transaction t = new Transaction(_doc);
                t.Start("Change beam type property");
                SetStringValueTo(beamSymbol, 
                    Properties.Settings.Default.PARA_NAME_BEAM_TYPE, 
                    targetBeamSign);
                t.Commit();
            }

            if (!beamMat.Equals(targetBeamMat))
            {
                Material targetMaterial =
                    (from Material m in new FilteredElementCollector(_doc)
                     .OfClass(typeof(Material))
                     where m != null
                     select m)
                     .Cast<Material>()
                     .FirstOrDefault(m => m.Name.Equals(targetBeamMat));

                Transaction t = new Transaction(_doc);
                t.Start("Change beam material property");
                SetElementIdValueTo(beamSymbol,
                    Properties.Settings.Default.PARA_NAME_BEAM_MATERIAL,
                    targetMaterial.Id);
                t.Commit();
            }

            if (beamHeight != targetHeight)
            {
                Transaction t = new Transaction(_doc);
                t.Start("Change beam height property");
                SetDoubleValueTo(beamSymbol,
                    Properties.Settings.Default.PARA_NAME_BEAM_HEIGHT,
                    UnitUtils.Convert(targetHeight, DisplayUnitType.DUT_CENTIMETERS, DisplayUnitType.DUT_DECIMAL_FEET));
                t.Commit();
            }

            if (beamWidth != targetWidth)
            {
                Transaction t = new Transaction(_doc);
                t.Start("Change beam width property");
                SetDoubleValueTo(beamSymbol,
                    Properties.Settings.Default.PARA_NAME_BEAM_WIDTH,
                    UnitUtils.Convert(targetWidth, DisplayUnitType.DUT_CENTIMETERS, DisplayUnitType.DUT_DECIMAL_FEET));
                t.Commit();
            }
        }

        public FamilySymbol GetBeamFamilyTypeOrCreateNew(string beamSign, string beamMat, double beamHeight, double beamWidth)
        {
            string synthaxe = BeamType.GetSyntaxe(beamSign);
            string matSign = BeamMaterial.GetMatSign(beamMat);

            string targetBeamTypeName = $"{synthaxe}-{matSign}-{beamWidth}x{beamHeight}";

            // find the family type for beam creation
            FamilySymbol beamSymbol =
                (from sm in BeamTypesList
                 where sm.Name.Equals(targetBeamTypeName)
                 select sm)
                 .FirstOrDefault();

            // if family type exists, adjust its name
            if (beamSymbol != null)
            {
                AdjustBeamTypeProperties(beamSymbol);
            }
            // if family type doesn't exist in the project, create a new one
            else
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

                    beamSymbol = s.Duplicate("InitialName") as FamilySymbol;

                    SetDoubleValueTo(
                        beamSymbol, Properties.Settings.Default.PARA_NAME_BEAM_HEIGHT,
                        UnitUtils.Convert(beamHeight, DisplayUnitType.DUT_CENTIMETERS, DisplayUnitType.DUT_DECIMAL_FEET));

                    SetDoubleValueTo(
                        beamSymbol, Properties.Settings.Default.PARA_NAME_BEAM_WIDTH,
                        UnitUtils.Convert(beamWidth, DisplayUnitType.DUT_CENTIMETERS, DisplayUnitType.DUT_DECIMAL_FEET));

                    SetStringValueTo(beamSymbol, Properties.Settings.Default.PARA_NAME_BEAM_TYPE, beamSign);

                    Material targetMaterial =
                    (from Material m in new FilteredElementCollector(_doc)
                     .OfClass(typeof(Material))
                     where m != null
                     select m)
                     .Cast<Material>()
                     .FirstOrDefault(m => m.Name.Equals(beamMat));

                    SetElementIdValueTo(beamSymbol, Properties.Settings.Default.PARA_NAME_BEAM_MATERIAL, targetMaterial.Id);

                    beamSymbol.Name = targetBeamTypeName;
                    t.Commit();

                    BeamTypesList.Add(beamSymbol);
                }
            }

            return beamSymbol;
        }

        private void SetElementIdValueTo(Element element, string paraName, ElementId value)
        {
            foreach (Parameter pr in element.Parameters)
            {
                if (pr.Definition.Name.Equals(paraName) && pr.StorageType == StorageType.ElementId)
                {
                    pr.Set(value);
                }
            }
        }

        private void SetDoubleValueTo(Element element, string paraName, double value)
        {
            foreach (Parameter pr in element.Parameters)
            {
                if (pr.Definition.Name.Equals(paraName) && pr.StorageType == StorageType.Double)
                {
                    pr.Set(value);
                }
            }
        }

        private void SetStringValueTo(Element element, string paraName, string value)
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
