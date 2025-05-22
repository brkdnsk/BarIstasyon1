using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Baristasyon.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ImageUrlFinalFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_CoffeeRecipes_CoffeeRecipeId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CoffeeRecipeId",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "CoffeeRecipes",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CoffeeRecipes");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CoffeeRecipeId",
                table: "Reviews",
                column: "CoffeeRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_CoffeeRecipes_CoffeeRecipeId",
                table: "Reviews",
                column: "CoffeeRecipeId",
                principalTable: "CoffeeRecipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
