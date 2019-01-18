namespace DCEStudyTools.Design.Beam
{
    partial class BeamDimensionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BeamDimensionForm));
            this.okButton = new System.Windows.Forms.Button();
            this.ucBeamType = new DCEStudyTools.Design.UserControls.ucBeamType();
            this.ucBeamLength = new DCEStudyTools.Design.UserControls.ucBeamLength();
            this.ucBeamWidth = new DCEStudyTools.Design.UserControls.ucBeamWidth();
            this.ucBeamHeight = new DCEStudyTools.Design.UserControls.ucBeamHeight();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(269, 163);
            this.okButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(65, 28);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // ucBeamType
            // 
            this.ucBeamType.Location = new System.Drawing.Point(55, 58);
            this.ucBeamType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ucBeamType.Name = "ucBeamType";
            this.ucBeamType.Size = new System.Drawing.Size(205, 27);
            this.ucBeamType.TabIndex = 7;
            // 
            // ucBeamLength
            // 
            this.ucBeamLength.Length = "2.35";
            this.ucBeamLength.Location = new System.Drawing.Point(55, 33);
            this.ucBeamLength.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ucBeamLength.Name = "ucBeamLength";
            this.ucBeamLength.Size = new System.Drawing.Size(186, 20);
            this.ucBeamLength.TabIndex = 6;
            // 
            // ucBeamWidth
            // 
            this.ucBeamWidth.Location = new System.Drawing.Point(55, 120);
            this.ucBeamWidth.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ucBeamWidth.Name = "ucBeamWidth";
            this.ucBeamWidth.Size = new System.Drawing.Size(234, 26);
            this.ucBeamWidth.TabIndex = 5;
            // 
            // ucBeamHeight
            // 
            this.ucBeamHeight.Location = new System.Drawing.Point(55, 90);
            this.ucBeamHeight.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ucBeamHeight.Name = "ucBeamHeight";
            this.ucBeamHeight.Size = new System.Drawing.Size(244, 25);
            this.ucBeamHeight.TabIndex = 4;
            // 
            // BeamDimensionForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 201);
            this.Controls.Add(this.ucBeamType);
            this.Controls.Add(this.ucBeamLength);
            this.Controls.Add(this.ucBeamWidth);
            this.Controls.Add(this.ucBeamHeight);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "BeamDimensionForm";
            this.Text = "Dimension de Section";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        private UserControls.ucBeamHeight ucBeamHeight;
        private UserControls.ucBeamWidth ucBeamWidth;
        private UserControls.ucBeamLength ucBeamLength;
        private UserControls.ucBeamType ucBeamType;
    }
}