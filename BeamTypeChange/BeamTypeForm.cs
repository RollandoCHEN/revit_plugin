using DCEStudyTools.Design.Beam.BeamCreation;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DCEStudyTools.BeamTypeChange
{
    public partial class BeamTypeForm : Form
    {

        public string ChoosenType
        {
            get
            {
                return choosonBeamType.Text;
            }
        }

        public BeamTypeForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BeamTypeForm_Load(object sender, EventArgs e)
        {
            choosonBeamType.Items.AddRange(BeamType.StringValues.Cast<string>().ToArray());
        }
    }
}
