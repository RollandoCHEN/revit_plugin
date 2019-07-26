using Autodesk.Revit.UI;
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
            string tabName = "Outils APS";
            application.CreateRibbonTab(tabName);

            // Add "APS" tools ribbon panel
            RibbonPanel apsToolRibbonPanel = application.CreateRibbonPanel(tabName, "APS avec DWG");

            // Create push button for LevelCreation
            PushButton pbLevelCreation = apsToolRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdLevelCreation",
                    "Create" + System.Environment.NewLine + "Levels",
                    thisAssemblyPath,
                    "DCEStudyTools.LevelCreation.LevelCreation",
                    "levels_32.png",
                    String.Empty,
                    "Enter Number Of Floors to Create Levels, ViewPlans and Sheets for Each Level")
            ) as PushButton;

            // Create push button for SheetCreation
            PushButton pbSheetCreation = apsToolRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdSheetCreation",
                    "Create" + System.Environment.NewLine + "Sheets",
                    thisAssemblyPath,
                    "DCEStudyTools.SheetCreation.SheetCreation",
                    "sheet_create_32.png",
                    String.Empty,
                    "Create Sheets for Each Level")
            ) as PushButton;

            // Create push button for CADLink
            PushButton pbAddLink = apsToolRibbonPanel.AddItem(
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
                    "Set underlayer for all the view plans");

            // Create push button for undo underlayer
            PushButtonData pbUndoUnderlayer =
                CreatePushButtonData(
                    "cmdUndoUnderlayer",
                    "Undo Underlayer",
                    thisAssemblyPath,
                    "DCEStudyTools.UnderlayUndo.UnderlayUndo",
                    "undo_32.png",
                    "undo_16.png",
                    "Undo underlayer for all the view plans");

            apsToolRibbonPanel.AddStackedItems(pbSetUnderlayer, pbUndoUnderlayer);

            // Create push button for setting halftone
            PushButtonData pbSetHalftone =
                CreatePushButtonData(
                    "cmdSetHalftone",
                    "Set Halftone",
                    thisAssemblyPath,
                    "DCEStudyTools.HalftoneSetting.HalftoneSetting",
                    "halftone_32.png",
                    "halftone_16.png",
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

            apsToolRibbonPanel.AddStackedItems(pbSetHalftone, pbUndoHalftone);

            // Create push button for ViewToSheet
            PushButton pbViewToSheet = apsToolRibbonPanel.AddItem(
                CreatePushButtonData(
                    "cmdViewToSheet",
                    "Add View" + System.Environment.NewLine + "To Sheet",
                    thisAssemblyPath,
                    "DCEStudyTools.ViewToSheet.ViewToSheet",
                    "view_to_sheet_32.png",
                    String.Empty,
                    "Add corresponding views to the sheet")
            ) as PushButton;

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
