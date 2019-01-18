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
    public partial class ucDuplicataNum : UserControl
    {
        public ucDuplicataNum()
        {
            InitializeComponent();
        }

        public int DuplicateNumber
        {
            get
            {
               return (int)duplicateNum.Value;
            }
        }
        private void DuplicataNum_Load(object sender, EventArgs e)
        {

        }
    }
}
