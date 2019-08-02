using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;

using static DCEStudyTools.Utils.Getter.RvtElementGetter;
using static DCEStudyTools.Properties.Settings;

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
                Reference originalLevelRef;
                try
                {
                    originalLevelRef = _uidoc.Selection.PickObject(
                    ObjectType.Element,
                    new LevelSelectionFilter(),
                    "Selectionner un niveau");
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                {
                    return Result.Cancelled;
                }

                Reference levelRef;
                try
                {
                    levelRef = _uidoc.Selection.PickObject(
                    ObjectType.Element,
                    new LevelSelectionFilter(),
                    "Selectionner un niveau en tant que niveau reference");
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                {
                    return Result.Cancelled;
                }

                Level originalLevelEle = _doc.GetElement(originalLevelRef) as Level;
                Level levelEle = _doc.GetElement(levelRef) as Level;

                Transaction t = new Transaction(_doc, "Modify elements ref level");
                t.Start();

                foreach (Floor floor in GetAllFloors(_doc))
                {
                    Parameter p = floor.get_Parameter(BuiltInParameter.LEVEL_PARAM);
                    ElementId levelId = p.AsElementId();
                    if (levelId == originalLevelEle.Id)
                    {
                        p.Set(levelEle.Id);
                    }
                }

                foreach (Wall wall in GetAllWalls(_doc))
                {
                    Parameter p1 = wall.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE);
                    Parameter p2 = wall.get_Parameter(BuiltInParameter.WALL_BASE_CONSTRAINT);
                    if (p1.AsElementId() == originalLevelEle.Id)
                    {
                        p1.Set(levelEle.Id);
                    }
                    else if (p2.AsElementId() == originalLevelEle.Id)
                    {
                        p2.Set(levelEle.Id);
                    }
                }

                foreach (FamilyInstance fi in GetAllFamilyInstances(_doc, BuiltInCategory.OST_StructuralColumns))
                {
                    Parameter p1 = fi.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM);
                    Parameter p2 = fi.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM);
                    if (p1.AsElementId() == originalLevelEle.Id)
                    {
                        p1.Set(levelEle.Id);
                    }
                    else if(p2.AsElementId() == originalLevelEle.Id)
                    {
                        p2.Set(levelEle.Id);
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

      
    }
}

