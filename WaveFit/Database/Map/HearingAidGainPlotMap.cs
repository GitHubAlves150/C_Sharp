using System.Data.Entity.ModelConfiguration;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Map
{
    public class HearingAidGainPlotMap : EntityTypeConfiguration<HearingAidGainPlotModel>
    {
        public void Configure()
        {
            ToTable("gainplot");
            HasKey(x => x.Id);
            Property(x => x.IdCalibration).IsRequired();
            Property(x => x.Plot).HasColumnName("plot").HasColumnType("bytea").IsRequired();
        }
    }
}