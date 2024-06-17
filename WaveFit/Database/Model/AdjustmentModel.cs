using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaveFit2.Database.Model
{
    [Table("adjustment")]
    public class AdjustmentModel
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [ForeignKey("Patient")]
        [Column("idpatient")]
        public int IdPatient { get; set; }

        public PatientModel Patient { get; set; }

        [ForeignKey("HearingAid")]
        [Column("idhearingaid")]
        public int IdHearingAid { get; set; }

        public HearingAidModel HearingAid { get; set; }

        [Column("channel")]
        public string Channel { get; set; }
    }
}