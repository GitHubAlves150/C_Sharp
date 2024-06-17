using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;
using WaveFit2.Login.ViewModel;

namespace WaveFit2.Login.View
{
    public partial class LoginView : Window
    {
        public string pbPassword;
        public string tbUsername;

        public string rName, rSurname, rUsername, rPassword, rPasswordC, rCertificate;

        public List<LoginModel> Users;

        private readonly Crud crud = new Crud();

        public LoginUserControl loginUserControl = new LoginUserControl();
        public RegisterUserControl registerUserControl = new RegisterUserControl();
        public LoginViewModel loginViewModel = new LoginViewModel();

        private Random random = new Random();

        public LoginView()
        {
            InitializeComponent();

            loginViewModel.AddUsers();
            loginViewModel.AddLocations();
            loginViewModel.AddPlaces();
            loginViewModel.AddAudiometers();
            loginViewModel.AddHealthCenters();

            LoginButtons();
            LoginTexts();

            UserControlLogin.Content = loginUserControl;
            registerUserControl.EditPassword.Visibility = Visibility.Collapsed;
            registerUserControl.EditUsername.Visibility = Visibility.Collapsed;
        }

        private void LoginButtons()
        {
            loginUserControl.CloseButton.Click += CloseButton_Click;
            loginUserControl.EnterButton.Click += EnterButton_Click;

            registerUserControl.RegisterButton.Click += RegisterButton_Click;
        }

        private void LoginTexts()
        {
            loginUserControl.Password.PasswordChanged += Password_PasswordChanged;
            loginUserControl.Username.TextChanged += Username_TextChanged;
            registerUserControl.Name.TextChanged += Name_TextChanged;
            registerUserControl.Surname.TextChanged += Surname_TextChanged;
            registerUserControl.Username.TextChanged += UsernameRegister_TextChanged;
            registerUserControl.Password.PasswordChanged += PasswordRegister_PasswordChanged;
            registerUserControl.PasswordConfirm.PasswordChanged += PasswordCRegister_PasswordChanged;
            registerUserControl.Certificate.TextChanged += Certificate_TextChanged;
        }

        private void Certificate_TextChanged(object sender, TextChangedEventArgs e)
        {
            rCertificate = registerUserControl.Certificate.Text.ToString();
        }

        private void PasswordCRegister_PasswordChanged(object sender, RoutedEventArgs e)
        {
            rPasswordC = registerUserControl.PasswordConfirm.Password.ToString();
            registerUserControl.PasswordConfirm.Tag = rPasswordC;
        }

        private void PasswordRegister_PasswordChanged(object sender, RoutedEventArgs e)
        {
            rPassword = registerUserControl.Password.Password.ToString();
            registerUserControl.Password.Tag = rPassword;
        }

        private void UsernameRegister_TextChanged(object sender, TextChangedEventArgs e)
        {
            rUsername = registerUserControl.Name.Text.ToString();
        }

        private void Surname_TextChanged(object sender, TextChangedEventArgs e)
        {
            rSurname = registerUserControl.Name.Text.ToString();
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            rName = registerUserControl.Name.Text.ToString();
        }

        private void Username_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbUsername = loginUserControl.Username.Text.ToString();
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            pbPassword = loginUserControl.Password.Password.ToString();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            bool register = loginViewModel.RegisterUser(registerUserControl.Name.Text.ToString(), registerUserControl.Surname.Text.ToString(),
                registerUserControl.Username.Text.ToString(), registerUserControl.Password.Password.ToString(),
                registerUserControl.PasswordConfirm.Password.ToString(), registerUserControl.Certificate.Text.ToString(),
                crud);
            if (register == true)
            {
                ClearRegisterForm();
                TabLogin.IsSelected = true;
                UserControlLogin.Content = loginUserControl;
            }
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (tbUsername == null || pbPassword == null || tbUsername == "" || pbPassword == "")
            {
                HandyControl.Controls.Growl.Warning("Por favor preencher usuário e senha!");
            }
            else
            {
                if (loginViewModel.VerifyUser(tbUsername, pbPassword))
                {
                    var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    loginViewModel.GetUserById(Properties.Settings.Default.userId);
                    mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
                else
                {
                    HandyControl.Controls.Growl.Error("Nome de usuário ou senha inválido!");
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem selectedTabItem = e.AddedItems[0] as TabItem;
            if (selectedTabItem != null)
            {
                string tabName = selectedTabItem.Name;
                switch (tabName)
                {
                    case "TabLogin":
                        UserControlLogin.Content = loginUserControl;
                        break;

                    case "TabRegister":
                        UserControlLogin.Content = registerUserControl;
                        break;
                }
            }
        }

        private void ClearRegisterForm()
        {
            registerUserControl.Name.Clear();
            registerUserControl.Surname.Clear();
            registerUserControl.Username.Clear();
            registerUserControl.Password.Clear();
            registerUserControl.PasswordConfirm.Clear();
            registerUserControl.Certificate.Clear();
        }
    }
}