using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WaveFit2.Database;
using WaveFit2.Database.CRUD;
using WaveFit2.Login.View;

namespace WaveFit2.Settings.View
{
    /// <summary>
    /// Interação lógica para EditProfileUserControl.xam
    /// </summary>
    public partial class EditProfileUserControl : UserControl
    {
        public string eName, eSurname, eUsername, ePassword, ePasswordC, certificate;

        public RegisterUserControl registerUserControl = new RegisterUserControl();
        public Crud crudOperations = new Crud();

        public EditProfileUserControl()
        {
            InitializeComponent();
            ImageSelector.Filter = "All Images Files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp";
            EditProfileTools();
            GetProfile();
        }

        private void EditProfileTools()
        {
            NameEdit.TextChanged += NameEdit_TextChanged;
            SurnameEdit.TextChanged += SurnameEdit_TextChanged;
            UsernameEdit.TextChanged += UsernameEdit_TextChanged;
            CRMEdit.TextChanged += CRMEdit_TextChanged;
            PasswordEdit.PasswordChanged += PasswordEdit_PasswordChanged;
            PasswordCEdit.PasswordChanged += PasswordCEdit_PasswordChanged;
        }

        private void PasswordCEdit_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ePasswordC = PasswordCEdit.Password;
        }

        private void PasswordEdit_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ePassword = PasswordEdit.Password;
        }

        private void CRMEdit_TextChanged(object sender, TextChangedEventArgs e)
        {
            certificate = CRMEdit.Text;
        }

        private void NameEdit_TextChanged(object sender, TextChangedEventArgs e)
        {
            eName = NameEdit.Text;
        }

        private void SurnameEdit_TextChanged(object sender, TextChangedEventArgs e)
        {
            eSurname = SurnameEdit.Text;
        }

        private void UsernameEdit_TextChanged(object sender, TextChangedEventArgs e)
        {
            eUsername = UsernameEdit.Text;
        }

        public void GetProfile()
        {
            Name.Text = Properties.Settings.Default.userName;
            UserName.Text = Properties.Settings.Default.userUsername;
            NameEdit.Text = Properties.Settings.Default.userName;
            UsernameEdit.Text = Properties.Settings.Default.userUsername;
            SurnameEdit.Text = Properties.Settings.Default.userSurname;
            CRMEdit.Text = Properties.Settings.Default.userCRM;
        }

        public void SetProfile()
        {
            Properties.Settings.Default.userName = Name.Text;
            Properties.Settings.Default.userUsername = UserName.Text;
            Properties.Settings.Default.userName = NameEdit.Text;
            Properties.Settings.Default.userUsername = UsernameEdit.Text;
            Properties.Settings.Default.userSurname = SurnameEdit.Text;
            Properties.Settings.Default.userCRM = CRMEdit.Text;
        }

        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int idUser = Properties.Settings.Default.userId;

            if (string.IsNullOrEmpty(eName) || string.IsNullOrEmpty(eSurname)
                || string.IsNullOrEmpty(ePassword) || string.IsNullOrEmpty(ePasswordC)
                || string.IsNullOrEmpty(eUsername))
            {
                HandyControl.Controls.Growl.Warning("Preencha os campos * em branco.");
            }
            else
            {
                if (registerUserControl.Password.Password.ToString() != registerUserControl.PasswordConfirm.Password.ToString())
                {
                    HandyControl.Controls.Growl.Warning("As senhas devem ser iguais.");
                }
                else
                {
                    crudOperations.UpdateColString("dbo.users", BCrypt.Net.BCrypt.HashPassword(ePassword), idUser, "password");

                    if (ImageSelector.HasValue)
                    {
                        string selectedImagePath = ImageSelector.Uri.LocalPath;
                        ImageOverwrite(idUser, selectedImagePath);
                        crudOperations.UpdateColImage("dbo.users", Convert.FromBase64String(Properties.Settings.Default.imageUser), idUser, "image");
                    }

                    crudOperations.UpdateColString("dbo.users", eName, idUser, "name");
                    crudOperations.UpdateColString("dbo.users", eSurname, idUser, "surname");
                    crudOperations.UpdateColString("dbo.users", certificate, idUser, "crfa");
                    SetProfile();

                    HandyControl.Controls.Growl.Success("Usuário editado com sucesso.");
                }
            }
        }

        public void ImageOverwrite(int userId, string path)
        {
            byte[] imageBytes = File.ReadAllBytes(path);

            using (var dbContext = new WaveFitContext())
            {
                var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    user.Image = imageBytes;
                    dbContext.SaveChanges();
                    Properties.Settings.Default.imageUser = Convert.ToBase64String(imageBytes);
                    Properties.Settings.Default.Save();
                }
            }
        }
    }
}