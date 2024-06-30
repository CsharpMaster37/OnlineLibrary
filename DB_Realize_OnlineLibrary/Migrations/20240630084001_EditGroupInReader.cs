using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB_Realize_OnlineLibrary.Migrations
{
    /// <inheritdoc />
    public partial class EditGroupInReader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_GroupReaders_GroupId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_GroupReaders_GroupId",
                table: "AspNetUsers",
                column: "GroupId",
                principalTable: "GroupReaders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_GroupReaders_GroupId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_GroupReaders_GroupId",
                table: "AspNetUsers",
                column: "GroupId",
                principalTable: "GroupReaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
