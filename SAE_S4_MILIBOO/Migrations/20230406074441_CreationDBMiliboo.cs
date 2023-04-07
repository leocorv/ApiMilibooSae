using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SAE_S4_MILIBOO.Migrations
{
    public partial class CreationDBMiliboo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_adresse_adr",
                columns: table => new
                {
                    adr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    adr_rue = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    adr_numero = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    adr_cp = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    adr_ville = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    adr_pays = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    adr_tel_fixe = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    adr_remarque = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_adresse_adr", x => x.adr_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_categorie_ctg",
                columns: table => new
                {
                    ctg_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ctg_parent_id = table.Column<int>(type: "integer", nullable: true),
                    ctg_libelle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ctg_description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_categorie_ctg", x => x.ctg_id);
                    table.ForeignKey(
                        name: "fk_sous_categorie_parent",
                        column: x => x.ctg_parent_id,
                        principalTable: "t_e_categorie_ctg",
                        principalColumn: "ctg_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_client_clt",
                columns: table => new
                {
                    clt_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clt_email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    clt_password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    clt_nom = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    clt_prenom = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    clt_portable = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    clt_newsletter_miliboo = table.Column<bool>(type: "boolean", nullable: false),
                    clt_newsletter_partenaire = table.Column<bool>(type: "boolean", nullable: false),
                    clt_solde_fidelite = table.Column<int>(type: "integer", nullable: false),
                    clt_derniere_connexion = table.Column<DateTime>(type: "date", nullable: true),
                    clt_date_creation = table.Column<DateTime>(type: "date", nullable: false),
                    clt_civilite = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_client_clt", x => x.clt_id);
                    table.CheckConstraint("CK_clt_solde_fidelite", "clt_solde_fidelite >= 0");
                });

            migrationBuilder.CreateTable(
                name: "t_e_collection_cln",
                columns: table => new
                {
                    cln_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cln_libelle = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    cln_prix = table.Column<decimal>(type: "numeric(38,17)", nullable: false),
                    cln_promo = table.Column<decimal>(type: "numeric(38,17)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_collection_cln", x => x.cln_id);
                    table.CheckConstraint("CK_cln_prix", "cln_prix >= 0");
                    table.CheckConstraint("CK_cln_promo", "cln_promo between 0.01 and 1");
                });

            migrationBuilder.CreateTable(
                name: "t_e_couleur_clr",
                columns: table => new
                {
                    clr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clr_libelle = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    clr_code = table.Column<string>(type: "char(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_couleur_clr", x => x.clr_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_etat_eta",
                columns: table => new
                {
                    eta_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    eta_libelle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_etat_eta", x => x.eta_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_cartebancaire_cbr",
                columns: table => new
                {
                    cbr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clt_id = table.Column<int>(type: "integer", nullable: false),
                    cbr_numero = table.Column<string>(type: "Text", nullable: false),
                    cbr_cryptogramme = table.Column<string>(type: "text", nullable: false),
                    cbr_date_expiration = table.Column<string>(type: "text", nullable: false),
                    cbr_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cbr_prenom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_cartebancaire_cbr", x => x.cbr_id);
                    table.ForeignKey(
                        name: "fk_client_panier",
                        column: x => x.clt_id,
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_liste_lst",
                columns: table => new
                {
                    lst_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clt_id = table.Column<int>(type: "integer", nullable: false),
                    lst_libelle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    lst_date_derniere_modif = table.Column<DateTime>(type: "date", nullable: false),
                    lst_date_creation = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_liste_lst", x => x.lst_id);
                    table.ForeignKey(
                        name: "fk_liste_client",
                        column: x => x.clt_id,
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_id");
                });

            migrationBuilder.CreateTable(
                name: "t_j_adresse_livraison_adl",
                columns: table => new
                {
                    clt_id = table.Column<int>(type: "integer", nullable: false),
                    adr_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_adresse_livraison_adl", x => new { x.clt_id, x.adr_id });
                    table.ForeignKey(
                        name: "fk_adresse_livraison_adresse",
                        column: x => x.adr_id,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id");
                    table.ForeignKey(
                        name: "fk_adresse_livraison_client",
                        column: x => x.clt_id,
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_produit_prd",
                columns: table => new
                {
                    prd_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ctg_id = table.Column<int>(type: "integer", nullable: false),
                    cln_id = table.Column<int>(type: "integer", nullable: true),
                    prd_lib = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    prd_description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    prd_instructions_entretien = table.Column<string>(type: "text", nullable: true),
                    prd_revetement = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    prd_matiere = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    prd_matiere_pieds = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    prd_hauteur_pieds = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    prd_type_mousse_assise = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    prd_type_mousse_dossier = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    prd_densite_assise = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    prd_densite_dossier = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    prd_poids_colis = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    prd_made_in_france = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_produit_prd", x => x.prd_id);
                    table.ForeignKey(
                        name: "fk_produit_categorie",
                        column: x => x.ctg_id,
                        principalTable: "t_e_categorie_ctg",
                        principalColumn: "ctg_id");
                    table.ForeignKey(
                        name: "fk_produit_collection",
                        column: x => x.cln_id,
                        principalTable: "t_e_collection_cln",
                        principalColumn: "cln_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_commande_cmd",
                columns: table => new
                {
                    cmd_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    eta_id = table.Column<int>(type: "integer", nullable: false),
                    clt_id = table.Column<int>(type: "integer", nullable: false),
                    adr_id = table.Column<int>(type: "integer", nullable: false),
                    cmd_express = table.Column<bool>(type: "boolean", nullable: false),
                    cmd_collect = table.Column<bool>(type: "boolean", nullable: false),
                    cmd_points_fidelite_utilises = table.Column<int>(type: "integer", nullable: false),
                    cmd_instructions = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_commande_cmd", x => x.cmd_id);
                    table.CheckConstraint("CK_cmd_point_fidelite_utilises", "cmd_points_fidelite_utilises >= 0");
                    table.ForeignKey(
                        name: "fk_commande_adresse",
                        column: x => x.adr_id,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id");
                    table.ForeignKey(
                        name: "fk_commande_client",
                        column: x => x.clt_id,
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_id");
                    table.ForeignKey(
                        name: "fk_commande_etat",
                        column: x => x.eta_id,
                        principalTable: "t_e_etat_eta",
                        principalColumn: "eta_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_variante_vrt",
                columns: table => new
                {
                    vrt_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clr_id = table.Column<int>(type: "integer", nullable: false),
                    prd_id = table.Column<int>(type: "integer", nullable: false),
                    vrt_prix = table.Column<decimal>(type: "numeric(38,17)", nullable: false),
                    vrt_promo = table.Column<decimal>(type: "numeric(4,2)", nullable: false),
                    vrt_stock = table.Column<int>(type: "integer", nullable: false),
                    vrt_date_creation = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_variante_vrt", x => x.vrt_id);
                    table.CheckConstraint("CK_prd_stock", "vrt_stock >= 0");
                    table.CheckConstraint("CK_vrt_prix", "vrt_prix >= 0");
                    table.CheckConstraint("CK_vrt_promo", "vrt_promo between 0.01 and 1");
                    table.ForeignKey(
                        name: "fk_variante_couleur",
                        column: x => x.clr_id,
                        principalTable: "t_e_couleur_clr",
                        principalColumn: "clr_id");
                    table.ForeignKey(
                        name: "fk_variante_produit",
                        column: x => x.prd_id,
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_id");
                });

            migrationBuilder.CreateTable(
                name: "t_j_prd_lst",
                columns: table => new
                {
                    prd_id = table.Column<int>(type: "integer", nullable: false),
                    lst_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_prd_lst", x => new { x.prd_id, x.lst_id });
                    table.ForeignKey(
                        name: "fk_produitliste_liste",
                        column: x => x.lst_id,
                        principalTable: "t_e_liste_lst",
                        principalColumn: "lst_id");
                    table.ForeignKey(
                        name: "fk_produitliste_produit",
                        column: x => x.prd_id,
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_avis_avi",
                columns: table => new
                {
                    avi_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vrt_id = table.Column<int>(type: "integer", nullable: false),
                    clt_id = table.Column<int>(type: "integer", nullable: false),
                    avi_titre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    avi_texte = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    avi_note = table.Column<int>(type: "integer", nullable: false),
                    avi_date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_avis_avi", x => x.avi_id);
                    table.CheckConstraint("CK_avi_note", "avi_note between 1 and 5");
                    table.ForeignKey(
                        name: "fk_avis_client",
                        column: x => x.clt_id,
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_id");
                    table.ForeignKey(
                        name: "fk_avis_variante",
                        column: x => x.vrt_id,
                        principalTable: "t_e_variante_vrt",
                        principalColumn: "vrt_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_ligne_commande_lcm",
                columns: table => new
                {
                    lcm_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vrt_id = table.Column<int>(type: "integer", nullable: false),
                    cmd_id = table.Column<int>(type: "integer", nullable: false),
                    lcm_quantite = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_ligne_commande_lcm", x => x.lcm_id);
                    table.CheckConstraint("CK_lcm_quantite", "lcm_quantite >= 1");
                    table.ForeignKey(
                        name: "fk_lignecommande_commande",
                        column: x => x.cmd_id,
                        principalTable: "t_e_commande_cmd",
                        principalColumn: "cmd_id");
                    table.ForeignKey(
                        name: "fk_lignecommande_variante",
                        column: x => x.vrt_id,
                        principalTable: "t_e_variante_vrt",
                        principalColumn: "vrt_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_ligne_panier_lpn",
                columns: table => new
                {
                    lpn_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clt_id = table.Column<int>(type: "integer", nullable: false),
                    vrt_id = table.Column<int>(type: "integer", nullable: false),
                    lpn_quantite = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_ligne_panier_lpn", x => x.lpn_id);
                    table.CheckConstraint("CK_lpn_quantite", "lpn_quantite >= 1");
                    table.ForeignKey(
                        name: "fk_ligne_panier_panier",
                        column: x => x.clt_id,
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_id");
                    table.ForeignKey(
                        name: "fk_ligne_panier_variante",
                        column: x => x.vrt_id,
                        principalTable: "t_e_variante_vrt",
                        principalColumn: "vrt_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_photo_pht",
                columns: table => new
                {
                    pht_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ctg_id = table.Column<int>(type: "integer", nullable: true),
                    avi_id = table.Column<int>(type: "integer", nullable: true),
                    vrt_id = table.Column<int>(type: "integer", nullable: true),
                    pht_chemin = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_photo_pht", x => x.pht_id);
                    table.ForeignKey(
                        name: "fk_photo_avis",
                        column: x => x.avi_id,
                        principalTable: "t_e_avis_avi",
                        principalColumn: "avi_id");
                    table.ForeignKey(
                        name: "fk_photo_categorie",
                        column: x => x.ctg_id,
                        principalTable: "t_e_categorie_ctg",
                        principalColumn: "ctg_id");
                    table.ForeignKey(
                        name: "fk_photo_variante",
                        column: x => x.vrt_id,
                        principalTable: "t_e_variante_vrt",
                        principalColumn: "vrt_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_avis_avi_clt_id",
                table: "t_e_avis_avi",
                column: "clt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_avis_avi_vrt_id",
                table: "t_e_avis_avi",
                column: "vrt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_cartebancaire_cbr_clt_id",
                table: "t_e_cartebancaire_cbr",
                column: "clt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_categorie_ctg_ctg_parent_id",
                table: "t_e_categorie_ctg",
                column: "ctg_parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_client_clt_clt_email",
                table: "t_e_client_clt",
                column: "clt_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_adr_id",
                table: "t_e_commande_cmd",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_clt_id",
                table: "t_e_commande_cmd",
                column: "clt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_eta_id",
                table: "t_e_commande_cmd",
                column: "eta_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_couleur_clr_clr_libelle",
                table: "t_e_couleur_clr",
                column: "clr_libelle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_ligne_commande_lcm_cmd_id",
                table: "t_e_ligne_commande_lcm",
                column: "cmd_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_ligne_commande_lcm_vrt_id",
                table: "t_e_ligne_commande_lcm",
                column: "vrt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_ligne_panier_lpn_clt_id",
                table: "t_e_ligne_panier_lpn",
                column: "clt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_ligne_panier_lpn_vrt_id",
                table: "t_e_ligne_panier_lpn",
                column: "vrt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_liste_lst_clt_id",
                table: "t_e_liste_lst",
                column: "clt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_photo_pht_avi_id",
                table: "t_e_photo_pht",
                column: "avi_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_photo_pht_ctg_id",
                table: "t_e_photo_pht",
                column: "ctg_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_photo_pht_vrt_id",
                table: "t_e_photo_pht",
                column: "vrt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_produit_prd_cln_id",
                table: "t_e_produit_prd",
                column: "cln_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_produit_prd_ctg_id",
                table: "t_e_produit_prd",
                column: "ctg_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_variante_vrt_clr_id",
                table: "t_e_variante_vrt",
                column: "clr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_variante_vrt_prd_id",
                table: "t_e_variante_vrt",
                column: "prd_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_adresse_livraison_adl_adr_id",
                table: "t_j_adresse_livraison_adl",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_prd_lst_lst_id",
                table: "t_j_prd_lst",
                column: "lst_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_cartebancaire_cbr");

            migrationBuilder.DropTable(
                name: "t_e_ligne_commande_lcm");

            migrationBuilder.DropTable(
                name: "t_e_ligne_panier_lpn");

            migrationBuilder.DropTable(
                name: "t_e_photo_pht");

            migrationBuilder.DropTable(
                name: "t_j_adresse_livraison_adl");

            migrationBuilder.DropTable(
                name: "t_j_prd_lst");

            migrationBuilder.DropTable(
                name: "t_e_commande_cmd");

            migrationBuilder.DropTable(
                name: "t_e_avis_avi");

            migrationBuilder.DropTable(
                name: "t_e_liste_lst");

            migrationBuilder.DropTable(
                name: "t_e_adresse_adr");

            migrationBuilder.DropTable(
                name: "t_e_etat_eta");

            migrationBuilder.DropTable(
                name: "t_e_variante_vrt");

            migrationBuilder.DropTable(
                name: "t_e_client_clt");

            migrationBuilder.DropTable(
                name: "t_e_couleur_clr");

            migrationBuilder.DropTable(
                name: "t_e_produit_prd");

            migrationBuilder.DropTable(
                name: "t_e_categorie_ctg");

            migrationBuilder.DropTable(
                name: "t_e_collection_cln");
        }
    }
}
