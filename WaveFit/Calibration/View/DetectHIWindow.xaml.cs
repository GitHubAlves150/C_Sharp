using System;
using System.Threading.Tasks;
using System.Windows;
using WaveFit2.Calibration.ViewModel;

namespace WaveFit2.Calibration.View
{
    /// <summary>
    /// Lógica interna para DetectHIWindow.xaml
    /// </summary>
    public partial class DetectHIWindow : Window
    {
        public IntriconViewModel intriconViewModel = new IntriconViewModel();
        public int counter = 0;
        public string lado;

        public DetectHIWindow()
        {
            InitializeComponent();
            CleanMessages();
            DetectHIWindowToolsActions();

            Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith((task) =>
            {
                Dispatcher.Invoke(() => NextSetp());
            });
        }

        public void CleanMessages()
        {
            HandyControl.Controls.Growl.Clear();
            HandyControl.Controls.Growl.ClearGlobal();
        }

        public void DetectHIWindowToolsActions()
        {
            CurrentStep.StepChanged += CurrentStep_StepChanged;
        }

        private void CurrentStep_StepChanged(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            if (CurrentStep.StepIndex == 1)
            {
                Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith((task) =>
                {
                    Dispatcher.Invoke(() => intriconViewModel.InitializeProgramer());

                    if (intriconViewModel.detectProgrammer == 0)
                    {
                        Dispatcher.Invoke(() => Close());
                    }
                    else
                    {
                        Dispatcher.Invoke(() => NextSetp());
                    }
                });
            }
            else if (CurrentStep.StepIndex == 2)
            {
                Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith((task) =>
                {
                    Dispatcher.Invoke(() => intriconViewModel.StartProgrammer('R'));
                    Dispatcher.Invoke(() => NextSetp());
                });
            }
            else if (CurrentStep.StepIndex == 3)
            {
                Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith((task) =>
                {
                    Dispatcher.Invoke(() => intriconViewModel.StartProgrammer('L'));
                    Dispatcher.Invoke(() => NextSetp());
                });
            }
            else if (CurrentStep.StepIndex == 4)
            {
                Task.Delay(TimeSpan.FromSeconds(3)).ContinueWith((task) =>
                {
                    Dispatcher.Invoke(() => CheckDetect());
                    Dispatcher.Invoke(() => intriconViewModel.CloseProgramer());
                    Dispatcher.Invoke(() => NextSetp());

                    //if (counter == 0)
                    //{
                    //    Dispatcher.Invoke(() => Close());
                    //}
                    //else if (counter == 1)
                    //{
                    //    HandyControl.Controls.Growl.AskGlobal($"Somente foi detectado aparelho no lado {lado}, deseja continuar?", isConfirmed =>
                    //    {
                    //        if (isConfirmed)
                    //        {
                    //            Dispatcher.Invoke(() => NextSetp());
                    //        }
                    //        else
                    //        {
                    //            Dispatcher.Invoke(() => Close());
                    //        }
                    //        return true;
                    //    });

                    //    //HandyControl.Controls.MessageBox.Show(new MessageBoxInfo
                    //    //{
                    //    //    Message = $"Somente foi detectado aparelho no lado {lado}, deseja continuar?",
                    //    //    Caption = "Continuar",
                    //    //    Button = MessageBoxButton.YesNoCancel,
                    //    //    IconBrushKey = ResourceToken.AccentBrush,
                    //    //    IconKey = ResourceToken.AskGeometry,
                    //    //    StyleKey = "MessageBoxCustom"
                    //    //});
                    //}
                    //else
                    //{
                    //    Dispatcher.Invoke(() => NextSetp());
                    //}
                });
            }
            else if (CurrentStep.StepIndex == 5)
            {
                Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith((task) =>
                {
                    Dispatcher.Invoke(() => NextSetp());
                });
            }
            else if (CurrentStep.StepIndex == 6)
            {
                Task.Delay(TimeSpan.FromSeconds(3)).ContinueWith((task) =>
                {
                    Dispatcher.Invoke(() => Close());
                });
            }
        }

        public void NextSetp()
        {
            CurrentStep.StepIndex = CurrentStep.StepIndex + 1;
        }

        public void CheckDetect()
        {
            if (intriconViewModel.detectProgrammer == 0 || (intriconViewModel.detectedHI[0] == 0 && intriconViewModel.detectedHI[1] == 0))
            {
                Properties.Settings.Default.continuedToCalibrarion = false;
            }
            else
            {
                counter = intriconViewModel.detectedHI[0] + intriconViewModel.detectedHI[1];
                if (counter == 1)
                {
                    if (intriconViewModel.detectedHI[0] == 1)
                    {
                        lado = "esquerdo";
                    }
                    else
                    {
                        lado = "direito";
                    }
                }
                Properties.Settings.Default.continuedToCalibrarion = true;
                HandyControl.Controls.Growl.Clear();
                HandyControl.Controls.Growl.ClearGlobal();
            }
        }
    }
}