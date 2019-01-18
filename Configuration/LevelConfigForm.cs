using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace DCEStudyTools.Configuration
{

    public partial class LevelConfigForm : System.Windows.Forms.Form
    {
        private LevelConfiguration _dataBuffer;

        public LevelConfigForm(LevelConfiguration dataBuffer)
        {
            InitializeComponent();
            _dataBuffer = dataBuffer;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            List<string> strLevels = checkedListBox.CheckedItems.Cast<string>().ToList();
            _dataBuffer.AddSTRMarkToLevel(strLevels);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void LevelConfigForm_Load(object sender, EventArgs e)
        {
            checkedListBox.Items.AddRange(_dataBuffer.AllLevelNames);
            
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                string levelName = checkedListBox.Items[i] as string;
                if (_dataBuffer.ActuralStrLevelNames.Any(name => name.Equals(levelName)))
                {
                    checkedListBox.SetItemChecked(i, true);
                }
            }

            if (checkedListBox.CheckedItems.Count == checkedListBox.Items.Count)
            {
                checkAll.Checked = true;
            }
            else
            {
                checkAll.Checked = false;
            }
        }

        private void allCheck_Click(object sender, EventArgs e)
        {
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            for (int index = 0; index < checkedListBox.Items.Count; index++)
            {
                checkedListBox.SetItemChecked(index, checkAll.Checked);
            }
        }

        private void checkedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool allChecked = true;
            for (int index = 0; index < checkedListBox.Items.Count; index++)
            {
                if (!checkedListBox.GetItemChecked(index))
                {
                    allChecked = false;
                    break;
                }
            }
            checkAll.Checked = allChecked;
        }

        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool allChecked = true;
            for (int index = 0; index < checkedListBox.Items.Count; index++)
            {
                if (!checkedListBox.GetItemChecked(index))
                {
                    allChecked = false;
                    break;
                }
            }
            checkAll.Checked = allChecked;
        }
    }
}
