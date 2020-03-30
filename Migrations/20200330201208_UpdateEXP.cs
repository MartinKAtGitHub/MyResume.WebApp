using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResume.WebApp.Migrations
{
    public partial class UpdateEXP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_AspNetUsers_UserId",
                table: "Experiences");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_UserId",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Experiences");

            migrationBuilder.AddColumn<Guid>(
                name: "UserInformationId",
                table: "Experiences",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_UserInformationId",
                table: "Experiences",
                column: "UserInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_UserInformation_UserInformationId",
                table: "Experiences",
                column: "UserInformationId",
                principalTable: "UserInformation",
                principalColumn: "UserInformationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_UserInformation_UserInformationId",
                table: "Experiences");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_UserInformationId",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "UserInformationId",
                table: "Experiences");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Experiences",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_UserId",
                table: "Experiences",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_AspNetUsers_UserId",
                table: "Experiences",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
