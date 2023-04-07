using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    public partial class MilibooDBContext : DbContext
    {
        //public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        public MilibooDBContext()
        {
        }

        public MilibooDBContext(DbContextOptions<MilibooDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adresse> Adresses { get; set; } = null!;
        public virtual DbSet<AdresseLivraison> AdresseLivraisons { get; set; } = null!;
        public virtual DbSet<Avis> Avis { get; set; } = null!;
        public virtual DbSet<CarteBancaire> CarteBancaires { get; set; } = null!;
        public virtual DbSet<Categorie> Categories { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Collection> Collections { get; set; } = null!;
        public virtual DbSet<Commande> Commandes { get; set; } = null!;
        public virtual DbSet<Couleur> Couleurs { get; set; } = null!;
        public virtual DbSet<Etat> Etats { get; set; } = null!;
        public virtual DbSet<LigneCommande> LigneCommandes { get; set; } = null!;
        public virtual DbSet<LignePanier> LignePaniers { get; set; } = null!;
        public virtual DbSet<Liste> Listes { get; set; } = null!;
        public virtual DbSet<Photo> Photos { get; set; } = null!;
        public virtual DbSet<Produit> Produits { get; set; } = null!;
        public virtual DbSet<ProduitListe> ProduitListes { get; set; } = null!;
        public virtual DbSet<Variante> Variantes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<TDimensions>();

            //Table CarteBancaire
            modelBuilder.Entity<CarteBancaire>(entity =>
            {
                entity.HasKey(e => e.CarteBancaireId);

                entity.HasOne(d => d.ClientCarteBancaireNavigation)
                    .WithMany(p => p.CarteBancaireClientNavigation)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_client_panier");
            });


            //Table Adresse
            modelBuilder.Entity<Adresse>(entity =>
            {
                entity.HasKey(e => e.AdresseId);

            });

            //Table AdresseLivraison Jointure
            modelBuilder.Entity<AdresseLivraison>(entity =>
            {
                entity.HasKey(e => new { e.ClientId, e.AdresseId });

                entity.HasOne(d => d.AdresseALivreNavigation)
                    .WithMany(p => p.AdressesClientsNavigation)
                    .HasForeignKey(d => d.AdresseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_adresse_livraison_adresse");

                entity.HasOne(d => d.ClientALivreNavigation)
                    .WithMany(p => p.ClientsLivraisonsNavigation)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_adresse_livraison_client");

            });

            //Table Liste
            modelBuilder.Entity<Liste>(entity =>
            {
                entity.HasKey(e => e.ListeId);

                entity.HasOne(d => d.ClientNavigation)
                    .WithMany(p => p.ListesNavigation)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_liste_client");
            });

            //Table Client
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(ev => ev.ClientId);

                entity.HasIndex(e => e.Mail).IsUnique();

                entity.HasCheckConstraint("CK_clt_solde_fidelite", "clt_solde_fidelite >= 0");
            });


            //Table Categorie PK: CategorieId ; FK: PhotoId
            modelBuilder.Entity<Categorie>(entity =>
            {
                entity.HasKey(e => e.Categorieid);

                entity.HasMany(d => d.SousCategoriesNavigation)
                    .WithOne(d => d.CategorieParentNavigation)
                    .HasForeignKey(d => d.CategorieParentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sous_categorie_parent");
            });

            //Table Produit PK: IdProduit; FK: CollectionId et CategorieId
            modelBuilder.Entity<Produit>(entity =>
            {
                entity.HasKey(e => e.IdProduit);

                entity.HasOne(d => d.CollectionProduitNavigation)
                    .WithMany(p => p.ProduitsCollectionNavigation)
                    .HasForeignKey(d => d.CollectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_produit_collection")
                    .IsRequired(false);

                entity.HasOne(d => d.CategorieProduitNavigation)
                    .WithMany(p => p.ProduitsCategorieNavigation)
                    .HasForeignKey(d => d.CategorieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_produit_categorie")
                    .IsRequired(false);

            });

            //Table Collection PK: CollectionId
            modelBuilder.Entity<Collection>(entity =>
            {
                entity.HasKey(e => e.CollectionId);

                entity.HasCheckConstraint("CK_cln_prix", "cln_prix >= 0");
                entity.HasCheckConstraint("CK_cln_promo", "cln_promo between 0.01 and 1");

            });

            //Table Photo PK: PhotoId; FK: AviId et PhotoId
            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasKey(e => e.PhotoId);

                entity.HasOne(d => d.AvisPhotosNavigation)
                    .WithMany(p => p.PhotosAvisNavigation)
                    .HasForeignKey(d => d.AviId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_photo_avis")
                    .IsRequired(false);

                entity.HasOne(d => d.VariantePhotoNavigation)
                    .WithMany(p => p.PhotosVarianteNavigation)
                    .HasForeignKey(d => d.VarianteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_photo_variante")
                    .IsRequired(false);

                entity.HasOne(d => d.CategoriePhotoNavigation)
                    .WithMany(p => p.PhotoCategorieNavigation)
                    .HasForeignKey(d => d.CategorieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_photo_categorie")
                    .IsRequired(false);
            });

            //Table Avis PK: AvisId; FK: CLientId et AvisId
            modelBuilder.Entity<Avis>(entity =>
            {
                entity.HasKey(e => e.AvisId);

                entity.HasOne(d => d.ClientsAvisNavigation)
                    .WithMany(p => p.AvisClientsNavigation)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_avis_client");

                entity.HasOne(d => d.VarianteAvisNavigation)
                    .WithMany(p => p.AvisVarianteNavigation)
                    .HasForeignKey(d => d.VarianteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_avis_variante");

                entity.HasCheckConstraint("CK_avi_note", "avi_note between 1 and 5");
            });

            //Table Couleur PK: IdCouleur ; Unique Index: Libelle
            modelBuilder.Entity<Couleur>(entity =>
            {
                entity.HasKey(e => e.IdCouleur);

                entity.HasIndex(e => e.Libelle).IsUnique();

            });

            //Table LignePanier PK: LigneId; FK: PanierId et VarianteId
            modelBuilder.Entity<LignePanier>(entity =>
            {
                entity.HasKey(e => e.LigneId);

                entity.HasOne(d => d.ClientLignePanierNavigation)
                    .WithMany(p => p.LignesPanierClientNavigation)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ligne_panier_panier");

                entity.HasOne(d => d.VariantesLignePanierNavigation)
                    .WithMany(p => p.LignePanierVarianteNavigation)
                    .HasForeignKey(d => d.VarianteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ligne_panier_variante");

                entity.HasCheckConstraint("CK_lpn_quantite", "lpn_quantite >= 1");

            });

            //Table ProduitListe PK: (ProduitId , ListeId) ; FK: ProduitId et ListeId
            modelBuilder.Entity<ProduitListe>(entity =>
            {
                entity.HasKey(e => new {e.ProduitId, e.ListeId});

                entity.HasOne(d => d.ProduitDansListeNavigation)
                    .WithMany(p => p.ListeProduitNavigation)
                    .HasForeignKey(d => d.ProduitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_produitliste_produit");

                entity.HasOne(d => d.ListeDeProduitNavigation)
                    .WithMany(p => p.ProduitListeNavigation)
                    .HasForeignKey(d => d.ListeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_produitliste_liste");

            });

            //Table LigneCommande PK: LigneCommandeId; FK: VarianteId et CommandeId
            modelBuilder.Entity<LigneCommande>(entity =>
            {
                entity.HasKey(e => e.LigneCommandeId);

                entity.HasOne(d => d.VarianteLigneCommandeNavigation)
                    .WithMany(p => p.LignesCommandeVarianteNavigation)
                    .HasForeignKey(d => d.VarianteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_lignecommande_variante");

                entity.HasOne(d => d.CommandeLigneCommandeNavigation)
                    .WithMany(p => p.LigneCommandeCommandeNavigation)
                    .HasForeignKey(d => d.CommandeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_lignecommande_commande");

                entity.HasCheckConstraint("CK_lcm_quantite", "lcm_quantite >= 1");

            });

            //Table Etat PK: EtatId
            modelBuilder.Entity<Etat>(entity =>
            {
                entity.HasKey(e => e.EtatId);


            });

            //Table LigneCommande PK: LigneCommandeId; FK: VarianteId et CommandeId
            modelBuilder.Entity<Commande>(entity =>
            {
                entity.HasKey(e => e.CommandeId);

                entity.HasOne(d => d.EtatCommandeNavigation)
                    .WithMany(p => p.CommandeEtatNavigation)
                    .HasForeignKey(d => d.EtatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_commande_etat");

                entity.HasOne(d => d.ClientCommandeNavigation)
                    .WithMany(p => p.CommandesClientNavigation)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_commande_client");

                entity.HasOne(d => d.AdresseCommandeNavigation)
                    .WithMany(p => p.CommandeAdresseNavigation)
                    .HasForeignKey(d => d.AdresseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_commande_adresse");


                entity.HasCheckConstraint("CK_cmd_point_fidelite_utilises", "cmd_points_fidelite_utilises >= 0");
            });

            //Table Variante PK: IdVariante
            modelBuilder.Entity<Variante>(entity =>
            {
                entity.HasKey(e => e.IdVariante);

                entity.HasOne(d => d.CouleurVarianteNavigation)
                    .WithMany(p => p.VariantesCouleurNavigation)
                    .HasForeignKey(d => d.IdCouleur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_variante_couleur"); 

                entity.HasOne(d => d.ProduitVarianteNavigation)
                    .WithMany(p => p.VariantesProduitNavigation)
                    .HasForeignKey(d => d.IdProduit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_variante_produit");

                entity.HasCheckConstraint("CK_vrt_prix", "vrt_prix >= 0");
                entity.HasCheckConstraint("CK_vrt_promo", "vrt_promo between 0.01 and 1");
                entity.HasCheckConstraint("CK_prd_stock", "vrt_stock >= 0");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
    
}


