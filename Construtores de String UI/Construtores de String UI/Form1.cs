using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace Construtores_de_String_UI
{
    public partial class Form1: Form
    {
        short option = 0;
        public Form1()
        {
            InitializeComponent();
            radioButton_Copy.Checked = true;
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            string input = Input_String.Text;//Entra com string de entrada
            lb_output.Text = $"Length: {input.Length}";
            switch(option)
            {
                case 0:
                    char[] destination = new char[8];
                    if (input.Length >= 8)
                    {
                        input.CopyTo(0, destination, 0, 8);//do indice zero ao cinco apartir do zero do destination
                        lb_Result.Text = $"CopyTo {new string(destination)}";//Exibo os resultados
                    }
                    else
                    {
                        lb_Result.Text = "CopyTo: Input too short";
                    }
                        break;
                case 1:
                    string compareString = "example";
                    int comparisonResult = input.CompareTo(compareString);
                    lb_Result.Text = $"CompareTo: {comparisonResult} (comparado com \"{compareString}\")";

                    break;
                case 2:
                    lb_Result.Text = input.StartsWith("test") ? "StartsWith: True" : "StartWith: False";
                    break;
                case 3:
                    lb_Result.Text = input.EndsWith("end") ? "EndsWith: True" : "EndsWith: False";
                    break;
                case 4:
                    lb_Result.Text = "";
                    for (int i=input.Length -1; i>=0; i--)
                    {
                        lb_Result.Text += input[i];
                    }
                    break;
                default:
                    break;


            }//end switch

           

        }//end btn

        private void radioButton_Copy_CheckedChanged(object sender, EventArgs e)
        {
            option = 0;
        }

        private void radioButton_Compare_CheckedChanged(object sender, EventArgs e)
        {
            option = 1;
        }

        private void radioButton_Sart_With_CheckedChanged(object sender, EventArgs e)
        {
            option = 2;
        }

        private void radioButton_EndsWith_CheckedChanged(object sender, EventArgs e)
        {
            option = 3;
        }

        private void radioButton_InvertString_CheckedChanged(object sender, EventArgs e)
        {
            option = 4;
        }
    }//end class form


}//end namespace
