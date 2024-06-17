using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaveFit2.Database.Model
{
    [Table("hearingaid")]
    public class HearingAidModel
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("serialnumber")]
        public long SerialNumber { get; set; }

        [Column("device")]
        public string Device { get; set; }

        [Column("receptor")]
        public string Receptor { get; set; }

        public ICollection<CalibrationModel> Calibration { get; set; }
    }
}