using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCEStudyTools.LegendUpdate
{
    public partial class LegendUpdateForm : Form
    {
        public string SelectedLegendPosition
        {
            get
            {
                if (TopLeft.Checked)
                {
                    return TopLeft.Name;
                }
                else if (BottomLeft.Checked)
                {
                    return BottomLeft.Name;
                }
                else if (TopRight.Checked)
                {
                    return TopRight.Name;
                }
                else if (TopRight.Checked)
                {
                    return TopRight.Name;
                }
                return String.Empty;
            }
        }

        public LegendUpdateForm()
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
    }
}
