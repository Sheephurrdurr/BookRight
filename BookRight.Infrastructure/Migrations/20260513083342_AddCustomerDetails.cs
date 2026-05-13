using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerId1",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CustomerId1",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Bookings");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Customers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "HealthNotes",
                table: "Customers",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreferredTherapistId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "HealthNotes",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PreferredTherapistId",
                table: "Customers");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId1",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId1",
                table: "Bookings",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomerId1",
                table: "Bookings",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
