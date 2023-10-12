using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net_il_mio_fotoalbum.Migrations
{
    public partial class AddedSupportToImageFileFoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Fotos",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageFile",
                table: "Fotos",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "Fotos");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Fotos",
                newName: "Image");
        }
    }
}
