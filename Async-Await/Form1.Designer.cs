namespace Async_Await
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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button_Calc = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_Clear = new System.Windows.Forms.Button();
            this.checkBox_checkBox_Async_Sync = new System.Windows.Forms.CheckBox();
            this.label_Synchronous = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(66, 112);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 0;
            // 
            // button_Calc
            // 
            this.button_Calc.Location = new System.Drawing.Point(64, 160);
            this.button_Calc.Name = "button_Calc";
            this.button_Calc.Size = new System.Drawing.Size(122, 32);
            this.button_Calc.TabIndex = 1;
            this.button_Calc.Text = "CALC";
            this.button_Calc.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(64, 198);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(400, 242);
            this.textBox1.TabIndex = 2;
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(342, 160);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(122, 32);
            this.button_Clear.TabIndex = 3;
            this.button_Clear.Text = "CLEAR";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // checkBox_checkBox_Async_Sync
            // 
            this.checkBox_checkBox_Async_Sync.AutoSize = true;
            this.checkBox_checkBox_Async_Sync.Location = new System.Drawing.Point(228, 112);
            this.checkBox_checkBox_Async_Sync.Name = "checkBox_checkBox_Async_Sync";
            this.checkBox_checkBox_Async_Sync.Size = new System.Drawing.Size(84, 17);
            this.checkBox_checkBox_Async_Sync.TabIndex = 4;
            this.checkBox_checkBox_Async_Sync.Text = "Async/Sync";
            this.checkBox_checkBox_Async_Sync.UseVisualStyleBackColor = true;
            this.checkBox_checkBox_Async_Sync.CheckedChanged += new System.EventHandler(this.checkBox_checkBox_Async_Sync_CheckedChanged);
            // 
            // label_Synchronous
            // 
            this.label_Synchronous.AutoSize = true;
            this.label_Synchronous.Location = new System.Drawing.Point(225, 170);
            this.label_Synchronous.Name = "label_Synchronous";
            this.label_Synchronous.Size = new System.Drawing.Size(69, 13);
            this.label_Synchronous.TabIndex = 5;
            this.label_Synchronous.Text = "Synchronous";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 465);
            this.Controls.Add(this.label_Synchronous);
            this.Controls.Add(this.checkBox_checkBox_Async_Sync);
            this.Controls.Add(this.button_Clear);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_Calc);
            this.Controls.Add(this.numericUpDown1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button_Calc;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.CheckBox checkBox_checkBox_Async_Sync;
        private System.Windows.Forms.Label label_Synchronous;
    }
}

