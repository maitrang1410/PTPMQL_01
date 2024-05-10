using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMovie.Migrations
{
    /// <inheritdoc />
    public partial class DaiLy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HTPPTempId1",
                table: "DaiLy",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DaiLy_HTPPTempId1",
                table: "DaiLy",
                column: "HTPPTempId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DaiLy_Hethongphanphois_HTPPTempId1",
                table: "DaiLy",
                column: "HTPPTempId1",
                principalTable: "Hethongphanphois",
                principalColumn: "MaHTPP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaiLy_Hethongphanphois_HTPPTempId1",
                table: "DaiLy");

            migrationBuilder.DropIndex(
                name: "IX_DaiLy_HTPPTempId1",
                table: "DaiLy");

            migrationBuilder.DropColumn(
                name: "HTPPTempId1",
                table: "DaiLy");
        }
    }
}
