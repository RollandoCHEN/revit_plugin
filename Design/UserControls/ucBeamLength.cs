using System;
using System.Windows.Forms;

namespace DCEStudyTools.Design.UserControls
{
    public partial class ucBeamLength : UserControl
    {
        public ucBeamLength()
        {
            InitializeComponent();
        }

        public string Length
        {
            get
            {
                return lengthValue.Text;
            }
            set
            {
                lengthValue.Text = value;
            }
        }

        private void ucBeamLength_Load(object sender, EventArgs e)
        {
           
        }
    }
}
