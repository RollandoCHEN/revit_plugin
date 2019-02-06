using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DCEStudyTools.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCEStudyTools.BeamTypeCorrect
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class BeamTypeCorrect : IExternalCommand
    {
        static WindowHandle _hWndRevit = null;

        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;

        private BeamTypeCorrectForm _form;

        private ExternalEvent _exEvent;
        
        private readonly double DISTENCE_CONTINUOUS_SUPPORTER = 
            UnitUtils.Convert(25, DisplayUnitType.DUT_CENTIMETERS, DisplayUnitType.DUT_DECIMAL_FEET);
        private readonly double TOLERENCE_CONTAINED_BY_SUPPORTER = 
            UnitUtils.Convert(10, DisplayUnitType.DUT_CENTIMETERS, DisplayUnitType.DUT_DECIMAL_FEET);

        private View3D _auxiliary3DView;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (null == _hWndRevit)
            {
                Process[] processes
                  = Process.GetProcessesByName("Revit");

                if (0 < processes.Length && processes.Any(p => p == Process.GetCurrentProcess()))
                {
                    IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
                    _hWndRevit = new WindowHandle(h);
                }
            }

            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            // Retrieve data from revit document and add to the data grid view
            try
            {
                ChangeBeamFamilyTypeEvent handler = new ChangeBeamFamilyTypeEvent();
                _exEvent = ExternalEvent.Create(handler);

                _form = new BeamTypeCorrectForm(_uidoc, _exEvent, handler);

                // Capture the error of no-host beam
                bool allBeamsHaveHost = !new FilteredElementCollector(_doc)
                .OfCategory(BuiltInCategory.OST_StructuralFraming)
                .OfClass(typeof(FamilyInstance))
                .Cast<FamilyInstance>()
                .Any(beam => beam.Host == null);

                if (!allBeamsHaveHost)
                {
                    TaskDialog.Show("Revit", "Il y a des poutres qui n'ont pas de Host");
                    return Result.Cancelled;
                }

                // TODO : Extract GetAllStructuralLevels the methode
                // Get list of all structural levels
                IList<Level> strLevels =
                    (from lev in new FilteredElementCollector(_doc)
                    .OfClass(typeof(Level))
                     where lev.GetEntitySchemaGuids().Count != 0
                     select lev)
                    .Cast<Level>()
                    .OrderBy(l => l.Elevation)
                    .ToList();

                if (strLevels.Count == 0)
                {
                    TaskDialog.Show("Revit", "Configurer les niveaux structuraux avant de lancer cette commande.");
                    return Result.Cancelled;
                }

                TreatBeamsToBeAsNormal();

                TreatBeamsToBeAsGroundBeam();
                
                TreatUnsupportedWalls(strLevels);

            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }

            _form.Show(_hWndRevit);

            return Result.Succeeded;
        }

        private void TreatUnsupportedWalls(IList<Level> strLevels)
        {
            // Create ref 3D view to get outline of the selected wall
            ViewFamilyType viewFamilyType =
                (from v in new FilteredElementCollector(_doc)
                 .OfClass(typeof(ViewFamilyType))
                 .Cast<ViewFamilyType>()
                 where v.ViewFamily == ViewFamily.ThreeDimensional
                 select v).First();
            Transaction t = new Transaction(_doc);
            t.Start("Create auxiliary 3D View to get the size of the building");
            _auxiliary3DView = View3D.CreateIsometric(_doc, viewFamilyType.Id);
            t.Commit();

            List<Wall> toCheckWallList =
                new FilteredElementCollector(_doc)
                .OfClass(typeof(Wall))
                .Cast<Wall>()
                .ToList();

            List<Wall> unsupportedWallList = new List<Wall>();

            foreach (Wall toCheckdWall in toCheckWallList)
            {

                Line selectedWallAxeLine = (toCheckdWall.Location as LocationCurve).Curve as Line;

                Level selectedWallLevel = _doc.GetElement(toCheckdWall.LevelId) as Level;

                Level wallStructLevel = (from Level l in strLevels
                                         where l.Id == selectedWallLevel.Id
                                         select l).FirstOrDefault();

                int wallLevelInd = strLevels.IndexOf(wallStructLevel);

                ElementId lowerLevelId = wallLevelInd == 0 ?
                    ElementId.InvalidElementId : strLevels[wallLevelInd - 1].Id;

                // Get all the beams who are parallel with the selected wall 
                // and at the same level of the wall
                List<Element> beamList =
                    (from FamilyInstance beam in new FilteredElementCollector(_doc)
                     .OfCategory(BuiltInCategory.OST_StructuralFraming)
                     .OfClass(typeof(FamilyInstance))
                     .Cast<FamilyInstance>()
                     where beam.Host != null && beam.Host.Name.Equals(selectedWallLevel.Name) &&
                     IsCollinear(selectedWallAxeLine, (beam.Location as LocationCurve).Curve as Line)
                     select beam as Element)
                     .ToList();

                //TaskDialog.Show("Revit", $"{beamList.Count} beams as supporter");

                // Get all the walls who are parallel with the selected wall 
                // and at the lower level of the wall
                List<Element> wallList =
                    (from Wall wall in new FilteredElementCollector(_doc)
                     .OfClass(typeof(Wall))
                     .Cast<Wall>()
                     where wall.LevelId == lowerLevelId &&
                     IsCollinear(selectedWallAxeLine, (wall.Location as LocationCurve).Curve as Line)
                     select wall as Element)
                     .ToList();

                //TaskDialog.Show("Revit", $"{wallList.Count} walls as supporter");

                // List of beams & walls to loop
                List<Element> supporterList = new List<Element>();
                supporterList.AddRange(beamList);
                supporterList.AddRange(wallList);

                // Check the support situation
                bool wallIsEntirelySupported = false;

                if (supporterList.Count != 0)
                {
                    Element originalSupporter = supporterList.First();
                    List<Element> loopSupporterList = supporterList;
                    for (int i = 0; i < supporterList.Count; i++)
                    {
                        wallIsEntirelySupported = FinalOutlinContainseRefOutline(
                            toCheckdWall, originalSupporter, loopSupporterList,
                            out List<Element> remainList);

                        if (wallIsEntirelySupported || remainList.Count == 0)
                        {
                            break;
                        }

                        originalSupporter = remainList.First();
                        loopSupporterList = remainList;
                    }
                }

                if (!wallIsEntirelySupported)
                {
                    unsupportedWallList.Add(toCheckdWall);
                }

            }

            if (unsupportedWallList.Count != 0)
            {
                AddWallListToTable(unsupportedWallList, _form.UnsupportedWallDataTable);
            }

            // Delete the ref 3D view
            t.Start("Delete the auxiliary 3D View");
            _doc.Delete(_auxiliary3DView.Id);
            t.Commit();
           
        }

        private void TreatBeamsToBeAsGroundBeam()
        {
            // Get the list of nomal beam who are on the level of "Bas"
            FilteredElementCollector nomalBeamCollect
                = GetFamilyInstanceByShareParamValue(
                    _doc,
                    BuiltInCategory.OST_StructuralFraming,
                    Properties.Settings.Default.PARA_NAME_BEAM_TYPE, 
                    "");

            FilteredElementCollector pouRCollect
                = GetFamilyInstanceByShareParamValue(
                    _doc,
                    BuiltInCategory.OST_StructuralFraming,
                    Properties.Settings.Default.PARA_NAME_BEAM_TYPE,
                    "PR");

            List<Element> normalBeamList =
            (from Element elem in nomalBeamCollect
             where (elem as FamilyInstance).Host.Name.ToLower().Contains(Properties.Settings.Default.KEYWORD_BOTTOM_LEVEL)
             select elem)
             .ToList();

            List<Element> pouRList =
            (from Element elem in pouRCollect
             where (elem as FamilyInstance).Host.Name.ToLower().Contains(Properties.Settings.Default.KEYWORD_BOTTOM_LEVEL)
             select elem)
             .ToList();

            // List of beams should be changed as Ground Beam
            List<Element> beamToBeGroundBeamList = new List<Element>();
            beamToBeGroundBeamList.AddRange(normalBeamList);
            beamToBeGroundBeamList.AddRange(pouRList);

            if (beamToBeGroundBeamList.Count != 0)
            {
                AddBeamListToTable(beamToBeGroundBeamList, _form.ToGroundBeamDataTable);
            }
        }

        private void TreatBeamsToBeAsNormal()
        {
            // Get the list of BN whose height are greater than BN_MAX_HEIGHT_METER
            FilteredElementCollector bnCollect
            = GetFamilyInstanceByShareParamValue(
                _doc,
                BuiltInCategory.OST_StructuralFraming,
                Properties.Settings.Default.PARA_NAME_BEAM_TYPE, 
                Properties.Settings.Default.BEAM_TYPE_SIGN_BN);
            ElementParameterFilter height25Filter
                = EleParamGreaterFilter(
                    _doc,
                    Properties.Settings.Default.PARA_NAME_BEAM_HEIGHT,
                    Properties.Settings.Default.CHECK_BN_MAX_HEIGHT_METER);
            List<Element> bnHeightGreaterList
                = bnCollect.WherePasses(height25Filter).ToList();

            // Get the list of PV whose width are greater than PV_MAX_WIDTH_METER
            FilteredElementCollector pvCollect
                = GetFamilyInstanceByShareParamValue(
                    _doc,
                    BuiltInCategory.OST_StructuralFraming,
                    Properties.Settings.Default.PARA_NAME_BEAM_TYPE, 
                    Properties.Settings.Default.BEAM_TYPE_SIGN_TAL);
            ElementParameterFilter width18Filter
                = EleParamGreaterFilter(
                    _doc,
                    Properties.Settings.Default.PARA_NAME_BEAM_WIDTH,
                    Properties.Settings.Default.CHECK_PV_MAX_WIDTH_METER);
            List<Element> pvWidthGreaterList
                = pvCollect.WherePasses(width18Filter).ToList();

            // Get the list of ground beam who are NOT on the level of "Bas"
            FilteredElementCollector groundBeamCollect
                = GetFamilyInstanceByShareParamValue(
                    _doc,
                    BuiltInCategory.OST_StructuralFraming,
                    Properties.Settings.Default.PARA_NAME_BEAM_TYPE, 
                    Properties.Settings.Default.BEAM_TYPE_SIGN_LON);
            List<Element> groundBeamList =
            (from Element elem in groundBeamCollect
             where !(elem as FamilyInstance).Host.Name.ToLower().Contains(Properties.Settings.Default.KEYWORD_BOTTOM_LEVEL)
             select elem)
             .ToList();

            // List of beams should be changed as Nomal Beam
            List<Element> beamToBeNormalList = new List<Element>();
            beamToBeNormalList.AddRange(bnHeightGreaterList);
            beamToBeNormalList.AddRange(pvWidthGreaterList);
            beamToBeNormalList.AddRange(groundBeamList);

            if (beamToBeNormalList.Count != 0)
            {
                AddBeamListToTable(beamToBeNormalList, _form.ToNormalDataTable);
            }
        }

        private void AddBeamListToTable(List<Element> beamList, DataTable dataTable)
        {
            foreach (var elem in beamList)
            {
                FamilyInstance beam = elem as FamilyInstance;
                int beamId = beam.Id.IntegerValue;

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

                string beamLevel = beam.Host.Name;

                dataTable.Rows.Add(
                    beamId,
                    1,
                    beamLevel,
                    beamSign.Equals(string.Empty) ? "Normale" : beamSign,
                    Math.Round(beamHeight, 0),
                    Math.Round(beamWidth, 0));
            }
        }

        private ElementParameterFilter EleParamGreaterFilter(
            Document doc, string sParaName, double dValue)
        {
            SharedParameterElement para =
                                (from p in new FilteredElementCollector(doc)
                                 .OfClass(typeof(SharedParameterElement))
                                 .Cast<SharedParameterElement>()
                                 where p.Name.Equals(sParaName)
                                 select p).First();

            ParameterValueProvider provider
                = new ParameterValueProvider(para.Id);

            FilterNumericRuleEvaluator evaluator
                = new FilterNumericGreater();

            double sLength_feet = UnitUtils.Convert(
                dValue,
                DisplayUnitType.DUT_METERS,
                DisplayUnitType.DUT_DECIMAL_FEET);
            double epsilon = 0.0001;

            FilterRule rule = new FilterDoubleRule(
                provider, evaluator, sLength_feet, epsilon);

            ElementParameterFilter filter
                = new ElementParameterFilter(rule);
            return filter;
        }

        private FilteredElementCollector GetFamilyInstanceByShareParamValue(
            Document doc, BuiltInCategory bic, string sParaName, double dValue
            )
        {
            SharedParameterElement para =
                                (from p in new FilteredElementCollector(doc)
                                 .OfClass(typeof(SharedParameterElement))
                                 .Cast<SharedParameterElement>()
                                 where p.Name.Equals(sParaName)
                                 select p).First();

            ParameterValueProvider provider
                = new ParameterValueProvider(para.Id);

            FilterNumericRuleEvaluator evaluator
                = new FilterNumericEquals();

            double sLength_feet = UnitUtils.Convert(
                dValue,
                DisplayUnitType.DUT_METERS,
                DisplayUnitType.DUT_DECIMAL_FEET);
            double epsilon = 0.0001;

            FilterRule rule = new FilterDoubleRule(
                provider, evaluator, sLength_feet, epsilon);

            ElementParameterFilter filter
                = new ElementParameterFilter(rule);

            FilteredElementCollector collector
                = new FilteredElementCollector(doc)
                .OfCategory(bic)
                .OfClass(typeof(FamilyInstance))
                .WherePasses(filter);
            return collector;
        }

        private FilteredElementCollector GetFamilyInstanceByShareParamValue(
            Document doc, BuiltInCategory bic, string sParaName, string sValue
            )
        {
            SharedParameterElement para =
                                (from p in new FilteredElementCollector(doc)
                                 .OfClass(typeof(SharedParameterElement))
                                 .Cast<SharedParameterElement>()
                                 where p.Name.Equals(sParaName)
                                 select p).First();

            ParameterValueProvider provider
                = new ParameterValueProvider(para.Id);

            FilterStringRuleEvaluator evaluator
                = new FilterStringEquals();

            string sType = sValue;

            FilterRule rule = new FilterStringRule(
                provider, evaluator, sType, false);

            ElementParameterFilter filter
                = new ElementParameterFilter(rule);

            FilteredElementCollector collector
                = new FilteredElementCollector(doc)
                .OfCategory(bic)
                .OfClass(typeof(FamilyInstance))
                .WherePasses(filter);
            return collector;
        }
        
        private bool IsParallel(XYZ p, XYZ q)
        {
            return p.CrossProduct(q).IsZeroLength();
        }

        private bool IsCollinear(Line a, Line b)
        {
            XYZ v = a.Direction;
            XYZ w = b.Origin - a.Origin;
            XYZ newV = new XYZ(v.X, v.Y, 0);
            XYZ newW = new XYZ(w.X, w.Y, 0);
            return IsParallel(v, b.Direction)
              && IsParallel(newV, newW);
        }

        private bool FinalOutlinContainseRefOutline(
            Wall toCheckdWall, Element originalSupporter, List<Element> loopSupporterList,
            out List<Element> remainList)
        {
            // Get outline of the ref volumn of the selected wall
            BoundingBoxXYZ ibb = toCheckdWall.get_BoundingBox(_auxiliary3DView);
            double offset = UnitUtils.Convert(20, DisplayUnitType.DUT_CENTIMETERS, DisplayUnitType.DUT_DECIMAL_FEET);
            XYZ newMin = new XYZ(ibb.Min.X, ibb.Min.Y, ibb.Min.Z - DISTENCE_CONTINUOUS_SUPPORTER);
            XYZ newMax = new XYZ(ibb.Max.X, ibb.Max.Y, ibb.Min.Z);
            Outline refOutline = new Outline(newMin, newMax);

            BoundingBoxXYZ originalSupporterBB = originalSupporter.get_BoundingBox(_auxiliary3DView);
            Outline orignialSupporterOutline = new Outline(originalSupporterBB.Min, originalSupporterBB.Max);
            Outline finalOutline = orignialSupporterOutline;

            remainList = new List<Element>();

            foreach (Element supporter in loopSupporterList)
            {
                BoundingBoxXYZ bb = supporter.get_BoundingBox(_auxiliary3DView);
                Outline supporterOutline = new Outline(bb.Min, bb.Max);

                if (finalOutline.Intersects(supporterOutline, DISTENCE_CONTINUOUS_SUPPORTER))
                {
                    if (!supporterOutline.Equals(finalOutline))
                    {
                        finalOutline.AddPoint(bb.Min);
                        finalOutline.AddPoint(bb.Max);
                    }
                }
                else
                {
                    remainList.Add(supporter);
                }
            }
            
            return finalOutline.ContainsOtherOutline(refOutline, TOLERENCE_CONTAINED_BY_SUPPORTER);
        }

        private void AddWallListToTable(List<Wall> wallList, DataTable dataTable)
        {
            foreach (Wall wall in wallList)
            {
                int wallId = wall.Id.IntegerValue;

                string wallLevel = _doc.GetElement(wall.LevelId).Name;

                string wallType = wall.WallType.Name;

                double wallThickness = UnitUtils.Convert(wall.Width, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_CENTIMETERS);

                dataTable.Rows.Add(
                    wallId,
                    wallLevel,
                    wallType,
                    wallThickness);
            }
        }
    }
}
