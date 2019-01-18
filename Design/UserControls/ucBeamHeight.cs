using System;
using System.Windows.Forms;

namespace DCEStudyTools.Design.UserControls
{
    public partial class ucBeamHeight : UserControl
    {
        public ucBeamHeight()
        {
            InitializeComponent();
        }

        public double BeamHeight
        {
            get
            {
                return usLengthBox.Length;
            }
        }

        private void BeamHeight_Load(object sender, EventArgs e)
        {

        }
    }
}
