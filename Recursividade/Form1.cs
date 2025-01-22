using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recursividade
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


    private int Fibonacci(int number)
    {
            if(number ==0 || number==1)
            {
                return number;
            }
            else
            {
                return Fibonacci( number - 1) + Fibonacci(number -2);
            }

    }

    private void Calculate_Click(object sender, EventArgs e)
    {
            label6.Text = "Calculating Please Wait";
            label6.Refresh();
            int number = Convert.ToInt32(textBox3.Text);
            int fibonacci = Fibonacci(number);
            textBox4.Text = fibonacci.ToString();
            label6.Text = "";
    }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }

}








