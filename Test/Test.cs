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
                IList<FamilySymbol> beamTypesList =
                    (from beam in new FilteredElementCollector(_doc)
                     .OfClass(typeof(FamilySymbol))
                     .OfCategory(BuiltInCategory.OST_StructuralFraming)
                     select beam)
                     .Cast<FamilySymbol>()
                     .ToList();

                Material target =
                    (from Material m in new FilteredElementCollector(_doc)
                     .OfClass(typeof(Material))
                     where m != null
                     select m)
                     .Cast<Material>()
                     .FirstOrDefault(m => m.Name.Equals("Béton - Coulé sur place - Béton25"));

                Transaction t = new Transaction(_doc, "Set mat");
                t.Start();
                foreach (FamilySymbol bt in beamTypesList)
                {
                    if (bt.Name.Contains("BA25"))
                    {
                        Parameter mat =
                            (from Parameter para in bt.Parameters
                             where para.Definition.Name.Equals("Matériau structurel")
                             select para)
                             .Cast<Parameter>()
                             .First();
                
                        mat.Set(target.Id);
                    }

                }
                t.Commit();
                

                
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

