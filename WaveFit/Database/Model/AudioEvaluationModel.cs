using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaveFit2.Database.Model
{
    [Table("audioevaluation")]
    public class AudioEvaluationModel
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        #region Meatoscopy

        [Column("leftmeatoscopy")]
        public string LeftMeatoscopy { get; set; }

        [Column("rightmeatoscopy")]
        public string RightMeatoscopy { get; set; }

        #endregion Meatoscopy

        #region Logo

        [Column("leftearlrf")]
        public string LeftEarLRF { get; set; }

        [Column("rightearlrf")]
        public string RightEarLRF { get; set; }

        [Column("leftearlaf")]
        public string LeftEarLAF { get; set; }

        [Column("rightearlaf")]
        public string RightEarLAF { get; set; }

        [Column("leftearintesity")]
        public string LeftEarIntesity { get; set; }

        [Column("rightearintesity")]
        public string RightEarIntesity { get; set; }

        [Column("wordsmono")]
        public string WordsMono { get; set; }

        [Column("wordsdi")]
        public string WordsDi { get; set; }

        [Column("wordstri")]
        public string WordsTri { get; set; }

        [Column("leftearmono")]
        public string OEMono { get; set; }

        [Column("lefteardi")]
        public string OEDi { get; set; }

        [Column("lefteartri")]
        public string OETri { get; set; }

        [Column("rightearmono")]
        public string ODMono { get; set; }

        [Column("righteardi")]
        public string ODDi { get; set; }

        [Column("righteartri")]
        public string ODTri { get; set; }

        #endregion Logo

        #region Mask

        [Column("leftearvamin")]
        public string LeftEarVAMin { get; set; }

        [Column("leftearvamax")]
        public string LeftEarVAMax { get; set; }

        [Column("leftearvomin")]
        public string LeftEarVOMin { get; set; }

        [Column("leftearvomax")]
        public string LeftEarVOMax { get; set; }

        [Column("leftearlogo")]
        public string LeftEarLogo { get; set; }

        [Column("rightearvamin")]
        public string RightEarVAMin { get; set; }

        [Column("rightearvamax")]
        public string RightEarVAMax { get; set; }

        [Column("rightearvomin")]
        public string RightEarVOMin { get; set; }

        [Column("rightearvomax")]
        public string RightEarVOMax { get; set; }

        [Column("rightearlogo")]
        public string RightEarLogo { get; set; }

        #endregion Mask

        #region Obs

        [Column("obs")]
        public string Obs { get; set; }

        #endregion Obs

        #region Session

        public ICollection<SessionModel> Session { get; set; }

        #endregion Session
    }
}