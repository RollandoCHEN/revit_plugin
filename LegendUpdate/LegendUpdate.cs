﻿using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

using static DCEStudyTools.Utils.Getter.RvtElementGetter;
using static DCEStudyTools.Properties.Settings;

namespace DCEStudyTools.LegendUpdate
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class LegendUpdate : IExternalCommand
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;
        private LegendUpdateForm _form;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            _form = new LegendUpdateForm();

            if (_form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    UpdateLegends();
                }
                catch (Exception e)
                {
                    message = e.Message;
                    return Result.Failed;
                }

                return Result.Succeeded;
            }
            else
            {
                return Result.Cancelled;
            }
        }

        private void UpdateLegends()
        {
            // Get the "Légendes" legend view 
            View legendView = GetLegendViewByName(_doc, Default.LEGEND_NAME_STANDARD);

            // TOGO: Get the foundation legend view 
            // View foundationLegend = GetLegendViewByName(_doc, Default.STANDARD_LEGEND_NAME + $" {_form.FoundationType}");

            // Get view port type "without title"
            ElementType viewPortType_WithoutTitle = GetElementTypeByName(_doc, Default.TYPE_NAME_VIEWPORT_WITHOUT_TITLE);

            // Get all sheets
            IList<ViewSheet> viewSheets = GetAllSheets(_doc, false);

            Transaction t = new Transaction(_doc, "Update legend");
            t.Start();
            // update legend
            foreach (ViewSheet viewSheet in viewSheets)
            {
                // for the sheet "Cartouche", skip it
                if (viewSheet.Name.ToLower().Contains(Default.KEYWORD_TITLEBLOCK))
                {
                    continue;
                }

                // for the sheet "Fondation"
                if (viewSheet.Name.ToLower().Contains(Default.KEYWORD_FOUNDATION))
                {
                    // fondation legend view existing on the sheet 
                    ElementId viewportId =
                    (from vpId in viewSheet.GetAllViewports()
                     where ((Viewport)_doc.GetElement(vpId))
                        .get_Parameter(BuiltInParameter.VIEWPORT_VIEW_NAME)
                        .AsString().Contains(Default.LEGEND_NAME_STANDARD)
                     select vpId)
                     .ToList()
                     .FirstOrDefault();

                    // if the legendView exists
                    if (viewportId != null)
                    {
                        Viewport foundationLegendViewport = (Viewport)_doc.GetElement(viewportId);

                        // get the corresponding fondation legend view
                        View foundationLegend = (View)_doc.GetElement(foundationLegendViewport.ViewId);

                        // delete the old one
                        viewSheet.DeleteViewport(foundationLegendViewport);

                        // if legend should be shown
                        if (_form.DoseShowLegend)
                        {
                            // add the legend view at the new position
                            AddLegendToSheetView(foundationLegend, viewPortType_WithoutTitle, viewSheet, _form.SelectedLegendPosition);
                        }
                    }
                }
                else
                {
                    // legend view existing on the sheet 
                    ElementId id =
                    (from viewportId in viewSheet.GetAllViewports()
                     where ((Viewport)_doc.GetElement(viewportId)).ViewId == legendView.Id
                     select viewportId)
                     .ToList()
                     .FirstOrDefault();

                    // if the legend view exists, delete it
                    if (id != null)
                    {
                        Viewport legendViewport = (Viewport)_doc.GetElement(id);

                        viewSheet.DeleteViewport(legendViewport);
                    }

                    // if legend should be shown
                    if (_form.DoseShowLegend)
                    {
                        // add the legend view at the new position
                        AddLegendToSheetView(legendView, viewPortType_WithoutTitle, viewSheet, _form.SelectedLegendPosition);
                    }
                }
            }
            t.Commit();
        }

        private void AddLegendToSheetView(View legendView, ElementType viewPortType, ViewSheet viewSheet, string position)
        {
            if (legendView != null)
            {
                Viewport v1 = Viewport.Create(_doc, viewSheet.Id, legendView.Id, XYZ.Zero);
                Outline lvOutline = v1.GetBoxOutline();
                XYZ legendViewCenter = (lvOutline.MaximumPoint + lvOutline.MinimumPoint) / 2.0;

                // The position of the legend is Bottom Right by default
                double refX = Default.LEGEND_POSITION_X_MAX;
                double refY = Default.LEGEND_POSITION_Y_MIN_R;

                XYZ legendRefPointToCenter = legendViewCenter - new XYZ(lvOutline.MaximumPoint.X, lvOutline.MinimumPoint.Y, 0);

                if (position.Equals("TopRight"))
                {
                    refX = Default.LEGEND_POSITION_X_MAX;
                    refY = Default.LEGEND_POSITION_Y_MAX_R;

                    legendRefPointToCenter = legendViewCenter - new XYZ(lvOutline.MaximumPoint.X, lvOutline.MaximumPoint.Y, 0);
                }
                else if (position.Equals("BottomLeft"))
                {
                    refX = Default.LEGEND_POSITION_X_MIN;
                    refY = Default.LEGEND_POSITION_Y_MIN_L;

                    legendRefPointToCenter = legendViewCenter - new XYZ(lvOutline.MinimumPoint.X, lvOutline.MinimumPoint.Y, 0);
                }
                else if (position.Equals("TopLeft"))
                {
                    refX = Default.LEGEND_POSITION_X_MIN;
                    refY = Default.LEGEND_POSITION_Y_MAX_L;

                    legendRefPointToCenter = legendViewCenter - new XYZ(lvOutline.MinimumPoint.X, lvOutline.MaximumPoint.Y, 0);
                }

                XYZ refPoint = new XYZ(refX, refY, 0);

                v1.ChangeTypeId(viewPortType.Id);
                XYZ diffToMove = refPoint + legendRefPointToCenter;
                ElementTransformUtils.MoveElement(_doc, v1.Id, diffToMove);
            }
        }
    }
}

