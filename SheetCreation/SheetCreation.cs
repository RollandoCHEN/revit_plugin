using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.UI;
using DCEStudyTools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

using static DCEStudyTools.Utils.Getter.RvtElementGetter;
using static DCEStudyTools.Properties.Settings;

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
                    // Get list of all levels for structural elements
                    IList<Level> strLevels = GetAllLevels(_doc, true);
                    if (strLevels.Count == 0){ return Result.Cancelled; }

                    CreateNewSheets(strLevels);
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

        private void CreateNewSheets(IList<Level> levels)
        {
            // Get the "Cartouche A3" title block 
            FamilySymbol familySymbol = GetFamilySymbolByName(_doc, BuiltInCategory.OST_TitleBlocks, Default.TYPE_NAME_TITLEBLOCK);
            if (familySymbol == null)
            {
                throw new MissingMemberException();
            }

            // Get the "Légendes" legend view 
            View legendView = GetLegendViewByName(_doc, Default.LEGEND_NAME_STANDARD);

            // Get the "Légendes" legend view 
            View foundationLegend = GetLegendViewByName(_doc, Default.LEGEND_NAME_STANDARD + $" {_form.FoundationType}");
            
            // If there exists the "Cartouche A3" title block, create sheets with it
            using (Transaction t = new Transaction(_doc, "Create new ViewSheets"))
            {
                t.Start();
                try
                {

                    for (int duplicateNum = 0; duplicateNum <= _form.DuplicateNumber; duplicateNum++)
                    {
                        CreateSheetsForEachLevel(_doc, levels, familySymbol, legendView, foundationLegend, duplicateNum);
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

        private void CreateSheetsForEachLevel(Document doc,
            IList<Level> levels,
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
                Level level = levels[i];
                if (!level.Name.ToLower().Contains(Properties.Settings.Default.KEYWORD_BOTTOM_LEVEL))
                {
                    ViewSheet viewSheet = ViewSheet.Create(doc, familySymbol.Id);

                    // Set "Niveau" Property Value
                    SetParameterValuefor(viewSheet, Properties.Settings.Default.PARA_NAME_SHEET_LEVEL, level.Name);

                    // Set Sheet Number & Sheet Name
                    if (_form.DuplicateNumber == 0)
                    {
                        viewSheet.SheetNumber = $"0{sheetNum}{duplicateNum}";
                        viewSheet.ViewName = level.Name;
                    }
                    else
                    {
                        viewSheet.SheetNumber = $"0{sheetNum}{duplicateNum + 1}"; // TODO to be improved
                        viewSheet.ViewName = $"{level.Name} -{duplicateNum + 1}"; // TODO to be improved
                    }

                    
                    if (!level.Name.ToLower().Contains(Properties.Settings.Default.KEYWORD_FOUNDATION))
                    {
                        // Set REI Value
                        SetParameterValuefor(viewSheet, Properties.Settings.Default.PARA_NAME_SHEET_REI, _form.FireResist);
                        // Add Legend
                        AddLegendToSheetView(legendView, viewPortType_WithoutTitle, viewSheet);
                        // Add Level Entity
                        AssociateLevelToNewSheet(level, viewSheet);
                    }
                    else
                    {
                        // Set REI Value "NA"
                        SetParameterValuefor(viewSheet, Properties.Settings.Default.PARA_NAME_SHEET_REI, Properties.Settings.Default.PARA_VALUE_SHEET_REI_NA);
                        // Set Foundation Type Value
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
                        // Set Foundation Page Title
                        SetParameterValuefor(viewSheet, Properties.Settings.Default.PARA_NAME_SHEET_TITLE, title);
                        // Add Legend
                        AddLegendToSheetView(foundationLegend, viewPortType_WithoutTitle, viewSheet);
                        // Add Level Entity
                        AssociateLevelToNewSheet(level, viewSheet);
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

        private void AssociateLevelToNewSheet(Level level, ViewSheet sheet)
        {
            SchemaBuilder builder = new SchemaBuilder(Guids.SHEET_SHEMA_GUID);

            builder.SetReadAccessLevel(AccessLevel.Public);
            builder.SetWriteAccessLevel(AccessLevel.Public);

            builder.SetSchemaName("AssociatedLevel");

            builder.SetDocumentation("Associated level");

            // Create field1
            FieldBuilder fieldBuilder1 = builder.AddSimpleField("Level", typeof(ElementId));

            // Register the schema object
            Schema schema = builder.Finish();

            Field levelId = schema.GetField("Level");

            Entity ent = new Entity(schema);

            ent.Set(levelId, level.Id);

            sheet.SetEntity(ent);
        }
    }
}
