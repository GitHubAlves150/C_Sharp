﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Indexadores_e_Membros_const_readonly
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdCalcADC newCalc = new AdCalcADC(double.Parse(textBox1.Text));
            MessageBox.Show("ADC Value: " + newCalc.AdcValue().ToString(), "ADC Value");
        }
    }
}
