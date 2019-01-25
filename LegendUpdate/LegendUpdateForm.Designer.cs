namespace DCEStudyTools.LegendUpdate
{
    partial class LegendUpdateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LegendUpdateForm));
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.BottomRight = new System.Windows.Forms.RadioButton();
            this.BottomLeft = new System.Windows.Forms.RadioButton();
            this.TopLeft = new System.Windows.Forms.RadioButton();
            this.TopRight = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(341, 292);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(71, 26);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(269, 290);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(67, 28);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // BottomRight
            // 
            this.BottomRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BottomRight.AutoSize = true;
            this.BottomRight.BackColor = System.Drawing.SystemColors.Window;
            this.BottomRight.Checked = true;
            this.BottomRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BottomRight.Location = new System.Drawing.Point(286, 213);
            this.BottomRight.Name = "BottomRight";
            this.BottomRight.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.BottomRight.Size = new System.Drawing.Size(81, 20);
            this.BottomRight.TabIndex = 12;
            this.BottomRight.TabStop = true;
            this.BottomRight.Text = "Bas Droit";
            this.BottomRight.UseVisualStyleBackColor = false;
            // 
            // BottomLeft
            // 
            this.BottomLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BottomLeft.AutoSize = true;
            this.BottomLeft.BackColor = System.Drawing.SystemColors.Window;
            this.BottomLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BottomLeft.Location = new System.Drawing.Point(29, 225);
            this.BottomLeft.Name = "BottomLeft";
            this.BottomLeft.Size = new System.Drawing.Size(100, 20);
            this.BottomLeft.TabIndex = 12;
            this.BottomLeft.Text = "Bas Gauche";
            this.BottomLeft.UseVisualStyleBackColor = false;
            // 
            // TopLeft
            // 
            this.TopLeft.AutoSize = true;
            this.TopLeft.BackColor = System.Drawing.SystemColors.Window;
            this.TopLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopLeft.Location = new System.Drawing.Point(28, 53);
            this.TopLeft.Name = "TopLeft";
            this.TopLeft.Size = new System.Drawing.Size(104, 20);
            this.TopLeft.TabIndex = 12;
            this.TopLeft.Text = "Haut Gauche";
            this.TopLeft.UseVisualStyleBackColor = false;
            // 
            // TopRight
            // 
            this.TopRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TopRight.AutoSize = true;
            this.TopRight.BackColor = System.Drawing.SystemColors.Window;
            this.TopRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopRight.Location = new System.Drawing.Point(280, 64);
            this.TopRight.Name = "TopRight";
            this.TopRight.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TopRight.Size = new System.Drawing.Size(85, 20);
            this.TopRight.TabIndex = 12;
            this.TopRight.Text = "Haut Droit";
            this.TopRight.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TopLeft);
            this.groupBox1.Controls.Add(this.BottomRight);
            this.groupBox1.Controls.Add(this.TopRight);
            this.groupBox1.Controls.Add(this.BottomLeft);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 272);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Position de la Légende";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(14, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(373, 233);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // LegendUpdateForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(427, 334);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LegendUpdateForm";
            this.Text = "Ajouter/Màj Légendes";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.RadioButton BottomRight;
        private System.Windows.Forms.RadioButton BottomLeft;
        private System.Windows.Forms.RadioButton TopLeft;
        private System.Windows.Forms.RadioButton TopRight;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}