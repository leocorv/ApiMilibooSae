using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAE_S4_MILIBOO.Migrations
{
    public partial class CreationDBMilibooDimension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("CREATE TYPE tdimensions AS ( x integer, y integer, z integer)");

            migrationBuilder.Sql("ALTER TABLE t_e_produit_prd ADD prd_dim_totale tdimensions NULL");
            migrationBuilder.Sql("ALTER TABLE t_e_produit_prd ADD prd_dim_dossier tdimensions NULL");
            migrationBuilder.Sql("ALTER TABLE t_e_produit_prd ADD prd_dim_assise tdimensions NULL");
            migrationBuilder.Sql("ALTER TABLE t_e_produit_prd ADD prd_dim_colis tdimensions NULL");
            migrationBuilder.Sql("ALTER TABLE t_e_produit_prd ADD prd_dim_deplie tdimensions NULL");
            migrationBuilder.Sql("ALTER TABLE t_e_produit_prd ADD prd_dim_accoudoir tdimensions NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE t_e_produit_prd DROP COLUMN prd_dim_totale");
            migrationBuilder.Sql("ALTER TABLE t_e_produit_prd DROP COLUMN prd_dim_dossier");
            migrationBuilder.Sql("ALTER TABLE t_e_produit_prd DROP COLUMN prd_dim_assise");
            migrationBuilder.Sql("ALTER TABLE t_e_produit_prd DROP COLUMN prd_dim_colis");
            migrationBuilder.Sql("ALTER TABLE t_e_produit_prd DROP COLUMN prd_dim_deplie");
            migrationBuilder.Sql("ALTER TABLE t_e_produit_prd DROP COLUMN prd_dim_accoudoir");
        }
    }
}
