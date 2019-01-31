using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace DCEStudyTools.CADLink
{
    public partial class CADLinkForm : System.Windows.Forms.Form
    {
        private Dictionary<string, ViewPlan> _viewDic;

        private bool _folderIsSelected = false;

        public DataTable DataTable
        {
            get
            {
                return dataTable;
            }
        }

        public DataGridView DataGridView
        {
            get
            {
                return dataGridView;
            }
        }

        public CADLinkForm(Dictionary<string, ViewPlan> viewDic)
        {
            InitializeComponent();
            _viewDic = viewDic;
            keywordDataGridViewTextBoxColumn.DataSource = new List<string>(_viewDic.Keys);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (_folderIsSelected)
            {
                DialogResult = DialogResult.OK;
            }
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            dataTable.Clear();
            using (var fbd = new FolderBrowserDialog())
            {
                if (!string.IsNullOrWhiteSpace(pathTextbox.Text))
                {
                    fbd.SelectedPath = $"{pathTextbox.Text}";
                }
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {

                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    foreach (string file in files)
                    {
                        if (file.ToLower().Contains("dwg") || file.ToLower().Contains("dxf"))
                        {
                            foreach (var item in _viewDic)
                            {
                                if (file.ToLower().Contains(item.Key))
                                {
                                    dataTable.Rows.Add(item.Key, file);
                                }
                                
                            }
                            
                        }
                    }
                    _folderIsSelected = true;
                    pathTextbox.Text = fbd.SelectedPath;
                }
            }
        }
        
    }
}
