using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResume.WebApp.Migrations
{
    public partial class UpdatedAchievmentDataAnon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnableComments",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "EnableRating",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "MainText",
                table: "Achievements");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Achievements",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(35)",
                oldMaxLength: 35);

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Achievements",
                maxLength: 600,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(380)",
                oldMaxLength: 380);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Achievements",
                type: "nvarchar(35)",
                maxLength: 35,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Achievements",
                type: "nvarchar(380)",
                maxLength: 380,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 600);

            migrationBuilder.AddColumn<bool>(
                name: "EnableComments",
                table: "Achievements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnableRating",
                table: "Achievements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MainText",
                table: "Achievements",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: true);
        }
    }
}
