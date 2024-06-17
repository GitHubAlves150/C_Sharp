using System.Data.Entity.ModelConfiguration;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Map
{
    public class AdjustmentMap : EntityTypeConfiguration<AdjustmentModel>
    {
        public void Configure()
        {
            ToTable("adjustment");
            HasKey(x => x.Id);
            Property(x => x.IdPatient).IsRequired();
            Property(x => x.IdHearingAid).IsRequired();
            Property(x => x.Channel).IsRequired();
        }
    }
}