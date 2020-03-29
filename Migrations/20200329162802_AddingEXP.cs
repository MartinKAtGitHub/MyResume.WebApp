using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResume.WebApp.Migrations
{
    public partial class AddingEXP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 30, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiences_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperiencePoint",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ExperienceId = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperiencePoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperiencePoint_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperiencePointDescription",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ExperiencePointId = table.Column<string>(nullable: false),
                    Discription = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperiencePointDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperiencePointDescription_ExperiencePoint_ExperiencePointId",
                        column: x => x.ExperiencePointId,
                        principalTable: "ExperiencePoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExperiencePoint_ExperienceId",
                table: "ExperiencePoint",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperiencePointDescription_ExperiencePointId",
                table: "ExperiencePointDescription",
                column: "ExperiencePointId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_UserId",
                table: "Experiences",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExperiencePointDescription");

            migrationBuilder.DropTable(
                name: "ExperiencePoint");

            migrationBuilder.DropTable(
                name: "Experiences");
        }
    }
}
