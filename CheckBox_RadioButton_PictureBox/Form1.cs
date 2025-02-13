using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckBox_RadioButton_PictureBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void StrikeOut_CB_CheckedChanged(object sender, EventArgs e)
        {
            Robots.Font = new Font(Robots.Font.Name, Robots.Font.Size, Robots.Font.Style ^ FontStyle.Strikeout);
        }

        private void Bold_CB_CheckedChanged(object sender, EventArgs e)
        {
            Robots.Font = new Font(Robots.Font.Name, Robots.Font.Size, Robots.Font.Style ^ FontStyle.Bold);

        }

        private void Italian_CB_CheckedChanged(object sender, EventArgs e)
        {
            Robots.Font = new Font(Robots.Font.Name, Robots.Font.Size, Robots.Font.Style ^ FontStyle.Italic);

        }

        private void UnderLine_CB_CheckedChanged(object sender, EventArgs e)
        {
            Robots.Font = new Font(Robots.Font.Name, Robots.Font.Size, Robots.Font.Style ^ FontStyle.Underline);
            Robots.Text = "Lucas";
        }
    }
}
