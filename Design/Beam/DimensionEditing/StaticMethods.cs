
using System.Drawing;
using System.Windows.Forms;

namespace DCEStudyTools.Design.Beam.DimensionEditing
{
    class StaticMethods
    {
        public static void InitializeDataTableColor(DataGridView dataGridView)
        {
            dataGridView.ClearSelection();
            
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.HeaderText.Equals("Niveau") || column.HeaderText.Contains("Longeur"))
                {
                    column.DefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
                }
                else
                {
                    column.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
    }
}
