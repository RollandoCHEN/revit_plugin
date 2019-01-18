using System;
using System.Linq;
using System.Windows.Forms;

namespace DCEStudyTools.Design.UserControls
{
    public partial class ucBeamType : UserControl
    {
        public ucBeamType()
        {
            InitializeComponent();
        }

        public string BeamType
        {
            get
            {
                return beamType.Text;
            }
        }

        private void ucBeamType_Load(object sender, EventArgs e)
        {
            beamType.Items.AddRange(Beam.BeamCreation.BeamType.StringValues.Cast<string>().ToArray());
        }
    }
}
