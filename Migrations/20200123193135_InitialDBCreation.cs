using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResume.WebApp.Migrations
{
    public partial class InitialDBCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserResumePages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    PageTitle = table.Column<string>(nullable: true),
                    Summery = table.Column<string>(nullable: true),
                    MainText = table.Column<string>(nullable: true),
                    EnableComments = table.Column<bool>(nullable: false),
                    EnableRating = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserResumePages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserResumePages");
        }
    }
}
