using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TratramentoExecoes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            double grade1 = double.Parse(inputGradeBox1.Text.Replace(".", ","));
            double grade2 = double.Parse(inputGradeBox2.Text.Replace(".", ","));
            double grade3 = double.Parse(inputGradeBox3.Text.Replace(".", ","));

            grade1 = IsNegative(grade1);
            grade2 = IsNegative(grade2);
            grade3 = IsNegative(grade3);


            double Weight1 = double.Parse(inputWeightBox1.Text.Replace(".", ","));
            double Weight2 = double.Parse(inputWeightBox2.Text.Replace(".", ","));
            double Weight3 = double.Parse(inputWeightBox3.Text.Replace(".", ","));

            Weight1= IsNegative(Weight1);
            Weight2 = IsNegative(Weight2);
            Weight3 = IsNegative(Weight3);

                //calculate
                double weightedAverage = (grade1 * Weight1 +
                                      grade2 * Weight2 +
                                      grade3 * Weight3 / (Weight1 + Weight2 + Weight3) );

            

            ResultOutPuttext.Text = "Resultado:" + weightedAverage.ToString();
            }
            catch(FormatException)
            {
                MessageBox.Show("You must enter a double value!,"+ "invalid Number Format"+MessageBoxButtons.OK +MessageBoxIcon.Error);
            }
            catch(NegativeNumberExeception error)
            {
                MessageBox.Show(error.Message + "...negative number..." + MessageBoxButtons.OK + MessageBoxIcon.Error);

            }
            


        }//END BUTTON

        private double IsNegative(double userValue)
        {
            if(userValue <0)
            {
                throw new NegativeNumberExeception("Is negative number");
            }
            return userValue;
        }//end IsNegative

    }//END CLASSFORMA
}
