using System.Windows;
using System.Windows.Controls;
using WaveFit2.Settings.ViewModel;

namespace WaveFit2.Settings.View
{
    /// <summary>
    /// Interação lógica para SettingsUserControl.xam
    /// </summary>
    public partial class SettingsUserControl : UserControl
    {
        public EditProfileUserControl editProfileUserControl = new EditProfileUserControl();
        public UsersDatagridUserControl usersDatagridUserControl = new UsersDatagridUserControl();
        public HealthCenterUserControl healthcenterUserControl = new HealthCenterUserControl();
        public SettingsViewModel SettingsViewModel = new SettingsViewModel();

        public SettingsUserControl()
        {
            InitializeComponent();
            SettingsViewModel.LoadUsersToDatagrid(usersDatagridUserControl.DatagridUsers);
            UserPermission();
        }

        private void UserPermission()
        {
            int UserPermission = Properties.Settings.Default.userPermission;

            TabUsers.Visibility = UserPermission != 1 ? Visibility.Visible : Visibility.Collapsed;
        }

        public void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem selectedTabItem = e.AddedItems[0] as TabItem;
            if (selectedTabItem != null)
            {
                string tabName = selectedTabItem.Name;
                switch (tabName)
                {
                    case "TabEdit":
                        SettingMode.Content = editProfileUserControl;
                        break;

                    case "TabUsers":
                        SettingMode.Content = usersDatagridUserControl;
                        break;

                    case "TabHealthCenter":
                        SettingMode.Content = healthcenterUserControl;
                        healthcenterUserControl.LoadDatagrid();
                        break;
                }
            }
        }
    }
}