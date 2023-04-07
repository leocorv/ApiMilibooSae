using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_produit_prd")]
    public class Produit
    {
        public Produit()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("prd_id")]
        public int IdProduit { get; set; }

        [Column("ctg_id")]
        public int CategorieId { get; set; }

        [Column("cln_id")]
        public int? CollectionId { get; set; }


        [Column("prd_lib")]
        [StringLength(100)]
        [Required]
        public string Libelle { get; set; }

        [Column("prd_description")]
        [StringLength(500)]
        public string? Description { get; set; }


        [Column("prd_instructions_entretien", TypeName = "text")]
        public string? InscructionsEntretien { get; set; }

        [Column("prd_revetement")]
        [StringLength(150)]
        public string? Revetement { get; set; }

        [Column("prd_matiere")]
        [StringLength(150)]
        public string? Matiere { get; set; }

        [Column("prd_matiere_pieds")]
        [StringLength(150)]
        public string? MatierePieds { get; set; }

        [Column("prd_hauteur_pieds", TypeName = "numeric(5,2)")]
        public double? HauteurPieds { get; set; }

        [Column("prd_type_mousse_assise")]
        [StringLength(150)]
        public string? TypeMousseAssise { get; set; }

        [Column("prd_type_mousse_dossier")]
        [StringLength(150)]
        public string? TypeMousseDossier { get; set; }

        [Column("prd_densite_assise", TypeName = "numeric(5,2)")]
        public double? DensiteAssise { get; set; }

        [Column("prd_densite_dossier", TypeName = "numeric(5,2)")]
        public double? DensiteDossier { get; set; }

        [Column("prd_poids_colis", TypeName = "numeric(5,2)")]
        public double? PoidsColis { get; set; }

        [Column("prd_dim_dossier", TypeName = "t_dimensions")]
        public TDimensions? DimDossier { get; set; }

        [Column("prd_dim_assise", TypeName = "t_dimensions")]
        public TDimensions? DimAssise { get; set; }

        [Column("prd_dim_totale", TypeName = "t_dimensions")]
        public TDimensions? DimTotale { get; set; }


        [Column("prd_dim_colis", TypeName = "t_dimensions")]
        public TDimensions? DimColis { get; set; }

        [Column("prd_dim_deplie", TypeName = "t_dimensions")]
        public TDimensions? DimDeplie { get; set; }

        [Column("prd_dim_accoudoir", TypeName = "t_dimensions")]
        public TDimensions? DimAccoudoir { get; set; }
        [Column("prd_made_in_france")]
        public bool MadeInFrance { get; set; }



        [InverseProperty("ProduitVarianteNavigation")]
        public ICollection<Variante> VariantesProduitNavigation { get; set; } = new List<Variante>();

        [InverseProperty("ProduitsCategorieNavigation")]
        public virtual Categorie? CategorieProduitNavigation { get; set; } = null!;

        [InverseProperty("ProduitsCollectionNavigation")]
        public virtual Collection? CollectionProduitNavigation { get; set; } = null!;

        [InverseProperty("ProduitDansListeNavigation")]
        public virtual ICollection<ProduitListe> ListeProduitNavigation { get; set; } = new List<ProduitListe>();
    }

    public class TDimensions
    {
        private decimal? x;
        private decimal? y;
        private decimal? z;

        public TDimensions(decimal? x, decimal? y, decimal? z)
        {
            this.x = X;
            this.y = Y;
            this.z = Z;
        }

        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Z { get; set; }
    }
}


