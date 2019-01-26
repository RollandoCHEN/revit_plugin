using System.Drawing;

namespace DCEStudyTools.FloorSpanDirection
{
    partial class FloorDirectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FloorDirectionForm));
            this.TopArrow = new System.Windows.Forms.RadioButton();
            this.LeftArrow = new System.Windows.Forms.RadioButton();
            this.RightArrow = new System.Windows.Forms.RadioButton();
            this.BottomArrow = new System.Windows.Forms.RadioButton();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TopArrow
            // 
            this.TopArrow.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TopArrow.AutoSize = true;
            this.TopArrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TopArrow.Location = new System.Drawing.Point(161, 22);
            this.TopArrow.Margin = new System.Windows.Forms.Padding(4);
            this.TopArrow.Name = "TopArrow";
            this.TopArrow.Size = new System.Drawing.Size(106, 24);
            this.TopArrow.TabIndex = 0;
            this.TopArrow.Text = "Vers Haut";
            this.TopArrow.UseVisualStyleBackColor = true;
            // 
            // LeftArrow
            // 
            this.LeftArrow.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LeftArrow.AutoSize = true;
            this.LeftArrow.Checked = true;
            this.LeftArrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LeftArrow.Location = new System.Drawing.Point(5, 145);
            this.LeftArrow.Margin = new System.Windows.Forms.Padding(4);
            this.LeftArrow.Name = "LeftArrow";
            this.LeftArrow.Size = new System.Drawing.Size(88, 44);
            this.LeftArrow.TabIndex = 0;
            this.LeftArrow.TabStop = true;
            this.LeftArrow.Text = "Vers\r\nGauche";
            this.LeftArrow.UseVisualStyleBackColor = true;
            // 
            // RightArrow
            // 
            this.RightArrow.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.RightArrow.AutoSize = true;
            this.RightArrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RightArrow.Location = new System.Drawing.Point(373, 151);
            this.RightArrow.Margin = new System.Windows.Forms.Padding(4);
            this.RightArrow.Name = "RightArrow";
            this.RightArrow.Size = new System.Drawing.Size(67, 44);
            this.RightArrow.TabIndex = 0;
            this.RightArrow.Text = "Vers\r\nDroit";
            this.RightArrow.UseVisualStyleBackColor = true;
            // 
            // BottomArrow
            // 
            this.BottomArrow.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BottomArrow.AutoSize = true;
            this.BottomArrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BottomArrow.Location = new System.Drawing.Point(164, 288);
            this.BottomArrow.Margin = new System.Windows.Forms.Padding(4);
            this.BottomArrow.Name = "BottomArrow";
            this.BottomArrow.Size = new System.Drawing.Size(100, 24);
            this.BottomArrow.TabIndex = 0;
            this.BottomArrow.Text = "Vers Bas";
            this.BottomArrow.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(373, 356);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(95, 32);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(277, 354);
            this.okButton.Margin = new System.Windows.Forms.Padding(4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(89, 34);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pictureBox);
            this.groupBox1.Controls.Add(this.BottomArrow);
            this.groupBox1.Controls.Add(this.TopArrow);
            this.groupBox1.Controls.Add(this.LeftArrow);
            this.groupBox1.Controls.Add(this.RightArrow);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(456, 335);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sens de portée";
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Image = global::DCEStudyTools.Properties.Resources.floor_span_direction;
            this.pictureBox.Location = new System.Drawing.Point(111, 53);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(244, 231);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // FloorDirectionForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(480, 401);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FloorDirectionForm";
            this.Text = "Changer Sens de Portée";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton TopArrow;
        private System.Windows.Forms.RadioButton LeftArrow;
        private System.Windows.Forms.RadioButton RightArrow;
        private System.Windows.Forms.RadioButton BottomArrow;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}