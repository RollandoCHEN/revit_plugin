using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using DCEStudyTools.Properties;
using DCEStudyTools.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DCEStudyTools.Design.Beam.BeamCreation
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class BeamCreation : IExternalCommand
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;
        private List<FamilySymbol> _beamMaps = new List<FamilySymbol>();        //list of beams' type
        private bool _axeIsOnArchView;
        private List<Level> _strLevels;

        const StructuralType STBEAM
          = StructuralType.Beam;

        public List<FamilySymbol> BeamMaps
        {
            get
            {
                return _beamMaps;
            }
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            // TODO : Extract GetAllStructuralLevels the methode
            _strLevels =
                (from l in new FilteredElementCollector(_doc)
                 .OfClass(typeof(Level))
                 where l.GetEntitySchemaGuids().Count != 0
                 select l)
                 .Cast<Level>()
                 .OrderBy(l => l.Elevation)
                 .ToList();

            if (_strLevels.Count == 0)
            {
                TaskDialog.Show("Revit", "Configurer les niveaux structuraux avant de lancer cette commande.");
                return Result.Cancelled;
            }

            BeamCreationForm form = new BeamCreationForm();

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _axeIsOnArchView = form.AxeIsOnArchView;
            }
            else
            {
                return Result.Cancelled;
            }

            Transaction t = new Transaction(_doc);

            // Get all the model lines created by plugin
            IList<CurveElement> lines =
                (from line in new FilteredElementCollector(_doc)
                    .OfClass(typeof(CurveElement))
                    .OfCategory(BuiltInCategory.OST_Lines)
                    .Cast<CurveElement>()
                 where (line as ModelLine) != null && line.GetEntitySchemaGuids().Count != 0
                 select line)
                    .ToList();
            BeamFamily beamFamily = new BeamFamily(_doc);

            t.Start("Create beams");
            foreach (CurveElement line in lines)
            {
                ModelLine modelLine = line as ModelLine;

                bool modelLineOnLevel = GetLevel(modelLine, out Level level);

                GetParameterFromEntity(
                    modelLine,
                    out string beamSign,
                    out double beamHeight,
                    out double beamWidth,
                    out ElementId beamId);

                if (!modelLineOnLevel)
                    continue;

                if (LineHasRelativeBeam(modelLine, beamSign, beamHeight, beamWidth, beamId))
                {
                    continue;
                }
                // TODO : Material should be choosed when creat model line
                FamilySymbol beamType = beamFamily.GetBeamFamilyTypeOrCreateNew(beamSign, "Béton25", beamHeight, beamWidth);

                ElementId newBeamId = PlaceBeam(line, level, beamType);

                SetBeamIdToModelLine(newBeamId, modelLine).ToString();
            }
            t.Commit();
            return Result.Succeeded;
        }

        private bool LineHasRelativeBeam(ModelLine modelLine, string beamSign, double beamHeight, double beamWidth, ElementId beamId)
        {
            // There exists a beam associate with the model line
            if (beamId != ElementId.InvalidElementId)
            {
                FamilyInstance beam = _doc.GetElement(beamId) as FamilyInstance;
                double realBeamHeight =
                    UnitUtils.Convert(
                        (from Parameter pr in beam.Symbol.Parameters
                         where pr.Definition.Name.Equals("Hauteur")
                         select pr)
                         .First()
                         .AsDouble(),
                        DisplayUnitType.DUT_DECIMAL_FEET,
                        DisplayUnitType.DUT_CENTIMETERS);

                double realBeamWidth =
                    UnitUtils.Convert(
                        (from Parameter pr in beam.Symbol.Parameters
                         where pr.Definition.Name.Equals("Largeur")
                         select pr)
                         .First()
                         .AsDouble(),
                        DisplayUnitType.DUT_DECIMAL_FEET,
                        DisplayUnitType.DUT_CENTIMETERS);

                string realBeamSign =
                    (from Parameter pr in beam.Symbol.Parameters
                     where pr.Definition.Name.Equals("Poutre type")
                     select pr)
                     .First()
                     .AsString();

                double realBeamLength = (beam.Location as LocationCurve).Curve.Length;

                double modelLineLength = modelLine.GeometryCurve.Length;

                // The beam has exactly the same param with model line
                if (beamHeight == realBeamHeight &&
                    beamWidth == realBeamWidth &&
                    modelLineLength == realBeamLength &&
                    beamSign.Equals(realBeamSign))
                {
                    return true;
                }
                else
                {
                    _doc.Delete(beamId);
                    return false;
                }

            }
            return false;
        }
        
        private void GetParameterFromEntity(ModelLine modelLine,
            out string beamSign, 
            out double beamHeight, 
            out double beamWidth,
            out ElementId beamId)
        {
            beamSign = string.Empty;
            beamHeight = 0;
            beamWidth = 0;
            beamId = ElementId.InvalidElementId;

            Schema lineSchema = Schema.Lookup(Guids.MODELLINE_SCHEMA_GUID);
            Entity lineSchemaEnt = modelLine.GetEntity(lineSchema);
            
            // Get beam sign
            string sBeamType = lineSchemaEnt.Get<string>(lineSchema.GetField("BeamType"));
            beamSign = BeamType.GetBeamTypeFromName(sBeamType).Sign;

            // Get beam section height
            beamHeight = lineSchemaEnt.Get<double>(
                    lineSchema.GetField("SectionHeight"),
                    DisplayUnitType.DUT_CENTIMETERS);

            // Get beam section width
            beamWidth = lineSchemaEnt.Get<double>(
                    lineSchema.GetField("SectionWidth"),
                    DisplayUnitType.DUT_CENTIMETERS);

            //
            beamId = lineSchemaEnt.Get<ElementId>(
                    lineSchema.GetField("BeamId"));
        }

        private bool GetLevel(
            ModelLine modelLine,
            out Level level)
        {
            // Get associated level of the line
            string lineLevelName = modelLine.SketchPlane.Name;
            level = null;

            for (int i = 0; i < _strLevels.Count; i++)
            {
                if (_strLevels[i].Name.Equals(lineLevelName))
                {
                    level = _axeIsOnArchView ? _strLevels[i + 1] : _strLevels[i];
                    break;
                }
            }

            if (null == level)
            {
                string message1 = $"{modelLine.Name} is not correctly placed on a structural level";
                TaskDialog.Show("Revit", message1);
                return false;
            }
            return true;
        }

        private bool SetBeamIdToModelLine(ElementId newBeamId, ModelLine modelLine)
        {
            Schema lineSchema = Schema.Lookup(Utils.Guids.MODELLINE_SCHEMA_GUID);
            Entity lineSchemaEnt = modelLine.GetEntity(lineSchema);
            try
            {
                lineSchemaEnt.Set(lineSchema.GetField("BeamId"), newBeamId);
                modelLine.SetEntity(lineSchemaEnt);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private ElementId PlaceBeam(CurveElement line, Level level, FamilySymbol beamType)
        {
            LocationCurve lc = line.Location as LocationCurve;
            if (!beamType.IsActive)
            {
                beamType.Activate();
            }
            FamilyInstance fi = _doc.Create.NewFamilyInstance(lc.Curve, beamType, level, STBEAM);
            ;
            return fi.Id;
        }
        

    }
}
