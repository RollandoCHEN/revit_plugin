using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCEStudyTools.SheetCreation
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class SheetCreation : IExternalCommand
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;

        private SheetCreationForm _form;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            _form = new SheetCreationForm();

            if (_form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    List<Level> levelsInDoc;

                    levelsInDoc = new FilteredElementCollector(_doc)
                        .OfClass(typeof(Level))
                        .Cast<Level>()
                        .OrderBy(l => l.Elevation)
                        .ToList();

                    CreateNewSheets(_doc, levelsInDoc);
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

        private void CreateNewSheets(Document doc, List<Level> levels)
        {
            // Get the "Cartouche A3" title block 
            FamilySymbol familySymbol =
                (from fs in new FilteredElementCollector(doc)
                 .OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_TitleBlocks)
                 .Cast<FamilySymbol>()
                 where fs.Name.Equals(Properties.Settings.Default.TYPE_NAME_TITLEBLOCK)
                 select fs).FirstOrDefault();

            // Get the "Légendes" legend view 
            View legendView =
                (from v in new FilteredElementCollector(doc)
                 .OfClass(typeof(View))
                 .Cast<View>()
                 where v.ViewType == ViewType.Legend && v.ViewName.Equals(Properties.Settings.Default.LEGEND_NAME_STANDARD)
                 select v).FirstOrDefault();

            // Get the "Légendes" legend view 
            View foundationLegend =
                (from v in new FilteredElementCollector(doc)
                 .OfClass(typeof(View))
                 .Cast<View>()
                 where v.ViewType == ViewType.Legend &&
                 v.ViewName.Equals(Properties.Settings.Default.LEGEND_NAME_STANDARD + $" {_form.FoundationType}")
                 select v).FirstOrDefault();

            // If there exists the "Cartouche A3" title block, create sheets with it
            if (familySymbol != null)
            {
                using (Transaction t = new Transaction(doc, "Create new ViewSheets"))
                {
                    t.Start();
                    try
                    {

                        for (int duplicateNum = 0; duplicateNum <= _form.DuplicateNumber; duplicateNum++)
                        {
                            CreateSheetsForEachLevel(doc, levels, familySymbol, legendView, foundationLegend, duplicateNum);
                        }
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        TaskDialog.Show("Revit", $"Exception when creating view sheets : {e.Message}");
                        t.RollBack();
                    }
                }
            }
            else
            {
                throw new MissingMemberException($"Ne pas trouver la cartouche \"{Properties.Settings.Default.TYPE_NAME_TITLEBLOCK}\"!");
            }
        }

        private void CreateSheetsForEachLevel(Document doc,
            List<Level> levels,
            FamilySymbol familySymbol,
            View legendView,
            View foundationLegend,
            int duplicateNum)
        {
            ElementType viewPortType_WithoutTitle =
                (from type in new FilteredElementCollector(_doc)
                 .OfClass(typeof(ElementType))
                 .Cast<ElementType>()
                 where type.Name.Equals(Properties.Settings.Default.TYPE_NAME_VIEWPORT_WITHOUT_TITLE)
                 select type)
                 .FirstOrDefault();

            int sheetNum = 1;

            for (int i = 0; i < levels.Count; i++)
            {
                if (!levels[i].Name.Contains(Properties.Settings.Default.KEYWORD_BOTTOM_LEVEL))
                {
                    ViewSheet viewSheet = ViewSheet.Create(doc, familySymbol.Id);
                    if (_form.DuplicateNumber == 0)
                    {
                        viewSheet.SheetNumber = $"0{sheetNum}{duplicateNum}";
                        viewSheet.ViewName = levels[i].Name;
                    }
                    else
                    {
                        viewSheet.SheetNumber = $"0{sheetNum}{duplicateNum + 1}";
                        viewSheet.ViewName = $"{levels[i].Name} -{duplicateNum + 1}";
                    }

                    if (!levels[i].Name.Contains(Properties.Settings.Default.KEYWORD_FOUNDATION))
                    {
                        SetParameterValuefor(viewSheet, Properties.Settings.Default.PARA_NAME_SHEET_REI, _form.FireResist);
                        AddLegendToSheetView(legendView, viewPortType_WithoutTitle, viewSheet);
                    }
                    else
                    {
                        SetParameterValuefor(viewSheet, Properties.Settings.Default.PARA_NAME_SHEET_REI, Properties.Settings.Default.PARA_VALUE_SHEET_REI_NA);
                        string title = string.Empty;
                        if (_form.FoundationType.Equals(Properties.Settings.Default.KEYWOARD_FOUNDATION_TYPE_PILE))
                        {
                            title = Properties.Settings.Default.FOUNDATION_TITLE_PILE;
                        }
                        else if (_form.FoundationType.Equals(Properties.Settings.Default.KEYWOARD_FOUNDATION_TYPE_FOOTING))
                        {
                            title = Properties.Settings.Default.FOUNDATION_TITLE_FOOTING;
                        }
                        else if (_form.FoundationType.Equals(Properties.Settings.Default.KEYWOARD_FOUNDATION_TYPE_RAFT))
                        {
                            title = Properties.Settings.Default.FOUNDATION_TITLE_RAFT;
                        }
                        else if (_form.FoundationType.Equals(Properties.Settings.Default.KEYWOARD_FOUNDATION_TYPE_DEEP_FOOTING))
                        {
                            title = Properties.Settings.Default.FOUNDATION_TITLE_DEEP_FOOTING;
                        }
                        
                        SetParameterValuefor(viewSheet, Properties.Settings.Default.PARA_NAME_SHEET_TITLE, title);
                        AddLegendToSheetView(foundationLegend, viewPortType_WithoutTitle, viewSheet);
                    }

                    if (null == viewSheet)
                    {
                        throw new Exception("Failed to create new ViewSheet.");
                    }
                    sheetNum++;
                }
            }
        }

        private void AddLegendToSheetView(View legendView, ElementType viewPortType, ViewSheet viewSheet)
        {
            XYZ refPoint = new XYZ(1.355, 0.061, 0);
            
            if (legendView != null)
            {
                Viewport v1 = Viewport.Create(_doc, viewSheet.Id, legendView.Id, XYZ.Zero);
                Outline lvOutline = v1.GetBoxOutline();
                XYZ legendViewCenter = (lvOutline.MaximumPoint + lvOutline.MinimumPoint) / 2.0;
                XYZ rightCornerToCenter = legendViewCenter - new XYZ(lvOutline.MaximumPoint.X, lvOutline.MinimumPoint.Y, 0);
                v1.ChangeTypeId(viewPortType.Id);
                XYZ diffToMove = refPoint + rightCornerToCenter;
                ElementTransformUtils.MoveElement(_doc, v1.Id, diffToMove);
            }
        }

        private static void SetParameterValuefor(ViewSheet viewSheet, string paraName, string value)
        {
            foreach (Parameter pr in viewSheet.Parameters)
            {
                if (pr.Definition.Name.Equals(paraName))
                {
                    pr.Set(value);
                }
            }
        }
    }
}
