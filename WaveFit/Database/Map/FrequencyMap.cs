using System.Data.Entity.ModelConfiguration;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Map
{
    public class FrequencyMap : EntityTypeConfiguration<FrequencyModel>
    {
        public void Configure()
        {
            ToTable("frequency");
            HasKey(x => x.Id);
            Property(x => x.IdAudiogram).IsRequired();
            Property(x => x.Ear).IsRequired();
            Property(x => x._125Hz);
            Property(x => x._250Hz);
            Property(x => x._500Hz);
            Property(x => x._750Hz);
            Property(x => x._1000Hz);
            Property(x => x._1500Hz);
            Property(x => x._2000Hz);
            Property(x => x._3000Hz);
            Property(x => x._4000Hz);
            Property(x => x._6000Hz);
            Property(x => x._8000Hz);
        }
    }
}