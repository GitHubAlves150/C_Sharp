using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaveFit2.Database.Model
{
    [Table("audiogram")]
    public class AudiogramModel
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        public ICollection<SessionModel> Session { get; set; }

        public ICollection<FrequencyModel> Frequency { get; set; }
    }
}