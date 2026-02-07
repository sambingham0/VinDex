using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VinDex.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Vin = table.Column<string>(type: "text", nullable: false),
                    Make = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Trim = table.Column<string>(type: "text", nullable: true),
                    Series = table.Column<string>(type: "text", nullable: true),
                    BodyStyle = table.Column<string>(type: "text", nullable: true),
                    VehicleType = table.Column<string>(type: "text", nullable: true),
                    Doors = table.Column<string>(type: "text", nullable: true),
                    EngineCylinders = table.Column<int>(type: "integer", nullable: true),
                    Horsepower = table.Column<int>(type: "integer", nullable: true),
                    DisplacementLiters = table.Column<double>(type: "double precision", nullable: true),
                    FuelType = table.Column<string>(type: "text", nullable: true),
                    TransmissionStyle = table.Column<string>(type: "text", nullable: true),
                    DriveType = table.Column<string>(type: "text", nullable: true),
                    Axles = table.Column<string>(type: "text", nullable: true),
                    Gvwr = table.Column<string>(type: "text", nullable: true),
                    PlantCountry = table.Column<string>(type: "text", nullable: true),
                    PlantState = table.Column<string>(type: "text", nullable: true),
                    PlantCity = table.Column<string>(type: "text", nullable: true),
                    Manufacturer = table.Column<string>(type: "text", nullable: true),
                    PlantCompanyName = table.Column<string>(type: "text", nullable: true),
                    Abs = table.Column<string>(type: "text", nullable: true),
                    Esc = table.Column<string>(type: "text", nullable: true),
                    AirBagFront = table.Column<string>(type: "text", nullable: true),
                    AirBagSide = table.Column<string>(type: "text", nullable: true),
                    AirBagCurtain = table.Column<string>(type: "text", nullable: true),
                    LaneKeepSystem = table.Column<string>(type: "text", nullable: true),
                    BlindSpotMonitoring = table.Column<string>(type: "text", nullable: true),
                    Tpms = table.Column<string>(type: "text", nullable: true),
                    DaytimeRunningLights = table.Column<string>(type: "text", nullable: true),
                    KeylessIgnition = table.Column<string>(type: "text", nullable: true),
                    AdaptiveCruiseControl = table.Column<string>(type: "text", nullable: true),
                    LaneDepartureWarning = table.Column<string>(type: "text", nullable: true),
                    ParkAssist = table.Column<string>(type: "text", nullable: true),
                    AutomaticPedestrianAlertingSound = table.Column<string>(type: "text", nullable: true),
                    BlindSpotIntervention = table.Column<string>(type: "text", nullable: true),
                    WheelBase = table.Column<string>(type: "text", nullable: true),
                    WheelSizeFront = table.Column<string>(type: "text", nullable: true),
                    WheelSizeRear = table.Column<string>(type: "text", nullable: true),
                    CurbWeight = table.Column<string>(type: "text", nullable: true),
                    TopSpeedMph = table.Column<string>(type: "text", nullable: true),
                    ErrorCode = table.Column<string>(type: "text", nullable: true),
                    ErrorText = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Vin",
                table: "Vehicles",
                column: "Vin",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
