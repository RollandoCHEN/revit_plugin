using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using DCEStudyTools.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCEStudyTools.Utils
{
    class BeamFamily
    {
        readonly static string path =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
            Path.DirectorySeparatorChar +
            Properties.Settings.Default.BEAM_FAMILY_NAME + Properties.Settings.Default.RVT_FAMILY_EXTENSION;

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
                    where fa.Name.Equals(Properties.Settings.Default.BEAM_FAMILY_NAME)
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
                if (Properties.Settings.Default.BEAM_CATEGORY_NAME.Equals(categoryName))
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

                string targetBeamTypeName =
                                beamSign.Equals(string.Empty) ?
                                $"{beamWidth}x{beamHeight}ht" :
                                $"{beamSign} {beamWidth}x{beamHeight}ht";

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
            string targetBeamTypeName =
                                beamSign.Equals(string.Empty) ?
                                $"{beamWidth}x{beamHeight}ht" :
                                $"{beamSign} {beamWidth}x{beamHeight}ht";

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
