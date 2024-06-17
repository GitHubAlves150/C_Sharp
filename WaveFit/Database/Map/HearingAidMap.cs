using System.Data.Entity.ModelConfiguration;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Map
{
    internal class HearingAidMap : EntityTypeConfiguration<HearingAidModel>
    {
        public void Configure()
        {
            ToTable("hearingaid");
            HasKey(x => x.Id);
            Property(x => x.SerialNumber).IsRequired();
            Property(x => x.Device).IsRequired();
            Property(x => x.Receptor);
        }
    }
}