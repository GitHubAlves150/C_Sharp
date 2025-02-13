namespace Eventos
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Confirm_BTN = new System.Windows.Forms.Button();
            this.clear_btn = new System.Windows.Forms.Button();
            this.Outputlabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CounterLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(49, 62);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '°';
            this.textBox2.Size = new System.Drawing.Size(234, 20);
            this.textBox2.TabIndex = 0;
            // 
            // Confirm_BTN
            // 
            this.Confirm_BTN.Location = new System.Drawing.Point(49, 101);
            this.Confirm_BTN.Name = "Confirm_BTN";
            this.Confirm_BTN.Size = new System.Drawing.Size(234, 23);
            this.Confirm_BTN.TabIndex = 1;
            this.Confirm_BTN.Text = "Confirm";
            this.Confirm_BTN.UseVisualStyleBackColor = true;
            this.Confirm_BTN.Click += new System.EventHandler(this.Confirm_BTN_Click);
            // 
            // clear_btn
            // 
            this.clear_btn.Location = new System.Drawing.Point(49, 154);
            this.clear_btn.Name = "clear_btn";
            this.clear_btn.Size = new System.Drawing.Size(234, 23);
            this.clear_btn.TabIndex = 2;
            this.clear_btn.Text = "Clear";
            this.clear_btn.UseVisualStyleBackColor = true;
            this.clear_btn.Click += new System.EventHandler(this.clear_btn_Click);
            // 
            // Outputlabel
            // 
            this.Outputlabel.AutoSize = true;
            this.Outputlabel.Location = new System.Drawing.Point(332, 62);
            this.Outputlabel.Name = "Outputlabel";
            this.Outputlabel.Size = new System.Drawing.Size(16, 13);
            this.Outputlabel.TabIndex = 4;
            this.Outputlabel.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(332, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Remanning attempts";
            // 
            // CounterLabel
            // 
            this.CounterLabel.AutoSize = true;
            this.CounterLabel.Location = new System.Drawing.Point(423, 106);
            this.CounterLabel.Name = "CounterLabel";
            this.CounterLabel.Size = new System.Drawing.Size(13, 13);
            this.CounterLabel.TabIndex = 6;
            this.CounterLabel.Text = "3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "PassWord";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(534, 331);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CounterLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Outputlabel);
            this.Controls.Add(this.clear_btn);
            this.Controls.Add(this.Confirm_BTN);
            this.Controls.Add(this.textBox2);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button ConfirmBTN;
        private System.Windows.Forms.Button ClearBTN;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button Confirm_BTN;
        private System.Windows.Forms.Button clear_btn;
        private System.Windows.Forms.Label Outputlabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label CounterLabel;
        private System.Windows.Forms.Label label2;
    }
}

