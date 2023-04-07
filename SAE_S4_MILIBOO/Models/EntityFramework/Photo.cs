using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_photo_pht")]
    public class Photo
    {
        public Photo()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("pht_id")]
        public int PhotoId { get; set; }

        [Column("ctg_id")]
        public int? CategorieId { get; set; }


        [Column("avi_id")]
        public int? AviId { get; set;}


        [Column("vrt_id")]
        public int? VarianteId { get; set; }

        [Column("pht_chemin")]
        [StringLength(200)]
        public string Chemin { get; set; } = null!;

        [InverseProperty("PhotoCategorieNavigation")]
        public virtual Categorie? CategoriePhotoNavigation { get; set; } = null!;

        [InverseProperty("PhotosVarianteNavigation")]
        public virtual Variante? VariantePhotoNavigation { get; set; } = null!;

        [InverseProperty("PhotosAvisNavigation")]
        public virtual Avis? AvisPhotosNavigation { get; set; } = null!;
    }
}
