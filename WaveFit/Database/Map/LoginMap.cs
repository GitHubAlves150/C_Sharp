using System.Data.Entity.ModelConfiguration;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Map
{
    public class LoginMap : EntityTypeConfiguration<LoginModel>
    {
        public LoginMap()
        {
            ToTable("users");
            HasKey(x => x.Id);
            Property(x => x.Name);
            Property(x => x.Surname);
            Property(x => x.Username);
            Property(x => x.Password);
            Property(x => x.CRFa);
            Property(x => x.Permission).IsRequired();
            Property(x => x.Enabled).IsRequired();
            Property(x => x.Image).HasColumnName("image").HasColumnType("bytea");
        }
    }
}