namespace MultithreadinFiboonati
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
            this.textBox_EnterInteger = new System.Windows.Forms.TextBox();
            this.textBox_Fibonacci = new System.Windows.Forms.TextBox();
            this.button_Calculate = new System.Windows.Forms.Button();
            this.label_EnterInteger = new System.Windows.Forms.Label();
            this.label_Fobonacci = new System.Windows.Forms.Label();
            this.label_processing = new System.Windows.Forms.Label();
            this.label1_processing = new System.Windows.Forms.Label();
            this.label2_Fibonacci = new System.Windows.Forms.Label();
            this.label3_EnterInteger = new System.Windows.Forms.Label();
            this.button1_Calculate = new System.Windows.Forms.Button();
            this.textBox1_Fibonacci = new System.Windows.Forms.TextBox();
            this.textBox2_EnterInteger = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox_EnterInteger
            // 
            this.textBox_EnterInteger.Location = new System.Drawing.Point(222, 78);
            this.textBox_EnterInteger.Name = "textBox_EnterInteger";
            this.textBox_EnterInteger.Size = new System.Drawing.Size(100, 20);
            this.textBox_EnterInteger.TabIndex = 0;
            // 
            // textBox_Fibonacci
            // 
            this.textBox_Fibonacci.Location = new System.Drawing.Point(222, 145);
            this.textBox_Fibonacci.Name = "textBox_Fibonacci";
            this.textBox_Fibonacci.Size = new System.Drawing.Size(100, 20);
            this.textBox_Fibonacci.TabIndex = 1;
            // 
            // button_Calculate
            // 
            this.button_Calculate.Location = new System.Drawing.Point(353, 143);
            this.button_Calculate.Name = "button_Calculate";
            this.button_Calculate.Size = new System.Drawing.Size(75, 23);
            this.button_Calculate.TabIndex = 2;
            this.button_Calculate.Text = "Calculate";
            this.button_Calculate.UseVisualStyleBackColor = true;
            this.button_Calculate.Click += new System.EventHandler(this.button_Calculate_Click);
            // 
            // label_EnterInteger
            // 
            this.label_EnterInteger.AutoSize = true;
            this.label_EnterInteger.Location = new System.Drawing.Point(155, 81);
            this.label_EnterInteger.Name = "label_EnterInteger";
            this.label_EnterInteger.Size = new System.Drawing.Size(67, 13);
            this.label_EnterInteger.TabIndex = 3;
            this.label_EnterInteger.Text = "Enter integer";
            // 
            // label_Fobonacci
            // 
            this.label_Fobonacci.AutoSize = true;
            this.label_Fobonacci.Location = new System.Drawing.Point(155, 148);
            this.label_Fobonacci.Name = "label_Fobonacci";
            this.label_Fobonacci.Size = new System.Drawing.Size(53, 13);
            this.label_Fobonacci.TabIndex = 4;
            this.label_Fobonacci.Text = "Fibonacci";
            // 
            // label_processing
            // 
            this.label_processing.AutoSize = true;
            this.label_processing.Location = new System.Drawing.Point(364, 85);
            this.label_processing.Name = "label_processing";
            this.label_processing.Size = new System.Drawing.Size(19, 13);
            this.label_processing.TabIndex = 5;
            this.label_processing.Text = "....";
            // 
            // label1_processing
            // 
            this.label1_processing.AutoSize = true;
            this.label1_processing.Location = new System.Drawing.Point(364, 237);
            this.label1_processing.Name = "label1_processing";
            this.label1_processing.Size = new System.Drawing.Size(19, 13);
            this.label1_processing.TabIndex = 11;
            this.label1_processing.Text = "....";
            // 
            // label2_Fibonacci
            // 
            this.label2_Fibonacci.AutoSize = true;
            this.label2_Fibonacci.Location = new System.Drawing.Point(155, 300);
            this.label2_Fibonacci.Name = "label2_Fibonacci";
            this.label2_Fibonacci.Size = new System.Drawing.Size(53, 13);
            this.label2_Fibonacci.TabIndex = 10;
            this.label2_Fibonacci.Text = "Fibonacci";
            // 
            // label3_EnterInteger
            // 
            this.label3_EnterInteger.AutoSize = true;
            this.label3_EnterInteger.Location = new System.Drawing.Point(155, 233);
            this.label3_EnterInteger.Name = "label3_EnterInteger";
            this.label3_EnterInteger.Size = new System.Drawing.Size(67, 13);
            this.label3_EnterInteger.TabIndex = 9;
            this.label3_EnterInteger.Text = "Enter integer";
            // 
            // button1_Calculate
            // 
            this.button1_Calculate.Location = new System.Drawing.Point(353, 295);
            this.button1_Calculate.Name = "button1_Calculate";
            this.button1_Calculate.Size = new System.Drawing.Size(75, 23);
            this.button1_Calculate.TabIndex = 8;
            this.button1_Calculate.Text = "Calculate";
            this.button1_Calculate.UseVisualStyleBackColor = true;
            this.button1_Calculate.Click += new System.EventHandler(this.button1_Calculate_Click);
            // 
            // textBox1_Fibonacci
            // 
            this.textBox1_Fibonacci.Location = new System.Drawing.Point(222, 297);
            this.textBox1_Fibonacci.Name = "textBox1_Fibonacci";
            this.textBox1_Fibonacci.Size = new System.Drawing.Size(100, 20);
            this.textBox1_Fibonacci.TabIndex = 7;
            // 
            // textBox2_EnterInteger
            // 
            this.textBox2_EnterInteger.Location = new System.Drawing.Point(222, 230);
            this.textBox2_EnterInteger.Name = "textBox2_EnterInteger";
            this.textBox2_EnterInteger.Size = new System.Drawing.Size(100, 20);
            this.textBox2_EnterInteger.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 422);
            this.Controls.Add(this.label1_processing);
            this.Controls.Add(this.label2_Fibonacci);
            this.Controls.Add(this.label3_EnterInteger);
            this.Controls.Add(this.button1_Calculate);
            this.Controls.Add(this.textBox1_Fibonacci);
            this.Controls.Add(this.textBox2_EnterInteger);
            this.Controls.Add(this.label_processing);
            this.Controls.Add(this.label_Fobonacci);
            this.Controls.Add(this.label_EnterInteger);
            this.Controls.Add(this.button_Calculate);
            this.Controls.Add(this.textBox_Fibonacci);
            this.Controls.Add(this.textBox_EnterInteger);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_EnterInteger;
        private System.Windows.Forms.TextBox textBox_Fibonacci;
        private System.Windows.Forms.Button button_Calculate;
        private System.Windows.Forms.Label label_EnterInteger;
        private System.Windows.Forms.Label label_Fobonacci;
        private System.Windows.Forms.Label label_processing;
        private System.Windows.Forms.Label label1_processing;
        private System.Windows.Forms.Label label2_Fibonacci;
        private System.Windows.Forms.Label label3_EnterInteger;
        private System.Windows.Forms.Button button1_Calculate;
        private System.Windows.Forms.TextBox textBox1_Fibonacci;
        private System.Windows.Forms.TextBox textBox2_EnterInteger;
    }
}

