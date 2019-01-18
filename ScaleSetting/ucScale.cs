using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DCEStudyTools.ScaleSetting
{
    public partial class ucScale : UserControl
    {
        public ucScale()
        {
            InitializeComponent();
        }

        public int ViewScale
        {
            get
            {
                string[] numbers = Regex.Split(scale.Text, @"\D+");
                if (numbers.Length == 0)
                {
                    return 0;
                }
                else
                {
                    return int.Parse(numbers[1]);
                }
            }

            set
            {
                if (value != 0)
                {
                    scale.Text = $"1:{value}";
                }
                else
                {
                    scale.Text = string.Empty;
                }
            }
        }

        public string Label2
        {
            set
            {
                label2.Text = value;
            }
        }

        private void ucScale_Load(object sender, EventArgs e)
        {

        }
    }
}
