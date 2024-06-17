using System.Windows;
using System.Windows.Controls;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;
using WaveFit2.Settings.ViewModel;

namespace WaveFit2.Settings.View
{
    /// <summary>
    /// Interação lógica para UsersDatagridUserControl.xam
    /// </summary>
    public partial class UsersDatagridUserControl : UserControl
    {
        public SettingsViewModel SettingsViewModel = new SettingsViewModel();
        public Crud crudOperations = new Crud();

        public UsersDatagridUserControl()
        {
            InitializeComponent();
        }

        public void CheckAdmin_Loaded(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            checkBox.Checked += CheckAdmin_Checked;
        }

        public void CheckMaster_Loaded(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            checkBox.Checked += CheckMaster_Checked;
        }

        public void CheckMaster_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsKeyboardFocused)
            {
                if (checkBox.DataContext is LoginModel user)
                {
                    string userName = user.Username;
                    string name = user.Name;

                    if (user.Active == false)
                    {
                        crudOperations.UpdateColBoolUserName("dbo.users", "enabled", true, userName);
                        SettingsViewModel.LoadUsersToDatagrid(DatagridUsers);
                        HandyControl.Controls.Growl.Success($"{name} está ativo.");
                    }
                    else
                    {
                        HandyControl.Controls.Growl.Warning($"{name} já está ativo");
                    }
                }
            }
        }

        public void CheckAdmin_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsKeyboardFocused)
            {
                if (checkBox.DataContext is LoginModel user)
                {
                    string userName = user.Username;
                    string name = user.Name;

                    if (user.PermissionLevel == "Usuário" && user.IsAdmin == false)
                    {
                        crudOperations.UpdateColIntByUserName("dbo.users", 2, userName, "permission");
                        SettingsViewModel.LoadUsersToDatagrid(DatagridUsers);
                        HandyControl.Controls.Growl.Success($"{name} tornou-se administrador com sucesso.");
                    }
                    else
                    {
                        HandyControl.Controls.Growl.Warning($"{name} já é administrador.");
                    }
                }
            }
        }

        public void CheckMaster_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox.DataContext is LoginModel user)
            {
                string userName = user.Username;
                string name = user.Name;

                if (user.Active == true)
                {
                    crudOperations.UpdateColBoolUserName("dbo.users", "enabled", false, userName);
                    SettingsViewModel.LoadUsersToDatagrid(DatagridUsers);
                    HandyControl.Controls.Growl.Success($"{name} está desativado.");
                }
            }
        }

        public void CheckAdmin_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox.DataContext is LoginModel user)
            {
                string userName = user.Username;
                string name = user.Name;

                if (user.PermissionLevel != "WaveMaster")
                {
                    crudOperations.UpdateColIntByUserName("dbo.users", 1, userName, "permission");
                    SettingsViewModel.LoadUsersToDatagrid(DatagridUsers);
                    HandyControl.Controls.Growl.Success($"{name} deixou de ser administrador.");
                }
            }
        }
    }
}