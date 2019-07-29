using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;

namespace DCEStudyTools.Utils
{
    class RvtElementGetter
    {
        public static IList<Level> GetAllStructLevels(Document doc)
        {
            IList<Level> levelsList =
                    (from lev in new FilteredElementCollector(doc)
                    .OfClass(typeof(Level))
                     where lev.GetEntitySchemaGuids().Count != 0
                     select lev)
                    .Cast<Level>()
                    .OrderBy(l => l.Elevation)
                    .ToList();

            if (levelsList.Count == 0)
            {
                TaskDialog.Show("Revit", "Configurer les niveaux structuraux avant de lancer cette commande.");
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
                     .OfClass(typeof(FamilyInstance)).Cast<FamilyInstance>()
                     .Where(f => f.Symbol.Name.Contains(Properties.Settings.Default.FAMILY_NAME_PILE))
                     .ToList();
            if (pilesList.Count == 0)
            {
                TaskDialog.Show("Revit", "Aucun pieu dans le projet.");
            }

            return pilesList;
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

        public static IList<ViewPlan> GetAllViewPlans(Document doc)
        {
            IList<ViewPlan> viewPlansList =
                            (from ViewPlan view in new FilteredElementCollector(doc)
                            .OfClass(typeof(ViewPlan))
                             where view.ViewType == ViewType.CeilingPlan && !view.IsTemplate
                             select view)
                            .Cast<ViewPlan>()
                            .ToList();
            if (viewPlansList.Count == 0)
            {
                TaskDialog.Show("Revit", "Aucune vue en plan liée au niveaux structuraux dans le projet.");
            }

            return viewPlansList;
        }

        public static IList<ViewSheet> GetAllSheets(Document doc)
        {
            IList<ViewSheet> viewsheetsList =
                    (from sheet in new FilteredElementCollector(doc)
                    .OfClass(typeof(ViewSheet))
                     where sheet.GetEntitySchemaGuids().Count != 0
                     select sheet)
                    .Cast<ViewSheet>()
                    .ToList();
            if (viewsheetsList.Count == 0)
            {
                TaskDialog.Show("Revit", "Aucune feuille liée au niveaux structuraux dans le projet.");
            }

            return viewsheetsList;
        }
    }
}
