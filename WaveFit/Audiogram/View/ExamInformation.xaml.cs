using System.Windows.Controls;
using System.Windows.Media;

namespace WaveFit2.Audiogram.View
{
    /// <summary>
    /// Lógica interna para ExamInformation.xaml
    /// </summary>
    public partial class ExamInformation : UserControl
    {
        public MeatoscopyUserControl meatoscopyUserControl = new MeatoscopyUserControl();
        public LogoUserControl logoUserControl = new LogoUserControl();
        public MaskUserControl maskUserControl = new MaskUserControl();
        public ObsUserControl obsUserControl = new ObsUserControl();

        public ExamInformation()
        {
            InitializeComponent();
            Meatoscopy.Content = meatoscopyUserControl;
            Logo.Content = logoUserControl;
            Mask.Content = maskUserControl;
            Observation.Content = obsUserControl;
        }

        public void MeatoscopyStep()
        {
            Meatoscopy.IsEnabled = true;
            meatoscopyUserControl.EnableLock.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#014F56"));
            Logo.IsEnabled = false;
            logoUserControl.EnableLock.Background = Brushes.LightGray;
            Mask.IsEnabled = false;
            maskUserControl.EnableLock.Background = Brushes.LightGray;
            Observation.IsEnabled = false;
            obsUserControl.EnableLock.Background = Brushes.LightGray;

            logoUserControl.Title.Background = Brushes.LightGray;
            obsUserControl.Title.Background = Brushes.LightGray;
            maskUserControl.Title.Background = Brushes.LightGray;
            meatoscopyUserControl.Title.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009CA3"));
        }

        public void LogoStep()
        {
            Meatoscopy.IsEnabled = false;
            meatoscopyUserControl.EnableLock.Background = Brushes.LightGray;
            Logo.IsEnabled = true;
            logoUserControl.EnableLock.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#014F56"));
            Mask.IsEnabled = false;
            maskUserControl.EnableLock.Background = Brushes.LightGray;
            Observation.IsEnabled = false;
            obsUserControl.EnableLock.Background = Brushes.LightGray;

            logoUserControl.Title.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009CA3"));
            obsUserControl.Title.Background = Brushes.LightGray;
            maskUserControl.Title.Background = Brushes.LightGray;
            meatoscopyUserControl.Title.Background = Brushes.LightGray;
        }

        public void MaskStep()
        {
            Meatoscopy.IsEnabled = false;
            meatoscopyUserControl.EnableLock.Background = Brushes.LightGray;
            Logo.IsEnabled = false;
            logoUserControl.EnableLock.Background = Brushes.LightGray;
            Mask.IsEnabled = true;
            maskUserControl.EnableLock.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#014F56"));
            Observation.IsEnabled = false;
            obsUserControl.EnableLock.Background = Brushes.LightGray;

            logoUserControl.Title.Background = Brushes.LightGray;
            obsUserControl.Title.Background = Brushes.LightGray;
            maskUserControl.Title.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009CA3"));
            meatoscopyUserControl.Title.Background = Brushes.LightGray;
        }

        public void ObsStep()
        {
            Meatoscopy.IsEnabled = false;
            meatoscopyUserControl.EnableLock.Background = Brushes.LightGray;
            Logo.IsEnabled = false;
            logoUserControl.EnableLock.Background = Brushes.LightGray;
            Mask.IsEnabled = false;
            maskUserControl.EnableLock.Background = Brushes.LightGray;
            Observation.IsEnabled = true;
            obsUserControl.EnableLock.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#014F56"));

            logoUserControl.Title.Background = Brushes.LightGray;
            obsUserControl.Title.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009CA3"));
            maskUserControl.Title.Background = Brushes.LightGray;
            meatoscopyUserControl.Title.Background = Brushes.LightGray;
        }

        public void FinalStep()
        {
            Meatoscopy.IsEnabled = true;
            meatoscopyUserControl.EnableLock.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#014F56"));
            Logo.IsEnabled = true;
            logoUserControl.EnableLock.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#014F56"));
            Mask.IsEnabled = true;
            maskUserControl.EnableLock.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#014F56"));
            Observation.IsEnabled = true;
            obsUserControl.EnableLock.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#014F56"));

            logoUserControl.Title.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009CA3"));
            obsUserControl.Title.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009CA3"));
            maskUserControl.Title.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009CA3"));
            meatoscopyUserControl.Title.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009CA3"));
        }
    }
}