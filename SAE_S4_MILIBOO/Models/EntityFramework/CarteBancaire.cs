using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_cartebancaire_cbr")]
    public class CarteBancaire
    {
        public CarteBancaire()
        {
            
        }

        
        [Column("cbr_id")]
        public int CarteBancaireId { get; set; }

        [Column("clt_id")]
        public int ClientId { get; set; }

        [Column("cbr_numero", TypeName ="Text")]
        public string NumeroCarte { get; set; }

        [Column("cbr_cryptogramme", TypeName ="text")]
        public string CryptoCarte { get; set; }

        [Column("cbr_date_expiration")]
        public string DateExpiration { get; set; }

        [Column("cbr_nom")]
        [StringLength(50)]
        public string Nom { get; set; }

        [Column("cbr_prenom")]
        [StringLength(50)]
        public string Prenom { get; set; }


        [InverseProperty("CarteBancaireClientNavigation")]
        public virtual Client? ClientCarteBancaireNavigation { get; set; }


    }
}
