namespace DCEStudyTools.LevelCreation.UserControls
{
    partial class ucLevels
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
            this.numOfLvls = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numOfBasements = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numOfLvls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOfBasements)).BeginInit();
            this.SuspendLayout();
            // 
            // numOfLvls
            // 
            this.numOfLvls.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numOfLvls.Location = new System.Drawing.Point(131, 7);
            this.numOfLvls.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numOfLvls.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numOfLvls.Name = "numOfLvls";
            this.numOfLvls.Size = new System.Drawing.Size(65, 27);
            this.numOfLvls.TabIndex = 3;
            this.numOfLvls.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 40);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nombre des \r\nétages";
            // 
            // numOfBasements
            // 
            this.numOfBasements.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numOfBasements.Location = new System.Drawing.Point(131, 60);
            this.numOfBasements.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numOfBasements.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numOfBasements.Name = "numOfBasements";
            this.numOfBasements.Size = new System.Drawing.Size(65, 27);
            this.numOfBasements.TabIndex = 5;
            this.numOfBasements.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 40);
            this.label1.TabIndex = 6;
            this.label1.Text = "Nombre des \r\nsous-sol";
            // 
            // ucLevels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numOfBasements);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numOfLvls);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucLevels";
            this.Size = new System.Drawing.Size(209, 102);
            this.Load += new System.EventHandler(this.usLevels_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numOfLvls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOfBasements)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numOfLvls;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numOfBasements;
        private System.Windows.Forms.Label label1;
    }
}
