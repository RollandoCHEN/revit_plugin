namespace DCEStudyTools.Design.Beam.BeamCreation
{
    partial class BeamCreationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BeamCreationForm));
            this.axeIsOnStrView = new System.Windows.Forms.RadioButton();
            this.axeIsOnArchView = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // axeIsOnStrView
            // 
            this.axeIsOnStrView.AutoSize = true;
            this.axeIsOnStrView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.axeIsOnStrView.Location = new System.Drawing.Point(16, 23);
            this.axeIsOnStrView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.axeIsOnStrView.Name = "axeIsOnStrView";
            this.axeIsOnStrView.Size = new System.Drawing.Size(522, 24);
            this.axeIsOnStrView.TabIndex = 0;
            this.axeIsOnStrView.Text = "Créer des poutres au niveau de plancher où se trouveent les Axes";
            this.axeIsOnStrView.UseVisualStyleBackColor = true;
            this.axeIsOnStrView.CheckedChanged += new System.EventHandler(this.axeIsOnStrView_CheckedChanged);
            // 
            // axeIsOnArchView
            // 
            this.axeIsOnArchView.AutoSize = true;
            this.axeIsOnArchView.Checked = true;
            this.axeIsOnArchView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.axeIsOnArchView.Location = new System.Drawing.Point(16, 78);
            this.axeIsOnArchView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.axeIsOnArchView.Name = "axeIsOnArchView";
            this.axeIsOnArchView.Size = new System.Drawing.Size(632, 24);
            this.axeIsOnArchView.TabIndex = 0;
            this.axeIsOnArchView.TabStop = true;
            this.axeIsOnArchView.Text = "Créer des poutres au niveau de plancher qui est AU DESSUS de celui  des Axes";
            this.axeIsOnArchView.UseVisualStyleBackColor = true;
            this.axeIsOnArchView.CheckedChanged += new System.EventHandler(this.axeIsOnArchView_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(39, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(499, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Dans le cas où les axes sont posés sur une vue en plan structure";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(39, 107);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(493, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Dans le cas où les axes sont posés sur un paln d\'étage architect";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(665, 166);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(95, 32);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(565, 166);
            this.okButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(95, 32);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // BeamCreationForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(775, 212);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.axeIsOnArchView);
            this.Controls.Add(this.axeIsOnStrView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "BeamCreationForm";
            this.Text = "Créer des Poutres à partir des Axes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton axeIsOnStrView;
        private System.Windows.Forms.RadioButton axeIsOnArchView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
    }
}