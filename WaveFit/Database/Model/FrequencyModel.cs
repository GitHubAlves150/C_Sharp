using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaveFit2.Database.Model
{
    [Table("frequency")]
    public class FrequencyModel
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [ForeignKey("Audiogram")]
        [Column("idaudiogram")]
        public int IdAudiogram { get; set; }

        public AudiogramModel Audiogram { get; set; }

        [Column("type")]
        [Required]
        [StringLength(3, MinimumLength = 2)]
        public string Type { get; set; }

        [Column("ear")]
        [StringLength(1, MinimumLength = 1)]
        [Required]
        public string Ear { get; set; }

        [Column("hz125")]
        public string _125Hz { get; set; }

        [Column("hz250")]
        public string _250Hz { get; set; }

        [Column("hz500")]
        public string _500Hz { get; set; }

        [Column("hz750")]
        public string _750Hz { get; set; }

        [Column("hz1000")]
        public string _1000Hz { get; set; }

        [Column("hz1500")]
        public string _1500Hz { get; set; }

        [Column("hz2000")]
        public string _2000Hz { get; set; }

        [Column("hz3000")]
        public string _3000Hz { get; set; }

        [Column("hz4000")]
        public string _4000Hz { get; set; }

        [Column("hz6000")]
        public string _6000Hz { get; set; }

        [Column("hz8000")]
        public string _8000Hz { get; set; }
    }
}