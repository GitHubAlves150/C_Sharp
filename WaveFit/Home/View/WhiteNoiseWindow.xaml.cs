using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WaveFit2.Home.View
{
    /// <summary>
    /// Lógica interna para WhiteNoiseWindow.xaml
    /// </summary>
    public partial class WhiteNoiseWindow : Window
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private string soundFilePath = @"Resources\Ruido Branco 15min.wav";

        public WhiteNoiseWindow()
        {
            InitializeComponent();
            InitializeMediaPlayer();
            play.Click += Play_Click;
            pause.Click += Pause_Click;
            close.Click += Close_Click;
        }

        private void InitializeMediaPlayer()
        {
            mediaPlayer.Open(new Uri(soundFilePath));
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
            mediaPlayer.Close();
            Close();
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double maxVolume = 10.0;
            double minVolume = 0.0;
            double volume = minVolume + (maxVolume - minVolume) * e.NewValue / 100.0;

            mediaPlayer.Volume = volume;
        }

        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}