using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Async_Await
{ 
    
    public partial class Form1 : Form
    {
        bool isAsync = false;
        public Form1()
        {
            InitializeComponent();
            UpdateCalculateButtonClickhandler();
        }

        private async void UpdateCalculateButtonClickhandler()
        {
            button_Calc.Click -= CalculateprimeSync;//Remove uma alteração por isso o -=
            button_Calc.Click -= async(sender, e) => await CalculateprimeASync();

            if(isAsync)
            {
                button_Calc.Click += async(sender, e) => await CalculateprimeASync();
            }
            else
            {
                button_Calc.Click += CalculateprimeSync;
            }


        }//end UpdateCalculateButtonClickhandler

        private void CalculateprimeSync(object sender, EventArgs e)
        {
            button_Calc.Enabled = false;
            int maxNumber = (int)numericUpDown1.Value;
            textBox1.Text = "PROCESSING.....";

            List<int> primes = GetPrimes(maxNumber);

            textBox1.Text = string.Join(", ", primes);
            button_Calc.Enabled = true;
        }

        private async Task CalculateprimeASync()
        {
            button_Calc.Enabled = false;
            int maxNumber = (int)numericUpDown1.Value;
            textBox1.Text = "PROCESSING....";

            List<int> primes = await Task.Run(() => GetPrimes(maxNumber));

            textBox1.Text = string.Join(", ", primes);
            button_Calc.Enabled = true;

        }//end CalculateprimeASync


        private List<int> GetPrimes(int maxNumber)
        {
            var primes = new List<int>();
            for(int number = 2; number <maxNumber; number++)
            {
                bool isPrime = true;
                for(int i =2; i<=Math.Sqrt(number); i++)
                {
                    if(number % i ==0)
                    {
                        isPrime = false;
                        break;
                    }//end if
                }//end internal for
                if(isPrime)
                {
                    primes.Add(number);
                }
            }//end for
            return primes;
        }

        private void checkBox_checkBox_Async_Sync_CheckedChanged(object sender, EventArgs e)
        {
            isAsync = !isAsync;
            label_Synchronous.Text = isAsync ? "Asynchronous" : "Synchronous";
            UpdateCalculateButtonClickhandler();
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
    }
}
