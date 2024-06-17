using System.Collections.Generic;
using System.Drawing;
using System.IO;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Table
{
    public class HealthCenterTable
    {
        private static byte[] ImageToByteArray(string imagePath)
        {
            // Carrega a imagem como um Bitmap
            Bitmap bitmap = new Bitmap(imagePath);

            // Define a cor de fundo como transparente (no caso, preto)
            bitmap.MakeTransparent(Color.Black);

            using (MemoryStream ms = new MemoryStream())
            {
                // Salva a imagem no MemoryStream no formato desejado (JPEG, PNG, etc.)
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                // Retorna o array de bytes
                return ms.ToArray();
            }
        }

        public static List<HealthCenterModel> HealthCenters()
        {
            return new List<HealthCenterModel>
            {
                //SP Sala 1
                new HealthCenterModel {
                                Name = "WAVETECH SOLUÇOES TECNOLOGICAS LIMITADA",
                                Nickname = "Clínica Wavetech SP",
                                Logo = ImageToByteArray(@"Resources/Logo_Wavetech.png"),
                                CNPJ = "155.658.69/0003-12",
                                Telephone = "(11)4240-5540​",
                                Status = true,
                                IdAudiometer = 1,
                                IdPlace = 1},
                //SP Sala 2
                new HealthCenterModel {
                                Name = "WAVETECH SOLUÇOES TECNOLOGICAS LIMITADA",
                                Nickname = "Clínica Wavetech SP",
                                Logo = ImageToByteArray(@"Resources/Logo_Wavetech.png"),
                                CNPJ = "155.658.69/0003-12",
                                Telephone = "(11)4240-5540​",
                                Status = true,
                                IdAudiometer = 2,
                                IdPlace = 1},
                //FL Sala 1
                new HealthCenterModel {
                                Name = "WAVETECH SOLUÇOES TECNOLOGICAS LIMITADA",
                                Nickname = "Clínica Wavetech Fl",
                                Logo = ImageToByteArray(@"Resources/Logo_Wavetech.png"),
                                CNPJ = "155.658.69/0002-31",
                                Telephone = "(48)3025-5858",
                                Status = true,
                                IdAudiometer = 3,
                                IdPlace = 2},
                //FL Sala 2
                new HealthCenterModel {
                                Name = "WAVETECH SOLUÇOES TECNOLOGICAS LIMITADA",
                                Nickname = "Clínica Wavetech Fl",
                                Logo = ImageToByteArray(@"Resources/Logo_Wavetech.png"),
                                CNPJ = "155.658.69/0002-31",
                                Telephone = "(48)3025-5858",
                                Status = true,
                                IdAudiometer = 4,
                                IdPlace = 2}
            };
        }
    }
}