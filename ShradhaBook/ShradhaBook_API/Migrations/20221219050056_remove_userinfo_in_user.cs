using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShradhaBook_API.Migrations
{
    public partial class remove_userinfo_in_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserInfo_UserInfoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserInfoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfo_Users_UserId",
                table: "UserInfo",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_Users_UserId",
                table: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserInfo");

            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserInfoId",
                table: "Users",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserInfo_UserInfoId",
                table: "Users",
                column: "UserInfoId",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
