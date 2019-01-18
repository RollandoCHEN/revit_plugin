using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCEStudyTools.LevelCreation.UserControls
{
    public partial class ucHeight : UserControl
    {
        public ucHeight()
        {
            InitializeComponent();
        }

        public double LevelHeight_Meters
        {
            get
            {
                Double.TryParse(levelHeight.Text, out double lvlsHghtMeters);
                return lvlsHghtMeters;
            }
        }

        public double BasementHeight_Meters
        {
            get
            {
                Double.TryParse(basementHeight.Text, out double baseHghtMeters);
                return baseHghtMeters;
            }
        }

        private void ucHeight_Load(object sender, EventArgs e)
        {

        }

        private void defaultValue_CheckedChanged(object sender, EventArgs e)
        {
            basementHeight.Enabled = (defaultHeight.CheckState != CheckState.Checked);
            levelHeight.Enabled = (defaultHeight.CheckState != CheckState.Checked);
        }
    }
}
