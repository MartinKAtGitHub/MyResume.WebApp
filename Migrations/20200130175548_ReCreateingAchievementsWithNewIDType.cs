using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResume.WebApp.Migrations
{
    public partial class ReCreateingAchievementsWithNewIDType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    MainText = table.Column<string>(nullable: true),
                    OrderPosition = table.Column<int>(nullable: false),
                    EnableComments = table.Column<bool>(nullable: false),
                    EnableRating = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");
        }
    }
}
