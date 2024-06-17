using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaveFit2.Database.Model
{
    [Table("location")]
    public class LocationModel
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("state")]
        public string State { get; set; }

        [Column("acronym")]
        public string Acronym { get; set; }

        public ICollection<PlaceModel> Place { get; set; }
    }
}