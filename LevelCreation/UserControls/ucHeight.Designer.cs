namespace DCEStudyTools.LevelCreation.UserControls
{
    partial class ucHeight
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.levelHeight = new System.Windows.Forms.TextBox();
            this.basementHeight = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.defaultHeight = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // levelHeight
            // 
            this.levelHeight.Enabled = false;
            this.levelHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelHeight.Location = new System.Drawing.Point(133, 35);
            this.levelHeight.Margin = new System.Windows.Forms.Padding(2);
            this.levelHeight.Name = "levelHeight";
            this.levelHeight.Size = new System.Drawing.Size(46, 23);
            this.levelHeight.TabIndex = 12;
            this.levelHeight.Text = "2,77";
            // 
            // basementHeight
            // 
            this.basementHeight.Enabled = false;
            this.basementHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.basementHeight.Location = new System.Drawing.Point(133, 78);
            this.basementHeight.Margin = new System.Windows.Forms.Padding(2);
            this.basementHeight.Name = "basementHeight";
            this.basementHeight.Size = new System.Drawing.Size(46, 23);
            this.basementHeight.TabIndex = 13;
            this.basementHeight.Text = "3,00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 29);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 34);
            this.label5.TabIndex = 11;
            this.label5.Text = "Hauteur de niveau\r\npour les étages";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(179, 38);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "m";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(179, 81);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "m";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 72);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 34);
            this.label4.TabIndex = 10;
            this.label4.Text = "Hauteur de niveau\r\npour les sous-sol";
            // 
            // defaultHeight
            // 
            this.defaultHeight.AutoSize = true;
            this.defaultHeight.Checked = true;
            this.defaultHeight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.defaultHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defaultHeight.Location = new System.Drawing.Point(5, 2);
            this.defaultHeight.Margin = new System.Windows.Forms.Padding(2);
            this.defaultHeight.Name = "defaultHeight";
            this.defaultHeight.Size = new System.Drawing.Size(147, 21);
            this.defaultHeight.TabIndex = 14;
            this.defaultHeight.Text = "Hauteur par défaut";
            this.defaultHeight.UseVisualStyleBackColor = true;
            this.defaultHeight.CheckedChanged += new System.EventHandler(this.defaultValue_CheckedChanged);
            // 
            // ucHeight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.defaultHeight);
            this.Controls.Add(this.levelHeight);
            this.Controls.Add(this.basementHeight);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Name = "ucHeight";
            this.Size = new System.Drawing.Size(202, 115);
            this.Load += new System.EventHandler(this.ucHeight_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox levelHeight;
        private System.Windows.Forms.TextBox basementHeight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox defaultHeight;
    }
}
