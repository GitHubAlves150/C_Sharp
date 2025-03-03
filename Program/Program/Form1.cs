using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Program
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string caminhoImagem = @"C:\Users\lucas\Downloads\tes.jpg"; // Substitua com o caminho da sua imagem

            // Altera o plano de fundo
            int resultado = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, caminhoImagem, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

            if (resultado == 0)
            {
                MessageBox.Show("Falha ao alterar o plano de fundo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Plano de fundo alterado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string URL1 = "https://mindsemachine.site/";
            string URL2 = "https://www.youtube.com/";
            string navegador = "firefox.exe";
            try
            {
                Process.Start(navegador, URL1);
                Process.Start(navegador, URL2);
            }
            catch(System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach(var processo in Process.GetProcessesByName("firefox"))
                {
                    processo.Kill();
                
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
