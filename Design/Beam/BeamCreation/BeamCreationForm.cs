using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCEStudyTools.Design.Beam.BeamCreation
{
    public partial class BeamCreationForm : Form
    {

        public bool AxeIsOnArchView
        {
            get
            {
                return axeIsOnArchView.Checked;
            }
        }

        public bool AxeIsOnStrView
        {
            get
            {
                return axeIsOnStrView.Checked;
            }
        }

        public BeamCreationForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        

        private void axeIsOnArchView_CheckedChanged(object sender, EventArgs e)
        {
            axeIsOnStrView.Checked = !axeIsOnArchView.Checked;
        }

        private void axeIsOnStrView_CheckedChanged(object sender, EventArgs e)
        {
            axeIsOnArchView.Checked = !axeIsOnStrView.Checked;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
