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
    public partial class ucFireResist : UserControl
    {
        public ucFireResist()
        {
            InitializeComponent();
        }

        public string REIValue
        {
            get
            {
                return fireResist.SelectedItem?.ToString();
            }
        }

        private void FireResist_Load(object sender, EventArgs e)
        {

        }
    }
}
