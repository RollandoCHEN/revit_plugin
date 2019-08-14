using Autodesk.Revit.DB;
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
    public partial class StringForm : System.Windows.Forms.Form
    {
        private IList<ViewPlan> _viewplansList;
        private Document _doc;
        public StringForm(Document doc,IList<ViewPlan> viewplansList)
        {
            InitializeComponent();
            _viewplansList = viewplansList;
            _doc = doc;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void StringForm_Load(object sender, EventArgs e)
        {
            foreach (ViewPlan viewPlan in _viewplansList)
            {
                if (viewPlan.GetPrimaryViewId() == ElementId.InvalidElementId)
                {
                    TreeNode node = treeView1.Nodes.Add(viewPlan.Name);
                    if (viewPlan.GetDependentViewIds().Count != 0)
                    {
                        foreach (ElementId elementId in viewPlan.GetDependentViewIds())
                        {
                            ViewPlan view = _doc.GetElement(elementId) as ViewPlan;
                            node.Nodes.Add(view.Name);
                        }
                    }
                }
            }

            
        }
    }
}
