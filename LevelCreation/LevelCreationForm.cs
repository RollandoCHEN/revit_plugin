using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DCEStudyTools.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DCEStudyTools.LevelCreation
{
    public partial class LevelCreationForm : System.Windows.Forms.Form
    {
        public int NumOfLevels
        {
            get
            {
                return ucLevels.NumOfLevels;
            }
        }

        public int NumOfBasements
        {
            get
            {
                return ucLevels.NumOfBasements;
            }
        }

        public double LevelHeight_Meters
        {
            get
            {
                return ucHeight.LevelHeight_Meters;
            }
        }

        public double BasementHeight_Meters
        {
            get
            {
                return ucHeight.BasementHeight_Meters;
            }
        }

        public LevelCreationForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
