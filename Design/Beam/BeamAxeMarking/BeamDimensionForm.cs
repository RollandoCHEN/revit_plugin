using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Forms;

namespace DCEStudyTools.Design.Beam
{
    public partial class BeamDimensionForm : System.Windows.Forms.Form
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;
        public double BeamHeight { get; private set; }
        public double BeamWidth { get; private set; }
        public string BeamType { get; private set; }

        public BeamDimensionForm(ExternalCommandData commandData, double lineLength)
        {
            InitializeComponent();
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            ucBeamLength.Length = string.Format("{0:N2}", lineLength);
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            BeamHeight = ucBeamHeight.BeamHeight;
            BeamWidth = ucBeamWidth.BeamWidth;
            BeamType = ucBeamType.BeamType;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
