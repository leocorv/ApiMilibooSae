using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    public class Login
    {
        [Column("clt_email")]
        [EmailAddress]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La longueur d’un email doit être comprise entre 6 et 100 caractères.")]
        [Required]
        public string? Mail { get; set; }

        [Column("clt_password")]
        [StringLength(100)]
        [Required]
        public string? Password { get; set; }
    }
}
