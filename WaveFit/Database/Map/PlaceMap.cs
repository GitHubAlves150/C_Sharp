using System.Data.Entity.ModelConfiguration;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Map
{
    internal class PlaceMap : EntityTypeConfiguration<PlaceModel>
    {
        public void Configure()
        {
            ToTable("place");
            HasKey(x => x.Id);
            Property(x => x.IdLocation).IsRequired();
            Property(x => x.City).IsRequired();
            Property(x => x.Area).IsRequired();
            Property(x => x.Address).IsRequired();
            Property(x => x.CEP).IsRequired();
        }
    }
}