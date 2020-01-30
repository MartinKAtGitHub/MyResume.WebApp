using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResume.WebApp.Migrations
{
    public partial class AddingAchievementAndUserInfoTbls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserResumePages");

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    MainText = table.Column<string>(nullable: true),
                    EnableComments = table.Column<bool>(nullable: false),
                    EnableRating = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userInformation",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    AvatarImgPath = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    MainText = table.Column<string>(nullable: true),
                    AvailableForContact = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userInformation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "userInformation");

            migrationBuilder.CreateTable(
                name: "UserResumePages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EnableComments = table.Column<bool>(type: "bit", nullable: false),
                    EnableRating = table.Column<bool>(type: "bit", nullable: false),
                    MainText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summery = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserResumePages", x => x.Id);
                });
        }
    }
}
