using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tipos_de_excecoes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void calcular_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int numerador = int.Parse(textBox_Numerador.Text);
                int denominador = int.Parse(textBox_Denominador.Text);
                int resultado = numerador / denominador;

                result.Text = resultado.ToString();
            }
            catch (DivideByZeroException error)
            {
                MessageBox.Show("OPA!!!!   " + error.Message);
            }
            catch (FormatException error)
            {
                MessageBox.Show("OPA!...." + error.Message);
            }
            finally
            {
                MessageBox.Show("Finalizado");
            }


        }

        private void GerarOverFlow_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int number = int.Parse(numberInt_textBox.Text);
                numberInt_label.Text = number.ToString();
                numberInt_textBox.Text = "";
            }
            catch (OverflowException error)
            {
                MessageBox.Show("Estouro: " + error.Message);
            }
            catch (FormatException error)
            {
                MessageBox.Show("OPA!...." + error.Message);
            }
            finally
            {
                MessageBox.Show("Finalizado");
            }

        }

        private void gerar_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int number = int.Parse(Input_TextBox.Text);
                resulta_LB.Text = number.ToString();
            }
            catch (Exception er)
            {
                MessageBox.Show("opsss..." + er.Message);
            }


        }
        public void numero_negativo(string msn)
        {
            try { }
            catch { MessageBox.Show(msn); }


        }
        private void Rodar_btn_Click(object sender, EventArgs e)
        {
           
                int numberPositivo = int.Parse(textBox1.Text);
                if(numberPositivo < 0)
                {
                    try
                    {
                        throw new numeroNegativo("negativo cara!!");
                    }
                    catch(Exception err)
                    {

                        MessageBox.Show("erro..."+err.Message);
                        textBox1.Text= "";
                    }
                    result_label.Text = "...";
                } 
                else
                {
                    result_label.Text = numberPositivo.ToString();
                }
                
        }

       
    }
}
