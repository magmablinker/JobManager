using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobManager.Migrations
{
    public partial class FixShit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_category_jobOffer_categorieIds",
                table: "category");

            migrationBuilder.DropForeignKey(
                name: "FK_jobOffer_user_employerId",
                table: "jobOffer");

            migrationBuilder.DropIndex(
                name: "IX_category_categorieIds",
                table: "category");

            migrationBuilder.DropColumn(
                name: "categorieIds",
                table: "category");

            migrationBuilder.RenameColumn(
                name: "employerId",
                table: "jobOffer",
                newName: "EmployerId");

            migrationBuilder.RenameIndex(
                name: "IX_jobOffer_employerId",
                table: "jobOffer",
                newName: "IX_jobOffer_EmployerId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "EmployerId",
                table: "jobOffer",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(16)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DbJobOfferCategories",
                columns: table => new
                {
                    JobOfferId = table.Column<byte[]>(nullable: false),
                    CategoryId = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbJobOfferCategories", x => new { x.CategoryId, x.JobOfferId });
                    table.ForeignKey(
                        name: "FK_DbJobOfferCategories_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbJobOfferCategories_jobOffer_JobOfferId",
                        column: x => x.JobOfferId,
                        principalTable: "jobOffer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbJobOfferCategories_JobOfferId",
                table: "DbJobOfferCategories",
                column: "JobOfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_jobOffer_user_EmployerId",
                table: "jobOffer",
                column: "EmployerId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_jobOffer_user_EmployerId",
                table: "jobOffer");

            migrationBuilder.DropTable(
                name: "DbJobOfferCategories");

            migrationBuilder.RenameColumn(
                name: "EmployerId",
                table: "jobOffer",
                newName: "employerId");

            migrationBuilder.RenameIndex(
                name: "IX_jobOffer_EmployerId",
                table: "jobOffer",
                newName: "IX_jobOffer_employerId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "employerId",
                table: "jobOffer",
                type: "varbinary(16)",
                nullable: true,
                oldClrType: typeof(byte[]));

            migrationBuilder.AddColumn<byte[]>(
                name: "categorieIds",
                table: "category",
                type: "varbinary(16)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_category_categorieIds",
                table: "category",
                column: "categorieIds");

            migrationBuilder.AddForeignKey(
                name: "FK_category_jobOffer_categorieIds",
                table: "category",
                column: "categorieIds",
                principalTable: "jobOffer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_jobOffer_user_employerId",
                table: "jobOffer",
                column: "employerId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
