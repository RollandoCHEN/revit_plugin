namespace DCEStudyTools.LevelsCreation
{
    partial class LevelsCreationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelsCreationForm));
            this.okButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.levelCreationCheck = new System.Windows.Forms.CheckBox();
            this.ucHeight = new DCEStudyTools.LevelsCreation.UserControls.ucHeight();
            this.ucLevels = new DCEStudyTools.LevelsCreation.UserControls.ucLevels();
            this.ucFoundation = new DCEStudyTools.LevelsCreation.UserControls.ucFoundation();
            this.sheetCreationCheck = new System.Windows.Forms.CheckBox();
            this.ucFireResist = new DCEStudyTools.LevelsCreation.UserControls.ucFireResist();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(350, 129);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(67, 28);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.levelCreationCheck);
            this.splitContainer1.Panel1.Controls.Add(this.ucHeight);
            this.splitContainer1.Panel1.Controls.Add(this.ucLevels);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.ucFoundation);
            this.splitContainer1.Panel2.Controls.Add(this.sheetCreationCheck);
            this.splitContainer1.Panel2.Controls.Add(this.ucFireResist);
            this.splitContainer1.Panel2.Controls.Add(this.okButton);
            this.splitContainer1.Size = new System.Drawing.Size(429, 359);
            this.splitContainer1.SplitterDistance = 186;
            this.splitContainer1.TabIndex = 8;
            // 
            // levelCreationCheck
            // 
            this.levelCreationCheck.AutoSize = true;
            this.levelCreationCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.levelCreationCheck.Checked = true;
            this.levelCreationCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.levelCreationCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelCreationCheck.ForeColor = System.Drawing.SystemColors.Highlight;
            this.levelCreationCheck.Location = new System.Drawing.Point(12, 12);
            this.levelCreationCheck.Name = "levelCreationCheck";
            this.levelCreationCheck.Size = new System.Drawing.Size(175, 20);
            this.levelCreationCheck.TabIndex = 11;
            this.levelCreationCheck.Text = "Création des Niveaux";
            this.levelCreationCheck.UseVisualStyleBackColor = true;
            this.levelCreationCheck.CheckedChanged += new System.EventHandler(this.levelCreationChcBox_CheckedChanged);
            // 
            // ucHeight
            // 
            this.ucHeight.Location = new System.Drawing.Point(197, 48);
            this.ucHeight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ucHeight.Name = "ucHeight";
            this.ucHeight.Size = new System.Drawing.Size(202, 115);
            this.ucHeight.TabIndex = 10;
            // 
            // ucLevels
            // 
            this.ucLevels.Location = new System.Drawing.Point(12, 48);
            this.ucLevels.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ucLevels.Name = "ucLevels";
            this.ucLevels.Size = new System.Drawing.Size(157, 83);
            this.ucLevels.TabIndex = 9;
            // 
            // ucFoundation
            // 
            this.ucFoundation.Location = new System.Drawing.Point(12, 78);
            this.ucFoundation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ucFoundation.Name = "ucFoundation";
            this.ucFoundation.Size = new System.Drawing.Size(248, 30);
            this.ucFoundation.TabIndex = 12;
            // 
            // sheetCreationCheck
            // 
            this.sheetCreationCheck.AutoSize = true;
            this.sheetCreationCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sheetCreationCheck.Checked = true;
            this.sheetCreationCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sheetCreationCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sheetCreationCheck.ForeColor = System.Drawing.SystemColors.Highlight;
            this.sheetCreationCheck.Location = new System.Drawing.Point(12, 17);
            this.sheetCreationCheck.Name = "sheetCreationCheck";
            this.sheetCreationCheck.Size = new System.Drawing.Size(174, 20);
            this.sheetCreationCheck.TabIndex = 11;
            this.sheetCreationCheck.Text = "Création des Feuilles";
            this.sheetCreationCheck.UseVisualStyleBackColor = true;
            this.sheetCreationCheck.CheckedChanged += new System.EventHandler(this.sheetCreationChcBox_CheckedChanged);
            // 
            // ucFireResist
            // 
            this.ucFireResist.Location = new System.Drawing.Point(10, 44);
            this.ucFireResist.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ucFireResist.Name = "ucFireResist";
            this.ucFireResist.Size = new System.Drawing.Size(231, 28);
            this.ucFireResist.TabIndex = 9;
            // 
            // LevelsCreationForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(429, 359);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LevelsCreationForm";
            this.Text = "Créer Niveaux / Vues en plan / Feuilles";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private UserControls.ucLevels ucLevels;
        private UserControls.ucHeight ucHeight;
        private UserControls.ucFireResist ucFireResist;
        private System.Windows.Forms.CheckBox levelCreationCheck;
        private System.Windows.Forms.CheckBox sheetCreationCheck;
        private UserControls.ucFoundation ucFoundation;
    }
}