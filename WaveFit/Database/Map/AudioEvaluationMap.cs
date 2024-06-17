using System.Data.Entity.ModelConfiguration;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Map
{
    public class AudioEvaluationMap : EntityTypeConfiguration<AudioEvaluationModel>
    {
        public void Configure()
        {
            ToTable("audioevaluation");
            HasKey(x => x.Id);

            Property(x => x.LeftMeatoscopy);
            Property(x => x.RightMeatoscopy);

            Property(x => x.LeftEarLRF);
            Property(x => x.RightEarLRF);
            Property(x => x.LeftEarLAF);
            Property(x => x.LeftEarLAF);
            Property(x => x.LeftEarIntesity);
            Property(x => x.RightEarIntesity);
            Property(x => x.WordsMono);
            Property(x => x.WordsDi);
            Property(x => x.WordsTri);
            Property(x => x.OEMono);
            Property(x => x.OEDi);
            Property(x => x.OETri);
            Property(x => x.ODMono);
            Property(x => x.ODDi);
            Property(x => x.ODTri);

            Property(x => x.LeftEarVAMin);
            Property(x => x.LeftEarVAMax);
            Property(x => x.LeftEarVOMin);
            Property(x => x.LeftEarVOMax);
            Property(x => x.LeftEarLogo);
            Property(x => x.RightEarVAMin);
            Property(x => x.RightEarVAMax);
            Property(x => x.RightEarVOMin);
            Property(x => x.RightEarVOMax);
            Property(x => x.RightEarLogo);

            Property(x => x.Obs);
        }
    }
}