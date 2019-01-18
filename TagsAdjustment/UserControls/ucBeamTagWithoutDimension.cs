using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCEStudyTools.TagsAdjustment.UserControls
{
    public partial class ucBeamTagWithoutDimension : UserControl
    {
        public string SelectedTagName
        {
            get
            {
                return tagComboBox.Text;
            }

            set
            {
                tagComboBox.Text = value;
            }
        }

        public bool ComboBoxEnable
        {
            set
            {
                tagComboBox.Enabled = value;
            }
        }

        public IList<string> TagList
        {
            set
            {
                tagComboBox.Items.AddRange(value.ToArray());
            }
        }

        public ucBeamTagWithoutDimension()
        {
            InitializeComponent();
        }
    }
}
