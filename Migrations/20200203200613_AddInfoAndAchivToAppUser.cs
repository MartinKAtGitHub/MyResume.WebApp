using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResume.WebApp.Migrations
{
    public partial class AddInfoAndAchivToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserInformationId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Achievements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserInformationId",
                table: "AspNetUsers",
                column: "UserInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_ApplicationUserId",
                table: "Achievements",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_AspNetUsers_ApplicationUserId",
                table: "Achievements",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserInformation_UserInformationId",
                table: "AspNetUsers",
                column: "UserInformationId",
                principalTable: "UserInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_AspNetUsers_ApplicationUserId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserInformation_UserInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_ApplicationUserId",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "UserInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Achievements");
        }
    }
}
