﻿using Autodesk.Revit.UI;
using System;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace DCEStudyTools
{
    class App : IExternalApplication
    {
        // define a method that will create our tab and button
        private static void AddRibbonPanel(UIControlledApplication application)
        {
            // Get dll assembly path
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            // Create a custom ribbon tab
            string tabName = "Take It Easy";
            application.CreateRibbonTab(tabName);

            // Add level tools ribbon panel
            RibbonPanel levelToolRibbonPanel = application.CreateRibbonPanel(tabName, "Level Tools");

            // Create push button for LevelCreation
            PushButton pbLevelCreation = levelToolRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdLevelCreation",
                    "Create" + System.Environment.NewLine + "Levels",
                    thisAssemblyPath,
                    "DCEStudyTools.LevelCreation.LevelCreation",
                    "levels_32.png",
                    String.Empty,
                    "Enter Number Of Floors to Create Levels, ViewPlans and Sheets for Each Level")
            ) as PushButton;

            // Create push button for LevelConfigration
            PushButton pbLevelConfig = levelToolRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdLevelConfig",
                    "Configurate" + System.Environment.NewLine + "Structural Levels",
                    thisAssemblyPath,
                    "DCEStudyTools.Configuration.LevelConfiguration",
                    "level_setting_32.png",
                    String.Empty,
                    "Set the levels for structural elements")
            ) as PushButton;

            // Create push button for LevelRenaming
            PushButton pbLevelRenaming = levelToolRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdLevelRenaming",
                    "Rename" + System.Environment.NewLine + "Structural Levels",
                    thisAssemblyPath,
                    "DCEStudyTools.LevelRenaming.LevelRenaming",
                    "rename_32.png",
                    String.Empty,
                    "Rename the levels for structural elements")
            ) as PushButton;

            // Create push button for LevelTransfer
            PushButton pbLevelTransfer = levelToolRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdLevelTranfer",
                    "Transfer" + System.Environment.NewLine + "Structural Levels",
                    thisAssemblyPath,
                    "DCEStudyTools.LevelTransfer.LevelTransfer",
                    "transfer_32.png",
                    String.Empty,
                    "Tranfer all structural elements from actural levels to the structural levels")
            ) as PushButton;

            ////////////////////////////////////////////////////// Seperate line ////////////////////////////////////////////////////

            // Add view tools ribbon panel
            RibbonPanel viewToolRibbonPanel = application.CreateRibbonPanel(tabName, "View Tools");

            // Create push button for ViewDuplicate
            PushButton pbViewDuplicate = viewToolRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdViewDuplicate",
                    "Duplicate" + System.Environment.NewLine + "Views",
                    thisAssemblyPath,
                    "DCEStudyTools.ViewDuplicate.ViewDuplicate",
                    "duplicate_32.png",
                    String.Empty,
                    "Duplicate the selected view according to the number of zone de définition")
            ) as PushButton;

            // Create push button for 3DViewCreation
            PushButton pb3DView = viewToolRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmd3DViewCreation",
                    "Create/Update" + System.Environment.NewLine + "3D Views",
                    thisAssemblyPath,
                    "DCEStudyTools.ThreeDViewsCreation.ThreeDViewsCreation",
                    "3dview_32.png",
                    String.Empty,
                    "Create a 3D View for each Level")
            ) as PushButton;

            // Create push button for ViewDuplicate
            PushButton pbViewToSheet = viewToolRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdViewToSheet",
                    "Add View" + System.Environment.NewLine + "To Sheet",
                    thisAssemblyPath,
                    "DCEStudyTools.ViewToSheet.ViewToSheet",
                    "view_to_sheet_32.png",
                    String.Empty,
                    "Add corresponding views to the sheet")
            ) as PushButton;

            ////////////////////////////////////////////////////// Seperate line ////////////////////////////////////////////////////

            // Add sheet tools ribbon panel
            RibbonPanel sheetToolRibbonPanel = application.CreateRibbonPanel(tabName, "Sheet Tools");

            // Create push button for SheetCreation
            PushButton pbSheetCreation = sheetToolRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdSheetCreation",
                    "Create" + System.Environment.NewLine + "Sheets",
                    thisAssemblyPath,
                    "DCEStudyTools.SheetCreation.SheetCreation",
                    "sheet_create_32.png",
                    String.Empty,
                    "Create Sheets for Each Level")
            ) as PushButton;

            PushButton pbLegendUpdate = sheetToolRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdLegendUpdate",
                    "Add/Update" + System.Environment.NewLine + "Legend",
                    thisAssemblyPath,
                    "DCEStudyTools.LegendUpdate.LegendUpdate",
                    "legend_32.png",
                    String.Empty,
                    "Update the legend on each sheet")
            ) as PushButton;

            ////////////////////////////////////////////////////// Seperate line ////////////////////////////////////////////////////

            // Add beam tools ribbon panel
            RibbonPanel beamToolRibbonPanel = application.CreateRibbonPanel(tabName, "Beam Tools");

            // Create push button for beam type detect
            PushButton pbBeamTypeDetect = beamToolRibbonPanel.AddItem(
                CreatePushButtonData(
                "cmdBeamTypeCorrect",
                "Correct" + System.Environment.NewLine + "Beam Type",
                thisAssemblyPath,
                "DCEStudyTools.BeamTypeCorrect.BeamTypeCorrect",
                "beam_correct_32.png",
                "beam_correct_16.png",
                "Correct the type of beams whose type are not appropriate")
            ) as PushButton;

            // Create push button for beam type detect
            PushButton pbBeamUnjoint = beamToolRibbonPanel.AddItem(
                CreatePushButtonData(
                "cmdBeamUnjoint",
                "Unjoint" + System.Environment.NewLine + "BN & Talon PV",
                thisAssemblyPath,
                "DCEStudyTools.BeamUnjoint.BeamUnjoint",
                "unjoint_32.png",
                "unjoint_16.png",
                "Unjoint all the \"BN\" and  \"Talon PV\"")
            ) as PushButton;

            // Create push button for beam type change
            PushButton pbBeamTypeChangeData = beamToolRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdBeamTypeChange",
                    "Change" + System.Environment.NewLine + "Beam Type",
                    thisAssemblyPath,
                    "DCEStudyTools.BeamTypeChange.BeamTypeChange",
                    "beam_update_32.png",
                    "beam_update_16.png",
                    "Select beams to be changed and choose the target type")
            ) as PushButton;

            // Create push button data for beam type name adjustment
            PushButtonData pbBeamTypeNameAdjustData = 
                CreatePushButtonData(
                    "cmdBeamTypeNameAdjust",
                    "Adjust Beam" + System.Environment.NewLine + "Type Name",
                    thisAssemblyPath,
                    "DCEStudyTools.BeamTypeNameAdjust.BeamTypeNameAdjust",
                    "beam_name_32.png",
                    "beam_name_16.png",
                    "Update the type name of ALL beam types according to their properties");

            // Create push button data for beam type properties adjustment
            PushButtonData pbBeamTypePropertiesAdjustData = 
                CreatePushButtonData(
                    "cmdBeamTypePropertiesAdjust",
                    "Adjust Beam" + System.Environment.NewLine + "Type Properties",
                    thisAssemblyPath,
                    "DCEStudyTools.BeamTypePropertiesAdjust.BeamTypePropertiesAdjust",
                    "beam_name_32.png",
                    "beam_name_16.png",
                    "Update the properties \"Poutre type\", \"Matérial\", \"Largeur\" and \"Hauteur\" of ALL beam types according to their type name, " +
                    "if their type name is in format of \"XXX-BAdd-ddxdd\"");

            // Create "Beam Adjust" split button
            SplitButtonData sbBeamAdjustData = new SplitButtonData("cmdBeamAdjust", "Beam Type Adjust");
            SplitButton sbBeamAdjust = beamToolRibbonPanel.AddItem(sbBeamAdjustData) as SplitButton;
            sbBeamAdjust.AddPushButton(pbBeamTypeNameAdjustData);
            sbBeamAdjust.AddPushButton(pbBeamTypePropertiesAdjustData);


            ////////////////////////////////////////////////////// Seperate line ////////////////////////////////////////////////////

            // Add column tools ribbon panel
            RibbonPanel columnToolRibbonPanel = application.CreateRibbonPanel(tabName, "Column Tools");

            // Create push button data for column type name adjustment
            PushButtonData pbColumnTypeNameAdjustData = CreatePushButtonData(
                "cmdColumnTypeNameAdjust",
                "Adjust Column" + System.Environment.NewLine + "Type Name",
                thisAssemblyPath,
                "DCEStudyTools.ColumnTypeNameAdjust.ColumnTypeNameAdjust",
                "column_32.png",
                "column_16.png",
                "Update the type name of ALL column types according to their properties");

            // Create push button data for column type properties adjustment
            PushButtonData pbColumnTypePropertiesAdjustData = CreatePushButtonData(
                "cmdColumnTypePropertiesAdjust",
                "Adjust Column" + System.Environment.NewLine + "Type Properties",
                thisAssemblyPath,
                "DCEStudyTools.ColumnTypePropertiesAdjust.ColumnTypePropertiesAdjust",
                "column_32.png",
                "column_16.png",
                "Update the properties \"Matérial\", \"Largeur\" and \"Hauteur\" / \"Diamètre\" of ALL column types according to their type name, " +
                    "if their type name is in format of \"POT-BAdd-ddxdd\" / \"POT-BAdd-Ddd\"");

            // Create "Column Adjust" split button
            SplitButtonData sbColAdjustData = new SplitButtonData("cmdColumnAdjust", "Column Type Adjust");
            SplitButton sbColAdjust = columnToolRibbonPanel.AddItem(sbColAdjustData) as SplitButton;
            sbColAdjust.AddPushButton(pbColumnTypeNameAdjustData);
            sbColAdjust.AddPushButton(pbColumnTypePropertiesAdjustData);

            ////////////////////////////////////////////////////// Seperate line ////////////////////////////////////////////////////

            // Add wall tools ribbon panel
            RibbonPanel wallToolRibbonPanel = application.CreateRibbonPanel(tabName, "Wall Tools");

            // Create push button data for wall type name adjustment
            PushButtonData pbWallTypeNameAdjustData = CreatePushButtonData(
                "cmdWallTypeNameAdjust",
                "Adjust Wall" + System.Environment.NewLine + "Type Name",
                thisAssemblyPath,
                "DCEStudyTools.WallTypeNameAdjust.WallTypeNameAdjust",
                "wall_name_32.png",
                "wall_name_16.png",
                "Update the type name of ALL wall types according to their properties");

            // Create push button data for wall type properties adjustment
            PushButtonData pbWallTypePropertiesAdjustData = CreatePushButtonData(
                "cmdWallTypePropertiesAdjust",
                "Adjust Wall" + System.Environment.NewLine + "Type Properties",
                thisAssemblyPath,
                "DCEStudyTools.WallTypePropertiesAdjust.WallTypePropertiesAdjust",
                "wall_name_32.png",
                "wall_name_16.png",
                "Update the properties \"Matérial\", \"Epaisseur\" of ALL wall types according to their type name, " +
                    "if their type name is in format of \"MUR-BAdd-EPdd\"");

            // Create "Wall Adjust" split button
            SplitButtonData sbWallAdjustData = new SplitButtonData("cmdWallAdjust", "Wall Type Adjust");
            SplitButton sbWallAdjust = wallToolRibbonPanel.AddItem(sbWallAdjustData) as SplitButton;
            sbWallAdjust.AddPushButton(pbWallTypeNameAdjustData);
            sbWallAdjust.AddPushButton(pbWallTypePropertiesAdjustData);

            ////////////////////////////////////////////////////// Seperate line ////////////////////////////////////////////////////

            // Add CAD link tools ribbon panel
            RibbonPanel cadLinkToolRibbonPanel = application.CreateRibbonPanel(tabName, "CAD Link Tools");

            // Create push button for CADLink
            PushButton pbAddLink = cadLinkToolRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdAddLink",
                    "Add" + System.Environment.NewLine + "CAD Link",
                    thisAssemblyPath,
                    "DCEStudyTools.CADLink.CADLink",
                    "link_32.png",
                    String.Empty,
                    "Add CAD links")
            ) as PushButton;

            // Create push button for setting underlayer
            PushButtonData pbSetUnderlayer = 
                CreatePushButtonData(
                    "cmdSetUnderlayer",
                    "Set Underlayer",
                    thisAssemblyPath,
                    "DCEStudyTools.UnderlaySetting.UnderlaySetting",
                    "underlayer_32.png",
                    "underlayer_16.png",
                    "Set underlayer and halftone for all the view plans");

            // Create push button for underlayer undo
            PushButtonData pbUndoUnderlayer =
                CreatePushButtonData(
                    "cmdUndoUnderlayer",
                    "Undo Underlayer",
                    thisAssemblyPath,
                    "DCEStudyTools.UnderlayUndo.UnderlayUndo",
                    "undo_32.png",
                    "undo_16.png",
                    "Undo underlayer for all the view plans");

            cadLinkToolRibbonPanel.AddStackedItems(pbSetUnderlayer, pbUndoUnderlayer);


            // Create push button for setting halftone
            PushButtonData pbSettingHalftone =
                CreatePushButtonData(
                    "cmdSetHalftone",
                    "Set Halftone",
                    thisAssemblyPath,
                    "DCEStudyTools.HalftoneSetting.HalftoneSetting",
                    "Halftone_32.png",
                    "Halftone_16.png",
                    "Set halftone for all the view plans");

            // Create push button for undoing halftone
            PushButtonData pbUndoHalftone = 
                CreatePushButtonData(
                    "cmdUndoHalftone",
                    "Undo Halftone",
                    thisAssemblyPath,
                    "DCEStudyTools.HalftoneUndo.HalftoneUndo",
                    "undo_32.png",
                    "undo_16.png",
                    "Undo halftone for all the view plans");

            cadLinkToolRibbonPanel.AddStackedItems(pbSettingHalftone, pbUndoHalftone);

            ////////////////////////////////////////////////////// Seperate line ////////////////////////////////////////////////////

            // Add tool ribbon panel
            RibbonPanel genericToolsRibbonPanel = application.CreateRibbonPanel(tabName, "Generic Tools");

            // Create push button for Test
            PushButton pbTest = genericToolsRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdTest",
                    "Test/Debug",
                    thisAssemblyPath,
                    "DCEStudyTools.Test.Test",
                    "test_32.png",
                    String.Empty,
                    "")
            ) as PushButton;

            // Create push button for LevelsCreation
            PushButton pbScaleSetting = genericToolsRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdScaleSetting",
                    "Set" + System.Environment.NewLine + "Scale",
                    thisAssemblyPath,
                    "DCEStudyTools.ScaleSetting.ScaleSetting",
                    "scale_32.png",
                    String.Empty,
                    "Set the scale to fit the size of sheet")
            ) as PushButton;

            // Create push button for hiding categories
            PushButton pbCatHide = genericToolsRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdCategoryHiding",
                    "Hide" + System.Environment.NewLine + "Categories",
                    thisAssemblyPath,
                    "DCEStudyTools.CategoriesHiding.CategoriesHiding",
                    "hide_32.png",
                    String.Empty,
                    "Pick up elements, the categories of the elements will be hidden on the active view or on the view template of the active view")
            ) as PushButton;

            // Create push button for TagsAdjustment
            PushButton pbTags = genericToolsRibbonPanel.AddItem(
                CreatePushButtonData(
                   "cmdTagsAdjustment",
                   "Adjust" + System.Environment.NewLine + "Tags",
                   thisAssemblyPath,
                   "DCEStudyTools.TagsAdjustment.TagsAdjustment",
                   "tag_32.png",
                   String.Empty,
                   "Adjust Beam and Column Tags")
            ) as PushButton;

            // Create push button for FloorSpanDirectionChange
            PushButton pbFloorSpanDirection = genericToolsRibbonPanel.AddItem(
                CreatePushButtonData(
                   "cmdFloorSpanDirectionChange",
                   "Change Floor" + System.Environment.NewLine + "Span Direction",
                   thisAssemblyPath,
                   "DCEStudyTools.FloorSpanDirection.FloorSpanDirectionChange",
                   "direction_32.png",
                   String.Empty,
                   "Change Floor Span Direction")
            ) as PushButton;

            // Create push button for FoundationLoad
            PushButton pbFoundationLoad = genericToolsRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdFoundationLoadCreation",
                    "Create" + System.Environment.NewLine + "Foundation Load",
                    thisAssemblyPath,
                    "DCEStudyTools.FoundationLoadCreation.FoundationLoadCreation",
                    "load_32.png",
                    String.Empty,
                    "Create load for each foundation")
            ) as PushButton;


  ////////////////////////////////////////////////////// Seperate line ////////////////////////////////////////////////////

            // Add a new ribbon panel
            RibbonPanel conceptionRibbonPanel = application.CreateRibbonPanel(tabName, "Conception Tools");


            // Create "Wall Marking" button for "Wall Design" split button
            PushButtonData pbAddWallData = CreatePushButtonData(
                "cmdWallMarking",
                "Mark Concrete" + System.Environment.NewLine + "Bearing Walls",
                thisAssemblyPath,
                "DCEStudyTools.Design.Wall.WallMarking",
                "wall_32.png",
                String.Empty,
                "Select the Walls who will be marked as a Concrete Bearing Wall");

            // Create "Wall Removing" button for "Wall Design" split button
            PushButtonData pbRmvWallData = CreatePushButtonData(
                "cmdWallUnmarking",
                "Unmark Concrete" + System.Environment.NewLine + "Bearing Walls",
                thisAssemblyPath,
                "DCEStudyTools.Design.Wall.WallUnmarking",
                "wall_remove_32.png",
                String.Empty,
                "Select the Walls who will be unmarked as a Concrete Bearing Walls");

            // Create "Wall Design" split button
            SplitButtonData sbWallData = new SplitButtonData("cmdWallDesign", "Wall Design");
            SplitButton sbWall = conceptionRibbonPanel.AddItem(sbWallData) as SplitButton;
            sbWall.AddPushButton(pbAddWallData);
            sbWall.AddPushButton(pbRmvWallData);


            // Create "Beam Axe Creation" button for "Beam Design" split button
            PushButtonData pbAddBeamData = CreatePushButtonData(
                "cmdBeamMarking",
                "Mark" + System.Environment.NewLine + "Beam Axe",
                thisAssemblyPath,
                "DCEStudyTools.Design.Beam.BeamAxeMarking.BeamMarkingWithoutDimension",
                "beam_32.png",
                String.Empty,
                "Add Structural Beams for Principal Design");

            // Create "Beam Marking With Dimension" button for "Beam Design" split button
            PushButtonData pbAddWDBeamData = CreatePushButtonData(
                "cmdBeamMarkingWithDimension",
                "Mark Beam Axe" + System.Environment.NewLine + "With Dimension",
                thisAssemblyPath,
                "DCEStudyTools.Design.Beam..BeamAxeMarking.BeamMarkingWithDimension",
                "beam_dimension_32.png",
                String.Empty,
                "Add Structural Beams with Entering Section Dimensions");

            // Create "Beam Dimension Editing" button for "Beam Design" split button
            PushButtonData pbEditSectionData = CreatePushButtonData(
                "cmdBeamSectionEditing",
                "Edit Beam" + System.Environment.NewLine + "Section Dimension",
                thisAssemblyPath,
                "DCEStudyTools.Design.Beam.DimensionEditing.DimensionEditing",
                "dimension_edit_32.png",
                String.Empty,
                "Edit Beam Section Dimension");

            // Create "Structural Beam Creation" button for "Beam Design" split button
            PushButtonData pbCreateBeamData = CreatePushButtonData(
                "cmdBeamCreation",
                "Create Beam" + System.Environment.NewLine + "By Axes",
                thisAssemblyPath,
                "DCEStudyTools.Design.Beam.BeamCreation.BeamCreation",
                "beam_confirm_32.png",
                String.Empty,
                "Create Structural Beams according to the beam axes");

            // Create "Beam Design" split button
            SplitButtonData sbBeamData = new SplitButtonData("cmdBeamDesign", "Beam Design");
            SplitButton sbBeam = conceptionRibbonPanel.AddItem(sbBeamData) as SplitButton;
            sbBeam.AddPushButton(pbAddBeamData);
            sbBeam.AddPushButton(pbAddWDBeamData);
            sbBeam.AddPushButton(pbEditSectionData);
            sbBeam.AddPushButton(pbCreateBeamData);

        }

        private static PushButtonData CreatePushButtonData(
            string buttonDataName,
            string buttonText,
            string assemblyName,
            string className,
            string imageName_32,
            string imageName_16,
            string toolTip)
        {
            PushButtonData pbAddBeamData = new PushButtonData(
                buttonDataName,
                buttonText,
                assemblyName,
                className);
            if (!imageName_32.Equals(String.Empty))
            {
                BitmapImage pbAddBeamImage_32 = new BitmapImage(new Uri($"pack://application:,,,/DCEStudyTools;component/Resources/{imageName_32}"));
                pbAddBeamData.LargeImage = pbAddBeamImage_32;
            }
            if (!imageName_16.Equals(String.Empty))
            {
                BitmapImage pbAddBeamImage_16 = new BitmapImage(new Uri($"pack://application:,,,/DCEStudyTools;component/Resources/{imageName_16}"));
                pbAddBeamData.Image = pbAddBeamImage_16;
            }
            pbAddBeamData.ToolTip = toolTip;
            return pbAddBeamData;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            // do nothing
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            // call our method that will load up our toolbar
            AddRibbonPanel(application);
            return Result.Succeeded;
        }

    }
}
