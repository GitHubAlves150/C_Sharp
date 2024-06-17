using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaveFit2.Database.Model
{
    [Table("audiometer")]
    public class AudiometerModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("model")]
        public string Model { get; set; }

        [Column("maintenance")]
        public DateTime Maintenance { get; set; }

        public ICollection<HealthCenterModel> HealthCenter { get; set; }
    }
}