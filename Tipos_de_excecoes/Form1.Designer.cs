namespace Tipos_de_excecoes
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
            this.groupBox1_divideByZero = new System.Windows.Forms.GroupBox();
            this.textBox_Denominador = new System.Windows.Forms.TextBox();
            this.textBox_Numerador = new System.Windows.Forms.TextBox();
            this.result = new System.Windows.Forms.Label();
            this.Denominador_lb = new System.Windows.Forms.Label();
            this.Numerado_lb = new System.Windows.Forms.Label();
            this.calcular_btn = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox_OverFlow = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numberInt_textBox = new System.Windows.Forms.TextBox();
            this.numberint_lb = new System.Windows.Forms.Label();
            this.GerarOverFlow_btn = new System.Windows.Forms.Button();
            this.numberInt_label = new System.Windows.Forms.Label();
            this.groupBox_Exception = new System.Windows.Forms.GroupBox();
            this.resulta_LB = new System.Windows.Forms.Label();
            this.Input_TextBox = new System.Windows.Forms.TextBox();
            this.Input_lb = new System.Windows.Forms.Label();
            this.gerar_btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.result_label = new System.Windows.Forms.Label();
            this.Rodar_btn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox1_divideByZero.SuspendLayout();
            this.groupBox_OverFlow.SuspendLayout();
            this.groupBox_Exception.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1_divideByZero
            // 
            this.groupBox1_divideByZero.Controls.Add(this.textBox_Denominador);
            this.groupBox1_divideByZero.Controls.Add(this.textBox_Numerador);
            this.groupBox1_divideByZero.Controls.Add(this.result);
            this.groupBox1_divideByZero.Controls.Add(this.Denominador_lb);
            this.groupBox1_divideByZero.Controls.Add(this.Numerado_lb);
            this.groupBox1_divideByZero.Controls.Add(this.calcular_btn);
            this.groupBox1_divideByZero.Location = new System.Drawing.Point(21, 12);
            this.groupBox1_divideByZero.Name = "groupBox1_divideByZero";
            this.groupBox1_divideByZero.Size = new System.Drawing.Size(278, 146);
            this.groupBox1_divideByZero.TabIndex = 0;
            this.groupBox1_divideByZero.TabStop = false;
            this.groupBox1_divideByZero.Text = "Divide por zero";
            // 
            // textBox_Denominador
            // 
            this.textBox_Denominador.Location = new System.Drawing.Point(82, 40);
            this.textBox_Denominador.Name = "textBox_Denominador";
            this.textBox_Denominador.Size = new System.Drawing.Size(132, 20);
            this.textBox_Denominador.TabIndex = 5;
            // 
            // textBox_Numerador
            // 
            this.textBox_Numerador.Location = new System.Drawing.Point(82, 13);
            this.textBox_Numerador.Name = "textBox_Numerador";
            this.textBox_Numerador.Size = new System.Drawing.Size(132, 20);
            this.textBox_Numerador.TabIndex = 4;
            // 
            // result
            // 
            this.result.AutoSize = true;
            this.result.Location = new System.Drawing.Point(119, 88);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(19, 13);
            this.result.TabIndex = 3;
            this.result.Text = "....";
            // 
            // Denominador_lb
            // 
            this.Denominador_lb.AutoSize = true;
            this.Denominador_lb.Location = new System.Drawing.Point(6, 46);
            this.Denominador_lb.Name = "Denominador_lb";
            this.Denominador_lb.Size = new System.Drawing.Size(70, 13);
            this.Denominador_lb.TabIndex = 2;
            this.Denominador_lb.Text = "Denominador";
            // 
            // Numerado_lb
            // 
            this.Numerado_lb.AutoSize = true;
            this.Numerado_lb.Location = new System.Drawing.Point(6, 16);
            this.Numerado_lb.Name = "Numerado_lb";
            this.Numerado_lb.Size = new System.Drawing.Size(59, 13);
            this.Numerado_lb.TabIndex = 1;
            this.Numerado_lb.Text = "Numerador";
            // 
            // calcular_btn
            // 
            this.calcular_btn.Location = new System.Drawing.Point(101, 117);
            this.calcular_btn.Name = "calcular_btn";
            this.calcular_btn.Size = new System.Drawing.Size(75, 23);
            this.calcular_btn.TabIndex = 0;
            this.calcular_btn.Text = "CALCULAR";
            this.calcular_btn.UseVisualStyleBackColor = true;
            this.calcular_btn.Click += new System.EventHandler(this.calcular_btn_Click);
            // 
            // groupBox_OverFlow
            // 
            this.groupBox_OverFlow.Controls.Add(this.label1);
            this.groupBox_OverFlow.Controls.Add(this.numberInt_textBox);
            this.groupBox_OverFlow.Controls.Add(this.numberint_lb);
            this.groupBox_OverFlow.Controls.Add(this.GerarOverFlow_btn);
            this.groupBox_OverFlow.Controls.Add(this.numberInt_label);
            this.groupBox_OverFlow.Location = new System.Drawing.Point(316, 13);
            this.groupBox_OverFlow.Name = "groupBox_OverFlow";
            this.groupBox_OverFlow.Size = new System.Drawing.Size(252, 145);
            this.groupBox_OverFlow.TabIndex = 1;
            this.groupBox_OverFlow.TabStop = false;
            this.groupBox_OverFlow.Text = "Overver Flow Exception";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Testa o numero 2.147.483.647 e sooma +1";
            // 
            // numberInt_textBox
            // 
            this.numberInt_textBox.Location = new System.Drawing.Point(79, 19);
            this.numberInt_textBox.Name = "numberInt_textBox";
            this.numberInt_textBox.Size = new System.Drawing.Size(100, 20);
            this.numberInt_textBox.TabIndex = 4;
            // 
            // numberint_lb
            // 
            this.numberint_lb.AutoSize = true;
            this.numberint_lb.Location = new System.Drawing.Point(7, 19);
            this.numberint_lb.Name = "numberint_lb";
            this.numberint_lb.Size = new System.Drawing.Size(58, 13);
            this.numberint_lb.TabIndex = 3;
            this.numberint_lb.Text = "Number int";
            // 
            // GerarOverFlow_btn
            // 
            this.GerarOverFlow_btn.Location = new System.Drawing.Point(95, 116);
            this.GerarOverFlow_btn.Name = "GerarOverFlow_btn";
            this.GerarOverFlow_btn.Size = new System.Drawing.Size(75, 23);
            this.GerarOverFlow_btn.TabIndex = 1;
            this.GerarOverFlow_btn.Text = "GERAR";
            this.GerarOverFlow_btn.UseVisualStyleBackColor = true;
            this.GerarOverFlow_btn.Click += new System.EventHandler(this.GerarOverFlow_btn_Click);
            // 
            // numberInt_label
            // 
            this.numberInt_label.AutoSize = true;
            this.numberInt_label.Location = new System.Drawing.Point(113, 87);
            this.numberInt_label.Name = "numberInt_label";
            this.numberInt_label.Size = new System.Drawing.Size(16, 13);
            this.numberInt_label.TabIndex = 0;
            this.numberInt_label.Text = "...";
            // 
            // groupBox_Exception
            // 
            this.groupBox_Exception.Controls.Add(this.resulta_LB);
            this.groupBox_Exception.Controls.Add(this.Input_TextBox);
            this.groupBox_Exception.Controls.Add(this.Input_lb);
            this.groupBox_Exception.Controls.Add(this.gerar_btn);
            this.groupBox_Exception.Location = new System.Drawing.Point(21, 174);
            this.groupBox_Exception.Name = "groupBox_Exception";
            this.groupBox_Exception.Size = new System.Drawing.Size(278, 155);
            this.groupBox_Exception.TabIndex = 2;
            this.groupBox_Exception.TabStop = false;
            this.groupBox_Exception.Text = "Exception";
            // 
            // resulta_LB
            // 
            this.resulta_LB.AutoSize = true;
            this.resulta_LB.Location = new System.Drawing.Point(119, 99);
            this.resulta_LB.Name = "resulta_LB";
            this.resulta_LB.Size = new System.Drawing.Size(19, 13);
            this.resulta_LB.TabIndex = 3;
            this.resulta_LB.Text = "....";
            // 
            // Input_TextBox
            // 
            this.Input_TextBox.Location = new System.Drawing.Point(82, 25);
            this.Input_TextBox.Name = "Input_TextBox";
            this.Input_TextBox.Size = new System.Drawing.Size(132, 20);
            this.Input_TextBox.TabIndex = 2;
            // 
            // Input_lb
            // 
            this.Input_lb.AutoSize = true;
            this.Input_lb.Location = new System.Drawing.Point(9, 33);
            this.Input_lb.Name = "Input_lb";
            this.Input_lb.Size = new System.Drawing.Size(31, 13);
            this.Input_lb.TabIndex = 1;
            this.Input_lb.Text = "Input";
            // 
            // gerar_btn
            // 
            this.gerar_btn.Location = new System.Drawing.Point(101, 126);
            this.gerar_btn.Name = "gerar_btn";
            this.gerar_btn.Size = new System.Drawing.Size(75, 23);
            this.gerar_btn.TabIndex = 0;
            this.gerar_btn.Text = "GERAR";
            this.gerar_btn.UseVisualStyleBackColor = true;
            this.gerar_btn.Click += new System.EventHandler(this.gerar_btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.result_label);
            this.groupBox1.Controls.Add(this.Rodar_btn);
            this.groupBox1.Location = new System.Drawing.Point(316, 174);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 155);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "when";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(95, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(84, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Numero positivo";
            // 
            // result_label
            // 
            this.result_label.AutoSize = true;
            this.result_label.Location = new System.Drawing.Point(116, 101);
            this.result_label.Name = "result_label";
            this.result_label.Size = new System.Drawing.Size(16, 13);
            this.result_label.TabIndex = 1;
            this.result_label.Text = "...";
            // 
            // Rodar_btn
            // 
            this.Rodar_btn.Location = new System.Drawing.Point(95, 120);
            this.Rodar_btn.Name = "Rodar_btn";
            this.Rodar_btn.Size = new System.Drawing.Size(75, 23);
            this.Rodar_btn.TabIndex = 0;
            this.Rodar_btn.Text = "RODAR";
            this.Rodar_btn.UseVisualStyleBackColor = true;
            this.Rodar_btn.Click += new System.EventHandler(this.Rodar_btn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Location = new System.Drawing.Point(590, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 145);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cheked";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(140, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "...";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(127, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "CHEKED";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Input";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(112, 19);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_Exception);
            this.Controls.Add(this.groupBox_OverFlow);
            this.Controls.Add(this.groupBox1_divideByZero);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Geradores de exeçoes";
            this.groupBox1_divideByZero.ResumeLayout(false);
            this.groupBox1_divideByZero.PerformLayout();
            this.groupBox_OverFlow.ResumeLayout(false);
            this.groupBox_OverFlow.PerformLayout();
            this.groupBox_Exception.ResumeLayout(false);
            this.groupBox_Exception.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1_divideByZero;
        private System.Windows.Forms.Label Denominador_lb;
        private System.Windows.Forms.Label Numerado_lb;
        private System.Windows.Forms.Button calcular_btn;
        private System.Windows.Forms.TextBox textBox_Denominador;
        private System.Windows.Forms.TextBox textBox_Numerador;
        private System.Windows.Forms.Label result;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox_OverFlow;
        private System.Windows.Forms.TextBox numberInt_textBox;
        private System.Windows.Forms.Label numberint_lb;
        private System.Windows.Forms.Button GerarOverFlow_btn;
        private System.Windows.Forms.Label numberInt_label;
        private System.Windows.Forms.GroupBox groupBox_Exception;
        private System.Windows.Forms.Label Input_lb;
        private System.Windows.Forms.Button gerar_btn;
        private System.Windows.Forms.TextBox Input_TextBox;
        private System.Windows.Forms.Label resulta_LB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Rodar_btn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label result_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
    }
}

