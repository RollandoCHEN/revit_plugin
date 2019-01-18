using System;
using System.ComponentModel;
using System.Windows.Forms;
using Autodesk.Revit.UI;

namespace DCEStudyTools.Design.UserControls
{
    public partial class usLengthTextBox : UserControl
    {
        public usLengthTextBox()
        {
            InitializeComponent();
        }

        public usLengthTextBox(double maxLimitWarn, double minLimitWarn)
        {
            InitializeComponent();
            MaxLimitWarn = maxLimitWarn;
            MinLimitWarn = minLimitWarn;
            lengthBox.Validating += new CancelEventHandler(lengthTextBox1_Validating);
        }

        public double Length
        {
            get
            {
                return double.Parse(lengthBox.Text);
            }
        }

        public double MaxLimitWarn { get; set; }
        public double MinLimitWarn { get; set; }

        private void lengthTextBox1_Validating(object sender, CancelEventArgs e)
        {
            double value = MinLimitWarn;
            try
            {
                value = double.Parse(lengthBox.Text);
                if (value <= MinLimitWarn)
                {
                    TaskDialog.Show("Revit", $"Attention, vous avez saisi une longeur moins de {string.Format("{0:f0}",MinLimitWarn)} cm.");
                }
                if (value >= MaxLimitWarn)
                {
                    TaskDialog.Show("Revit", $"Attention, vous avez saisi une longeur plus de {string.Format("{0:f0}", MaxLimitWarn)} cm.");
                }
            }
            catch (Exception)
            {
                TaskDialog.Show("Revit", "Veuillez entrer une valeur de longeur en cm.");
                lengthBox.Text = "";
            }
        }
    }
}
