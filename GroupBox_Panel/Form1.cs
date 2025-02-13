using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroupBox_Panel
{
    public partial class Form1 : Form
    {
        string FonteName ="Arial" ;
        float fontSize = 12f;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Arial
            FonteName = "Arial";
            YoureTextLB.Font = new Font(FonteName, fontSize);
            FonteNameLB.Text = FonteName;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Arial
            FonteName = "Tahoma";
            YoureTextLB.Font = new Font(FonteName, fontSize);
            FonteNameLB.Text = FonteName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Arial
            FonteName = "Verdana";
            YoureTextLB.Font = new Font(FonteName, fontSize);
            FonteNameLB.Text = FonteName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Arial
            FonteName = "Cambria";
            YoureTextLB.Font = new Font(FonteName, fontSize);
            FonteNameLB.Text = FonteName;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
