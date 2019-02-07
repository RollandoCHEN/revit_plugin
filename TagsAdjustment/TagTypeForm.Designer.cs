namespace DCEStudyTools.TagsAdjustment
{
    partial class TagTypeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagTypeForm));
            this.defaultCheck = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ucRectColumn1 = new DCEStudyTools.TagsAdjustment.UserControls.ucRectColumn();
            this.ucSquareColumn1 = new DCEStudyTools.TagsAdjustment.UserControls.ucSquareColumn();
            this.ucRoundColumn1 = new DCEStudyTools.TagsAdjustment.UserControls.ucRoundColumn();
            this.ucBeamTagWithoutDimension1 = new DCEStudyTools.TagsAdjustment.UserControls.ucBeamTagWithoutDimension();
            this.ucBeamTagWithDimension1 = new DCEStudyTools.TagsAdjustment.UserControls.ucBeamTagWithDimension();
            this.noDimension = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // defaultCheck
            // 
            this.defaultCheck.AutoSize = true;
            this.defaultCheck.Checked = true;
            this.defaultCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.defaultCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defaultCheck.Location = new System.Drawing.Point(12, 19);
            this.defaultCheck.Name = "defaultCheck";
            this.defaultCheck.Size = new System.Drawing.Size(183, 21);
            this.defaultCheck.TabIndex = 2;
            this.defaultCheck.Text = "Configuration par défault";
            this.defaultCheck.UseVisualStyleBackColor = true;
            this.defaultCheck.CheckedChanged += new System.EventHandler(this.defaultCheck_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Poutre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Poteau";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(502, 539);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(71, 26);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(427, 539);
            this.okButton.Margin = new System.Windows.Forms.Padding(2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(71, 26);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(424, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Les BN de 20cm en largeur et les Talon PV seront étiquetés par : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(274, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Les autres poutres seront étiquetées par :";
            // 
            // ucRectColumn1
            // 
            this.ucRectColumn1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucRectColumn1.Location = new System.Drawing.Point(12, 444);
            this.ucRectColumn1.Margin = new System.Windows.Forms.Padding(4);
            this.ucRectColumn1.Name = "ucRectColumn1";
            this.ucRectColumn1.SelectedFamilyName = "";
            this.ucRectColumn1.SelectedTagName = "";
            this.ucRectColumn1.Size = new System.Drawing.Size(560, 66);
            this.ucRectColumn1.TabIndex = 10;
            // 
            // ucSquareColumn1
            // 
            this.ucSquareColumn1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucSquareColumn1.Location = new System.Drawing.Point(12, 350);
            this.ucSquareColumn1.Margin = new System.Windows.Forms.Padding(4);
            this.ucSquareColumn1.Name = "ucSquareColumn1";
            this.ucSquareColumn1.SelectedFamilyName = "";
            this.ucSquareColumn1.SelectedTagName = "";
            this.ucSquareColumn1.Size = new System.Drawing.Size(560, 66);
            this.ucSquareColumn1.TabIndex = 9;
            // 
            // ucRoundColumn1
            // 
            this.ucRoundColumn1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucRoundColumn1.Location = new System.Drawing.Point(12, 260);
            this.ucRoundColumn1.Margin = new System.Windows.Forms.Padding(4);
            this.ucRoundColumn1.Name = "ucRoundColumn1";
            this.ucRoundColumn1.SelectedFamilyName = "";
            this.ucRoundColumn1.SelectedTagName = "";
            this.ucRoundColumn1.Size = new System.Drawing.Size(562, 66);
            this.ucRoundColumn1.TabIndex = 8;
            // 
            // ucBeamTagWithoutDimension1
            // 
            this.ucBeamTagWithoutDimension1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucBeamTagWithoutDimension1.Location = new System.Drawing.Point(37, 112);
            this.ucBeamTagWithoutDimension1.Margin = new System.Windows.Forms.Padding(4);
            this.ucBeamTagWithoutDimension1.Name = "ucBeamTagWithoutDimension1";
            this.ucBeamTagWithoutDimension1.SelectedTagName = "";
            this.ucBeamTagWithoutDimension1.Size = new System.Drawing.Size(537, 31);
            this.ucBeamTagWithoutDimension1.TabIndex = 4;
            // 
            // ucBeamTagWithDimension1
            // 
            this.ucBeamTagWithDimension1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucBeamTagWithDimension1.Location = new System.Drawing.Point(37, 187);
            this.ucBeamTagWithDimension1.Margin = new System.Windows.Forms.Padding(4);
            this.ucBeamTagWithDimension1.Name = "ucBeamTagWithDimension1";
            this.ucBeamTagWithDimension1.SelectedTagName = "";
            this.ucBeamTagWithDimension1.Size = new System.Drawing.Size(536, 29);
            this.ucBeamTagWithDimension1.TabIndex = 3;
            // 
            // noDimension
            // 
            this.noDimension.AutoSize = true;
            this.noDimension.Checked = true;
            this.noDimension.CheckState = System.Windows.Forms.CheckState.Checked;
            this.noDimension.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noDimension.Location = new System.Drawing.Point(201, 19);
            this.noDimension.Name = "noDimension";
            this.noDimension.Size = new System.Drawing.Size(183, 21);
            this.noDimension.TabIndex = 2;
            this.noDimension.Text = "Sans dimension pour BN";
            this.noDimension.UseVisualStyleBackColor = true;
            this.noDimension.CheckedChanged += new System.EventHandler(this.defaultCheck_CheckedChanged);
            // 
            // TagTypeForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(584, 576);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ucRectColumn1);
            this.Controls.Add(this.ucSquareColumn1);
            this.Controls.Add(this.ucRoundColumn1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucBeamTagWithoutDimension1);
            this.Controls.Add(this.ucBeamTagWithDimension1);
            this.Controls.Add(this.noDimension);
            this.Controls.Add(this.defaultCheck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TagTypeForm";
            this.Text = "Ajuster Etiquettes";
            this.Load += new System.EventHandler(this.TagTypeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox defaultCheck;
        private UserControls.ucBeamTagWithDimension ucBeamTagWithDimension1;
        private UserControls.ucBeamTagWithoutDimension ucBeamTagWithoutDimension1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private UserControls.ucRoundColumn ucRoundColumn1;
        private UserControls.ucSquareColumn ucSquareColumn1;
        private UserControls.ucRectColumn ucRectColumn1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox noDimension;
    }
}