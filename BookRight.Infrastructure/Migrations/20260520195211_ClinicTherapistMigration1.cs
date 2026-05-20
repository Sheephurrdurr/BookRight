using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClinicTherapistMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClinicId",
                table: "Therapists",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Therapists_ClinicId",
                table: "Therapists",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Therapists_Clinics_ClinicId",
                table: "Therapists",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Therapists_Clinics_ClinicId",
                table: "Therapists");

            migrationBuilder.DropIndex(
                name: "IX_Therapists_ClinicId",
                table: "Therapists");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "Therapists");
        }
    }
}
