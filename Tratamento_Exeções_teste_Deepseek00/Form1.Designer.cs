namespace Tratamento_Exeções_teste_Deepseek00
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.DentesAnelar = new System.Windows.Forms.GroupBox();
            this.button1_Calcular = new System.Windows.Forms.Button();
            this.textBox1_Solar = new System.Windows.Forms.TextBox();
            this.textBox2_Planetaria = new System.Windows.Forms.TextBox();
            this.Solar_dth = new System.Windows.Forms.Label();
            this.Planetaria_dth = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DentesAnelar.SuspendLayout();
            this.SuspendLayout();
            // 
            // DentesAnelar
            // 
            this.DentesAnelar.Controls.Add(this.label1);
            this.DentesAnelar.Controls.Add(this.label3);
            this.DentesAnelar.Controls.Add(this.Planetaria_dth);
            this.DentesAnelar.Controls.Add(this.Solar_dth);
            this.DentesAnelar.Controls.Add(this.textBox2_Planetaria);
            this.DentesAnelar.Controls.Add(this.textBox1_Solar);
            this.DentesAnelar.Controls.Add(this.button1_Calcular);
            this.DentesAnelar.Location = new System.Drawing.Point(157, 88);
            this.DentesAnelar.Name = "DentesAnelar";
            this.DentesAnelar.Size = new System.Drawing.Size(267, 169);
            this.DentesAnelar.TabIndex = 0;
            this.DentesAnelar.TabStop = false;
            this.DentesAnelar.Text = "Dentes Anelar";
            // 
            // button1_Calcular
            // 
            this.button1_Calcular.Location = new System.Drawing.Point(89, 122);
            this.button1_Calcular.Name = "button1_Calcular";
            this.button1_Calcular.Size = new System.Drawing.Size(75, 23);
            this.button1_Calcular.TabIndex = 1;
            this.button1_Calcular.Text = "CALCULAR";
            this.button1_Calcular.UseVisualStyleBackColor = true;
            this.button1_Calcular.Click += new System.EventHandler(this.button1_Calcular_Click);
            // 
            // textBox1_Solar
            // 
            this.textBox1_Solar.Location = new System.Drawing.Point(151, 27);
            this.textBox1_Solar.Name = "textBox1_Solar";
            this.textBox1_Solar.Size = new System.Drawing.Size(100, 20);
            this.textBox1_Solar.TabIndex = 1;
            // 
            // textBox2_Planetaria
            // 
            this.textBox2_Planetaria.Location = new System.Drawing.Point(151, 53);
            this.textBox2_Planetaria.Name = "textBox2_Planetaria";
            this.textBox2_Planetaria.Size = new System.Drawing.Size(100, 20);
            this.textBox2_Planetaria.TabIndex = 2;
            // 
            // Solar_dth
            // 
            this.Solar_dth.AutoSize = true;
            this.Solar_dth.Location = new System.Drawing.Point(19, 30);
            this.Solar_dth.Name = "Solar_dth";
            this.Solar_dth.Size = new System.Drawing.Size(61, 13);
            this.Solar_dth.TabIndex = 4;
            this.Solar_dth.Text = "N°dth-Solar";
            // 
            // Planetaria_dth
            // 
            this.Planetaria_dth.AutoSize = true;
            this.Planetaria_dth.Location = new System.Drawing.Point(19, 59);
            this.Planetaria_dth.Name = "Planetaria_dth";
            this.Planetaria_dth.Size = new System.Drawing.Size(87, 13);
            this.Planetaria_dth.TabIndex = 5;
            this.Planetaria_dth.Text = "N° dth Planetaria";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Numero de dentes Anelar: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 368);
            this.Controls.Add(this.DentesAnelar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(20, 20);
            this.MaximumSize = new System.Drawing.Size(616, 589);
            this.Name = "Form1";
            this.Text = "Calculos de Engrenagem Planetaria";
            this.DentesAnelar.ResumeLayout(false);
            this.DentesAnelar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox DentesAnelar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Planetaria_dth;
        private System.Windows.Forms.Label Solar_dth;
        private System.Windows.Forms.TextBox textBox2_Planetaria;
        private System.Windows.Forms.TextBox textBox1_Solar;
        private System.Windows.Forms.Button button1_Calcular;
        private System.Windows.Forms.Label label1;
    }
}

