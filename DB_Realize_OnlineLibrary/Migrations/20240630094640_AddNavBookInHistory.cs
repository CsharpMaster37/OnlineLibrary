using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB_Realize_OnlineLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddNavBookInHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_History_BookId",
                table: "History",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Books_BookId",
                table: "History",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Books_BookId",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_BookId",
                table: "History");
        }
    }
}
