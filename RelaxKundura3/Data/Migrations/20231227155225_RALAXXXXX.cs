using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelaxKundura3.Data.Migrations
{
    /// <inheritdoc />
    public partial class RALAXXXXX : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoris",
                columns: table => new
                {
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoris", x => x.KategoriId);
                });

            migrationBuilder.CreateTable(
                name: "Uruns",
                columns: table => new
                {
                    UrunId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UrunAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrunKategoriId = table.Column<int>(type: "int", nullable: false),
                    UrunCinsiyet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrunStokDurumu = table.Column<int>(type: "int", nullable: false),
                    UrunFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UrunImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uruns", x => x.UrunId);
                    table.ForeignKey(
                        name: "FK_Uruns_Kategoris_UrunKategoriId",
                        column: x => x.UrunKategoriId,
                        principalTable: "Kategoris",
                        principalColumn: "KategoriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uruns_UrunKategoriId",
                table: "Uruns",
                column: "UrunKategoriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uruns");

            migrationBuilder.DropTable(
                name: "Kategoris");
        }
    }
}
