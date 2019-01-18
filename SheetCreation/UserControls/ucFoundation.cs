using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCEStudyTools.SheetCreation.UserControls
{
    public partial class ucFoundation : UserControl
    {
        public ucFoundation()
        {
            InitializeComponent();
        }

        public string FoundationType
        {
            get
            {
                return foundation.SelectedItem?.ToString();
            }
        }

        private void ucFoundation_Load(object sender, EventArgs e)
        {

        }
    }
}
