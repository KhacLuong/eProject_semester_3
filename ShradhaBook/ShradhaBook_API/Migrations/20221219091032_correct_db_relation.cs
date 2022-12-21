using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShradhaBook_API.Migrations
{
    public partial class correct_db_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "UserInfo");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "UserInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo",
                column: "UserId");
        }
    }
}
