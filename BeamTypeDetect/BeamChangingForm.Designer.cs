using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DCEStudyTools.BeamTypeDetect
{
    partial class BeamChangingForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BeamChangingForm));
            this.toNormalDataTable = new System.Data.DataTable();
            this.idDataTextBoxColumn1 = new System.Data.DataColumn();
            this.isBeamToBeChangedColumn1 = new System.Data.DataColumn();
            this.levelDataTextBoxColumn1 = new System.Data.DataColumn();
            this.typeDataTextBoxColumn1 = new System.Data.DataColumn();
            this.hauteurcmDataTextBoxColumn1 = new System.Data.DataColumn();
            this.largeurcmDataTextBoxColumn1 = new System.Data.DataColumn();
            this.toGroundBeamDataTable = new System.Data.DataTable();
            this.idDataTextBoxColumn2 = new System.Data.DataColumn();
            this.isBeamToBeChangedColumn2 = new System.Data.DataColumn();
            this.levelDataTextBoxColumn2 = new System.Data.DataColumn();
            this.typeDataTextBoxColumn2 = new System.Data.DataColumn();
            this.hauteurcmDataTextBoxColumn2 = new System.Data.DataColumn();
            this.largeurcmDataTextBoxColumn2 = new System.Data.DataColumn();
            this.unsupportedWallDataTable = new System.Data.DataTable();
            this.wall_idDataColumn = new System.Data.DataColumn();
            this.wall_levelDataColumn = new System.Data.DataColumn();
            this.wall_typeDataColumn = new System.Data.DataColumn();
            this.wall_thicknessDataColumn = new System.Data.DataColumn();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.wall_tabPage = new System.Windows.Forms.TabPage();
            this.toPV_ApplyButton = new System.Windows.Forms.Button();
            this.unsupportedWallGridView = new System.Windows.Forms.DataGridView();
            this.wall_IdGridColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wall_LevelGridColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wall_TypeGridColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wall_ThicknessGridColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.toGB_TabPage = new System.Windows.Forms.TabPage();
            this.toGB_ApplyButton = new System.Windows.Forms.Button();
            this.toGroundBeamDataView = new System.Windows.Forms.DataGridView();
            this.toL_idGridColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toL_checkGridColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.toL_levelGridColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toL_typeGridColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toL_heightGridColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toL_widthGridColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.toN_TabPage = new System.Windows.Forms.TabPage();
            this.toNormalDataView = new System.Windows.Forms.DataGridView();
            this.toN_idGridColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toN_chekGridColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.toN_levelGridColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toN_typeGridColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toN_heightGridColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toN_widthGridColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ucBeamFilterCondition = new DCEStudyTools.BeamTypeDetect.ucNomalBeamFilterCondition();
            this.toN_ApplyButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.toNormalDataTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toGroundBeamDataTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unsupportedWallDataTable)).BeginInit();
            this.wall_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unsupportedWallGridView)).BeginInit();
            this.toGB_TabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toGroundBeamDataView)).BeginInit();
            this.toN_TabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toNormalDataView)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toNomalDataTable
            // 
            this.toNormalDataTable.Columns.AddRange(new System.Data.DataColumn[] {
            this.idDataTextBoxColumn1,
            this.isBeamToBeChangedColumn1,
            this.levelDataTextBoxColumn1,
            this.typeDataTextBoxColumn1,
            this.hauteurcmDataTextBoxColumn1,
            this.largeurcmDataTextBoxColumn1});
            // 
            // idDataTextBoxColumn1
            // 
            this.idDataTextBoxColumn1.ColumnName = "Id";
            this.idDataTextBoxColumn1.DataType = typeof(int);
            // 
            // isBeamToBeChangedColumn1
            // 
            this.isBeamToBeChangedColumn1.ColumnName = "Poutre à changer";
            this.isBeamToBeChangedColumn1.DataType = typeof(int);
            // 
            // levelDataTextBoxColumn1
            // 
            this.levelDataTextBoxColumn1.ColumnName = "Niveau";
            // 
            // typeDataTextBoxColumn1
            // 
            this.typeDataTextBoxColumn1.ColumnName = "Type";
            // 
            // hauteurcmDataTextBoxColumn1
            // 
            this.hauteurcmDataTextBoxColumn1.ColumnName = "Hauteur(cm)";
            this.hauteurcmDataTextBoxColumn1.DataType = typeof(double);
            // 
            // largeurcmDataTextBoxColumn1
            // 
            this.largeurcmDataTextBoxColumn1.ColumnName = "Largeur(cm)";
            this.largeurcmDataTextBoxColumn1.DataType = typeof(double);
            // 
            // toGroundBeamDataTable
            // 
            this.toGroundBeamDataTable.Columns.AddRange(new System.Data.DataColumn[] {
            this.idDataTextBoxColumn2,
            this.isBeamToBeChangedColumn2,
            this.levelDataTextBoxColumn2,
            this.typeDataTextBoxColumn2,
            this.hauteurcmDataTextBoxColumn2,
            this.largeurcmDataTextBoxColumn2});
            // 
            // idDataTextBoxColumn2
            // 
            this.idDataTextBoxColumn2.ColumnName = "Id";
            this.idDataTextBoxColumn2.DataType = typeof(int);
            // 
            // isBeamToBeChangedColumn2
            // 
            this.isBeamToBeChangedColumn2.ColumnName = "Poutre à changer";
            this.isBeamToBeChangedColumn2.DataType = typeof(int);
            // 
            // levelDataTextBoxColumn2
            // 
            this.levelDataTextBoxColumn2.ColumnName = "Niveau";
            // 
            // typeDataTextBoxColumn2
            // 
            this.typeDataTextBoxColumn2.ColumnName = "Type";
            // 
            // hauteurcmDataTextBoxColumn2
            // 
            this.hauteurcmDataTextBoxColumn2.ColumnName = "Hauteur(cm)";
            this.hauteurcmDataTextBoxColumn2.DataType = typeof(double);
            // 
            // largeurcmDataTextBoxColumn2
            // 
            this.largeurcmDataTextBoxColumn2.ColumnName = "Largeur(cm)";
            this.largeurcmDataTextBoxColumn2.DataType = typeof(double);
            // 
            // unsupportedWallDataTable
            // 
            this.unsupportedWallDataTable.Columns.AddRange(new System.Data.DataColumn[] {
            this.wall_idDataColumn,
            this.wall_levelDataColumn,
            this.wall_typeDataColumn,
            this.wall_thicknessDataColumn});
            // 
            // wall_idDataColumn
            // 
            this.wall_idDataColumn.ColumnName = "Id";
            this.wall_idDataColumn.DataType = typeof(int);
            // 
            // wall_levelDataColumn
            // 
            this.wall_levelDataColumn.ColumnName = "Niveau";
            // 
            // wall_typeDataColumn
            // 
            this.wall_typeDataColumn.ColumnName = "Type";
            // 
            // wall_thicknessDataColumn
            // 
            this.wall_thicknessDataColumn.ColumnName = "Epaisseur(cm)";
            this.wall_thicknessDataColumn.DataType = typeof(double);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(456, 403);
            this.okButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(71, 26);
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
            this.cancelButton.Location = new System.Drawing.Point(530, 403);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(71, 26);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // wall_tabPage
            // 
            this.wall_tabPage.Controls.Add(this.toPV_ApplyButton);
            this.wall_tabPage.Controls.Add(this.unsupportedWallGridView);
            this.wall_tabPage.Controls.Add(this.label3);
            this.wall_tabPage.Location = new System.Drawing.Point(4, 29);
            this.wall_tabPage.Name = "wall_tabPage";
            this.wall_tabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.wall_tabPage.Size = new System.Drawing.Size(599, 354);
            this.wall_tabPage.TabIndex = 2;
            this.wall_tabPage.Text = "Murs";
            this.wall_tabPage.UseVisualStyleBackColor = true;
            // 
            // toPV_ApplyButton
            // 
            this.toPV_ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.toPV_ApplyButton.Enabled = false;
            this.toPV_ApplyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toPV_ApplyButton.Location = new System.Drawing.Point(513, 321);
            this.toPV_ApplyButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.toPV_ApplyButton.Name = "toPV_ApplyButton";
            this.toPV_ApplyButton.Size = new System.Drawing.Size(81, 28);
            this.toPV_ApplyButton.TabIndex = 11;
            this.toPV_ApplyButton.Text = "Appliquer";
            this.toolTip1.SetToolTip(this.toPV_ApplyButton, "Les poutres normaux selectionnés seront marquées comme des talon PV");
            this.toPV_ApplyButton.UseVisualStyleBackColor = true;
            // 
            // unsupportedWallGridView
            // 
            this.unsupportedWallGridView.AllowUserToAddRows = false;
            this.unsupportedWallGridView.AllowUserToDeleteRows = false;
            this.unsupportedWallGridView.AllowUserToResizeRows = false;
            this.unsupportedWallGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.unsupportedWallGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.unsupportedWallGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.unsupportedWallGridView.ColumnHeadersHeight = 32;
            this.unsupportedWallGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.unsupportedWallGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.wall_IdGridColumn,
            this.wall_LevelGridColumn,
            this.wall_TypeGridColumn,
            this.wall_ThicknessGridColumn});
            this.unsupportedWallGridView.DataSource = this.unsupportedWallDataTable;
            this.unsupportedWallGridView.Location = new System.Drawing.Point(5, 33);
            this.unsupportedWallGridView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.unsupportedWallGridView.MultiSelect = false;
            this.unsupportedWallGridView.Name = "unsupportedWallGridView";
            this.unsupportedWallGridView.ReadOnly = true;
            this.unsupportedWallGridView.RowHeadersVisible = false;
            this.unsupportedWallGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.unsupportedWallGridView.RowTemplate.Height = 26;
            this.unsupportedWallGridView.Size = new System.Drawing.Size(589, 284);
            this.unsupportedWallGridView.TabIndex = 9;
            this.unsupportedWallGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.unsupportedWallGridView_CellDoubleClick);
            // 
            // wall_IdGridColumn
            // 
            this.wall_IdGridColumn.DataPropertyName = "Id";
            this.wall_IdGridColumn.HeaderText = "Id";
            this.wall_IdGridColumn.Name = "wall_IdGridColumn";
            this.wall_IdGridColumn.ReadOnly = true;
            this.wall_IdGridColumn.Visible = false;
            // 
            // wall_LevelGridColumn
            // 
            this.wall_LevelGridColumn.DataPropertyName = "Niveau";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.wall_LevelGridColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.wall_LevelGridColumn.HeaderText = "Niveau Bas";
            this.wall_LevelGridColumn.Name = "wall_LevelGridColumn";
            this.wall_LevelGridColumn.ReadOnly = true;
            this.wall_LevelGridColumn.Width = 180;
            // 
            // wall_TypeGridColumn
            // 
            this.wall_TypeGridColumn.DataPropertyName = "Type";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.wall_TypeGridColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.wall_TypeGridColumn.HeaderText = "Type";
            this.wall_TypeGridColumn.Name = "wall_TypeGridColumn";
            this.wall_TypeGridColumn.ReadOnly = true;
            this.wall_TypeGridColumn.Width = 210;
            // 
            // wall_ThicknessGridColumn
            // 
            this.wall_ThicknessGridColumn.DataPropertyName = "Epaisseur(cm)";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.wall_ThicknessGridColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.wall_ThicknessGridColumn.HeaderText = "Epaisseur(cm)";
            this.wall_ThicknessGridColumn.Name = "wall_ThicknessGridColumn";
            this.wall_ThicknessGridColumn.ReadOnly = true;
            this.wall_ThicknessGridColumn.Width = 160;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(5, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(315, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Les murs ne sont pas entièrement portées\r\n";
            // 
            // toGB_TabPage
            // 
            this.toGB_TabPage.Controls.Add(this.toGB_ApplyButton);
            this.toGB_TabPage.Controls.Add(this.toGroundBeamDataView);
            this.toGB_TabPage.Controls.Add(this.label2);
            this.toGB_TabPage.Location = new System.Drawing.Point(4, 29);
            this.toGB_TabPage.Name = "toGB_TabPage";
            this.toGB_TabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.toGB_TabPage.Size = new System.Drawing.Size(598, 354);
            this.toGB_TabPage.TabIndex = 1;
            this.toGB_TabPage.Text = "Longrines";
            this.toGB_TabPage.UseVisualStyleBackColor = true;
            // 
            // toGB_ApplyButton
            // 
            this.toGB_ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.toGB_ApplyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toGB_ApplyButton.Location = new System.Drawing.Point(514, 321);
            this.toGB_ApplyButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.toGB_ApplyButton.Name = "toGB_ApplyButton";
            this.toGB_ApplyButton.Size = new System.Drawing.Size(81, 28);
            this.toGB_ApplyButton.TabIndex = 5;
            this.toGB_ApplyButton.Text = "Appliquer";
            this.toolTip1.SetToolTip(this.toGB_ApplyButton, "Les poutres normaux selectionnées seront marquées comme des longrines");
            this.toGB_ApplyButton.UseVisualStyleBackColor = true;
            this.toGB_ApplyButton.Click += new System.EventHandler(this.toGB_ApplyButton_Click);
            // 
            // toGroundBeamDataView
            // 
            this.toGroundBeamDataView.AllowUserToAddRows = false;
            this.toGroundBeamDataView.AllowUserToDeleteRows = false;
            this.toGroundBeamDataView.AllowUserToResizeRows = false;
            this.toGroundBeamDataView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toGroundBeamDataView.AutoGenerateColumns = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.toGroundBeamDataView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.toGroundBeamDataView.ColumnHeadersHeight = 32;
            this.toGroundBeamDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.toGroundBeamDataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.toL_idGridColumn,
            this.toL_checkGridColumn,
            this.toL_levelGridColumn,
            this.toL_typeGridColumn,
            this.toL_heightGridColumn,
            this.toL_widthGridColumn});
            this.toGroundBeamDataView.DataSource = this.toGroundBeamDataTable;
            this.toGroundBeamDataView.Location = new System.Drawing.Point(5, 34);
            this.toGroundBeamDataView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.toGroundBeamDataView.MultiSelect = false;
            this.toGroundBeamDataView.Name = "toGroundBeamDataView";
            this.toGroundBeamDataView.RowHeadersVisible = false;
            this.toGroundBeamDataView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.toGroundBeamDataView.RowTemplate.Height = 26;
            this.toGroundBeamDataView.Size = new System.Drawing.Size(590, 283);
            this.toGroundBeamDataView.TabIndex = 3;
            this.toGroundBeamDataView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.toGroundBeamDataView_CellValueChanged);
            // 
            // toL_idGridColumn
            // 
            this.toL_idGridColumn.DataPropertyName = "Id";
            this.toL_idGridColumn.HeaderText = "Id";
            this.toL_idGridColumn.Name = "toL_idGridColumn";
            this.toL_idGridColumn.Visible = false;
            // 
            // toL_checkGridColumn
            // 
            this.toL_checkGridColumn.DataPropertyName = "Poutre à changer";
            this.toL_checkGridColumn.FalseValue = "0";
            this.toL_checkGridColumn.HeaderText = "";
            this.toL_checkGridColumn.Name = "toL_checkGridColumn";
            this.toL_checkGridColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.toL_checkGridColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.toL_checkGridColumn.TrueValue = "1";
            this.toL_checkGridColumn.Width = 40;
            // 
            // toL_levelGridColumn
            // 
            this.toL_levelGridColumn.DataPropertyName = "Niveau";
            this.toL_levelGridColumn.HeaderText = "Niveau";
            this.toL_levelGridColumn.Name = "toL_levelGridColumn";
            this.toL_levelGridColumn.ReadOnly = true;
            this.toL_levelGridColumn.Width = 160;
            // 
            // toL_typeGridColumn
            // 
            this.toL_typeGridColumn.DataPropertyName = "Type";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.toL_typeGridColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.toL_typeGridColumn.HeaderText = "Type actuel";
            this.toL_typeGridColumn.Name = "toL_typeGridColumn";
            this.toL_typeGridColumn.ReadOnly = true;
            this.toL_typeGridColumn.Width = 110;
            // 
            // toL_heightGridColumn
            // 
            this.toL_heightGridColumn.DataPropertyName = "Hauteur(cm)";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.toL_heightGridColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.toL_heightGridColumn.HeaderText = "Hauteur(cm)";
            this.toL_heightGridColumn.Name = "toL_heightGridColumn";
            this.toL_heightGridColumn.ReadOnly = true;
            // 
            // toL_widthGridColumn
            // 
            this.toL_widthGridColumn.DataPropertyName = "Largeur(cm)";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.toL_widthGridColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.toL_widthGridColumn.HeaderText = "Largeur(cm)";
            this.toL_widthGridColumn.Name = "toL_widthGridColumn";
            this.toL_widthGridColumn.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoEllipsis = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(5, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(590, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Cochez les poutres qui doivent être des Longrines au lieu des poutres normaux";
            // 
            // toN_TabPage
            // 
            this.toN_TabPage.Controls.Add(this.toNormalDataView);
            this.toN_TabPage.Controls.Add(this.ucBeamFilterCondition);
            this.toN_TabPage.Controls.Add(this.toN_ApplyButton);
            this.toN_TabPage.Controls.Add(this.label1);
            this.toN_TabPage.Location = new System.Drawing.Point(4, 29);
            this.toN_TabPage.Name = "toN_TabPage";
            this.toN_TabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.toN_TabPage.Size = new System.Drawing.Size(588, 354);
            this.toN_TabPage.TabIndex = 0;
            this.toN_TabPage.Text = "Poutres Normaux";
            this.toN_TabPage.UseVisualStyleBackColor = true;
            // 
            // toNomalDataView
            // 
            this.toNormalDataView.AllowUserToAddRows = false;
            this.toNormalDataView.AllowUserToDeleteRows = false;
            this.toNormalDataView.AllowUserToResizeRows = false;
            this.toNormalDataView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toNormalDataView.AutoGenerateColumns = false;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.toNormalDataView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.toNormalDataView.ColumnHeadersHeight = 32;
            this.toNormalDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.toNormalDataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.toN_idGridColumn,
            this.toN_chekGridColumn,
            this.toN_levelGridColumn,
            this.toN_typeGridColumn,
            this.toN_heightGridColumn,
            this.toN_widthGridColumn});
            this.toNormalDataView.DataSource = this.toNormalDataTable;
            this.toNormalDataView.Location = new System.Drawing.Point(5, 123);
            this.toNormalDataView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.toNormalDataView.MultiSelect = false;
            this.toNormalDataView.Name = "toNomalDataView";
            this.toNormalDataView.RowHeadersVisible = false;
            this.toNormalDataView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.toNormalDataView.RowTemplate.Height = 26;
            this.toNormalDataView.Size = new System.Drawing.Size(578, 194);
            this.toNormalDataView.TabIndex = 0;
            this.toNormalDataView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.toNomalDataView_CellValueChanged);
            // 
            // toN_idGridColumn
            // 
            this.toN_idGridColumn.DataPropertyName = "Id";
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toN_idGridColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.toN_idGridColumn.FillWeight = 20F;
            this.toN_idGridColumn.HeaderText = "Id";
            this.toN_idGridColumn.MinimumWidth = 50;
            this.toN_idGridColumn.Name = "toN_idGridColumn";
            this.toN_idGridColumn.ReadOnly = true;
            this.toN_idGridColumn.Visible = false;
            this.toN_idGridColumn.Width = 50;
            // 
            // toN_chekGridColumn
            // 
            this.toN_chekGridColumn.DataPropertyName = "Poutre à changer";
            this.toN_chekGridColumn.FalseValue = 0;
            this.toN_chekGridColumn.HeaderText = "";
            this.toN_chekGridColumn.Name = "toN_chekGridColumn";
            this.toN_chekGridColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.toN_chekGridColumn.TrueValue = 1;
            this.toN_chekGridColumn.Width = 40;
            // 
            // toN_levelGridColumn
            // 
            this.toN_levelGridColumn.DataPropertyName = "Niveau";
            this.toN_levelGridColumn.HeaderText = "Niveau";
            this.toN_levelGridColumn.MinimumWidth = 50;
            this.toN_levelGridColumn.Name = "toN_levelGridColumn";
            this.toN_levelGridColumn.ReadOnly = true;
            this.toN_levelGridColumn.Width = 160;
            // 
            // toN_typeGridColumn
            // 
            this.toN_typeGridColumn.DataPropertyName = "Type";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.toN_typeGridColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.toN_typeGridColumn.FillWeight = 20F;
            this.toN_typeGridColumn.HeaderText = "Type actuel";
            this.toN_typeGridColumn.MinimumWidth = 50;
            this.toN_typeGridColumn.Name = "toN_typeGridColumn";
            this.toN_typeGridColumn.ReadOnly = true;
            this.toN_typeGridColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.toN_typeGridColumn.Width = 110;
            // 
            // toN_heightGridColumn
            // 
            this.toN_heightGridColumn.DataPropertyName = "Hauteur(cm)";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.toN_heightGridColumn.DefaultCellStyle = dataGridViewCellStyle12;
            this.toN_heightGridColumn.FillWeight = 20F;
            this.toN_heightGridColumn.HeaderText = "Hauteur(cm)";
            this.toN_heightGridColumn.MinimumWidth = 50;
            this.toN_heightGridColumn.Name = "toN_heightGridColumn";
            this.toN_heightGridColumn.ReadOnly = true;
            // 
            // toN_widthGridColumn
            // 
            this.toN_widthGridColumn.DataPropertyName = "Largeur(cm)";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.toN_widthGridColumn.DefaultCellStyle = dataGridViewCellStyle13;
            this.toN_widthGridColumn.FillWeight = 20F;
            this.toN_widthGridColumn.HeaderText = "Largeur(cm)";
            this.toN_widthGridColumn.MinimumWidth = 50;
            this.toN_widthGridColumn.Name = "toN_widthGridColumn";
            this.toN_widthGridColumn.ReadOnly = true;
            // 
            // ucBeamFilterCondition
            // 
            this.ucBeamFilterCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucBeamFilterCondition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucBeamFilterCondition.Location = new System.Drawing.Point(5, 8);
            this.ucBeamFilterCondition.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ucBeamFilterCondition.Name = "ucBeamFilterCondition";
            this.ucBeamFilterCondition.Size = new System.Drawing.Size(578, 71);
            this.ucBeamFilterCondition.TabIndex = 3;
            // 
            // toN_ApplyButton
            // 
            this.toN_ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.toN_ApplyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toN_ApplyButton.Location = new System.Drawing.Point(502, 321);
            this.toN_ApplyButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.toN_ApplyButton.Name = "toN_ApplyButton";
            this.toN_ApplyButton.Size = new System.Drawing.Size(81, 28);
            this.toN_ApplyButton.TabIndex = 1;
            this.toN_ApplyButton.Text = "Appliquer";
            this.toolTip1.SetToolTip(this.toN_ApplyButton, "Les BN, Talon PV et longrines selectionnés seront marquées comme des poutres norm" +
        "aux");
            this.toN_ApplyButton.UseVisualStyleBackColor = true;
            this.toN_ApplyButton.Click += new System.EventHandler(this.toN_ApplyButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(5, 81);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(578, 40);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cochez les poutres qui doivent être des poutres normaux au lieu des BN, des Talon" +
    " PV ou des Longrines.";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.toN_TabPage);
            this.tabControl1.Controls.Add(this.toGB_TabPage);
            this.tabControl1.Controls.Add(this.wall_tabPage);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(108, 25);
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(10, 3);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(606, 387);
            this.tabControl1.TabIndex = 6;
            // 
            // BeamChangingForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(614, 440);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(630, 398);
            this.Name = "BeamChangingForm";
            this.Text = "Poutres en doute";
            this.Load += new System.EventHandler(this.BeamChangingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.toNormalDataTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toGroundBeamDataTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unsupportedWallDataTable)).EndInit();
            this.wall_tabPage.ResumeLayout(false);
            this.wall_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unsupportedWallGridView)).EndInit();
            this.toGB_TabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toGroundBeamDataView)).EndInit();
            this.toN_TabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toNormalDataView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        private System.Data.DataTable toNormalDataTable;
        private System.Data.DataTable toGroundBeamDataTable;
        private System.Data.DataTable unsupportedWallDataTable;
        private DataColumn idDataTextBoxColumn1;
        private DataColumn levelDataTextBoxColumn1;
        private DataColumn typeDataTextBoxColumn1;
        private DataColumn hauteurcmDataTextBoxColumn1;
        private DataColumn largeurcmDataTextBoxColumn1;
        private DataColumn isBeamToBeChangedColumn1;
        private Button cancelButton;
        private DataColumn idDataTextBoxColumn2;
        private DataColumn isBeamToBeChangedColumn2;
        private DataColumn levelDataTextBoxColumn2;
        private DataColumn typeDataTextBoxColumn2;
        private DataColumn hauteurcmDataTextBoxColumn2;
        private DataColumn largeurcmDataTextBoxColumn2;
        private DataColumn wall_idDataColumn;
        private DataColumn wall_levelDataColumn;
        private DataColumn wall_typeDataColumn;
        private DataColumn wall_thicknessDataColumn;
        private TabPage wall_tabPage;
        private DataGridView unsupportedWallGridView;
        private DataGridViewTextBoxColumn wall_IdGridColumn;
        private DataGridViewTextBoxColumn wall_LevelGridColumn;
        private DataGridViewTextBoxColumn wall_TypeGridColumn;
        private DataGridViewTextBoxColumn wall_ThicknessGridColumn;
        private Label label3;
        private TabPage toGB_TabPage;
        private DataGridView toGroundBeamDataView;
        private Label label2;
        private TabPage toN_TabPage;
        private DataGridView toNormalDataView;
        private ucNomalBeamFilterCondition ucBeamFilterCondition;
        private Button toN_ApplyButton;
        private Label label1;
        private TabControl tabControl1;
        private ToolTip toolTip1;
        private Button toPV_ApplyButton;
        private Button toGB_ApplyButton;
        private DataGridViewTextBoxColumn toL_idGridColumn;
        private DataGridViewCheckBoxColumn toL_checkGridColumn;
        private DataGridViewTextBoxColumn toL_levelGridColumn;
        private DataGridViewTextBoxColumn toL_typeGridColumn;
        private DataGridViewTextBoxColumn toL_heightGridColumn;
        private DataGridViewTextBoxColumn toL_widthGridColumn;
        private DataGridViewTextBoxColumn toN_idGridColumn;
        private DataGridViewCheckBoxColumn toN_chekGridColumn;
        private DataGridViewTextBoxColumn toN_levelGridColumn;
        private DataGridViewTextBoxColumn toN_typeGridColumn;
        private DataGridViewTextBoxColumn toN_heightGridColumn;
        private DataGridViewTextBoxColumn toN_widthGridColumn;
    }
}