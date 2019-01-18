using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DCEStudyTools.Design.Beam.BeamAxeMarking;
using System.Collections.Generic;

namespace DCEStudyTools.Design.Beam.DimensionEditing
{
    public class SaveDimensionEvent : IExternalEventHandler
    {
        public List<EntityToUpdateLine> LinesToUpdate
        {
            set; get;
        }

        public SaveDimensionEvent()
        {
            LinesToUpdate = new List<EntityToUpdateLine>();
        }

        public void Execute(UIApplication app)
        {
            UIDocument uidoc = app.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Apply the update
            if ( LinesToUpdate.Count != 0)
            {
                foreach (var lineToUpdate in LinesToUpdate)
                {
                    BeamMarking.SetEntityFieldsValue(
                        doc,
                        lineToUpdate.Line,
                        lineToUpdate.Height,
                        lineToUpdate.Width,
                        lineToUpdate.Type,
                        ElementId.InvalidElementId);
                }
                LinesToUpdate.Clear();
            }
        }

        public string GetName()
        {
            return "Update model lines infos";
        }
    }
}
