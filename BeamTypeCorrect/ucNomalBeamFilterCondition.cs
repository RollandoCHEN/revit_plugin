using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCEStudyTools.BeamTypeCorrect
{
    public partial class ucNomalBeamFilterCondition : UserControl
    {
        public double BNMaxHeight
        {
            set
            {
                BNMaxH.Text = value.ToString("N0");
            }
        }
        public double PVMaxWidth
        {
            set
            {
                PVMaxW.Text = value.ToString("N0");
            }
        }
        public ucNomalBeamFilterCondition()
        {
            InitializeComponent();
        }
    }
}
