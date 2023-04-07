using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_categorie_ctg")]
    public class Categorie
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ctg_id")]
        public int Categorieid { get; set; }

        [Column("ctg_parent_id")]
        public int? CategorieParentid { get; set; }

        [Column("ctg_libelle")]
        [StringLength(100)]
        public string Libelle { get; set; } = null!;

        [Column("ctg_description")]
        [StringLength(500)]
        public string? Description { get; set; }


        [InverseProperty("CategoriePhotoNavigation")]
        public virtual ICollection<Photo> PhotoCategorieNavigation { get; set; } = new List<Photo>();

        [InverseProperty("CategorieProduitNavigation")]
        public virtual ICollection<Produit> ProduitsCategorieNavigation { get; set; } = new List<Produit>();

        [InverseProperty("SousCategoriesNavigation")]
        public virtual Categorie? CategorieParentNavigation { get; set; } = null!;

        [InverseProperty("CategorieParentNavigation")]
        public virtual ICollection<Categorie> SousCategoriesNavigation { get; set; } = new List<Categorie>();
    }
}

