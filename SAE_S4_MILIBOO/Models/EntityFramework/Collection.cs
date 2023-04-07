using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_collection_cln")]
    public class Collection
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("cln_id")]
        public int CollectionId { get; set; }

        [Column("cln_libelle")]
        [StringLength(150)]
        public string CollectionLibelle { get; set; } = null!;

        [Column("cln_prix", TypeName ="numeric")]
        public double CollectionPrix { get; set; } 

        [Column("cln_promo", TypeName ="decimal")]
        public double CollectionPromo { get; set; }

        [InverseProperty("CollectionProduitNavigation")]
        public virtual ICollection<Produit> ProduitsCollectionNavigation { get; set; } = new List<Produit>();
    }

}

