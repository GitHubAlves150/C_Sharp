namespace Construtores_de_String_UI
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
            this.Input_String = new System.Windows.Forms.TextBox();
            this.lb_youre_string = new System.Windows.Forms.Label();
            this.lb_output = new System.Windows.Forms.Label();
            this.btn_run = new System.Windows.Forms.Button();
            this.lb_Result = new System.Windows.Forms.Label();
            this.radioButton_Copy = new System.Windows.Forms.RadioButton();
            this.radioButton_Compare = new System.Windows.Forms.RadioButton();
            this.radioButton_EndsWith = new System.Windows.Forms.RadioButton();
            this.radioButton_Sart_With = new System.Windows.Forms.RadioButton();
            this.radioButton_InvertString = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // Input_String
            // 
            this.Input_String.Location = new System.Drawing.Point(66, 159);
            this.Input_String.Name = "Input_String";
            this.Input_String.Size = new System.Drawing.Size(665, 20);
            this.Input_String.TabIndex = 0;
            // 
            // lb_youre_string
            // 
            this.lb_youre_string.AutoSize = true;
            this.lb_youre_string.Location = new System.Drawing.Point(63, 143);
            this.lb_youre_string.Name = "lb_youre_string";
            this.lb_youre_string.Size = new System.Drawing.Size(65, 13);
            this.lb_youre_string.TabIndex = 2;
            this.lb_youre_string.Text = "Youre String";
            // 
            // lb_output
            // 
            this.lb_output.AutoSize = true;
            this.lb_output.Location = new System.Drawing.Point(187, 143);
            this.lb_output.Name = "lb_output";
            this.lb_output.Size = new System.Drawing.Size(40, 13);
            this.lb_output.TabIndex = 3;
            this.lb_output.Text = "Length";
            // 
            // btn_run
            // 
            this.btn_run.Location = new System.Drawing.Point(66, 207);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(198, 33);
            this.btn_run.TabIndex = 4;
            this.btn_run.Text = "RUN";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // lb_Result
            // 
            this.lb_Result.AutoSize = true;
            this.lb_Result.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Result.ForeColor = System.Drawing.Color.Blue;
            this.lb_Result.Location = new System.Drawing.Point(62, 343);
            this.lb_Result.Name = "lb_Result";
            this.lb_Result.Padding = new System.Windows.Forms.Padding(0, 0, 500, 0);
            this.lb_Result.Size = new System.Drawing.Size(613, 24);
            this.lb_Result.TabIndex = 5;
            this.lb_Result.Text = "RESULT....";
            // 
            // radioButton_Copy
            // 
            this.radioButton_Copy.AutoSize = true;
            this.radioButton_Copy.Location = new System.Drawing.Point(292, 223);
            this.radioButton_Copy.Name = "radioButton_Copy";
            this.radioButton_Copy.Size = new System.Drawing.Size(65, 17);
            this.radioButton_Copy.TabIndex = 6;
            this.radioButton_Copy.TabStop = true;
            this.radioButton_Copy.Text = "Copy To";
            this.radioButton_Copy.UseVisualStyleBackColor = true;
            this.radioButton_Copy.CheckedChanged += new System.EventHandler(this.radioButton_Copy_CheckedChanged);
            // 
            // radioButton_Compare
            // 
            this.radioButton_Compare.AutoSize = true;
            this.radioButton_Compare.Location = new System.Drawing.Point(292, 246);
            this.radioButton_Compare.Name = "radioButton_Compare";
            this.radioButton_Compare.Size = new System.Drawing.Size(67, 17);
            this.radioButton_Compare.TabIndex = 7;
            this.radioButton_Compare.TabStop = true;
            this.radioButton_Compare.Text = "Compare";
            this.radioButton_Compare.UseVisualStyleBackColor = true;
            this.radioButton_Compare.CheckedChanged += new System.EventHandler(this.radioButton_Compare_CheckedChanged);
            // 
            // radioButton_EndsWith
            // 
            this.radioButton_EndsWith.AutoSize = true;
            this.radioButton_EndsWith.Location = new System.Drawing.Point(292, 292);
            this.radioButton_EndsWith.Name = "radioButton_EndsWith";
            this.radioButton_EndsWith.Size = new System.Drawing.Size(74, 17);
            this.radioButton_EndsWith.TabIndex = 9;
            this.radioButton_EndsWith.TabStop = true;
            this.radioButton_EndsWith.Text = "Ends With";
            this.radioButton_EndsWith.UseVisualStyleBackColor = true;
            this.radioButton_EndsWith.CheckedChanged += new System.EventHandler(this.radioButton_EndsWith_CheckedChanged);
            // 
            // radioButton_Sart_With
            // 
            this.radioButton_Sart_With.AutoSize = true;
            this.radioButton_Sart_With.Location = new System.Drawing.Point(292, 269);
            this.radioButton_Sart_With.Name = "radioButton_Sart_With";
            this.radioButton_Sart_With.Size = new System.Drawing.Size(72, 17);
            this.radioButton_Sart_With.TabIndex = 8;
            this.radioButton_Sart_With.TabStop = true;
            this.radioButton_Sart_With.Text = "Start With";
            this.radioButton_Sart_With.UseVisualStyleBackColor = true;
            this.radioButton_Sart_With.CheckedChanged += new System.EventHandler(this.radioButton_Sart_With_CheckedChanged);
            // 
            // radioButton_InvertString
            // 
            this.radioButton_InvertString.AutoSize = true;
            this.radioButton_InvertString.Location = new System.Drawing.Point(292, 315);
            this.radioButton_InvertString.Name = "radioButton_InvertString";
            this.radioButton_InvertString.Size = new System.Drawing.Size(82, 17);
            this.radioButton_InvertString.TabIndex = 10;
            this.radioButton_InvertString.TabStop = true;
            this.radioButton_InvertString.Text = "Invert String";
            this.radioButton_InvertString.UseVisualStyleBackColor = true;
            this.radioButton_InvertString.CheckedChanged += new System.EventHandler(this.radioButton_InvertString_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.radioButton_InvertString);
            this.Controls.Add(this.radioButton_EndsWith);
            this.Controls.Add(this.radioButton_Sart_With);
            this.Controls.Add(this.radioButton_Compare);
            this.Controls.Add(this.radioButton_Copy);
            this.Controls.Add(this.lb_Result);
            this.Controls.Add(this.btn_run);
            this.Controls.Add(this.lb_output);
            this.Controls.Add(this.lb_youre_string);
            this.Controls.Add(this.Input_String);
            this.Name = "Form1";
            this.Text = "CONSTRUTOR DE STRING";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Input_String;
        private System.Windows.Forms.Label lb_youre_string;
        private System.Windows.Forms.Label lb_output;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.Label lb_Result;
        private System.Windows.Forms.RadioButton radioButton_Copy;
        private System.Windows.Forms.RadioButton radioButton_Compare;
        private System.Windows.Forms.RadioButton radioButton_EndsWith;
        private System.Windows.Forms.RadioButton radioButton_Sart_With;
        private System.Windows.Forms.RadioButton radioButton_InvertString;
    }
}

