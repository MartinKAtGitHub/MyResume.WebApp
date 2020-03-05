using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResume.WebApp.Migrations
{
    public partial class AddingGalleryImagePathTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemGalleryImageFilePath",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    GalleryImageFilePath = table.Column<string>(nullable: true),
                    AchievementId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGalleryImageFilePath", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemGalleryImageFilePath_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "AchievementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemGalleryImageFilePath_AchievementId",
                table: "ItemGalleryImageFilePath",
                column: "AchievementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemGalleryImageFilePath");
        }
    }
}
