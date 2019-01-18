using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCEStudyTools.Design.UserControls
{
    public partial class ucBeamWidth : UserControl
    {
        public ucBeamWidth()
        {
            InitializeComponent();
        }

        public double BeamWidth
        {
            get
            {
                return usLengthBox.Length;
            }
        }

        private void ucBeamWidth_Load(object sender, EventArgs e)
        {

        }
    }
}
