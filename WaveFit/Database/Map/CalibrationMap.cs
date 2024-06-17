using System.Data.Entity.ModelConfiguration;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Map
{
    public class CalibrationMap : EntityTypeConfiguration<CalibrationModel>
    {
        public void Configure()
        {
            ToTable("Fitting");
            HasKey(x => x.Id);
            Property(x => x.IdPatient).IsRequired();
            Property(x => x.IdHearingAid).IsRequired();
            Property(x => x.Channel).IsRequired();
            Property(x => x.Program).IsRequired();
            Property(x => x.Params).IsRequired();
            Property(x => x.Config).IsRequired();
            Property(x => x.Date).IsRequired();
        }
    }
}