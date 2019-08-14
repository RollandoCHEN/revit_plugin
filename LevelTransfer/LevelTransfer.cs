using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;

using static DCEStudyTools.Utils.Getter.RvtElementGetter;
using static DCEStudyTools.Properties.Settings;

namespace DCEStudyTools.LevelTransfer
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class LevelTransfer : IExternalCommand
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
                IList<Reference> originalLevelRefsList;
                try
                {
                    originalLevelRefsList = _uidoc.Selection.PickObjects(
                    ObjectType.Element,
                    new LevelSelectionFilter(),
                    "Selectionner les niveaux auxquels les éléments structuraux sont liés actuellement");
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                {
                    return Result.Cancelled;
                }
                IList<Level> originalLevelsList =
                    (from Reference levelRef in originalLevelRefsList
                     select (_doc.GetElement(levelRef) as Level))
                    .ToList();

                IList<Level> allStructLevels = GetAllLevels(_doc, true, true);

                int matchedLevel = 0;
                foreach (Level originalLevel in originalLevelsList)
                {
                    if (allStructLevels.Any(level => EqualWithTolerance(level.Elevation, originalLevel.Elevation, 0.01)))
                    {
                        matchedLevel++;
                    }
                }
                TaskDialog.Show("Revit", $"{matchedLevel} niveaux structuraux sont accouplés avec les niveaux selectionnés.");

                Transaction t = new Transaction(_doc);
                t.Start("Modify elements ref levels");
                foreach (Floor floor in GetAllFloors(_doc))
                {
                    Parameter p = floor.get_Parameter(BuiltInParameter.LEVEL_PARAM);
                    ElementId floorlevelId = p.AsElementId();
                    Level floorLevel = _doc.GetElement(floorlevelId) as Level;

                    if (originalLevelsList.Any(level => level.Id == floorlevelId)
                        && allStructLevels.Any(level => EqualWithTolerance(level.Elevation, floorLevel.Elevation, 0.01))
                        )
                    {
                        Level targetLevel =
                            (from Level l in allStructLevels
                             where EqualWithTolerance(l.Elevation, floorLevel.Elevation, 0.01)
                             select l)
                             .First();
                        p.Set(targetLevel.Id);
                    }
                }

                foreach (Wall wall in GetAllWalls(_doc))
                {
                    Parameter p1 = wall.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE);
                    ElementId wallUpperLevelId = p1.AsElementId();
                    Level wallUpperLevel = _doc.GetElement(wallUpperLevelId) as Level;

                    Parameter p2 = wall.get_Parameter(BuiltInParameter.WALL_BASE_CONSTRAINT);
                    ElementId wallLowerLevelId = p2.AsElementId();
                    Level wallLowerLevel = _doc.GetElement(wallLowerLevelId) as Level;

                    if (originalLevelsList.Any(level => level.Id == wallUpperLevelId)
                        && allStructLevels.Any(level => EqualWithTolerance(level.Elevation, wallUpperLevel.Elevation, 0.01))
                        )
                    {
                        Level targetLevel =
                            (from Level l in allStructLevels
                             where EqualWithTolerance(l.Elevation, wallUpperLevel.Elevation, 0.01)
                             select l)
                             .First();
                        p1.Set(targetLevel.Id);
                    }

                    if (originalLevelsList.Any(level => level.Id == wallLowerLevelId)
                        && allStructLevels.Any(level => EqualWithTolerance(level.Elevation, wallLowerLevel.Elevation, 0.01))
                        )
                    {
                        Level targetLevel =
                            (from Level l in allStructLevels
                             where EqualWithTolerance(l.Elevation, wallLowerLevel.Elevation, 0.01)
                             select l)
                             .First();
                        p2.Set(targetLevel.Id);
                    }
                }

                foreach (FamilyInstance fi in GetAllFamilyInstances(_doc, BuiltInCategory.OST_StructuralColumns))
                {
                    Parameter p1 = fi.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM);
                    ElementId columnUpperLevelId = p1.AsElementId();
                    Level columnUpperLevel = _doc.GetElement(columnUpperLevelId) as Level;

                    Parameter p2 = fi.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM);
                    ElementId columnLowerLevelId = p2.AsElementId();
                    Level columnLowerLevel = _doc.GetElement(columnLowerLevelId) as Level;

                    if (originalLevelsList.Any(level => level.Id == columnUpperLevelId)
                        && allStructLevels.Any(level => EqualWithTolerance(level.Elevation, columnUpperLevel.Elevation, 0.01))
                        )
                    {
                        Level targetLevel =
                            (from Level l in allStructLevels
                             where EqualWithTolerance(l.Elevation, columnUpperLevel.Elevation, 0.01)
                             select l)
                             .First();
                        p1.Set(targetLevel.Id);
                    }

                    if (originalLevelsList.Any(level => level.Id == columnLowerLevelId)
                        && allStructLevels.Any(level => EqualWithTolerance(level.Elevation, columnLowerLevel.Elevation, 0.01))
                        )
                    {
                        Level targetLevel =
                            (from Level l in allStructLevels
                             where EqualWithTolerance(l.Elevation, columnLowerLevel.Elevation, 0.01)
                             select l)
                             .First();
                        p2.Set(targetLevel.Id);
                    }
                }

                foreach (FamilyInstance fi in GetAllFoundations(_doc))
                {
                    Parameter p = fi.get_Parameter(BuiltInParameter.FAMILY_LEVEL_PARAM);
                    ElementId foundationlevelId = p.AsElementId();
                    Level foundationLevel = _doc.GetElement(foundationlevelId) as Level;

                    if (originalLevelsList.Any(level => level.Id == foundationlevelId)
                        && allStructLevels.Any(level => EqualWithTolerance(level.Elevation, foundationLevel.Elevation, 0.01))
                        )
                    {
                        Level targetLevel =
                            (from Level l in allStructLevels
                             where EqualWithTolerance(l.Elevation, foundationLevel.Elevation, 0.01)
                             select l)
                             .First();
                        p.Set(targetLevel.Id);
                    }
                }
                t.Commit();

                //// Delete "Etage 1 - " in the level name
                //string pattern = @"(Etage\s?[0-9]{0,2}\s?-?\s?)";

                //Transaction t = new Transaction(_doc, "Modify level name");
                //t.Start();
                //foreach (Level lvl in strLevels)
                //{
                //    lvl.Name = Regex.Replace(lvl.Name, pattern, String.Empty);
                //}
                //t.Commit();

                return Result.Succeeded;
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }

        }

        private bool EqualWithTolerance(double a, double b, double delta)
        {
            double delta_feet = UnitUtils.Convert(delta, DisplayUnitType.DUT_METERS, DisplayUnitType.DUT_DECIMAL_FEET);

            if (Math.Abs(a - b) < delta_feet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
