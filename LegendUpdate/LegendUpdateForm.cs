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
        public bool DoseShowLegend
        {
            get
            {
                return ShowLegend.Checked;
            }
        }

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
                else if (BottomRight.Checked)
                {
                    return BottomRight.Name;
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

        private void LegendUpdateForm_Load(object sender, EventArgs e)
        {
            TopLeft.Enabled = ShowLegend.Checked;
            BottomLeft.Enabled = ShowLegend.Checked;
            TopRight.Enabled = ShowLegend.Checked;
            BottomRight.Enabled = ShowLegend.Checked;
        }

        private void ShowLegend_CheckedChanged(object sender, EventArgs e)
        {
            TopLeft.Enabled = ShowLegend.Checked;
            BottomLeft.Enabled = ShowLegend.Checked;
            TopRight.Enabled = ShowLegend.Checked;
            BottomRight.Enabled = ShowLegend.Checked;
        }
    }
}
