using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaveFit2.Database.Model
{
    [Table("place")]
    public class PlaceModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Location")]
        [Column("idlocation")]
        public int IdLocation { get; set; }

        public virtual LocationModel Location { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("area")]
        public string Area { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("cep")]
        public string CEP { get; set; }

        public ICollection<HealthCenterModel> HealthCenter { get; set; }
    }
}