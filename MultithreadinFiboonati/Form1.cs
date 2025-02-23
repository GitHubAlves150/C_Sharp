using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace MultithreadinFiboonati
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Calculate_Click(object sender, EventArgs e)
        {       
                label_processing.Visible = true;
                label_processing.Text = "Calculate, please wait...";
                label_processing.Refresh();
            try
            {
                
                int number = Convert.ToInt32(textBox_EnterInteger.Text);

                Thread thread1 = new Thread(  ()=>CalculateFibanacci(number, textBox_Fibonacci, label_processing)   );
                thread1.Start();
            }
            catch (FormatException )
            {
                MessageBox.Show("Error");

            }

        }


        private int Fibanacci(int number)
        {
            if (number == 0 || number == 1)
            {
                return number;
            }
            else
            {
                return Fibanacci(number - 1) + Fibanacci(number - 2);
            }
        }//end Fibonacci

        private void button1_Calculate_Click(object sender, EventArgs e)
        {
            label1_processing.Visible = true;
            label1_processing.Text = "Calculate, please wait...";
            label1_processing.Refresh();
           
            try
            {
                int number = Convert.ToInt32(textBox2_EnterInteger.Text);
                Thread threadi2 = new Thread( ()=>CalculateFibanacci(number, textBox1_Fibonacci, label1_processing)  );
                threadi2.Start();
            }
            catch(FormatException)
            {
                MessageBox.Show("Erro de formato");

            }
        }



        //Threading
        private void CalculateFibanacci(int number, TextBox output, Label statuslabel)
        {
            int fibNumber = Fibanacci(number);
            this.Invoke( (MethodInvoker )delegate
            {
                output.Text = fibNumber.ToString();
                statuslabel.Text = "";

            });
        }
    }
}
