using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTherapistOwnedValueObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SurchargePercent",
                table: "BookingLines");

            migrationBuilder.AddColumn<Guid>(
                name: "AddOnId",
                table: "BookingLines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AddOn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddOn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    MaxParticipants = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingLines_AddOnId",
                table: "BookingLines",
                column: "AddOnId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingLines_AddOn_AddOnId",
                table: "BookingLines",
                column: "AddOnId",
                principalTable: "AddOn",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingLines_AddOn_AddOnId",
                table: "BookingLines");

            migrationBuilder.DropTable(
                name: "AddOn");

            migrationBuilder.DropTable(
                name: "TreatmentType");

            migrationBuilder.DropIndex(
                name: "IX_BookingLines_AddOnId",
                table: "BookingLines");

            migrationBuilder.DropColumn(
                name: "AddOnId",
                table: "BookingLines");

            migrationBuilder.AddColumn<decimal>(
                name: "SurchargePercent",
                table: "BookingLines",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
