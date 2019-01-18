using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Linq;

namespace DCEStudyTools.Design.Beam.BeamAxeMarking
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class BeamMarkingWithoutDimension : BeamMarking, IExternalCommand
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
                    
                CreateEntitiesForModelLineAndSetValue(
                    _doc, line, 
                    HEIGHT_BY_DEFAULT, 
                    WIDTH_BY_DEFAULT, 
                    TYPE_BY_DEFAULT, 
                    ElementId.InvalidElementId);
            } while (true);
        }
    }
}
