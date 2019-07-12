using Autodesk.Revit.DB;
using System;
using System.Linq;
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

        public ImportUnit impUnit;

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

        // TODO: to be enhanced
        public ImportUnit Unit
        {
            get
            {
                switch (unit.Text)
                {
                    case "Mètre":
                        impUnit = ImportUnit.Meter;
                        break;
                    case "Décimètre":
                        impUnit = ImportUnit.Decimeter;
                        break;
                    case "Centimètre":
                        impUnit = ImportUnit.Centimeter;
                        break;
                    case "Milimètre":
                        impUnit = ImportUnit.Millimeter;
                        break;
                }
                return impUnit;
            }

        }

        public CADLinkForm(Dictionary<string, ViewPlan> viewDic)
        {
            InitializeComponent();
            _viewDic = viewDic;
            List<string> keysList = new List<string>(_viewDic.Keys);
            keysList.Add(string.Empty);
            keywordDataGridViewTextBoxColumn.DataSource = keysList;
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

                    string[] filesPaths = Directory.GetFiles(fbd.SelectedPath);
                    foreach (string filePath in filesPaths)
                    {
                        if (filePath.ToLower().Contains("dwg") || filePath.ToLower().Contains("dxf"))
                        {
                            string fileName = Utils.SubstringExtensions.After(filePath, "\\");

                            IList<string> keyList = new List<string>(_viewDic.Keys);
                            if (!keyList.Any(key => filePath.ToLower().Contains(key)))
                            {
                                dataTable.Rows.Add(string.Empty, fileName, filePath);
                            }
                            else
                            {
                                foreach (var item in _viewDic)
                                {
                                    if (filePath.ToLower().Contains(item.Key))
                                    {
                                        dataTable.Rows.Add(item.Key, fileName, filePath);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    _folderIsSelected = true;
                    pathTextbox.Text = fbd.SelectedPath;
                }
            }
        }

        // TODO: to be enhanced
        private void CADLinkForm_Load(object sender, EventArgs e)
        {
            unit.Items.AddRange(new String[] {
                "Mètre","Décimètre","Centimètre","Milimètre"
            });
        }
    }
}
