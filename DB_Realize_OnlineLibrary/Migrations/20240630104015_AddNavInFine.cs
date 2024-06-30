using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB_Realize_OnlineLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddNavInFine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Fines_BookId",
                table: "Fines",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_Books_BookId",
                table: "Fines",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fines_Books_BookId",
                table: "Fines");

            migrationBuilder.DropIndex(
                name: "IX_Fines_BookId",
                table: "Fines");
        }
    }
}
