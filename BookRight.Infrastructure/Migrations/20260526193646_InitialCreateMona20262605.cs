using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateMona20262605 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_CampaignDiscount_CampaignDiscountId",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TreatmentType",
                table: "TreatmentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CampaignDiscount",
                table: "CampaignDiscount");

            migrationBuilder.RenameTable(
                name: "TreatmentType",
                newName: "TreatmentTypes");

            migrationBuilder.RenameTable(
                name: "CampaignDiscount",
                newName: "CampaignDiscounts");

            migrationBuilder.AddColumn<string>(
                name: "Authorization_Number",
                table: "Therapists",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Authorization_Type",
                table: "Therapists",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ClinicId",
                table: "Therapists",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Address_PostalCode",
                table: "Clinics",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "TherapistId",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "CanBeCombined",
                table: "TreatmentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TreatmentTypes",
                table: "TreatmentTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CampaignDiscounts",
                table: "CampaignDiscounts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ClinicOpeningHour",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClinicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    OpenTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    CloseTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicOpeningHour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicOpeningHour_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TherapistSchedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TherapistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClinicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    IsWorking = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapistSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TherapistSchedule_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TherapistTreatmentType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TherapistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreatmentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapistTreatmentType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TherapistTreatmentType_Therapists_TherapistId",
                        column: x => x.TherapistId,
                        principalTable: "Therapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TherapistSchedule_BlockedSlots",
                columns: table => new
                {
                    TherapistScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapistSchedule_BlockedSlots", x => new { x.TherapistScheduleId, x.Id });
                    table.ForeignKey(
                        name: "FK_TherapistSchedule_BlockedSlots_TherapistSchedule_TherapistScheduleId",
                        column: x => x.TherapistScheduleId,
                        principalTable: "TherapistSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Therapists_ClinicId",
                table: "Therapists",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TherapistId",
                table: "Bookings",
                column: "TherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicOpeningHour_ClinicId",
                table: "ClinicOpeningHour",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_TherapistSchedule_ClinicId",
                table: "TherapistSchedule",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_TherapistTreatmentType_TherapistId",
                table: "TherapistTreatmentType",
                column: "TherapistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_CampaignDiscounts_CampaignDiscountId",
                table: "Bookings",
                column: "CampaignDiscountId",
                principalTable: "CampaignDiscounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Therapists_TherapistId",
                table: "Bookings",
                column: "TherapistId",
                principalTable: "Therapists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Bookings_CampaignDiscounts_CampaignDiscountId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Therapists_TherapistId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Therapists_Clinics_ClinicId",
                table: "Therapists");

            migrationBuilder.DropTable(
                name: "ClinicOpeningHour");

            migrationBuilder.DropTable(
                name: "TherapistSchedule_BlockedSlots");

            migrationBuilder.DropTable(
                name: "TherapistTreatmentType");

            migrationBuilder.DropTable(
                name: "TherapistSchedule");

            migrationBuilder.DropIndex(
                name: "IX_Therapists_ClinicId",
                table: "Therapists");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_TherapistId",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TreatmentTypes",
                table: "TreatmentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CampaignDiscounts",
                table: "CampaignDiscounts");

            migrationBuilder.DropColumn(
                name: "Authorization_Number",
                table: "Therapists");

            migrationBuilder.DropColumn(
                name: "Authorization_Type",
                table: "Therapists");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "Therapists");

            migrationBuilder.DropColumn(
                name: "TherapistId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CanBeCombined",
                table: "TreatmentTypes");

            migrationBuilder.RenameTable(
                name: "TreatmentTypes",
                newName: "TreatmentType");

            migrationBuilder.RenameTable(
                name: "CampaignDiscounts",
                newName: "CampaignDiscount");

            migrationBuilder.AlterColumn<string>(
                name: "Address_PostalCode",
                table: "Clinics",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TreatmentType",
                table: "TreatmentType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CampaignDiscount",
                table: "CampaignDiscount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_CampaignDiscount_CampaignDiscountId",
                table: "Bookings",
                column: "CampaignDiscountId",
                principalTable: "CampaignDiscount",
                principalColumn: "Id");
        }
    }
}
