﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xyzies.TWC.Public.Data.Migrations
{
    public partial class BranchTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TWC_BranchContactType",
                columns: table => new
                {
                    BranchContactTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TWC_BranchContactType", x => x.BranchContactTypeID);
                });

            migrationBuilder.CreateTable(
                name: "TWC_Companies",
                columns: table => new
                {
                    CompanyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(nullable: true),
                    LegalName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    StoreID = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    Agentid = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    StoreLocationCount = table.Column<int>(nullable: true),
                    PrimaryContactName = table.Column<string>(nullable: true),
                    PrimaryContactTitle = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    FedId = table.Column<string>(nullable: true),
                    TypeOfCompany = table.Column<int>(nullable: true),
                    StateEstablished = table.Column<string>(nullable: true),
                    CompanyType = table.Column<byte>(nullable: true),
                    CallerId = table.Column<string>(nullable: true),
                    IsAgreement = table.Column<bool>(nullable: true),
                    ActivityStatus = table.Column<string>(nullable: true),
                    CompanyKey = table.Column<Guid>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CellNumber = table.Column<string>(nullable: true),
                    BankNumber = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(nullable: true),
                    BankAccountNumber = table.Column<string>(nullable: true),
                    XyziesId = table.Column<string>(nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: true),
                    BankInfoGiven = table.Column<bool>(nullable: true),
                    AccountManager = table.Column<int>(nullable: true),
                    CrmCompanyId = table.Column<string>(nullable: true),
                    IsCallCenter = table.Column<bool>(nullable: true),
                    ParentCompanyId = table.Column<int>(nullable: true),
                    TeamKey = table.Column<Guid>(nullable: true),
                    RetailerGroupKey = table.Column<Guid>(nullable: true),
                    SocialMediaAccount = table.Column<string>(nullable: true),
                    RetailerGoogleAccount = table.Column<string>(nullable: true),
                    RetailerGooglePassword = table.Column<string>(nullable: true),
                    PaymentMode = table.Column<int>(nullable: true),
                    CustomerDemographicId = table.Column<int>(nullable: true),
                    LocationTypeId = table.Column<int>(nullable: true),
                    IsOwnerPassBackground = table.Column<bool>(nullable: true),
                    IsWebsite = table.Column<bool>(nullable: true),
                    IsSellsLifelineWireless = table.Column<bool>(nullable: true),
                    NumberofStores = table.Column<int>(nullable: true),
                    BusinessDescription = table.Column<string>(nullable: true),
                    WebsiteList = table.Column<string>(nullable: true),
                    IsSpectrum = table.Column<bool>(nullable: true),
                    BusinessSource = table.Column<int>(nullable: true),
                    GeoLat = table.Column<string>(nullable: true),
                    GeoLon = table.Column<string>(nullable: true),
                    IsMarketPlace = table.Column<bool>(nullable: true),
                    MarketPlaceName = table.Column<string>(nullable: true),
                    PhysicalName = table.Column<string>(nullable: true),
                    MarketStrategy = table.Column<string>(nullable: true),
                    NoSyncInfusion = table.Column<bool>(nullable: true),
                    StorePhoneNumber = table.Column<string>(nullable: true),
                    ReferralUserId = table.Column<int>(nullable: true),
                    CompanyStatusKey = table.Column<Guid>(nullable: true),
                    CompanyStatusChangedOn = table.Column<DateTime>(nullable: true),
                    CompanyStatusChangedBy = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TWC_Companies", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "TWC_Branches",
                columns: table => new
                {
                    BranchID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchName = table.Column<string>(maxLength: 250, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Fax = table.Column<string>(maxLength: 50, nullable: true),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 50, nullable: true),
                    GeoLat = table.Column<string>(maxLength: 50, nullable: true),
                    GeoLon = table.Column<string>(maxLength: 50, nullable: true),
                    Status = table.Column<int>(nullable: true),
                    State = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    CompanyID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TWC_Branches", x => x.BranchID);
                    table.ForeignKey(
                        name: "FK_TWC_Branches_TWC_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "TWC_Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TWC_BranchContact",
                columns: table => new
                {
                    BranchContactID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonName = table.Column<string>(maxLength: 50, nullable: true),
                    PersonLastName = table.Column<string>(maxLength: 50, nullable: true),
                    PersonTitle = table.Column<string>(maxLength: 100, nullable: true),
                    Value = table.Column<string>(maxLength: 250, nullable: false),
                    BranchContactTypeId = table.Column<int>(nullable: false),
                    BranchId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TWC_BranchContact", x => x.BranchContactID);
                    table.ForeignKey(
                        name: "FK_TWC_BranchContact_TWC_BranchContactType_BranchContactTypeId",
                        column: x => x.BranchContactTypeId,
                        principalTable: "TWC_BranchContactType",
                        principalColumn: "BranchContactTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TWC_BranchContact_TWC_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "TWC_Branches",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TWC_BranchContact_BranchContactTypeId",
                table: "TWC_BranchContact",
                column: "BranchContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TWC_BranchContact_BranchId",
                table: "TWC_BranchContact",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_TWC_Branches_CompanyID",
                table: "TWC_Branches",
                column: "CompanyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TWC_BranchContact");

            migrationBuilder.DropTable(
                name: "TWC_BranchContactType");

            migrationBuilder.DropTable(
                name: "TWC_Branches");

            migrationBuilder.DropTable(
                name: "TWC_Companies");
        }
    }
}
