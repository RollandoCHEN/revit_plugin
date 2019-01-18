using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCEStudyTools.ScaleSetting
{
    public partial class SetScaleForm : Form
    {
        public SetScaleForm(int scale)
        {
            InitializeComponent();
            ucScale.ViewScale = scale;
            if (scale == 0)
            {
                ucScale.Label2 = "Diviser les plans de niveau en plusieurs parties !";
            }
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
    }
}
