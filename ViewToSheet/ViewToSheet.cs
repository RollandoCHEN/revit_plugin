﻿using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.UI;
using DCEStudyTools.Utils;
using System.Collections.Generic;
using System.Linq;

using static DCEStudyTools.Utils.Getter.RvtElementGetter;
using static DCEStudyTools.Properties.Settings;

namespace DCEStudyTools.ViewToSheet
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class ViewToSheet : IExternalCommand
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;
        
        // TODO : Enhencement for this function
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            try
            {
                // Get list of all viewplans who have entity
                IList<ViewPlan> viewplans = GetAllStructuralPlans(_doc, true);
                // If no viewplan has entity, show a message
                if (viewplans.Count == 0) { return Result.Cancelled; }

                // Get list of all sheets who have entity
                IList<ViewSheet> viewsheets = GetAllSheets(_doc, true);
                // If no sheet has entity, show a message
                if (viewsheets.Count == 0) {  return Result.Cancelled; }

                // Get view port type for adding viewplans to sheets
                ElementType viewPortType_WithoutTitle = GetElementTypeByName(_doc, Default.TYPE_NAME_VIEWPORT_WITHOUT_TITLE);

                // Count num of views added to all sheets
                int numOfViews = 0;

                using (Transaction t = new Transaction(_doc, "Add ViewPlans to the corresponding Sheets"))
                {
                    t.Start();

                    foreach (ViewSheet sheet in viewsheets)
                    {
                        ElementId sheetLevelId = GetAssociateLevelOfSheet(sheet);
                        foreach (ViewPlan viewplan in viewplans)
                        {
                            ElementId viewplanLevelId = GetAssociateLevelOfViewPlan(viewplan);
                            // Add viewplan to the sheet
                            if (viewplanLevelId == sheetLevelId)
                            {
                                // if the viewplan is already on the sheet
                                bool viewExistsOnSheet = sheet.GetAllViewports().Any(
                                    viewportId =>
                                    (_doc.GetElement(viewportId) as Viewport).ViewId == viewplan.Id
                                );

                                if (!viewExistsOnSheet)
                                {
                                    BoundingBoxUV sheetOutline = sheet.Outline;
                                    UV titleblockCenter = (sheetOutline.Max + sheetOutline.Min) / 2.0;
                                    XYZ xyzToMove = new XYZ(titleblockCenter.U, titleblockCenter.V, 0);
                                    Viewport v1 = Viewport.Create(_doc, sheet.Id, viewplan.Id, xyzToMove);
                                    if (v1 != null)
                                    {
                                        v1.ChangeTypeId(viewPortType_WithoutTitle.Id);
                                        // num +1
                                        numOfViews++;
                                    }
                                }
                            }
                        }
                    }
                    t.Commit();
                }

                // TODO : show user which view is added to which sheet
                TaskDialog.Show("Revit", $"{numOfViews} vues en plan sont ajoutées aux toutes les feuilles");

                return Result.Succeeded;
            }
            catch (Autodesk.Revit.Exceptions.ArgumentException)
            {
                TaskDialog.Show("Revit", "Erreur dans le process, la commande va être annulée");
                return Result.Cancelled;
            }
        }

        private ElementId GetAssociateLevelOfViewPlan(ViewPlan view)
        {
            Schema ViewPlanSchema = Schema.Lookup(Guids.VIEW_PLAN_SHEMA_GUID);
            Entity ViewPlanSchemaEnt = view.GetEntity(ViewPlanSchema);
            ElementId levelId = ViewPlanSchemaEnt.Get<ElementId>(ViewPlanSchema.GetField("Level"));
            return levelId;
        }

        private ElementId GetAssociateLevelOf3DView(View3D view)
        {
            Schema threeDViewSchema = Schema.Lookup(Guids.THREE_D_VIEW_SHEMA_GUID);
            Entity threeDViewSchemaEnt = view.GetEntity(threeDViewSchema);
            ElementId levelId = threeDViewSchemaEnt.Get<ElementId>(threeDViewSchema.GetField("Level"));
            return levelId;
        }

        private ElementId GetAssociateLevelOfSheet(ViewSheet sheet)
        {
            Schema sheetSchema = Schema.Lookup(Guids.SHEET_SHEMA_GUID);
            Entity sheetSchemaEnt = sheet.GetEntity(sheetSchema);
            ElementId levelId = sheetSchemaEnt.Get<ElementId>(sheetSchema.GetField("Level"));
            return levelId;
        }
    }
}
