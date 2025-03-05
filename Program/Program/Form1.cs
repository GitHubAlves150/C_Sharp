using System;
using System.Windows.Forms;//Importa classes para criar aplicativos baseados em interface gráfica usando Windows Forms.
using System.Runtime.InteropServices; //Permite interoperabilidade com código nativo (não gerenciado) usando P/Invoke.
using System.Diagnostics;//Fornece classes para interagir com processos, depuração e rastreamento.
using System.Runtime.CompilerServices;
using System.IO;//Contém funcionalidades avançadas para compiladores e metadados.

namespace Program
{
    public partial class Form1 : Form
    {

        //Importa a função SystemParametersInfo da biblioteca nativa user32.dll, usada para manipular configurações do sistema.
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        public Form1()
        {
            InitializeComponent();
        }
        //------------------iNGLES-----------------------------
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (var processo in Process.GetProcessesByName("firefox"))
                {
                    processo.Kill();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string URL1 = "https://www.youtube.com/watch?v=XbL9_FDaVYU&list=PL7BDB07039775D0A6";
            string URL2 = "file:///C:/Users/lucas/Documents/GitHub_gmail/index-HTML/Recursos%20de%20aprendizado%20em%20ingles.html";
            string URL3 = "https://youglish.com/";
            string URL4 = "https://www.youtube.com/watch?v=E6KXA1mVBBE";
            string URL5 = "https://youglish.com/pronounce/i_have/english#google_vignette";

            string navegador = "firefox.exe";
            try
            {
                string caminhoImagem = @"C:\Users\lucas\Documents\GitHub_gmail\index-HTML\EUA.jpg"; // Substitua com o caminho da sua imagem

                // Altera o plano de fundo
                int resultado = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, caminhoImagem, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

                if (resultado == 0)
                {
                    MessageBox.Show("Falha ao alterar o plano de fundo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Area de trabalho alterado!", "Inglês");
                }
                Process.Start(navegador, URL1);
                Process.Start(navegador, URL2);
                Process.Start(navegador, URL3);
                Process.Start(navegador, URL4);
                Process.Start(navegador, URL5);
            }
            catch(System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }// fIM iNGLES


        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                foreach (var processo in Process.GetProcessesByName("firefox"))
                {
                    processo.Kill();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string URL = "";
            
            string navegador = "firefox.exe";
            try
            {
                string caminhoImagem = @"C:\Users\lucas\Documents\GitHub_gmail\index-HTML\WALLPAPER.jpg"; // Substitua com o caminho da sua imagem

                // Altera o plano de fundo
                int resultado = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, caminhoImagem, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

                if (resultado == 0)
                {
                    MessageBox.Show("Falha ao alterar o plano de fundo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Area de trabalho alterado!", "ENTRETERIMENTO");
                }
                Process.Start(navegador, URL);
                
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        //----------------pYTHON------------------------

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

            try
            {
                foreach (var processo in Process.GetProcessesByName("firefox"))
                {
                    processo.Kill();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string URL1 = "file:///C:/Users/lucas/Documents/GitHub_gmail/index-HTML/indice%20Pyhton.html";
            string URL2 = "file:///C:/Users/lucas/Documents/GitHub_gmail/index-HTML/Curso_Intensivo_de_Python_Uma_Eric_Matth.pdf";
            string URL3 = "https://www.youtube.com/watch?v=Ay-MakuSg08&list=PLx4x_zx8csUhuVgWfy7keQQAy7t1J35TR";
            string URL4 = "https://www.youtube.com/watch?v=-VeVq64Fgw0";



            string navegador = "firefox.exe";
            try
            {
                string caminhoImagem = @"C:\Users\lucas\Documents\GitHub_gmail\index-HTML\python.png"; // Substitua com o caminho da sua imagem

                // Altera o plano de fundo
                int resultado = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, caminhoImagem, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

                if (resultado == 0)
                {
                    MessageBox.Show("Falha ao alterar o plano de fundo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Area de trabalho alterado!", "PYTHON");
                }
                Process.Start(navegador, URL1);
                Process.Start(navegador, URL2);
                Process.Start(navegador, URL3);
                Process.Start(navegador, URL4);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //FIM PYTHON



        //----------------Programação C Sharp-------------------------------------
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (var processo in Process.GetProcessesByName("firefox"))
                {
                    processo.Kill();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string URL1 = "https://hotmart.com/pt-br/club/wrkits/products/3931716";
            



            string navegador = "firefox.exe";
            try
            {
                string caminhoImagem = @"C:\Users\lucas\Documents\GitHub_gmail\index-HTML\csharp.png"; // Substitua com o caminho da sua imagem

                // Altera o plano de fundo
                int resultado = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, caminhoImagem, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

                if (resultado == 0)
                {
                    MessageBox.Show("Falha ao alterar o plano de fundo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Area de trabalho alterado!", "C SHARP");
                }
                Process.Start(navegador, URL1);
               
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }//------------Fim programação CSharp-------------------


        //-------------uNISUL___________________________________
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (var processo in Process.GetProcessesByName("firefox"))
                {
                    processo.Kill();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string URL1 = "https://estudantesunisul.ead.br/";
            string URL2 = "file:///C:/Users/lucas/Documents/GitHub_gmail/index-HTML/Livro_Computacao_Pesquisa%20e%20Ordenacao%20de%20Dados.pdf";




            string navegador = "firefox.exe";
            try
            {
                string caminhoImagem = @"C:\Users\lucas\Documents\GitHub_gmail\index-HTML\computacao.jpg"; // Substitua com o caminho da sua imagem

                // Altera o plano de fundo
                int resultado = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, caminhoImagem, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

                if (resultado == 0)
                {
                    MessageBox.Show("Falha ao alterar o plano de fundo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Area de trabalho alterado!", "UNISUL");
                }
                Process.Start(navegador, URL1);
                Process.Start(navegador, URL2);

            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //---------------------------fIM UNISUL--------------------------

        //---------------------------jAVAsCRIPT--------------------------
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (var processo in Process.GetProcessesByName("firefox"))
                {
                    processo.Kill();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string URL1 = "file:///C:/Users/lucas/Documents/GitHub_gmail/index-HTML/JavaScript_%20O%20guia%20definitivo%20(%20PDFDrive%20).pdf";
            string URL2 = "https://www.youtube.com/watch?v=vEwPnjqWQ-g&list=PL2Fdisxwzt_d590u3uad46W-kHA0PTjjw";
            string URL3 = "https://www.youtube.com/watch?v=E4DBTqgxHGM&list=PLx4x_zx8csUg_AxxbVWHEyAJ6cBdsYc0T";



            string navegador = "firefox.exe";
            try
            {
                string caminhoImagem = @"C:\Users\lucas\Documents\GitHub_gmail\index-HTML\JS.jpg"; // Substitua com o caminho da sua imagem

                // Altera o plano de fundo
                int resultado = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, caminhoImagem, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

                if (resultado == 0)
                {
                    MessageBox.Show("Falha ao alterar o plano de fundo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Area de trabalho alterado!", "JAVASCRIPT");
                }
                Process.Start(navegador, URL1);
                Process.Start(navegador, URL2);
                Process.Start(navegador, URL3);


            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //---------------------------Fim JAVAsCRIPT-------------------------

        //--------------------------BANCO DE DADOS__--------------------------
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (var processo in Process.GetProcessesByName("firefox"))
                {
                    processo.Kill();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string URL1 = "file:///C:/Users/lucas/Documents/GitHub_gmail/index-HTML/Sistemas%20de%20Banco%20de%20Dados%20(Ramez%20Elmasri,%20Shamkant%20B.%20Navathe).pdf";
            string URL2 = "https://www.youtube.com/watch?v=pmAxIs5U1KI&list=PLxI8Can9yAHeHQr2McJ01e-ANyh3K0Lfq";
            string URL3 = "https://www.youtube.com/watch?v=Q_KTYFgvu1s&list=PLucm8g_ezqNoNHU8tjVeHmRGBFnjDIlxD";
            string URL4 = "https://www.youtube.com/watch?v=dpanYy8IrcU&t=498s";
            String URL5 = "https://www.youtube.com/watch?v=SEnnucNP1h0&t=485s";
            string URL6 = "https://www.youtube.com/watch?v=SLDNQrJOX78&list=PL3JRjVnXiTBZ2EMkni7hyqdr-JChtVIwB";

            string navegador = "firefox.exe";
            try
            {
                string caminhoImagem = @"C:\Users\lucas\Documents\GitHub_gmail\index-HTML\BD.png"; // Substitua com o caminho da sua imagem

                // Altera o plano de fundo
                int resultado = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, caminhoImagem, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

                if (resultado == 0)
                {
                    MessageBox.Show("Falha ao alterar o plano de fundo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Area de trabalho alterado!", "BANCO DE DADOS");
                }
                Process.Start(navegador, URL1);
                Process.Start(navegador, URL2);
                Process.Start(navegador, URL3);
                Process.Start(navegador, URL4);
                Process.Start(navegador, URL5);
                Process.Start(navegador, URL6);


            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //----------------------fIM BANCO DE DADOS ---------------------

        //----------------------Internet das coisas______________

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (var processo in Process.GetProcessesByName("firefox"))
                {
                    processo.Kill();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string URL1 = "https://consumer.hotmart.com/main";
            string URL2 = "https://www.youtube.com/watch?v=drKdz02VHMg&t=2397s";



            string navegador = "firefox.exe";
            try
            {
                string caminhoImagem = @"C:\Users\lucas\Documents\GitHub_gmail\index-HTML\BD.png"; // Substitua com o caminho da sua imagem

                // Altera o plano de fundo
                int resultado = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, caminhoImagem, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

                if (resultado == 0)
                {
                    MessageBox.Show("Falha ao alterar o plano de fundo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Area de trabalho alterado!", "INTERNET DAS COISAS");
                }
                Process.Start(navegador, URL1);
                Process.Start(navegador, URL2);
              


            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //------------------------------fIM INTERNET DAS COISAS----------------------

        //--------------------------------sISTEMAS EMBARCADOS-------------------------
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

            string caminho ="";
            try
            {
                foreach (var processo in Process.GetProcessesByName("firefox"))
                {
                    processo.Kill();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           

            //string URL1 = "C:\\Users\\lucas\\Documents\\Curso STM32 Gabriel vagio";
            



            string navegador = "firefox.exe";
            try
            {
                string caminhoImagem = @"C:\Users\lucas\Documents\GitHub_gmail\index-HTML\stm32.png"; // Substitua com o caminho da sua imagem

                // Altera o plano de fundo
                int resultado = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, caminhoImagem, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

                if (resultado == 0)
                {
                    MessageBox.Show("Falha ao alterar o plano de fundo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Area de trabalho alterado!\n C:\\Users\\lucas\\Documents\\Curso STM32 Gabriel vagio", "SISTEMAS EMBARCADOS");
                }
                Process.Start(navegador, caminho);

            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
        //----------------------------------fIM sISTEMAS EMBARCADOS_____________________






    }
}



