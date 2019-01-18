using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCEStudyTools.FloorSpanDirection
{
    public partial class FloorDirectionForm : Form
    {
        public double SelectedSpanDirectionAngle
        {
            get
            {
                double angle = 0;
                if (LeftArrow.Checked)
                {
                    angle = 0;
                }
                else if (BottomArrow.Checked)
                {
                    angle = Math.PI / 2;
                }
                else if (RightArrow.Checked)
                {
                    angle = Math.PI;
                }
                else if (TopArrow.Checked)
                {
                    angle = Math.PI * 3 / 2;
                }
                return angle;
            }
        }

        public FloorDirectionForm()
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
