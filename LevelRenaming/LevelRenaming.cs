using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCEStudyTools.LevelRenaming
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class LevelRenaming : IExternalCommand
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
                Reference refId = _uidoc.Selection.PickObject(
                    ObjectType.Element, 
                    new LevelSelectionFilter(), 
                    "Selectionner un niveau qui est le \"PH RDC\"");

                Level refLvl = _doc.GetElement(refId) as Level;
                int refLvlInd = -1;
                foreach (Level lvl in strLevels)
                {
                    if (refLvl.Id == lvl.Id)
                    {
                        refLvlInd = strLevels.IndexOf(lvl);
                    }
                }

                if (refLvlInd < 0)
                {
                    TaskDialog.Show("Revit", "Le niveau selectionné n'est pas un niveau structural.");
                    return Result.Cancelled;
                }

                int numOfBasements = refLvlInd - 2;
                using (Transaction tx = new Transaction(_doc))
                {
                    tx.Start("Initalize levels name");
                    for (int i = 0; i < strLevels.Count(); i++)
                    {
                        strLevels[i].Name = $"Initial - {i}";
                    }
                    tx.Commit();

                    tx.Start("Rename levels");
                    refLvl.Name = Properties.Settings.Default.LEVEL_NAME_TOP_L1;
                    // TODO : 
                    for (int i = refLvlInd + 1; i < strLevels.Count(); i++)
                    {
                        strLevels[i].Name = $"PH R+{i - refLvlInd}";
                    }

                    for (int i = 0; i < refLvlInd; i++)
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
