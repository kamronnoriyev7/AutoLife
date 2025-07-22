using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoLife.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    UzName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    RuName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    EnName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.BasaEntityId);
                });

            migrationBuilder.CreateTable(
                name: "FuelType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IdentityUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    UzName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    RuName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    EnName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.BasaEntityId);
                    table.ForeignKey(
                        name: "FK_Regions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "BasaEntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FuelSubType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FuelTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelSubType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuelSubType_FuelType_FuelTypeId",
                        column: x => x.FuelTypeId,
                        principalTable: "FuelType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppFeedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFeedbacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true),
                    LogoUrl = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.BasaEntityId);
                    table.ForeignKey(
                        name: "FK_Companies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    UzName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    RuName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    EnName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.BasaEntityId);
                    table.ForeignKey(
                        name: "FK_Districts_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "BasaEntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Brand = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NumberPlate = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FuelTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyBasaEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Companies_CompanyBasaEntityId",
                        column: x => x.CompanyBasaEntityId,
                        principalTable: "Companies",
                        principalColumn: "BasaEntityId");
                    table.ForeignKey(
                        name: "FK_Vehicles_FuelType_FuelTypeId",
                        column: x => x.FuelTypeId,
                        principalTable: "FuelType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Orientation = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Street = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    HouseNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: true),
                    DistrictId = table.Column<Guid>(type: "uuid", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "BasaEntityId");
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "BasaEntityId");
                    table.ForeignKey(
                        name: "FK_Addresses_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "BasaEntityId");
                    table.ForeignKey(
                        name: "FK_Addresses_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "BasaEntityId");
                    table.ForeignKey(
                        name: "FK_Addresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "FuelStations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    OperatorName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    FuelTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    FuelSubTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    CompanyBasaEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuelStations_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FuelStations_Companies_CompanyBasaEntityId",
                        column: x => x.CompanyBasaEntityId,
                        principalTable: "Companies",
                        principalColumn: "BasaEntityId");
                    table.ForeignKey(
                        name: "FK_FuelStations_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "BasaEntityId");
                    table.ForeignKey(
                        name: "FK_FuelStations_FuelSubType_FuelSubTypeId",
                        column: x => x.FuelSubTypeId,
                        principalTable: "FuelSubType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FuelStations_FuelType_FuelTypeId",
                        column: x => x.FuelTypeId,
                        principalTable: "FuelType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FuelStations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GeoLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeoLocations_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parkings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    HourlyRate = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    DailyRate = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    IsFree = table.Column<bool>(type: "boolean", nullable: false),
                    HasCameras = table.Column<bool>(type: "boolean", nullable: false),
                    IsCovered = table.Column<bool>(type: "boolean", nullable: false),
                    TotalSpaces = table.Column<int>(type: "integer", nullable: false),
                    AvailableSpaces = table.Column<int>(type: "integer", nullable: false),
                    OpeningTime = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ClosingTime = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    IsPreBookingAllowed = table.Column<bool>(type: "boolean", nullable: true),
                    AverageRating = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parkings_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parkings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "BasaEntityId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Parkings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCenters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ServiceType = table.Column<int>(type: "integer", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCenters_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ServiceCenters_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "BasaEntityId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ServiceCenters_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "FuelPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FuelStationId = table.Column<Guid>(type: "uuid", nullable: false),
                    FuelSubTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuelPrices_FuelStations_FuelStationId",
                        column: x => x.FuelStationId,
                        principalTable: "FuelStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FuelPrices_FuelSubType_FuelSubTypeId",
                        column: x => x.FuelSubTypeId,
                        principalTable: "FuelSubType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParkingPrices",
                columns: table => new
                {
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParkingId = table.Column<Guid>(type: "uuid", nullable: false),
                    FromTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    ToTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingPrices", x => x.BasaEntityId);
                    table.ForeignKey(
                        name: "FK_ParkingPrices_Parkings_ParkingId",
                        column: x => x.ParkingId,
                        principalTable: "Parkings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParkingId = table.Column<Guid>(type: "uuid", nullable: true),
                    FuelStationId = table.Column<Guid>(type: "uuid", nullable: true),
                    ServiceCenterId = table.Column<Guid>(type: "uuid", nullable: true),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: true),
                    From = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    To = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SpotCount = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_FuelStations_FuelStationId",
                        column: x => x.FuelStationId,
                        principalTable: "FuelStations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_Parkings_ParkingId",
                        column: x => x.ParkingId,
                        principalTable: "Parkings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_ServiceCenters_ServiceCenterId",
                        column: x => x.ServiceCenterId,
                        principalTable: "ServiceCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    FuelStationId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParkingId = table.Column<Guid>(type: "uuid", nullable: true),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: true),
                    ServiceCenterId = table.Column<Guid>(type: "uuid", nullable: true),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_FuelStations_FuelStationId",
                        column: x => x.FuelStationId,
                        principalTable: "FuelStations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Favorites_Parkings_ParkingId",
                        column: x => x.ParkingId,
                        principalTable: "Parkings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_ServiceCenters_ServiceCenterId",
                        column: x => x.ServiceCenterId,
                        principalTable: "ServiceCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Body = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    FuelStationId = table.Column<Guid>(type: "uuid", nullable: true),
                    ServiceCenterId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParkingId = table.Column<Guid>(type: "uuid", nullable: true),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: true),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "BasaEntityId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_News_FuelStations_FuelStationId",
                        column: x => x.FuelStationId,
                        principalTable: "FuelStations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_News_Parkings_ParkingId",
                        column: x => x.ParkingId,
                        principalTable: "Parkings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_News_ServiceCenters_ServiceCenterId",
                        column: x => x.ServiceCenterId,
                        principalTable: "ServiceCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_News_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_News_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Body = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: true),
                    ServiceCenterId = table.Column<Guid>(type: "uuid", nullable: true),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: true),
                    FuelStationId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParkingId = table.Column<Guid>(type: "uuid", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Notifications_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "BasaEntityId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Notifications_FuelStations_FuelStationId",
                        column: x => x.FuelStationId,
                        principalTable: "FuelStations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_Parkings_ParkingId",
                        column: x => x.ParkingId,
                        principalTable: "Parkings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_ServiceCenters_ServiceCenterId",
                        column: x => x.ServiceCenterId,
                        principalTable: "ServiceCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Stars = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    Comment = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ServiceCenterId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParkingId = table.Column<Guid>(type: "uuid", nullable: true),
                    FuelStationId = table.Column<Guid>(type: "uuid", nullable: true),
                    Companyid = table.Column<Guid>(type: "uuid", nullable: true),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: true),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: true),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_Companies_Companyid",
                        column: x => x.Companyid,
                        principalTable: "Companies",
                        principalColumn: "BasaEntityId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Ratings_FuelStations_FuelStationId",
                        column: x => x.FuelStationId,
                        principalTable: "FuelStations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_Parkings_ParkingId",
                        column: x => x.ParkingId,
                        principalTable: "Parkings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Ratings_ServiceCenters_ServiceCenterId",
                        column: x => x.ServiceCenterId,
                        principalTable: "ServiceCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    FuelStationId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParkingId = table.Column<Guid>(type: "uuid", nullable: true),
                    ServiceCenterId = table.Column<Guid>(type: "uuid", nullable: true),
                    NewsId = table.Column<Guid>(type: "uuid", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    BasaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "BasaEntityId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Images_FuelStations_FuelStationId",
                        column: x => x.FuelStationId,
                        principalTable: "FuelStations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Images_Parkings_ParkingId",
                        column: x => x.ParkingId,
                        principalTable: "Parkings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Images_ServiceCenters_ServiceCenterId",
                        column: x => x.ServiceCenterId,
                        principalTable: "ServiceCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Images_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Images_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CompanyId",
                table: "Addresses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_DistrictId",
                table: "Addresses",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RegionId",
                table: "Addresses",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFeedbacks_UserId",
                table: "AppFeedbacks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AddressId",
                table: "Bookings",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FuelStationId",
                table: "Bookings",
                column: "FuelStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ParkingId",
                table: "Bookings",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ServiceCenterId",
                table: "Bookings",
                column: "ServiceCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_VehicleId",
                table: "Bookings",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserId",
                table: "Companies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_RegionId",
                table: "Districts",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_FuelStationId",
                table: "Favorites",
                column: "FuelStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ParkingId",
                table: "Favorites",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ServiceCenterId",
                table: "Favorites",
                column: "ServiceCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_VehicleId",
                table: "Favorites",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelPrices_FuelStationId",
                table: "FuelPrices",
                column: "FuelStationId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelPrices_FuelSubTypeId",
                table: "FuelPrices",
                column: "FuelSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelStations_AddressId",
                table: "FuelStations",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelStations_CompanyBasaEntityId",
                table: "FuelStations",
                column: "CompanyBasaEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelStations_CompanyId",
                table: "FuelStations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelStations_FuelSubTypeId",
                table: "FuelStations",
                column: "FuelSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelStations_FuelTypeId",
                table: "FuelStations",
                column: "FuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelStations_UserId",
                table: "FuelStations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelSubType_FuelTypeId",
                table: "FuelSubType",
                column: "FuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GeoLocations_AddressId",
                table: "GeoLocations",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_CompanyId",
                table: "Images",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_FuelStationId",
                table: "Images",
                column: "FuelStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_NewsId",
                table: "Images",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ParkingId",
                table: "Images",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ServiceCenterId",
                table: "Images",
                column: "ServiceCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserId",
                table: "Images",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_VehicleId",
                table: "Images",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_News_CompanyId",
                table: "News",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_News_FuelStationId",
                table: "News",
                column: "FuelStationId");

            migrationBuilder.CreateIndex(
                name: "IX_News_ParkingId",
                table: "News",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_News_ServiceCenterId",
                table: "News",
                column: "ServiceCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_News_UserId",
                table: "News",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_News_VehicleId",
                table: "News",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_BookingId",
                table: "Notifications",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CompanyId",
                table: "Notifications",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_FuelStationId",
                table: "Notifications",
                column: "FuelStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ParkingId",
                table: "Notifications",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ServiceCenterId",
                table: "Notifications",
                column: "ServiceCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_VehicleId",
                table: "Notifications",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingPrices_ParkingId",
                table: "ParkingPrices",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Parkings_AddressId",
                table: "Parkings",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Parkings_CompanyId",
                table: "Parkings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Parkings_UserId",
                table: "Parkings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_BookingId",
                table: "Ratings",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_Companyid",
                table: "Ratings",
                column: "Companyid");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_FuelStationId",
                table: "Ratings",
                column: "FuelStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ParkingId",
                table: "Ratings",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ServiceCenterId",
                table: "Ratings",
                column: "ServiceCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_VehicleId",
                table: "Ratings",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCenters_AddressId",
                table: "ServiceCenters",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCenters_CompanyId",
                table: "ServiceCenters",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCenters_UserId",
                table: "ServiceCenters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CompanyBasaEntityId",
                table: "Vehicles",
                column: "CompanyBasaEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_FuelTypeId",
                table: "Vehicles",
                column: "FuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppFeedbacks");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "FuelPrices");

            migrationBuilder.DropTable(
                name: "GeoLocations");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "ParkingPrices");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "FuelStations");

            migrationBuilder.DropTable(
                name: "Parkings");

            migrationBuilder.DropTable(
                name: "ServiceCenters");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "FuelSubType");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "FuelType");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
