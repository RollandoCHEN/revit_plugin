using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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


                //// Duplicate the selected view selon the number of zone de définition

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

                Reference refId = _uidoc.Selection.PickObject( ObjectType.Element, new LevelSelectionFilter(), "Selectionner le niveau PH RDC");
                Level refLvl = _doc.GetElement(refId) as Level;

                int refLvlInd = strLevels.IndexOf(refLvl);
                int numOfBasements = refLvlInd - 2;
                using (Transaction tx = new Transaction(_doc))
                {
                    tx.Start("Rename levels");
                    refLvl.Name = Properties.Settings.Default.LEVEL_NAME_TOP_L1;
                    // TODO : 
                    for (int i = refLvlInd + 1; i < strLevels.Count(); i++)
                    {
                        strLevels[i].Name = $"PH R+{i - refLvlInd}";
                    }

                    for (int i = 0; i < refLvlInd - 1; i++)
                    {
                        // Foundation level
                        if (i == 0)
                        {
                            strLevels[i].Name = Properties.Settings.Default.LEVEL_NAME_FOUDATION;
                        }
                        // Base level of the lowest basement or RDC
                        else if (i == 1)
                        {
                            if (numOfBasements == 0)
                            {
                                strLevels[i].Name = Properties.Settings.Default.LEVEL_NAME_BOTTOM_L1;
                            }
                            else
                            {
                                strLevels[i].Name = $"Bas SS-{numOfBasements}";
                            }
                        }
                        else if (i < refLvlInd)
                        {
                            strLevels[i].Name = $"PH SS-{refLvlInd - i}";
                        }
                    }
                    tx.Commit();
                }
                return Result.Succeeded;
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }

        }

      
    }
}

