using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTherapistOwnedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_CampaignDiscount_CampaignDiscountId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Customers_CustomerId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Customers_CustomerId1",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingLines_Booking_BookingId",
                table: "BookingLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Clinics");

            migrationBuilder.RenameTable(
                name: "Booking",
                newName: "Bookings");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Therapists",
                newName: "Name_LastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Therapists",
                newName: "Name_FirstName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Therapists",
                newName: "Email_Value");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Clinics",
                newName: "Phone_Value");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Bookings",
                newName: "TimeSlot_StartTime");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Bookings",
                newName: "TimeSlot_EndTime");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_CustomerId1",
                table: "Bookings",
                newName: "IX_Bookings_CustomerId1");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_CustomerId",
                table: "Bookings",
                newName: "IX_Bookings_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_CampaignDiscountId",
                table: "Bookings",
                newName: "IX_Bookings_CampaignDiscountId");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Clinics",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_PostalCode",
                table: "Clinics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Clinics",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingLines_Bookings_BookingId",
                table: "BookingLines",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_CampaignDiscount_CampaignDiscountId",
                table: "Bookings",
                column: "CampaignDiscountId",
                principalTable: "CampaignDiscount",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomerId",
                table: "Bookings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomerId1",
                table: "Bookings",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingLines_Bookings_BookingId",
                table: "BookingLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_CampaignDiscount_CampaignDiscountId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerId1",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "Address_PostalCode",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Clinics");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "Booking");

            migrationBuilder.RenameColumn(
                name: "Name_LastName",
                table: "Therapists",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name_FirstName",
                table: "Therapists",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Email_Value",
                table: "Therapists",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Phone_Value",
                table: "Clinics",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "TimeSlot_StartTime",
                table: "Booking",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "TimeSlot_EndTime",
                table: "Booking",
                newName: "EndTime");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_CustomerId1",
                table: "Booking",
                newName: "IX_Booking_CustomerId1");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_CustomerId",
                table: "Booking",
                newName: "IX_Booking_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_CampaignDiscountId",
                table: "Booking",
                newName: "IX_Booking_CampaignDiscountId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Clinics",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_CampaignDiscount_CampaignDiscountId",
                table: "Booking",
                column: "CampaignDiscountId",
                principalTable: "CampaignDiscount",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Customers_CustomerId",
                table: "Booking",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Customers_CustomerId1",
                table: "Booking",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingLines_Booking_BookingId",
                table: "BookingLines",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
