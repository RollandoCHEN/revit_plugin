﻿using Autodesk.Revit.Creation;
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

        private readonly List<int> _scales = new List<int>()
        {
            100, 125,
            150, 200,
            250, 500
        };

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            try
            {
                // Get all the floor elements
                FilteredElementCollector floorFec = new FilteredElementCollector(_doc).OfClass(typeof(Floor));
                if (floorFec.Count() != 0)
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
                    t.Commit();

                    // Get the outline of the entire 3D model
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
                        if (maxLength / scale < Properties.Settings.Default.TITLEBLOCK_LENGTH &&
                            minLength / scale < Properties.Settings.Default.TITLEBLOCK_WIDTH)
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

                    t.Start("Delete the 3D View");
                    _doc.Delete(view.Id);
                    t.Commit();
                }
                else
                {
                    TaskDialog.Show("Revit", "No floor element exists in the document !");
                }

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
                    Properties.Settings.Default.TEMPLATE_NAME_THREED_VIEW,
                    out bool templateIsFound);

            View foundationViewTemplate =
                ViewTemplate.FindViewTemplateOrDefault(
                    _doc,
                    ViewType.FloorPlan,
                    Properties.Settings.Default.TEMPLATE_NAME_FOUNDATION,
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
