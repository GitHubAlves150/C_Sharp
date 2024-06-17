using Npgsql;
using System;
using System.Linq;
using System.Windows;
using WaveFit2.Database;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;
using WaveFit2.Database.Table;

namespace WaveFit2.Login.ViewModel
{
    public class LoginViewModel : Crud
    {
        private readonly WaveFitContext context;
        public int userId;
        public string userName;

        public LoginViewModel()
        {
            context = new WaveFitContext();
        }

        public void AddUsers()
        {
            var context = new WaveFitContext();

            foreach (var users in LoginTable.Users())
            {
                if (!context.Users.Any(u => u.Username == users.Username))
                {
                    context.Users.Add(users);
                }
            }
            context.SaveChanges();
            Properties.Settings.Default.Flag = true;
        }

        public bool VerifyUser(string username, string password)
        {
            try
            {
                using (var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB))
                {
                    connection.Open();
                    var command = new NpgsqlCommand("SELECT id, password " +
                                                    "FROM dbo.users " +
                                                    "WHERE username = @username AND enabled = true", connection);
                    command.Parameters.AddWithValue("@username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string hashedPassword = reader.GetString(1);

                            if (BCrypt.Net.BCrypt.Verify(password, hashedPassword))
                            {
                                Properties.Settings.Default.userId = userId;

                                return true;
                            }
                        }
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool RegisterUser(string name, string surname,
                  string username, string password, string passwordConfirm,
                  string certificate,
                  Crud crud)
        {
            if (name == "" || surname == ""
                || username == "" || password == ""
                || passwordConfirm == "")
            {
                HandyControl.Controls.Growl.Warning("Preencha os campos obrigatórios (*) abaixo.");
                return false;
            }
            else
            {
                if ((context.Users.Any(u => u.Username == username)))
                {
                    HandyControl.Controls.Growl.Warning("Esse usuário já existe.");
                    return false;
                }
                else
                {
                    if (password == passwordConfirm)
                    {
                        LoginModel Users =
                          new LoginModel
                          {
                              Name = name,
                              Surname = surname,
                              Username = username,
                              Password = BCrypt.Net.BCrypt.HashPassword(password),
                              CRFa = certificate,
                              Permission = 1,
                              Enabled = true
                          };

                        crud.AddUser(Users);
                        HandyControl.Controls.Growl.Success("Usuário registrado com sucesso.");

                        return true;
                    }
                    else
                    {
                        HandyControl.Controls.Growl.Warning("Senhas estão diferentes.");
                        return false;
                    }
                }
            }
        }
    }
}