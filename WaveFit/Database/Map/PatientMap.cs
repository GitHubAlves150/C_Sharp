using System.Data.Entity.ModelConfiguration;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Map
{
    public class PatientMap : EntityTypeConfiguration<PatientModel>
    {
        public void Configure()
        {
            ToTable("patients");
            HasKey(x => x.Id);
            Property(x => x.PatientCode).IsRequired();
            Property(x => x.Name).IsRequired();
            Property(x => x.Surname).IsRequired();
            Property(x => x.Birthday).IsRequired();
            Property(x => x.Gender).IsRequired();
            Property(x => x.TypeDocument);
            Property(x => x.NumDocument);
            Property(x => x.Status);
        }
    }
}