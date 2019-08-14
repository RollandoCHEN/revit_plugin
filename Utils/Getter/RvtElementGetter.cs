using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;

using static DCEStudyTools.Properties.Settings;

namespace DCEStudyTools.Utils.Getter
{
    class RvtElementGetter
    {

        ///////////////////////////////// ALL ELEMENTS GETTERS ////////////////////////////////////

        /////////////////////////////////////// START /////////////////////////////////////////////

        public static IList<Level> GetAllLevels(Document doc, bool isOnlyStruct, bool showMessage)
        {
            IList<Level> levelsList;
            string message;

            if (isOnlyStruct)
            {
                levelsList =
                    (from lev in new FilteredElementCollector(doc)
                    .OfClass(typeof(Level))
                        where lev.GetEntitySchemaGuids().Count != 0
                        select lev)
                    .Cast<Level>()
                    .OrderBy(l => l.Elevation)
                    .ToList();
                message = "Configurer les niveaux structuraux avant de lancer cette commande.";
            }
            else
            {
                levelsList =
                    new FilteredElementCollector(doc)
                    .OfClass(typeof(Level))
                    .Cast<Level>()
                    .OrderBy(l => l.Elevation)
                    .ToList();
                message = "Aucun niveau dans le projet.";
            }

            if (showMessage && levelsList.Count == 0)
            {
                TaskDialog.Show("Revit", message);
            }

            return levelsList;
        }

        public static IList<Element> GetAllBuildingZones(Document doc)
        {
            IList<Element> zonesList =
                    new FilteredElementCollector(doc)
                    .OfCategory(BuiltInCategory.OST_VolumeOfInterest)
                    .Cast<Element>()
                    .ToList();
            if (zonesList.Count == 0)
            {
                TaskDialog.Show("Revit", "Aucune zone de définition dans le projet.");
            }

            return zonesList;
        }

        public static IList<FamilyInstance> GetAllPileFoundations(Document doc)
        {
            IList<FamilyInstance> pilesList =
                      new FilteredElementCollector(doc)
                     .OfClass(typeof(FamilyInstance))
                     .Cast<FamilyInstance>()
                     .Where(f => f.Symbol.Name.Contains(Default.FAMILY_NAME_PILE))
                     .ToList();
            if (pilesList.Count == 0)
            {
                TaskDialog.Show("Revit", "Aucun pieu dans le projet.");
            }

            return pilesList;
        }

        public static IList<FamilyInstance> GetAllFoundations(Document doc)
        {
            IList<FamilyInstance> foundationsList =
                      new FilteredElementCollector(doc)
                     .OfClass(typeof(FamilyInstance))
                     .OfCategory(BuiltInCategory.OST_StructuralFoundation)
                     .Cast<FamilyInstance>()
                     .Where(fi => fi.SuperComponent == null)
                     .ToList();
            if (foundationsList.Count == 0)
            {
                TaskDialog.Show("Revit", "Aucune fondation dans le projet.");
            }

            return foundationsList;
        }

        public static IList<ImportInstance> GetAllCADFiles(Document doc)
        {
            IList<ImportInstance> cadFileLinksList =
                    new FilteredElementCollector(doc)
                    .OfClass(typeof(ImportInstance))
                    .Cast<ImportInstance>()
                    .ToList();
            if (cadFileLinksList.Count == 0)
            {
                TaskDialog.Show("Revit", "Aucun plan en dwg/dxf dans le projet.");
            }

            return cadFileLinksList;
        }

        public static IList<ViewPlan> GetAllStructuralPlans(Document doc, bool isOnlyStruct)
        {
            IList<ViewPlan> viewPlansList;
            if (isOnlyStruct)
            {
                viewPlansList =
                                (from ViewPlan view in new FilteredElementCollector(doc)
                                .OfClass(typeof(ViewPlan))
                                 where view.ViewType == ViewType.CeilingPlan && !view.IsTemplate
                                 && view.GetEntitySchemaGuids().Count != 0
                                 select view)
                                .Cast<ViewPlan>()
                                .ToList();
            }
            else
            {
                viewPlansList =
                                (from ViewPlan view in new FilteredElementCollector(doc)
                                .OfClass(typeof(ViewPlan))
                                 where view.ViewType == ViewType.CeilingPlan && !view.IsTemplate
                                 select view)
                                .Cast<ViewPlan>()
                                .ToList();
            }

            string message = "Aucune vue en plan" + (isOnlyStruct ? " liée aux niveaux structuraux" : "") + " dans le projet.";

            if (viewPlansList.Count == 0)
            {
                TaskDialog.Show("Revit", message);
            }

            return viewPlansList;
        }

        public static IList<ViewSheet> GetAllSheets(Document doc, bool isOnlyStruct)
        {
            IList<ViewSheet> viewsheetsList;
            if (isOnlyStruct)
            {
                viewsheetsList =
                    (from sheet in new FilteredElementCollector(doc)
                    .OfClass(typeof(ViewSheet))
                        where sheet.GetEntitySchemaGuids().Count != 0
                        select sheet)
                    .Cast<ViewSheet>()
                    .ToList();
            }
            else
            {
                viewsheetsList =
                    new FilteredElementCollector(doc)
                     .OfClass(typeof(ViewSheet))
                     .Cast<ViewSheet>()
                     .ToList();
            }

            string message = "Aucune feuille" + (isOnlyStruct?" liée aux niveaux structuraux":"") + " dans le projet.";

            if (viewsheetsList.Count == 0)
            {
                TaskDialog.Show("Revit", message);
            }

            return viewsheetsList;
        }

        public static IList<Wall> GetAllWalls(Document doc)
        {
            IList<Wall> wallsList =
                (from wall in new FilteredElementCollector(doc)
                 .OfClass(typeof(Wall))
                 .Cast<Wall>()
                 select wall)
                 .ToList();
            return wallsList;
        }

        public static IList<Floor> GetAllFloors(Document doc)
        {
            IList<Floor> floorsList =
                (from floor in new FilteredElementCollector(doc)
                 .OfClass(typeof(Floor))
                 .Cast<Floor>()
                 select floor)
                 .ToList();
            return floorsList;
        }

        public static IList<FamilyInstance> GetAllFamilyInstances(Document doc, BuiltInCategory category)
        {
            IList<FamilyInstance> familyInstancesList =
                (from fi in new FilteredElementCollector(doc)
                 .OfCategory(category)
                 .OfClass(typeof(FamilyInstance))
                 .Cast<FamilyInstance>()
                 select fi)
                 .ToList();
            return familyInstancesList;
        }




        /////////////////////////////////////// END ///////////////////////////////////////////////

        ///////////////////////////////// ALL ELEMENTS GETTERS ////////////////////////////////////


        ///////////////////////////////// BY NAME GETTERS /////////////////////////////////////////

        /////////////////////////////////////// START /////////////////////////////////////////////

        public static View GetLegendViewByName(Document doc, string name)
        {
            View legendView =
                (from v in new FilteredElementCollector(doc)
                 .OfClass(typeof(View))
                 .Cast<View>()
                 where v.ViewType == ViewType.Legend && v.Name.Equals(name)
                 select v)
                 .FirstOrDefault();
            if (legendView == null)
            {
                TaskDialog.Show("Revit", $"La légende \"{name}\" n'existe pas dans le projet.");
            }

            return legendView;
        }

        public static Family GetFamilyByName(Document doc, string name)
        {
            Family family =
                (from fa in new FilteredElementCollector(doc)
                .OfClass(typeof(Family))
                .Cast<Family>()
                 where fa.Name.Equals(name)
                 select fa)
                .FirstOrDefault();
            return family;
        }

        public static ElementType GetElementTypeByName(Document doc, string name)
        {
            ElementType eleType =
                (from type in new FilteredElementCollector(doc)
                 .OfClass(typeof(ElementType))
                 .Cast<ElementType>()
                 where type.Name.Equals(name)
                 select type)
                 .FirstOrDefault();
            if (eleType == null)
            {
                TaskDialog.Show("Revit", $"Le type \"{name}\" n'existe pas dans le projet.");
            }

            return eleType;
        }

        public static FamilySymbol GetFamilySymbolByName(Document doc, BuiltInCategory category, string name)
        {
            FamilySymbol familySymbol =
                (from fs in new FilteredElementCollector(doc)
                 .OfCategory(category)
                 .OfClass(typeof(FamilySymbol))
                 .Cast<FamilySymbol>()
                 where fs.Name.Equals(name)
                 select fs)
                 .FirstOrDefault();
            return familySymbol;
        }

        public static Material GetMaterialByName(Document doc, string name)
        {
            Material familySymbol =
                (from Material m in new FilteredElementCollector(doc)
                    .OfClass(typeof(Material))
                    .Cast<Material>()
                    where m != null && m.Name.Equals(name)
                    select m)
                    .FirstOrDefault();
            return familySymbol;
        }

        /////////////////////////////////////// END ///////////////////////////////////////////////

        ///////////////////////////////// BY NAME GETTERS /////////////////////////////////////////



        ///////////////////////// UNDER CONDITION ELEMENTS GETTERS ////////////////////////////////

        /////////////////////////////////////// START /////////////////////////////////////////////

        public static IList<FamilySymbol> GetAllFamilySymbolsInFamily(Document doc, BuiltInCategory category, Family family)
        {
            IList<FamilySymbol> familySymbolsList =
                    (from fs in new FilteredElementCollector(doc)
                     .OfCategory(category)
                     .OfClass(typeof(FamilySymbol))
                     .Cast<FamilySymbol>()
                     where fs.Family.Id == family.Id
                     select fs)
                     .ToList();

            return familySymbolsList;
        }

        public static IList<WallType> GetAllWallTypesByFamilyName(Document doc, string familyName)
        {
            IList<WallType> wallTypesList =
                    (from WallType wt in new FilteredElementCollector(doc)
                     .OfCategory(BuiltInCategory.OST_Walls)
                     .OfClass(typeof(WallType))
                     where wt.FamilyName.Equals(familyName)
                     select wt)
                     .Cast<WallType>()
                     .ToList();

            return wallTypesList;
        }


        public static FilteredElementCollector GetElementsByShareParamValue(Document doc, BuiltInCategory bic, string sParaName, double dValue)
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
                .WherePasses(filter);
            return collector;
        }

        public static FilteredElementCollector GetElementsByShareParamValue(
            Document doc, 
            BuiltInCategory bic, 
            string sParaName, 
            string sValue)
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
                .WherePasses(filter);
            return collector;
        }

        public static IList<IndependentTag> GetAllTagsInCategory(Document doc, BuiltInCategory category) {
            IList<IndependentTag> allTagsList =
                new FilteredElementCollector(doc)
                    .OfCategory(category)
                    .OfClass(typeof(IndependentTag))
                    .Cast<IndependentTag>()
                    .ToList();

            return allTagsList;
        }

        /////////////////////////////////////// END ///////////////////////////////////////////////

        ///////////////////////// UNDER CONDITION ELEMENTS GETTERS ////////////////////////////////
    }
}
