using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;

namespace WaveFit2.Settings.ViewModel
{
    public class SettingsViewModel : Crud
    {
        #region User

        public ObservableCollection<LoginModel> Users { get; set; } = new ObservableCollection<LoginModel>();
        public List<LoginModel> dataFromDatabase = new List<LoginModel>();
        private int idUser = Properties.Settings.Default.userId;
        private int master = Properties.Settings.Default.userPermission;

        public void LoadUsersToDatagrid(DataGrid dataGrid)
        {
            Users.Clear();

            dataFromDatabase = LoadUsersDataFromDatabase(idUser);

            foreach (LoginModel item in dataFromDatabase)
            {
                switch (item.Permission)
                {
                    case 1:
                        item.PermissionLevel = "Usuário";
                        item.IsAdmin = false;
                        item.NotMaster = true;
                        break;

                    case 2:
                        item.PermissionLevel = "Administrador";
                        item.IsAdmin = true;
                        item.NotMaster = true;
                        break;

                    case 3:
                        item.PermissionLevel = "WaveMaster";
                        item.IsAdmin = true;
                        item.NotMaster = false;
                        break;
                }

                switch (item.Enabled)
                {
                    case true:
                        item.Situation = "Ativo";
                        item.Active = true;
                        break;

                    case false:
                        item.Situation = "Desativado";
                        item.Active = false;
                        break;
                }

                Users.Add(item);
            }

            dataGrid.ItemsSource = Users;

            if (master != 3)
            {
                var column = dataGrid.Columns.FirstOrDefault(c => c.Header.ToString() == "Ativo");
                if (column != null)
                {
                    column.IsReadOnly = true;
                    column.CellStyle = GetDisabledCellStyle();
                }
            }
        }

        private Style GetDisabledCellStyle()
        {
            Style style = new Style(typeof(DataGridCell));

            style.Setters.Add(new Setter(UIElement.IsEnabledProperty, false));
            return style;
        }

        #endregion User
    }
}