using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DCEStudyTools.Design.Beam.BeamCreation;
using DCEStudyTools.Design.Beam.DimensionEditing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DCEStudyTools.BeamTypeCorrect
{
    public partial class BeamChangingForm : System.Windows.Forms.Form
    {
        private ExternalEvent _exEvent;
        private ChangeBeamFamilyTypeEvent _handler;
        private UIDocument _uidoc;
        private Document _doc;

        private readonly int ID_COLUMN_INDEX = 0;
        private readonly int CHECKBOX_COLUMN_INDEX = 1;

        private readonly string NOMAL_TAB_NAME = "Poutres Normaux";
        private readonly string GROUND_BEAM_TAB_NAME = "Longrines";
        private readonly string WALL_TAB_NAME = "Murs";

        public BeamChangingForm(UIDocument uidoc, ExternalEvent exEvent, ChangeBeamFamilyTypeEvent handler)
        {
            InitializeComponent();
            _uidoc = uidoc;
            _doc = _uidoc.Document;
            _exEvent = exEvent;
            _handler = handler;
        }

        public DataTable ToNormalDataTable
        {
            get
            {
                return toNormalDataTable;
            }
        }

        public DataGridView ToNormalDataGridView
        {
            get
            {
                return toNormalDataView;
            }
        }

        public DataTable ToGroundBeamDataTable
        {
            get
            {
                return toGroundBeamDataTable;
            }
        }

        public DataGridView ToGroundBeamDataGridView
        {
            get
            {
                return toGroundBeamDataView;
            }
        }

        public DataTable UnsupportedWallDataTable
        {
            get
            {
                return unsupportedWallDataTable;
            }
        }

        public DataGridView UnsupportedWallDataGridView
        {
            get
            {
                return unsupportedWallGridView;
            }
        }

        public IList<Element> BeamsToBeNomal
        {
            get
            {
                IList<Element> beams = new List<Element>();
                foreach (DataGridViewRow row in ToNormalDataGridView.Rows)
                {
                    if (row.Cells[CHECKBOX_COLUMN_INDEX].Value.ToString().Equals("1"))
                    {
                        int intId = int.Parse(row.Cells[0].Value.ToString());
                        ElementId id = new ElementId(intId);
                        beams.Add(_doc.GetElement(id));
                    }
                }
                return beams;
            }
        }

        public IList<Element> BeamsToBeGroundBeam
        {
            get
            {
                IList<Element> beams = new List<Element>();
                foreach (DataGridViewRow row in ToGroundBeamDataGridView.Rows)
                {
                    if (row.Cells[CHECKBOX_COLUMN_INDEX].Value.ToString().Equals("1"))
                    {
                        int iId = int.Parse(row.Cells[0].Value.ToString());
                        ElementId id = new ElementId(iId);
                        beams.Add(_doc.GetElement(id));
                    }
                }
                return beams;
            }
        }

        private void toNomalDataView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            toN_ApplyButton.Enabled = true;
        }

        private void toGroundBeamDataView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            toGB_ApplyButton.Enabled = true;
        }

        private void toGB_ApplyButton_Click(object sender, EventArgs e)
        {
            _handler.BeamsToBeGoundBeam = BeamsToBeGroundBeam;
            _exEvent.Raise();
            toGB_ApplyButton.Enabled = false;
        }

        private void toN_ApplyButton_Click(object sender, EventArgs e)
        {
            _handler.BeamsToBeNormal = BeamsToBeNomal;
            _exEvent.Raise();
            toN_ApplyButton.Enabled = false;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (toGB_ApplyButton.Enabled)
            {
                _handler.BeamsToBeGoundBeam = BeamsToBeGroundBeam;
                _exEvent.Raise();
            }
            if (toN_ApplyButton.Enabled)
            {
                _handler.BeamsToBeNormal = BeamsToBeNomal;
                _exEvent.Raise();
            }
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BeamChangingForm_Load(object sender, EventArgs e)
        {
            ListenerHandler toNomalDataViewHandler 
                = new ListenerHandler(_uidoc, ToNormalDataGridView, ToNormalDataTable);

            ListenerHandler toGroundBeamDataViewHandler
                = new ListenerHandler(_uidoc, ToGroundBeamDataGridView, ToGroundBeamDataTable);

            if (toNormalDataView.Rows.Count == 0)
            {
                ((System.Windows.Forms.Control)toN_TabPage).Enabled = false;
                toN_ApplyButton.Enabled = false;
            }

            if (toGroundBeamDataView.Rows.Count == 0)
            {
                ((System.Windows.Forms.Control)toGB_TabPage).Enabled = false;
                toGB_ApplyButton.Enabled = false;
            }

            toN_TabPage.Text = NOMAL_TAB_NAME + $"({ToNormalDataGridView.Rows.Count})";
            toGB_TabPage.Text = GROUND_BEAM_TAB_NAME + $"({ToGroundBeamDataGridView.Rows.Count})";
            wall_tabPage.Text = WALL_TAB_NAME + $"({UnsupportedWallDataGridView.Rows.Count})";
        }

        internal class ListenerHandler
        {
            private UIDocument _uidoc;
            private DataTable _dataTable;
            private DataGridView _dataGridView;
            private CheckBox _headerCheckBox;

            private readonly int ID_COLUMN_INDEX = 0;
            private readonly int CHECKBOX_COLUMN_INDEX = 1;

            public ListenerHandler(UIDocument uidoc, DataGridView dataGridView, DataTable dataTable)
            {
                _uidoc = uidoc;
                _dataGridView = dataGridView;
                _dataTable = dataTable;

                AddHeadCheckBox();

                //Assign Click event to the DataGridView Cell.
                _dataGridView.CellContentClick += new DataGridViewCellEventHandler(dataGridView_CellClick);

                _dataGridView.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView_DoubleClick);
            }

            private void AddHeadCheckBox()
            {
                //Find the Location of Header Cell.
                System.Drawing.Point headerCellLocation = _dataGridView.GetCellDisplayRectangle(1, -1, true).Location;

                //Place the Header CheckBox in the Location of the Header Cell.
                _headerCheckBox = new CheckBox
                {
                    BackColor = System.Drawing.Color.White,
                    Size = new Size(18, 18),
                    Location = new System.Drawing.Point(headerCellLocation.X + 12, headerCellLocation.Y + 8),
                    Checked = true
                };

                //Assign Click event to the Header CheckBox.
                _headerCheckBox.Click += new EventHandler(headerCheckBox_Clicked);
                _dataGridView.Controls.Add(_headerCheckBox);

            }

            private void headerCheckBox_Clicked(object sender, EventArgs e)
            {
                //Necessary to end the edit mode of the Cell.
                _dataGridView.EndEdit();

                //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
                foreach (DataGridViewRow row in _dataGridView.Rows)
                {
                    DataGridViewCheckBoxCell checkBox 
                        = (row.Cells[CHECKBOX_COLUMN_INDEX] as DataGridViewCheckBoxCell);
                    checkBox.Value = _headerCheckBox.Checked;
                }
            }

            private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                //Check to ensure that the row CheckBox is clicked.
                if (e.RowIndex >= 0 && e.ColumnIndex == CHECKBOX_COLUMN_INDEX)
                {
                    //Loop to verify whether all row CheckBoxes are checked or not.
                    bool isChecked = true;
                    foreach (DataGridViewRow row in _dataGridView.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[CHECKBOX_COLUMN_INDEX].EditedFormattedValue) == false)
                        {
                            isChecked = false;
                            break;
                        }
                    }
                    _headerCheckBox.Checked = isChecked;
                }
            }

            private void dataGridView_DoubleClick(object sender, DataGridViewCellEventArgs e)
            {
                int currentRow = 0;
                try
                {
                    currentRow = e.RowIndex;
                }
                catch (Exception)
                {
                    return;
                }
                if (currentRow < _dataGridView.Rows.Count && currentRow >= 0)
                {
                    int idInt = Convert.ToInt32(_dataGridView.Rows[currentRow].Cells[ID_COLUMN_INDEX].Value);
                    ElementId id = new ElementId(idInt);
                    _uidoc.Selection.SetElementIds(new List<ElementId> { id });
                    _uidoc.ShowElements(new List<ElementId> { id });
                }
            }
        }

        private void unsupportedWallGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentRow = 0;
            try
            {
                currentRow = e.RowIndex;
            }
            catch (Exception)
            {
                return;
            }
            if (currentRow < UnsupportedWallDataGridView.Rows.Count && currentRow >= 0)
            {
                int idInt = Convert.ToInt32(UnsupportedWallDataGridView.Rows[currentRow].Cells[ID_COLUMN_INDEX].Value);
                ElementId id = new ElementId(idInt);
                _uidoc.Selection.SetElementIds(new List<ElementId> { id });
                _uidoc.ShowElements(new List<ElementId> { id });
            }
        }
    }
}
