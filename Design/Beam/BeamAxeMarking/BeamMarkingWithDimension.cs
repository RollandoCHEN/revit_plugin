using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace DCEStudyTools.Design.Beam.BeamAxeMarking
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class BeamMarkingWithDimension : BeamMarking, IExternalCommand
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;
            do
            {
                bool userOperIsEnd = Get2PointsFromUser(_uidoc, out XYZ startPoint, out XYZ endPoint);
                if (!userOperIsEnd)
                {
                    return Result.Succeeded;
                }

                CreateOrGetLineStyle(
                    _doc,
                    startPoint, endPoint,
                    out GraphicsStyle lineStyle);
                   
                ModelLine line = CreateModelLine(_doc, startPoint, endPoint, lineStyle);

                if (GetInfosFromUI(
                    commandData, line,
                    out double height, out double width, out string type))
                {
                    CreateEntitiesForModelLineAndSetValue(
                        _doc, line, 
                        height, 
                        width, 
                        type, 
                        ElementId.InvalidElementId);
                }
                else
                {
                    CreateEntitiesForModelLineAndSetValue(
                        _doc, line, 
                        HEIGHT_BY_DEFAULT, 
                        WIDTH_BY_DEFAULT, 
                        TYPE_BY_DEFAULT, 
                        ElementId.InvalidElementId);
                }
            } while (true);
        }

        private bool GetInfosFromUI(
           ExternalCommandData commandData, ModelLine line,
           out double height, out double width, out string type)
        {
            height = 0;
            width = 0;
            type = string.Empty;

            double beamLength_Meters = UnitUtils.Convert(
                line.GeometryCurve.Length, 
                DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_METERS);

            BeamDimensionForm form = new BeamDimensionForm(commandData, beamLength_Meters);
            
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                height = form.BeamHeight != 0 ? form.BeamHeight : HEIGHT_BY_DEFAULT;
                width = form.BeamWidth != 0 ? form.BeamWidth : WIDTH_BY_DEFAULT;
                type = form.BeamType != string.Empty ? form.BeamType : TYPE_BY_DEFAULT;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
