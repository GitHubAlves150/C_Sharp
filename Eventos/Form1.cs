using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eventos
{
    public partial class Form1 : Form
    {
        private int counterErros = 3;
        public Form1()
        {
            InitializeComponent();
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            textBox2.Text ="" ;
            
        }

        private void Confirm_BTN_Click(object sender, EventArgs e)
        {
            string password = "12345";
            string userpassWord = textBox2.Text;
            if (password == userpassWord)
            {
                Outputlabel.Text = "Authorized";
                Outputlabel.ForeColor = Color.Green;
                Confirm_BTN.ForeColor = Color.Green;


            }
            else 
            {

                Outputlabel.Text = "NO Authorized";
                Confirm_BTN.ForeColor = Color.Red;
                Outputlabel.ForeColor = Color.Red;
                --counterErros;
                CounterLabel.Text= counterErros.ToString();
                if(counterErros==0)
                {
                    MessageBox.Show("Tente mais tarde");
                    Close();
                }

            }

        }
    }
}
