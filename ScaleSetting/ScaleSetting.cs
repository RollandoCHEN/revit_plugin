using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DCEStudyTools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCEStudyTools.ScaleSetting
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class ScaleSetting : IExternalCommand
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Autodesk.Revit.DB.Document _doc;

        private List<int> _scales = new List<int>()
        {
            100, 125,
            150, 200,
            250, 500
        };

        private readonly string FLOOR_VIEW_TEMPLATE_NAME = "DCE Structure Etage Courant";
        private readonly string FOUNDATION_VIEW_TEMPLATE_NAME = "DCE Structure Fondation";

        private readonly double TITLE_BLOCK_LENGTH = 0.4;
        private readonly double TITLE_BLOCK_WIDTH = 0.27;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            try
            {
                // Create a 3D view in order to ge the outline of the entire model
                // Get a ViewFamilyType for a 3D view
                ViewFamilyType viewFamilyType =
                    (from v in new FilteredElementCollector(_doc)
                     .OfClass(typeof(ViewFamilyType))
                     .Cast<ViewFamilyType>()
                     where v.ViewFamily == ViewFamily.ThreeDimensional
                     select v).First();
                Transaction t = new Transaction(_doc);
                t.Start("Create 3D View to get the size of the building");
                View3D view = View3D.CreateIsometric(_doc, viewFamilyType.Id);

                // Get the outline of the entire 3D model
                FilteredElementCollector floorFec = new FilteredElementCollector(_doc).OfClass(typeof(Floor));
                BoundingBoxXYZ ibb = floorFec.Cast<Floor>().First().get_BoundingBox(view);
                Outline outline = new Outline(ibb.Min, ibb.Max);
                foreach (Floor floor in floorFec)
                {
                    BoundingBoxXYZ boxXYZ = floor.get_BoundingBox(view);
                    outline.AddPoint(boxXYZ.Min);
                    outline.AddPoint(boxXYZ.Max);
                }

                XYZ dimension = outline.MaximumPoint - outline.MinimumPoint;
                double xDimension = UnitUtils.Convert(
                    dimension.X,
                    DisplayUnitType.DUT_DECIMAL_FEET,
                    DisplayUnitType.DUT_METERS);
                double yDimension = UnitUtils.Convert(
                    dimension.Y,
                    DisplayUnitType.DUT_DECIMAL_FEET,
                    DisplayUnitType.DUT_METERS);

                double maxLength = Math.Max(xDimension, yDimension);
                double minLength = Math.Min(xDimension, yDimension);
                bool scaleIsFound = false;

                foreach (int scale in _scales)
                {
                    if (maxLength / scale < TITLE_BLOCK_LENGTH && minLength / scale < TITLE_BLOCK_WIDTH)
                    {
                        SetScaleForm form = new SetScaleForm(scale);
                        if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            ChangeViewTemplateScaleIfFound(scale);
                        }
                        scaleIsFound = true;
                        break;
                    }
                }

                if (!scaleIsFound)
                {
                    SetScaleForm form = new SetScaleForm(0);
                    form.ShowDialog();
                }

                _doc.Delete(view.Id);
                t.Commit();
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }

            return Result.Succeeded;
        }

        private void ChangeViewTemplateScaleIfFound(int scale)
        {
            View viewTemplate =
                ViewTemplate.FindViewTemplateOrDefault(
                    _doc,
                    ViewType.FloorPlan,
                    FLOOR_VIEW_TEMPLATE_NAME,
                    out bool templateIsFound);

            View foundationViewTemplate =
                ViewTemplate.FindViewTemplateOrDefault(
                    _doc,
                    ViewType.FloorPlan,
                    FOUNDATION_VIEW_TEMPLATE_NAME,
                    out bool foundationTemplateIsFound);

            if (templateIsFound)
            {
                viewTemplate.Scale = scale;
            }

            if (foundationTemplateIsFound)
            {
                foundationViewTemplate.Scale = scale;
            }
        }
    }
}
