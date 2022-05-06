using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobManager.Migrations
{
    public partial class AddJobOfferAndCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "jobOffer",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PayPerHour = table.Column<float>(nullable: false),
                    employerId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_jobOffer_user_employerId",
                        column: x => x.employerId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    categorieIds = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_category_jobOffer_categorieIds",
                        column: x => x.categorieIds,
                        principalTable: "jobOffer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_category_categorieIds",
                table: "category",
                column: "categorieIds");

            migrationBuilder.CreateIndex(
                name: "IX_jobOffer_employerId",
                table: "jobOffer",
                column: "employerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "jobOffer");
        }
    }
}
