using System.Windows;
using System.Windows.Controls;

namespace WaveFit2.Audiogram.View
{
    /// <summary>
    /// Interação lógica para PrintTemplateUserControl.xam
    /// </summary>
    public partial class PrintTemplateUserControl : UserControl
    {
        public StaticAudiographUserControl staticAudiographUserControlL = new StaticAudiographUserControl('L', "Orelha Esquerda", true, -1);
        public StaticAudiographUserControl staticAudiographUserControlR = new StaticAudiographUserControl('R', "Orelha Direita", true, -1);

        public PrintTemplateUserControl(int height, int width)
        {
            InitializeComponent();
            AudiogramD.Content = staticAudiographUserControlR.CreateAudiogramImage(height, width);
            AudiogramE.Content = staticAudiographUserControlL.CreateAudiogramImage(height, width);
        }
    }
}