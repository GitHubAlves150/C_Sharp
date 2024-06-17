using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaveFit2.Database.Model
{
    [Table("patients")]
    public class PatientModel
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("patientcode")]
        public long PatientCode { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        [Column("birthday", TypeName = "date")]
        public DateTime Birthday { get; set; }

        [Column("typedoc")]
        public string TypeDocument { get; set; }

        [Column("numdoc")]
        public string NumDocument { get; set; }

        [Column("status")]
        public bool? Status { get; set; }

        public ICollection<SessionModel> Session { get; set; }

        public ICollection<CalibrationModel> Calibration { get; set; }
    }
}