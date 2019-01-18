using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DCEStudyTools.Design.Beam.BeamCreation;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DCEStudyTools.Design.Beam.DimensionEditing
{
    public partial class DimensionEditingForm : System.Windows.Forms.Form
    {
        private ExternalEvent _exEvent;
        private SaveDimensionEvent _handler;
        private Document _doc;

        public DimensionEditingForm(Document doc, ExternalEvent exEvent, SaveDimensionEvent handler)
        {
            InitializeComponent();
            _exEvent = exEvent;
            _handler = handler;
            _doc = doc;
            ModifNotSaved = false;
        }

        public DataTable DataTable
        {
            get
            {
                return dataTable;
            }
        }

        public DataGridView DataGridView
        {
            get
            {
                return dataGridView;
            }
        }

        public Button ApplyButton
        {
            get
            {
                return applyButton;
            }
        }

        public bool ModifNotSaved { get; private set; }

        private void DataGridView_DataBindingComplete(object sender, EventArgs e)
        {
            StaticMethods.InitializeDataTableColor(dataGridView);
        }

        public void SaveModif()
        {
            _exEvent.Raise();
            ModifNotSaved = false;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            // TODO : Figure out why the data don't save when ok button clicked
            SaveModif();
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            SaveModif();
            applyButton.Enabled = false;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _exEvent.Dispose();
            _exEvent = null;
            _handler = null;

            base.OnFormClosed(e);
        }

        private void DimensionEditingForm_Load(object sender, EventArgs e)
        {
            typeDataGridViewTextBoxColumn1.Items.AddRange(BeamType.StringValues.Cast<string>().ToArray());
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                applyButton.Enabled = true;
                ModifNotSaved = true;

                DataRow changedDataRow = dataTable.Rows[e.RowIndex];
                int idInt = changedDataRow.Field<int>("Id");
                ElementId id = new ElementId(idInt);
                ModelLine modelLine = _doc.GetElement(id) as ModelLine;

                EntityToUpdateLine lineToUpdate = new EntityToUpdateLine(
                    modelLine,
                    changedDataRow.Field<double>("Hauteur(cm)"),
                    changedDataRow.Field<double>("Largeur(cm)"),
                    changedDataRow.Field<string>("Type"));

                _handler.LinesToUpdate.Add(lineToUpdate);
            }
        }

        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText =
                dataGridView.Columns[e.ColumnIndex].HeaderText;

            if (headerText.Equals("Hauteur(cm)") || headerText.Equals("Largeur(cm)"))
            {
                try
                {
                    double.Parse(e.FormattedValue.ToString());
                }
                catch (Exception)
                {
                    dataGridView.Rows[e.RowIndex].ErrorText =
                        $"Veuillez entrer une valeur de longeur pour {headerText}";
                    e.Cancel = true;
                }
            }
            else
            {
                return;
            }
        }

        void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string headerText =
                dataGridView.Columns[e.ColumnIndex].HeaderText;

            if (headerText.Equals("Hauteur(cm)") || headerText.Equals("Largeur(cm)"))
            {
                double value = double.Parse(dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                if (value <= 10)
                {
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText =
                        $"Attention, vous avez saisi une longeur moins de 10 cm";
                }
                if (value >= 100)
                {
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText =
                        $"Attention, vous avez saisi une longeur plus de 1 m"; ;
                }
            }
            // Clear the row error in case the user presses ESC.   
            dataGridView.Rows[e.RowIndex].ErrorText = String.Empty;
        }
    }
}
