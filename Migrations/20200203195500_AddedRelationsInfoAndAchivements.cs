using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResume.WebApp.Migrations
{
    public partial class AddedRelationsInfoAndAchivements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserInformation",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "UserInformation",
                maxLength: 380,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "UserInformation",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LasttName",
                table: "UserInformation",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddelName",
                table: "UserInformation",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "UserInformation",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Achievements",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Achievements",
                maxLength: 380,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MainText",
                table: "Achievements",
                maxLength: 900,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserInformationId",
                table: "Achievements",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_UserInformationId",
                table: "Achievements",
                column: "UserInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_UserInformation_UserInformationId",
                table: "Achievements",
                column: "UserInformationId",
                principalTable: "UserInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_UserInformation_UserInformationId",
                table: "Achievements");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_UserInformationId",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "LasttName",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "MiddelName",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "UserInformationId",
                table: "Achievements");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserInformation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "UserInformation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 380,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Achievements",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Achievements",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 380,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MainText",
                table: "Achievements",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 900,
                oldNullable: true);
        }
    }
}
