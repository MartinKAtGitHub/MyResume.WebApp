using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResume.WebApp.Migrations
{
    public partial class AddingRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_AspNetUsers_ApplicationUserId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_UserInformation_UserInformationId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserInformation_UserInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInformation",
                table: "UserInformation");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Achievements",
                table: "Achievements");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_ApplicationUserId",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "UserInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Achievements");

            migrationBuilder.AlterColumn<string>(
                name: "MainText",
                table: "UserInformation",
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserInformationId",
                table: "UserInformation",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UserInformation",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "AchievementId",
                table: "Achievements",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInformation",
                table: "UserInformation",
                column: "UserInformationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Achievements",
                table: "Achievements",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInformation_ApplicationUserId",
                table: "UserInformation",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_UserInformation_UserInformationId",
                table: "Achievements",
                column: "UserInformationId",
                principalTable: "UserInformation",
                principalColumn: "UserInformationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInformation_AspNetUsers_ApplicationUserId",
                table: "UserInformation",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_UserInformation_UserInformationId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInformation_AspNetUsers_ApplicationUserId",
                table: "UserInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInformation",
                table: "UserInformation");

            migrationBuilder.DropIndex(
                name: "IX_UserInformation_ApplicationUserId",
                table: "UserInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Achievements",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "UserInformationId",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "AchievementId",
                table: "Achievements");

            migrationBuilder.AlterColumn<string>(
                name: "MainText",
                table: "UserInformation",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserInformation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserInformation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserInformation",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserInformationId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Achievements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Achievements",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Achievements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInformation",
                table: "UserInformation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Achievements",
                table: "Achievements",
                column: "Id");

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
                name: "FK_Achievements_UserInformation_UserInformationId",
                table: "Achievements",
                column: "UserInformationId",
                principalTable: "UserInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserInformation_UserInformationId",
                table: "AspNetUsers",
                column: "UserInformationId",
                principalTable: "UserInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
