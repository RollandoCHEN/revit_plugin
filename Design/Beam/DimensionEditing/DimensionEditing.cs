using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using DCEStudyTools.Design.Beam.BeamAxeMarking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace DCEStudyTools.Design.Beam.DimensionEditing
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class DimensionEditing : BeamMarking, IExternalCommand
    {
        static WindowHandle _hWndRevit = null;

        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;

        private DimensionEditingForm _def;

        private ExternalEvent _exEvent;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (null == _hWndRevit)
            {
                Process[] processes
                  = Process.GetProcessesByName("Revit");

                if (0 < processes.Length && processes.Any(p => p == Process.GetCurrentProcess()))
                {
                    IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
                    _hWndRevit = new WindowHandle(h);
                }
            }
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            SaveDimensionEvent handler = new SaveDimensionEvent();
            _exEvent = ExternalEvent.Create(handler);

            _def = new DimensionEditingForm(_doc, _exEvent, handler);
            _def.Show(_hWndRevit);

            AddModelLineInfosToDataTable(_def);

            _def.DataGridView.CellDoubleClick += new DataGridViewCellEventHandler(dataGrid_DoubleClick);

            _uiapp.Application.DocumentChanged += 
                new EventHandler<DocumentChangedEventArgs>(CtrlApp_DocumentChanged);

            foreach (Autodesk.Windows.RibbonTab tab in Autodesk.Windows.ComponentManager.Ribbon.Tabs)
            {
                if (tab.Id == "Modify")
                {
                    tab.PropertyChanged += PanelEvent;
                    break;
                }
            }

            _def.FormClosed += new FormClosedEventHandler(formClosed);
            
            return Result.Succeeded;
        }

        private void formClosed(object sender, FormClosedEventArgs e)
        {
            // Remove the Selection Listener
            foreach (Autodesk.Windows.RibbonTab tab in Autodesk.Windows.ComponentManager.Ribbon.Tabs)
            {
                if (tab.Id == "Modify")
                {
                    tab.PropertyChanged -= PanelEvent;
                    break;
                }
            }
        }

        private void dataGrid_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentRow = 0;
            try
            {
                currentRow = _def.DataGridView.CurrentCell.RowIndex;
            }
            catch (Exception)
            {
                return;
            }
            if (currentRow < _def.DataTable.Rows.Count)
            {
                int idInt = _def.DataTable.Rows[currentRow].Field<int>(0);
                ElementId id = new ElementId(idInt);
                _uidoc.Selection.SetElementIds(new List<ElementId> { id });
                _uidoc.ShowElements(new List<ElementId> { id });  
            }
        }

        private void AddModelLineInfosToDataTable(DimensionEditingForm def)
        {
            def.DataTable.Rows.Clear();
            // Get all the model lines created by our plugin in the doc
            IList<CurveElement> lines =
                (from line in new FilteredElementCollector(_doc)
                    .OfClass(typeof(CurveElement))
                    .OfCategory(BuiltInCategory.OST_Lines)
                    .Cast<CurveElement>()
                 where (line as ModelLine) != null && line.GetEntitySchemaGuids().Count != 0
                 select line)
                    .ToList();

            // Add infos to eht data table for each model line
            IList<object[]> linesParams = new List<object[]>();
            foreach (CurveElement line in lines)
            {
                ModelLine modelLine = line as ModelLine;
                Schema lineSchema = Schema.Lookup(Utils.Guids.MODELLINE_SCHEMA_GUID);
                Entity lineSchemaEnt = modelLine.GetEntity(lineSchema);

                // Get line Id
                int lineId = modelLine.Id.IntegerValue;

                // Get associated level of the line
                string lineLevel = modelLine.SketchPlane.Name;

                // Get line length
                double lineLength = Math.Round(
                    UnitUtils.Convert(modelLine.GeometryCurve.Length,
                    DisplayUnitType.DUT_DECIMAL_FEET,
                    DisplayUnitType.DUT_METERS),
                    2);

                // Get beam type
                string beamType = lineSchemaEnt.Get<string>(lineSchema.GetField("BeamType"));

                // Get beam section height
                double beamHeight = lineSchemaEnt.Get<double>(
                        lineSchema.GetField("SectionHeight"),
                        DisplayUnitType.DUT_CENTIMETERS);

                // Get beam section width
                double beamWidth = lineSchemaEnt.Get<double>(
                        lineSchema.GetField("SectionWidth"),
                        DisplayUnitType.DUT_CENTIMETERS);

                // Add infos to the datatable
                def.DataTable.Rows.Add(
                    lineId, 
                    lineLevel,
                    lineLength, 
                    beamType, 
                    beamHeight, 
                    beamWidth);
            };
        }

        private void CtrlApp_DocumentChanged(object sender, DocumentChangedEventArgs e)
        {
            // TODO : if the data table is modified, prompt user to choose if save the modifiation
            if (_def.ModifNotSaved)
            {
                _def.SaveModif();
            }
            AddModelLineInfosToDataTable(_def);
        }

        void PanelEvent(object sender,
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            System.Diagnostics.Debug.Assert(sender is Autodesk.Windows.RibbonTab,
                "expected sender to be a ribbon tab");
            if (e.PropertyName == "Title")
            {
                try
                {
                    _def.DataGridView.ClearSelection();
                    // Get the element selection of current document.
                    Selection selection = _uidoc.Selection;
                    ICollection<ElementId> selectedIds = _uidoc.Selection.GetElementIds();

                    if (0 != selectedIds.Count)
                    {
                        foreach (DataRow row in _def.DataTable.AsEnumerable())
                        {
                            if (selectedIds.Any(id => id.IntegerValue == row.Field<int>("Id")))
                            {
                                _def.DataGridView.ClearSelection();
                                int indexOfRow = _def.DataTable.Rows.IndexOf(row);
                                _def.DataGridView.Rows[indexOfRow].Selected = true;
                            }

                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
