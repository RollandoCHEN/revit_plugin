using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCEStudyTools.LevelCreation.UserControls
{
    public partial class ucLevels : UserControl
    {
        public ucLevels()
        {
            InitializeComponent();
        }

        public int NumOfLevels
        {
            get
            {
                return (int)numOfLvls.Value;
            }
        }

        public int NumOfBasements
        {
            get
            {
                return (int)numOfBasements.Value;
            }
        }

        private void usLevels_Load(object sender, EventArgs e)
        {

        }
    }
}
