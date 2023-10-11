using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net_il_mio_fotoalbum.Migrations
{
    public partial class fixingCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryFoto_Fotos_fotosFotoId",
                table: "CategoryFoto");

            migrationBuilder.RenameColumn(
                name: "fotosFotoId",
                table: "CategoryFoto",
                newName: "FotosFotoId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryFoto_fotosFotoId",
                table: "CategoryFoto",
                newName: "IX_CategoryFoto_FotosFotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryFoto_Fotos_FotosFotoId",
                table: "CategoryFoto",
                column: "FotosFotoId",
                principalTable: "Fotos",
                principalColumn: "FotoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryFoto_Fotos_FotosFotoId",
                table: "CategoryFoto");

            migrationBuilder.RenameColumn(
                name: "FotosFotoId",
                table: "CategoryFoto",
                newName: "fotosFotoId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryFoto_FotosFotoId",
                table: "CategoryFoto",
                newName: "IX_CategoryFoto_fotosFotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryFoto_Fotos_fotosFotoId",
                table: "CategoryFoto",
                column: "fotosFotoId",
                principalTable: "Fotos",
                principalColumn: "FotoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
