using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResume.WebApp.Migrations
{
    public partial class DropAchevements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userInformation",
                table: "userInformation");

            migrationBuilder.RenameTable(
                name: "userInformation",
                newName: "UserInformation");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "UserInformation",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInformation",
                table: "UserInformation",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInformation",
                table: "UserInformation");

            migrationBuilder.RenameTable(
                name: "UserInformation",
                newName: "userInformation");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "userInformation",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddPrimaryKey(
                name: "PK_userInformation",
                table: "userInformation",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EnableComments = table.Column<bool>(type: "bit", nullable: false),
                    EnableRating = table.Column<bool>(type: "bit", nullable: false),
                    MainText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });
        }
    }
}
