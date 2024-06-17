using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaveFit2.Database.Model
{
    [Table("fitting")]
    public class CalibrationModel
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

        [Column("program")]
        public int Program { get; set; }

        [Column("paramters")]
        public string Params { get; set; }

        [Column("configuration")]
        public string Config { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        public ICollection<HearingAidGainPlotModel> HearingAidGainPlot { get; set; }
    }
}