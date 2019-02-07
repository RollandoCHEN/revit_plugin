using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
                List<FamilyInstance> bn_pvList =
                    (from FamilyInstance beam in new FilteredElementCollector(_doc)
                     .OfCategory(BuiltInCategory.OST_StructuralFraming)
                     .OfClass(typeof(FamilyInstance))
                     .Cast<FamilyInstance>()
                     where beam.Name.Contains("BN")||beam.Name.Contains("TAL")
                     select beam)
                     .ToList();


                TaskDialog.Show("Revit", $"{bn_pvList.Count()} BN/Tallon");
                using (Transaction tx = new Transaction(_doc))
                {
                    tx.Start("Unjoin");
                    foreach (FamilyInstance bn in bn_pvList)
                    {
                        IList<ElementId> eleList = JoinGeometryUtils.GetJoinedElements(_doc, bn).ToList();

                        foreach (ElementId id in eleList)
                        {
                            Element ele = _doc.GetElement(id);
                            JoinGeometryUtils.UnjoinGeometry(_doc, bn, ele);
                        }
                    }
                    tx.Commit();
                }

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

