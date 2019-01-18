using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DCEStudyTools.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DCEStudyTools.SheetCreation
{
    public partial class SheetCreationForm : System.Windows.Forms.Form
    {
        public string FoundationType
        {
            get
            {
                return ucFoundation.FoundationType;
            }
        }

        public string FireResist
        {
            get
            {
                return ucFireResist.REIValue;
            }
        }

        public int DuplicateNumber
        {
            get
            {
                return ucDuplicataNum.DuplicateNumber;
            }
        }

        public SheetCreationForm()
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
