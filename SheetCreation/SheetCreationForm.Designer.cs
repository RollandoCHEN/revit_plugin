namespace DCEStudyTools.SheetCreation
{
    partial class SheetCreationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SheetCreationForm));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.ucDuplicataNum = new DCEStudyTools.SheetCreation.UserControls.ucDuplicataNum();
            this.ucFoundation = new DCEStudyTools.SheetCreation.UserControls.ucFoundation();
            this.ucFireResist = new DCEStudyTools.SheetCreation.UserControls.ucFireResist();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(205, 132);
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
            this.cancelButton.Location = new System.Drawing.Point(277, 134);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(71, 26);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // ucDuplicataNum
            // 
            this.ucDuplicataNum.Location = new System.Drawing.Point(16, 17);
            this.ucDuplicataNum.Margin = new System.Windows.Forms.Padding(4);
            this.ucDuplicataNum.Name = "ucDuplicataNum";
            this.ucDuplicataNum.Size = new System.Drawing.Size(207, 26);
            this.ucDuplicataNum.TabIndex = 13;
            // 
            // ucFoundation
            // 
            this.ucFoundation.Location = new System.Drawing.Point(15, 84);
            this.ucFoundation.Margin = new System.Windows.Forms.Padding(2);
            this.ucFoundation.Name = "ucFoundation";
            this.ucFoundation.Size = new System.Drawing.Size(248, 30);
            this.ucFoundation.TabIndex = 12;
            // 
            // ucFireResist
            // 
            this.ucFireResist.Location = new System.Drawing.Point(13, 50);
            this.ucFireResist.Margin = new System.Windows.Forms.Padding(4);
            this.ucFireResist.Name = "ucFireResist";
            this.ucFireResist.Size = new System.Drawing.Size(231, 28);
            this.ucFireResist.TabIndex = 9;
            // 
            // SheetCreationForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(359, 171);
            this.Controls.Add(this.ucDuplicataNum);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.ucFoundation);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.ucFireResist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SheetCreationForm";
            this.Text = "Créer Feuilles";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        private UserControls.ucFireResist ucFireResist;
        private UserControls.ucFoundation ucFoundation;
        private UserControls.ucDuplicataNum ucDuplicataNum;
        private System.Windows.Forms.Button cancelButton;
    }
}