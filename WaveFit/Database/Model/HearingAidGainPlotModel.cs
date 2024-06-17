using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaveFit2.Database.Model
{
    [Table("gainplot")]
    public class HearingAidGainPlotModel
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [ForeignKey("Calibration")]
        [Column("idfitting")]
        public int IdCalibration { get; set; }

        public CalibrationModel Calibration { get; set; }

        [Column("plot", TypeName = "bytea")]
        public byte[] Plot { get; set; }
    }
}