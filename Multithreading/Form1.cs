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


namespace Multithreading
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CalculateFibonacci(int number, TextBox output, Label status)            
        {
            int fibonnaci = Fibonnaci(number);

            this.Invoke( (MethodInvoker)delegate
            {
                output.Text = fibonnaci.ToString();
                status.Text = "";
            });
        }


        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                label1.Text = "Calulate Please Wait..";
                label1.Refresh();
                int number = Convert.ToInt32(textBox1.Text);//converto umint simples para int32
                Thread thread1 = new Thread(()=>CalculateFibonacci(number, textBox_Resultado, label1) );
                thread1.Start();

                //int fiboNum = Fibonnaci(number);
                //textBox_Resultado.Text = fiboNum.ToString();//convertido para string
                //label1.Text = "";
            }
            catch(FormatException err)
            {
                MessageBox.Show("btn 1"+err.Message);
            }
                
        }

        private int Fibonnaci(int num)
        {
            if(num ==0 || num==1)            
                return num;            
            else            
                return Fibonnaci(num-1)+Fibonnaci(num-2);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                label2.Text = "Calulate Please Wait..";
                label2.Refresh();
                int number = Convert.ToInt32(textBox4.Text);//converto umint simples para int32
                Thread thread2 = new Thread(() => CalculateFibonacci(number, textBox2_Resultado, label2));
                thread2.Start();

                //int fiboNum = Fibonnaci(number);
                //textBox_Resultado.Text = fiboNum.ToString();//convertido para string
                //label1.Text = "";
            }
            catch (FormatException err)
            {
                MessageBox.Show("btn 1" + err.Message);
            }
        }
    }
    }

