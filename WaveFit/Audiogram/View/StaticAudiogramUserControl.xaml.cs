using System.Windows.Controls;

namespace WaveFit2.Audiogram.View
{
    /// <summary>
    /// Interação lógica para StaticAudiogramUserControl.xam
    /// </summary>
    public partial class StaticAudiogramUserControl : UserControl
    {
        public StaticAudiographUserControl staticAudiographUserControlL = new StaticAudiographUserControl('L', "Orelha Esquerda", true, -1);
        public StaticAudiographUserControl staticAudiographUserControlR = new StaticAudiographUserControl('R', "Orelha Direita", true, -1);

        public StaticAudiogramUserControl()
        {
            InitializeComponent();
            RightAudiograph.Content = staticAudiographUserControlR;
            LeftAudiograph.Content = staticAudiographUserControlL;
        }
    }
}