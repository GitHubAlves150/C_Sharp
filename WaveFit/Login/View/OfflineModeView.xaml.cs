using System;
using System.Threading.Tasks;
using System.Windows;

namespace WaveFit2.Login.View
{
    public partial class OfflineModeView : Window
    {
        public OfflineModeView()
        {
            InitializeComponent();
            OfflineModeTools();
        }

        public void OfflineModeTools()
        {
            Reconnect.Click += Reconnect_Click;
            Cancel.Click += Cancel_Click;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Reconnect_Click(object sender, RoutedEventArgs e)
        {
            if (!Properties.Settings.Default.statusConnection)
            {
                HandyControl.Controls.Growl.Clear();
                HandyControl.Controls.Growl.ErrorGlobal("Não foi possível conectar a rede.");
            }
            else
            {
                HandyControl.Controls.Growl.Success("Rede conectada com sucesso.");
                Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith((task) =>
                {
                    Dispatcher.Invoke(() => Close());
                });
            }
        }
    }
}