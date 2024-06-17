using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaveFit2.Database.Model
{
    [Table("users")]
    public class LoginModel
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("surname")]
        [Required]
        public string Surname { get; set; }

        [Column("username")]
        [Required]
        public string Username { get; set; }

        [Column("password")]
        [Required]
        public string Password { get; set; }

        [Column("crfa")]
        public string CRFa { get; set; }

        [Column("permission")]
        [Required]
        public int Permission { get; set; }

        [Column("enabled")]
        [Required]
        public bool Enabled { get; set; }

        [Column("image", TypeName = "bytea")]
        public byte[] Image { get; set; }

        [NotMapped]
        public string Situation { get; set; }

        [NotMapped]
        public bool Active { get; set; }

        [NotMapped]
        public string PermissionLevel { get; set; }

        [NotMapped]
        public bool IsAdmin { get; set; }

        [NotMapped]
        public bool NotMaster { get; set; }

        public ICollection<SessionModel> Session { get; set; }
    }
}