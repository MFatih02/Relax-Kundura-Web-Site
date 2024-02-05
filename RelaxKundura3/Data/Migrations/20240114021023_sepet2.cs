using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelaxKundura3.Data.Migrations
{
    /// <inheritdoc />
    public partial class sepet2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SepetUruns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sepets",
                table: "Sepets");

            migrationBuilder.RenameTable(
                name: "Sepets",
                newName: "Sepetts");

            migrationBuilder.RenameColumn(
                name: "KullaniciId",
                table: "Sepetts",
                newName: "UrunImage");

            migrationBuilder.AddColumn<string>(
                name: "UrunAdi",
                table: "Sepetts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "UrunFiyat",
                table: "Sepetts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "UrunId",
                table: "Sepetts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sepetts",
                table: "Sepetts",
                column: "SepetId");

            migrationBuilder.CreateTable(
                name: "Sepetlers",
                columns: table => new
                {
                    SepetlerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SepetToplamFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sepetlers", x => x.SepetlerId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sepetlers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sepetts",
                table: "Sepetts");

            migrationBuilder.DropColumn(
                name: "UrunAdi",
                table: "Sepetts");

            migrationBuilder.DropColumn(
                name: "UrunFiyat",
                table: "Sepetts");

            migrationBuilder.DropColumn(
                name: "UrunId",
                table: "Sepetts");

            migrationBuilder.RenameTable(
                name: "Sepetts",
                newName: "Sepets");

            migrationBuilder.RenameColumn(
                name: "UrunImage",
                table: "Sepets",
                newName: "KullaniciId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sepets",
                table: "Sepets",
                column: "SepetId");

            migrationBuilder.CreateTable(
                name: "SepetUruns",
                columns: table => new
                {
                    SepetUrunId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Adet = table.Column<int>(type: "int", nullable: false),
                    SepetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToplamFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UrunId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SepetUruns", x => x.SepetUrunId);
                    table.ForeignKey(
                        name: "FK_SepetUruns_Sepets_SepetId",
                        column: x => x.SepetId,
                        principalTable: "Sepets",
                        principalColumn: "SepetId");
                    table.ForeignKey(
                        name: "FK_SepetUruns_Uruns_UrunId1",
                        column: x => x.UrunId1,
                        principalTable: "Uruns",
                        principalColumn: "UrunId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SepetUruns_SepetId",
                table: "SepetUruns",
                column: "SepetId");

            migrationBuilder.CreateIndex(
                name: "IX_SepetUruns_UrunId1",
                table: "SepetUruns",
                column: "UrunId1");
        }
    }
}
