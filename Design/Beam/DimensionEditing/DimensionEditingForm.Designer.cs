using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DCEStudyTools.Design.Beam.DimensionEditing
{
    partial class DimensionEditingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DimensionEditingForm));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.levelDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.longeurmDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.hauteurcmDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.largeurcmDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataTable = new System.Data.DataTable();
            this.idDataGridViewTextBoxColumn = new System.Data.DataColumn();
            this.levelDataGridViewTextBoxColumn = new System.Data.DataColumn();
            this.longeurmDataGridViewTextBoxColumn = new System.Data.DataColumn();
            this.typeDataGridViewTextBoxColumn = new System.Data.DataColumn();
            this.hauteurcmDataGridViewTextBoxColumn = new System.Data.DataColumn();
            this.largeurcmDataGridViewTextBoxColumn = new System.Data.DataColumn();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeight = 32;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn1,
            this.levelDataGridViewTextBoxColumn1,
            this.longeurmDataGridViewTextBoxColumn1,
            this.typeDataGridViewTextBoxColumn1,
            this.hauteurcmDataGridViewTextBoxColumn1,
            this.largeurcmDataGridViewTextBoxColumn1});
            this.dataGridView.DataSource = this.dataTable;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView.Location = new System.Drawing.Point(12, 17);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView.Name = "dataGridView";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView.RowTemplate.Height = 26;
            this.dataGridView.Size = new System.Drawing.Size(720, 296);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            this.dataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView_CellValidating);
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            this.dataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.DataGridView_DataBindingComplete);
            // 
            // idDataGridViewTextBoxColumn1
            // 
            this.idDataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn1.FillWeight = 20F;
            this.idDataGridViewTextBoxColumn1.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn1.MinimumWidth = 50;
            this.idDataGridViewTextBoxColumn1.Name = "idDataGridViewTextBoxColumn1";
            this.idDataGridViewTextBoxColumn1.Visible = false;
            this.idDataGridViewTextBoxColumn1.Width = 50;
            // 
            // levelDataGridViewTextBoxColumn1
            // 
            this.levelDataGridViewTextBoxColumn1.DataPropertyName = "Niveau";
            this.levelDataGridViewTextBoxColumn1.HeaderText = "Niveau";
            this.levelDataGridViewTextBoxColumn1.MinimumWidth = 50;
            this.levelDataGridViewTextBoxColumn1.Name = "levelDataGridViewTextBoxColumn1";
            this.levelDataGridViewTextBoxColumn1.Width = 160;
            // 
            // longeurmDataGridViewTextBoxColumn1
            // 
            this.longeurmDataGridViewTextBoxColumn1.DataPropertyName = "Longeur(m)";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.longeurmDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.longeurmDataGridViewTextBoxColumn1.FillWeight = 20F;
            this.longeurmDataGridViewTextBoxColumn1.HeaderText = "Longeur(m)";
            this.longeurmDataGridViewTextBoxColumn1.MinimumWidth = 50;
            this.longeurmDataGridViewTextBoxColumn1.Name = "longeurmDataGridViewTextBoxColumn1";
            this.longeurmDataGridViewTextBoxColumn1.Width = 107;
            // 
            // typeDataGridViewTextBoxColumn1
            // 
            this.typeDataGridViewTextBoxColumn1.DataPropertyName = "Type";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.typeDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            this.typeDataGridViewTextBoxColumn1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.typeDataGridViewTextBoxColumn1.FillWeight = 20F;
            this.typeDataGridViewTextBoxColumn1.HeaderText = "Type";
            this.typeDataGridViewTextBoxColumn1.MinimumWidth = 50;
            this.typeDataGridViewTextBoxColumn1.Name = "typeDataGridViewTextBoxColumn1";
            this.typeDataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.typeDataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.typeDataGridViewTextBoxColumn1.Width = 120;
            // 
            // hauteurcmDataGridViewTextBoxColumn1
            // 
            this.hauteurcmDataGridViewTextBoxColumn1.DataPropertyName = "Hauteur(cm)";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.hauteurcmDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            this.hauteurcmDataGridViewTextBoxColumn1.FillWeight = 20F;
            this.hauteurcmDataGridViewTextBoxColumn1.HeaderText = "Hauteur(cm)";
            this.hauteurcmDataGridViewTextBoxColumn1.MinimumWidth = 50;
            this.hauteurcmDataGridViewTextBoxColumn1.Name = "hauteurcmDataGridViewTextBoxColumn1";
            this.hauteurcmDataGridViewTextBoxColumn1.Width = 112;
            // 
            // largeurcmDataGridViewTextBoxColumn1
            // 
            this.largeurcmDataGridViewTextBoxColumn1.DataPropertyName = "Largeur(cm)";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.largeurcmDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle5;
            this.largeurcmDataGridViewTextBoxColumn1.FillWeight = 20F;
            this.largeurcmDataGridViewTextBoxColumn1.HeaderText = "Largeur(cm)";
            this.largeurcmDataGridViewTextBoxColumn1.MinimumWidth = 50;
            this.largeurcmDataGridViewTextBoxColumn1.Name = "largeurcmDataGridViewTextBoxColumn1";
            this.largeurcmDataGridViewTextBoxColumn1.Width = 111;
            // 
            // dataTable
            // 
            this.dataTable.Columns.AddRange(new System.Data.DataColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.levelDataGridViewTextBoxColumn,
            this.longeurmDataGridViewTextBoxColumn,
            this.typeDataGridViewTextBoxColumn,
            this.hauteurcmDataGridViewTextBoxColumn,
            this.largeurcmDataGridViewTextBoxColumn});
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.ColumnName = "Id";
            this.idDataGridViewTextBoxColumn.DataType = typeof(int);
            // 
            // levelDataGridViewTextBoxColumn
            // 
            this.levelDataGridViewTextBoxColumn.ColumnName = "Niveau";
            // 
            // longeurmDataGridViewTextBoxColumn
            // 
            this.longeurmDataGridViewTextBoxColumn.ColumnName = "Longeur(m)";
            this.longeurmDataGridViewTextBoxColumn.DataType = typeof(double);
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.ColumnName = "Type";
            // 
            // hauteurcmDataGridViewTextBoxColumn
            // 
            this.hauteurcmDataGridViewTextBoxColumn.ColumnName = "Hauteur(cm)";
            this.hauteurcmDataGridViewTextBoxColumn.DataType = typeof(double);
            // 
            // largeurcmDataGridViewTextBoxColumn
            // 
            this.largeurcmDataGridViewTextBoxColumn.ColumnName = "Largeur(cm)";
            this.largeurcmDataGridViewTextBoxColumn.DataType = typeof(double);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(420, 321);
            this.okButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(95, 32);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(520, 321);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(95, 32);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Enabled = false;
            this.applyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applyButton.Location = new System.Drawing.Point(620, 321);
            this.applyButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(112, 32);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "Appliquer";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // DimensionEditingForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(744, 364);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.dataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "DimensionEditingForm";
            this.Text = "Beam Section Dimensions";
            this.Load += new System.EventHandler(this.DimensionEditingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button okButton;
        private System.Data.DataTable dataTable;
        private DataColumn idDataGridViewTextBoxColumn;
        private DataColumn levelDataGridViewTextBoxColumn;
        private DataColumn longeurmDataGridViewTextBoxColumn;
        private DataColumn typeDataGridViewTextBoxColumn;
        private DataColumn hauteurcmDataGridViewTextBoxColumn;
        private DataColumn largeurcmDataGridViewTextBoxColumn;
        private Button cancelButton;
        private Button applyButton;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn levelDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn longeurmDataGridViewTextBoxColumn1;
        private DataGridViewComboBoxColumn typeDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn hauteurcmDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn largeurcmDataGridViewTextBoxColumn1;
    }
}