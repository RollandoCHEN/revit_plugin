using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCEStudyTools.Test
{
    public partial class StringForm : Form
    {
        public StringForm()
        {
            InitializeComponent();
        }

        public string ViewName
        {
            get
            {
                return textBox1.Text;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
