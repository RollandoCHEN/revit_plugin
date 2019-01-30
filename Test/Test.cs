using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DCEStudyTools.Test
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class Test : IExternalCommand
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;
            
            try
            {
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

                IList<ImportInstance> dwgFilesList =
                    (from ImportInstance plan in new FilteredElementCollector(_doc)
                    .OfClass(typeof(ImportInstance))
                    where plan.Category.Name.ToLower().EndsWith(".dwg") || plan.Category.Name.ToLower().EndsWith(".dxf")
                    select plan)
                    .Cast<ImportInstance>()
                    .ToList();

                if (dwgFilesList.Count == 0)
                {
                    TaskDialog.Show("Revit", "No dwg file is found in the document.");
                    return Result.Cancelled;
                }

                IList<ViewPlan> viewPlanList =
                            (from ViewPlan view in new FilteredElementCollector(_doc)
                            .OfClass(typeof(ViewPlan))
                             where view.ViewType == ViewType.CeilingPlan && !view.IsTemplate
                             select view)
                            .Cast<ViewPlan>()
                            .ToList();

                if (viewPlanList.Count == 0)
                {
                    TaskDialog.Show("Revit", "No view plan is found in the document.");
                    return Result.Cancelled;
                }

                foreach (ViewPlan view in viewPlanList)
                {
                    Level viewLvl = view.GenLevel;
                    Level supLvl = strLevels.Where(lvl => lvl.Elevation > viewLvl.Elevation).OrderBy(l => l.Elevation).FirstOrDefault();
                    using (Transaction tx = new Transaction(_doc))
                    {
                        tx.Start("Underlay Range");
                        if (supLvl != null)
                        {
                            view.SetUnderlayRange(viewLvl.Id, supLvl.Id);
                        }
                        else
                        {
                            view.SetUnderlayBaseLevel(viewLvl.Id);
                        }
                        tx.Commit();
                    }

                    foreach (ImportInstance dwg in dwgFilesList)
                    {
                        // if the level linked to the dwg is one of the structural level

                        if (dwg.LevelId == supLvl.Id)
                        {
                            OverrideGraphicSettings ogs = new OverrideGraphicSettings();

                            //Set Halftone Element
                            using (Transaction tx = new Transaction(_doc))
                            {
                                tx.Start("Halftone");

                                ogs.SetHalftone(true);
                                view.SetCategoryOverrides(dwg.Category.Id, ogs);
                                tx.Commit();
                            }
                        }
                    }
                }
                //// Duplicate the selected view selon the number of zone de définition

                //// Get list of all structural levels
                //IList<Level> strLevels =
                //    (from lev in new FilteredElementCollector(_doc)
                //    .OfClass(typeof(Level))
                //     where lev.GetEntitySchemaGuids().Count != 0
                //     select lev)
                //    .Cast<Level>()
                //    .OrderBy(l => l.Elevation)
                //    .ToList();

                //if (strLevels.Count == 0)
                //{
                //    TaskDialog.Show("Revit", "Configurer les niveaux structuraux avant de lancer cette commande.");
                //    return Result.Cancelled;
                //}

                //// Get list of all zone de définition
                //List<Element> zoneList =
                //    new FilteredElementCollector(_doc)
                //    .OfCategory(BuiltInCategory.OST_VolumeOfInterest)
                //     .Cast<Element>()
                //     .ToList();

                //int zoneCount = zoneList.Count;  // Num of duplicata

                //if (zoneList.Count == 0)
                //{
                //    TaskDialog.Show("Revit", "Aucune zone de définition dans le projet");
                //    return Result.Cancelled;
                //}
                //else
                //{
                //    StringBuilder zones = new StringBuilder();
                //    foreach (var zone in zoneList)
                //    {
                //        zones.AppendLine(zone.Name);
                //    }
                //    TaskDialog.Show("Revit", $"Zone(s) de définition :\n{zones}");
                //}

                //// Get the selected view
                //ICollection<ElementId> selectedIds = _uidoc.Selection.GetElementIds();
                //if (selectedIds.Count == 0)
                //{
                //    TaskDialog.Show("Revit", "Au moins selecionner une vue à dupliquer");
                //    return Result.Cancelled;
                //}

                //foreach (ElementId id in selectedIds)
                //{
                //    View viewToDuplicate = (View)_doc.GetElement(id);
                //    String levelName = viewToDuplicate.GenLevel.Name;
                //    Transaction t = new Transaction(_doc, "Duplicate View");
                //    t.Start();
                //    for (int i = 0; i < zoneCount; i++)
                //    {
                //        ElementId viewId = viewToDuplicate.Duplicate(ViewDuplicateOption.AsDependent);
                //        View duplicatedView = _doc.GetElement(viewId) as View;
                //        duplicatedView.Name = $"{levelName} - {zoneList[i].Name}";
                //        duplicatedView.get_Parameter(BuiltInParameter.VIEWER_VOLUME_OF_INTEREST_CROP).Set(zoneList[i].Id);
                //    }
                //    t.Commit();
                //}

                //// Delete "Etage 1 - " in the level name
                //string pattern = @"(Etage\s?[0-9]{0,2}\s?-?\s?)";

                //Transaction t = new Transaction(_doc, "Modify level name");
                //t.Start();
                //foreach (Level lvl in strLevels)
                //{
                //    lvl.Name = Regex.Replace(lvl.Name, pattern, String.Empty);
                //}
                //t.Commit();



            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }

            return Result.Succeeded;
        }

      
    }
}

