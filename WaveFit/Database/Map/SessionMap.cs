using System.Data.Entity.ModelConfiguration;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Map
{
    public class SessionMap : EntityTypeConfiguration<SessionModel>
    {
        public void Configure()
        {
            ToTable("session");
            HasKey(x => x.Id);
            Property(x => x.IdUser).IsRequired();
            Property(x => x.IdPatient).IsRequired();
            Property(x => x.IdAudiogram).IsRequired();
            Property(x => x.IdAudioEvaluation).IsRequired();
            Property(x => x.IdHealthCenter);
        }
    }
}