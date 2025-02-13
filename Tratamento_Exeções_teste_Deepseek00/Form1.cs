using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tratamento_Exeções_teste_Deepseek00
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }


        

        private void button1_Calcular_Click(object sender, EventArgs e)
        {
            try
            {
                int solar = int.Parse(textBox1_Solar.Text);
                int planeta = int.Parse(textBox2_Planetaria.Text);
                int anelar= solar + (2*planeta);
                label1.Text = anelar.ToString();
            }
            catch(FormatException)
            {
                MessageBox.Show("Erro: number not valide");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:.." + ex.Message);

            }
            
            finally
            {
                MessageBox.Show("Finalizado....");
            }

        }

    }
}
