using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCEStudyTools.RefLevelChange
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class RefLevelChange
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
                IList<Reference> structEleRefs = new List<Reference>();
                try
                {
                    structEleRefs = _uidoc.Selection.PickObjects(
                    ObjectType.Element,
                    new StructElementsSelectionFilter(),
                    "Selectionner les voiles/poteaux/poutres/dalles qui doivent changer le niveau de reference");
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

                Level levelEle = _doc.GetElement(levelRef) as Level;

                int numBeams = 0;

                Transaction t = new Transaction(_doc, "Modify elements ref level");
                t.Start();
                foreach (Reference eleRef in structEleRefs)
                {
                    Element ele = _doc.GetElement(eleRef);
                    if (Properties.Settings.Default.CATEGORY_NAME_FLOOR.Equals(ele.Category.Name))
                    {
                        Floor floorEle = ele as Floor;
                        Parameter p = floorEle.get_Parameter(BuiltInParameter.LEVEL_PARAM);
                        p.Set(levelEle.Id);
                    }
                    else if (Properties.Settings.Default.CATEGORY_NAME_BEAM.Equals(ele.Category.Name))
                    {
                        FamilyInstance beamEle = ele as FamilyInstance;
                        Parameter p = beamEle.get_Parameter(BuiltInParameter.SKETCH_PLANE_PARAM);
                        if (p == null)
                        {
                            p.Set(levelEle.Id);
                        }
                        else
                        {
                            numBeams++;
                        }
                    }
                    else if (Properties.Settings.Default.CATEGORY_NAME_WALL.Equals(ele.Category.Name))
                    {
                        Wall wallEle = ele as Wall;
                        Parameter p = wallEle.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE);
                        p.Set(levelEle.Id);
                    }
                    else if (Properties.Settings.Default.CATEGORY_NAME_COLUMN.Equals(ele.Category.Name))
                    {
                        FamilyInstance columnEle = ele as FamilyInstance;
                        Parameter p = columnEle.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM);
                        p.Set(levelEle.Id);
                    }
                }

                if (numBeams != 0)
                {
                    TaskDialog.Show("Revit", $"{numBeams} poutres ne sont pas liées au niveau selectionné. "
                        + "Elles doivent être modifiées manuellement.");
                }

                t.Commit();
                
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
