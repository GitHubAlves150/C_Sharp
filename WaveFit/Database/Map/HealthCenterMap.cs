using System.Data.Entity.ModelConfiguration;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Map
{
    public class HealthCenterMap : EntityTypeConfiguration<HealthCenterModel>
    {
        public void Configure()
        {
            ToTable("healthcenter");
            HasKey(x => x.Id);
            Property(x => x.Name).IsRequired();
            Property(x => x.Nickname).IsRequired();
            Property(x => x.Logo);
            Property(x => x.CNPJ).IsRequired();
            Property(x => x.Telephone).IsRequired();
            Property(x => x.Status).IsRequired();
            Property(x => x.IdPlace).IsRequired();
            Property(x => x.IdAudiometer).IsRequired();
        }
    }
}