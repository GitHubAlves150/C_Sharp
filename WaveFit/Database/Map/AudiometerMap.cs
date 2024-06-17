using System.Data.Entity.ModelConfiguration;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Map
{
    public class AudiometerMap : EntityTypeConfiguration<AudiometerModel>
    {
        public void Configure()
        {
            ToTable("audiometer");
            HasKey(x => x.Id);
            Property(x => x.Code).IsRequired();
            Property(x => x.Model).IsRequired();
            Property(x => x.Maintenance).IsRequired();
        }
    }
}