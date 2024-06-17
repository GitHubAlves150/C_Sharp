using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaveFit2.Database.Model
{
    [Table("healthcenter")]
    public class HealthCenterModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("nickname")]
        public string Nickname { get; set; }

        [Column("logo", TypeName = "bytea")]
        public byte[] Logo { get; set; }

        [Column("cnpj")]
        public string CNPJ { get; set; }

        [Column("telephone")]
        public string Telephone { get; set; }

        [Column("status")]
        public bool Status { get; set; }

        [ForeignKey("Place")]
        [Column("idplace")]
        public int IdPlace { get; set; }

        public virtual PlaceModel Place { get; set; }

        [ForeignKey("Audiometer")]
        [Column("idaudiometer")]
        public int IdAudiometer { get; set; }

        public virtual AudiometerModel Audiometer { get; set; }

        public ICollection<SessionModel> Session { get; set; }
    }
}