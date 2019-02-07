using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCEStudyTools.BeamUnjoint
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class BeamUnjoint : IExternalCommand
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
                     where beam.Name.Contains("BN") || beam.Name.Contains("TAL")
                     select beam)
                     .ToList();


                TaskDialog.Show("Revit", $"{bn_pvList.Count()} BN/Talon");
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
