using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;
using System;
using DCEStudyTools.Utils;
using Autodesk.Revit.DB.ExtensibleStorage;

namespace DCEStudyTools.LevelCreation
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class LevelCreation : IExternalCommand
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;

        private LevelCreationForm _form;
        private int _numOfLvls;
        private int _numOfBasements;
        private int _totalNumOfLvls;
        private double _lvlsHghtFeets;
        private double _baseHghtFeets;

        private const double FONDATION_HEIGHT_METERS = 0.5;
        private double _foundationHghtFeets =
            UnitUtils.Convert(
                FONDATION_HEIGHT_METERS,
                DisplayUnitType.DUT_METERS,
                DisplayUnitType.DUT_DECIMAL_FEET);
        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            _form = new LevelCreationForm();
            
            if (_form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Retrieve the number of levels and number of basements
                _numOfLvls = _form.NumOfLevels;
                _numOfBasements = _form.NumOfBasements;
                _totalNumOfLvls = _numOfLvls + _numOfBasements;

                if (_totalNumOfLvls > 0)
                {
                    List<Level> newLevels = CreateNewLevels(_doc);

                    List<ViewPlan> newViewPlans = CreateNewViewPlan(_doc, newLevels);
                }

                return Result.Succeeded;
            }
            else
            {
                return Result.Cancelled;
            }
        }

        private List<Level> CreateNewLevels(Document doc)
        {
            PrepareLevelElevationAndName(out double[] elevations, out string[] names);
            
            PrepareShemaAndField(out Schema schema, out Field levelNumber);

            List<Level> newLevels = new List<Level>();

            using (Transaction t = new Transaction(doc, "Create Levels"))
            {
                try
                {
                    t.Start();
                    newLevels = CreateLevels(doc, elevations, names);
                    foreach (Level lev in newLevels)
                    {
                        Entity ent = new Entity(schema);

                        ent.Set(levelNumber, newLevels.IndexOf(lev));

                        lev.SetEntity(ent);
                    }
                    t.Commit();
                }
                catch (Exception e)
                {
                    TaskDialog.Show("Revit", $"Exception when creating levels : {e.Message}");
                    t.RollBack();
                }
            }
            return newLevels;
        }

        private static void PrepareShemaAndField(out Schema schema, out Field levelNumber)
        {
            SchemaBuilder builder = new SchemaBuilder(Guids.LEVEL_SHEMA_GUID);

            builder.SetReadAccessLevel(AccessLevel.Public);
            builder.SetWriteAccessLevel(AccessLevel.Public);

            builder.SetSchemaName("StructuralLevels");

            builder.SetDocumentation("Indicate if the level is for structural elements and the number of the level");

            // Create field1
            FieldBuilder fieldBuilder1 = builder.AddSimpleField("StrLevelNumber", typeof(int));

            // Register the schema object
            schema = builder.Finish();
            levelNumber = schema.GetField("StrLevelNumber");
        }

        private void PrepareLevelElevationAndName(out double[] elevations, out string[] names)
        {
            _lvlsHghtFeets = UnitUtils.Convert(
                            _form.LevelHeight_Meters,
                            DisplayUnitType.DUT_METERS,
                            DisplayUnitType.DUT_DECIMAL_FEET);
            _baseHghtFeets = UnitUtils.Convert(
                _form.BasementHeight_Meters,
                DisplayUnitType.DUT_METERS,
                DisplayUnitType.DUT_DECIMAL_FEET);

            double lowestLvlElv = -_numOfBasements * _baseHghtFeets;
            // totalNumOfLvls + 2 ---> foundation level and base level of lowest basement
            elevations = new double[_totalNumOfLvls + 2];
            names = new string[_totalNumOfLvls + 2];
            int refNumOfF1Top = _numOfBasements + 2;   // 2 ---> the PH SS levels start at i==2

            for (int i = 0; i < elevations.Length; i++)
            {
                // Foundation level
                if (i == 0)
                {
                    elevations[i] = lowestLvlElv - _foundationHghtFeets;
                    names[i] = Properties.Settings.Default.LEVEL_NAME_FOUDATION;
                }
                // Base level of the lowest basement or RDC
                else if (i == 1)
                {
                    elevations[i] = lowestLvlElv;
                    if (_numOfBasements == 0)
                    {
                        names[i] = Properties.Settings.Default.LEVEL_NAME_BOTTOM_L1;
                    }
                    else
                    {
                        names[i] = $"Bas SS-{_numOfBasements}";
                    }
                }
                else if (i < refNumOfF1Top)
                {
                    elevations[i] = elevations[i - 1] + _baseHghtFeets;
                    names[i] = $"PH SS-{refNumOfF1Top - i}";
                }
                else
                {
                    elevations[i] = elevations[i - 1] + _lvlsHghtFeets;
                    if (elevations[i - 1] == 0.0)
                    {
                        names[i] = Properties.Settings.Default.LEVEL_NAME_TOP_L1;
                    }
                    else
                    {
                        names[i] = $"PH R+{i - refNumOfF1Top}";
                    }
                }
            }
        }

        
        private static List<Level> CreateLevels(Document doc, double[] elevations, string[] names)
        {
            if (elevations.Length != names.Length)
            {
                throw new ArgumentException("The elevations array and names array should contain a same number of elements");
            }
            List<Level> list = new List<Level>();

            for (int i = 0; i < elevations.Length; i++)
            {
                double elevation = elevations[i];
                Level level = Level.Create(doc, elevation);
                if (names != null && names.Length >= i + 1)
                {
                    level.Name = names[i];
                }
                list.Add(level);
            }
            return list;
        }

        private List<ViewPlan> CreateNewViewPlan(Document doc, List<Level> newLevels)
        {
            List<ViewPlan> viewPlans = new List<ViewPlan>();

            // Prepare the ViewFamilyType for the ViewPlan creation
            ViewFamilyType viewFamilyType =
                            (from elem in new FilteredElementCollector(doc)
                            .OfClass(typeof(ViewFamilyType))
                            .Cast<ViewFamilyType>()
                             where elem.ViewFamily == ViewFamily.StructuralPlan
                             select elem)
                            .First();

            View standardTemplate =
                ViewTemplate.FindViewTemplateOrDefault(
                    _doc,
                    ViewType.FloorPlan,
                    Properties.Settings.Default.TEMPLATE_NAME_STANDARD_FLOOR,
                    out bool standardTemplateIsFound);

            View foundationTemplate =
                ViewTemplate.FindViewTemplateOrDefault(
                    _doc,
                    ViewType.FloorPlan,
                    Properties.Settings.Default.TEMPLATE_NAME_FOUNDATION,
                    out bool foundationTemplateIsFound);

            using (Transaction t = new Transaction(doc, "Create View Plans"))
            {
                try
                {
                    t.Start();
                    for (int i = 0; i < newLevels.Count; i++)
                    {
                        Level level = newLevels[i];
                        ViewPlan viewPlan = ViewPlan.Create(doc, viewFamilyType.Id, level.Id);
                        viewPlan.Name = $"0{i + 1} {level.Name}";
                        viewPlans.Add(viewPlan);
                        // TODO: Deal with the case when template is not found
                        if (!viewPlan.Name.Contains(Properties.Settings.Default.KEY_WORD_FOUNDATION) && standardTemplateIsFound)
                        {
                            viewPlan.ViewTemplateId = standardTemplate.Id;
                        }

                        if (viewPlan.Name.Contains(Properties.Settings.Default.KEY_WORD_FOUNDATION) && foundationTemplateIsFound)
                        {
                            viewPlan.ViewTemplateId = foundationTemplate.Id;
                        }
                    }
                    t.Commit();
                }
                catch (Exception e)
                {
                    TaskDialog.Show("Revit" ,$"Exception when creating view plans : {e.Message}");
                    t.RollBack();
                }
            }
            return viewPlans;
        }
    }
}
