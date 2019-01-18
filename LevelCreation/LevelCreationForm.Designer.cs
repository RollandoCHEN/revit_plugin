namespace DCEStudyTools.LevelCreation
{
    partial class LevelCreationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelCreationForm));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.ucHeight = new DCEStudyTools.LevelCreation.UserControls.ucHeight();
            this.ucLevels = new DCEStudyTools.LevelCreation.UserControls.ucLevels();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(275, 155);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(67, 28);
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
            this.cancelButton.Location = new System.Drawing.Point(347, 155);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(71, 26);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // ucHeight
            // 
            this.ucHeight.Location = new System.Drawing.Point(194, 22);
            this.ucHeight.Margin = new System.Windows.Forms.Padding(4);
            this.ucHeight.Name = "ucHeight";
            this.ucHeight.Size = new System.Drawing.Size(202, 115);
            this.ucHeight.TabIndex = 10;
            // 
            // ucLevels
            // 
            this.ucLevels.Location = new System.Drawing.Point(9, 22);
            this.ucLevels.Margin = new System.Windows.Forms.Padding(4);
            this.ucLevels.Name = "ucLevels";
            this.ucLevels.Size = new System.Drawing.Size(157, 83);
            this.ucLevels.TabIndex = 9;
            // 
            // LevelCreationForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(429, 195);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.ucHeight);
            this.Controls.Add(this.ucLevels);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LevelCreationForm";
            this.Text = "Créer Niveaux & Vues en plan";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        private UserControls.ucLevels ucLevels;
        private UserControls.ucHeight ucHeight;
        private System.Windows.Forms.Button cancelButton;
    }
}