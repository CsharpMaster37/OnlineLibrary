using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB_Realize_OnlineLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusInFine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Fines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Fines");
        }
    }
}
