using System.Data.Entity.ModelConfiguration;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Map
{
    internal class LocationMap : EntityTypeConfiguration<LocationModel>
    {
        public void Configure()
        {
            ToTable("location");
            HasKey(x => x.Id);
            Property(x => x.State).IsRequired();
            Property(x => x.Acronym).IsRequired();
        }
    }
}