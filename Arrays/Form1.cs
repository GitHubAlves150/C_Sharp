using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arrays
{       
        
    public partial class Form1 : Form
    {   
        
        private int tamanho = 20;
        private string[] array = new string[20];
        private int i = 0;
      
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            array.ToString();
            if(textBox1.Text == "")
            {
                label_Info.Text="Empty";
                Console.WriteLine("ok");
            }
            else
            {
                array[i] = textBox1.Text;
                textBox1.Text= "";
                Console.WriteLine("Array0: " + array[0]);
                Console.WriteLine("Array1: " + array[1]);
                Console.WriteLine("Array2: " + array[2]);
                i++;
            }
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
           
            
            textBox2.Text = Convert.ToString(array[0] + ", "+ array[1] + ", " + array[2] + ", " + array[3] + ", " + array[4] + array[5] + ", " + array[6] );
            Console.WriteLine("\n----Array: " + array[i]+" =="+ i);


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
            
        }
    }
}
