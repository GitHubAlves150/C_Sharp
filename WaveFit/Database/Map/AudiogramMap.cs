using System.Data.Entity.ModelConfiguration;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Map
{
    public class AudiogramMap : EntityTypeConfiguration<AudiogramModel>
    {
        public void Configure()
        {
            ToTable("audiogram");
            HasKey(x => x.Id);
            Property(x => x.Date).IsRequired();
        }
    }
}