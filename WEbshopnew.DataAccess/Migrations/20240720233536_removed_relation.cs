using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEbshopnew.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removed_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Catagories_CatagoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CatagoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CatagoryId",
                table: "Products",
                column: "CatagoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Catagories_CatagoryId",
                table: "Products",
                column: "CatagoryId",
                principalTable: "Catagories",
                principalColumn: "CatagoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
