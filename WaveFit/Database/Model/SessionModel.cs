using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaveFit2.Database.Model
{
    [Table("session")]
    public class SessionModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Patient")]
        [Column("idpatient")]
        public int IdPatient { get; set; }

        public PatientModel Patient { get; set; }

        [ForeignKey("Login")]
        [Column("iduser")]
        public int IdUser { get; set; }

        public LoginModel Login { get; set; }

        [ForeignKey("Audiogram")]
        [Column("idaudiogram")]
        public int IdAudiogram { get; set; }

        public virtual AudiogramModel Audiogram { get; set; }

        [ForeignKey("AudioEvaluation")]
        [Column("idaudioevaluation")]
        public int IdAudioEvaluation { get; set; }

        public virtual AudioEvaluationModel AudioEvaluation { get; set; }

        [ForeignKey("HealthCenter")]
        [Column("idhealthcenter")]
        public int IdHealthCenter { get; set; }

        public virtual HealthCenterModel HealthCenter { get; set; }
    }
}