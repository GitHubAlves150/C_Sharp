using System.Collections.Generic;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Table
{
    public class LoginTable
    {
        public static List<LoginModel> Users()
        {
            return new List<LoginModel>
            {
                new LoginModel {
                                Name = "Admin",
                                Surname = "Admin",
                                Username = "Admin",
                                Password = BCrypt.Net.BCrypt.HashPassword("Admin"),
                                CRFa = "0000",
                                Permission = 2,
                                Enabled = true},

                new LoginModel {
                                Name = "WaveMaster",
                                Surname = "WaveMaster",
                                Username = "WaveMaster",
                                Password = BCrypt.Net.BCrypt.HashPassword("WaveMaster"),
                                CRFa = "0000",
                                Permission = 3,
                                Enabled = true},

                new LoginModel {
                                Name = "Fono",
                                Surname = "Fono",
                                Username = "Fono",
                                Password = BCrypt.Net.BCrypt.HashPassword("Fono"),
                                CRFa = "1111111",
                                Permission = 1,
                                Enabled = true}
            };
        }
    }
}