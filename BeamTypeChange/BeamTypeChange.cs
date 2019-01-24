using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using DCEStudyTools.Design.Beam.BeamCreation;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using DCEStudyTools.Utils;

namespace DCEStudyTools.BeamTypeChange
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class BeamTypeChange : IExternalCommand
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            Autodesk.Revit.DB.View activeV = _doc.ActiveView;

            string targetTypeSign;
            IList<Reference> refIds = new List<Reference>();
            bool breakPick = false;

            do
            {
                BeamTypeForm form = new BeamTypeForm();
                if (form.ShowDialog() == DialogResult.OK && form.ChoosenType != string.Empty)
                {
                    targetTypeSign = Design.Beam.BeamCreation.BeamType.GetBeamTypeFromName(form.ChoosenType).Sign;
                }
                else
                {
                    return Result.Cancelled;
                }

                try
                {
                    refIds = _uidoc.Selection.PickObjects(ObjectType.Element,
                        new BeamSelectionFilter(activeV.GenLevel.Id));
                    breakPick = true;
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                {
                }

            } while (!breakPick);

            ChangeBeamFamilyType(targetTypeSign, refIds);

            return Result.Succeeded;
        }

        // TODO : Extraire method
        private void ChangeBeamFamilyType(string targetTypeSign, IList<Reference> refIds)
        {
            foreach (Reference reference in refIds)
            {
                FamilyInstance beam = _doc.GetElement(reference) as FamilyInstance;
                string selectedBeamSign, selectedBeamMat;
                double selectedBeamHeight, selectedBeamWidth;

                BeamFamily.GetBeamSymbolProperties(
                    beam.Symbol, 
                    out selectedBeamSign, out selectedBeamMat, 
                    out selectedBeamHeight, out selectedBeamWidth);

                if (!selectedBeamSign.Equals(targetTypeSign))
                {
                    BeamFamily beamFamily = new BeamFamily(_doc);
                    FamilySymbol beamType =
                        beamFamily.GetBeamFamilyTypeOrCreateNew(
                            targetTypeSign,
                            selectedBeamMat,
                            selectedBeamHeight,
                            selectedBeamWidth);
                    Transaction t = new Transaction(_doc);
                    t.Start("Change beam type");
                    beam.Symbol = beamType;
                    t.Commit();
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
