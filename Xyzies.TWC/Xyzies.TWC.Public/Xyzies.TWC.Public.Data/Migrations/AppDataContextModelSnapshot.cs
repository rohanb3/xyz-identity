﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Xyzies.TWC.Public.Data;

namespace Xyzies.TWC.Public.Data.Migrations
{
    [DbContext(typeof(AppDataContext))]
    partial class AppDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Xyzies.TWC.Public.Data.Entities.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BranchID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("BranchName")
                        .IsRequired();

                    b.Property<string>("City");

                    b.Property<Guid?>("CompanyID");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Fax");

                    b.Property<int?>("GeoLat");

                    b.Property<int?>("GeoLon");

                    b.Property<string>("Phone");

                    b.Property<string>("Status");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("CompanyID");

                    b.ToTable("TWC_Branches");
                });

            modelBuilder.Entity("Xyzies.TWC.Public.Data.Entities.BranchContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BranchContactID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BranchContactTypeId");

                    b.Property<int?>("BranchId");

                    b.Property<string>("PersonLastName");

                    b.Property<string>("PersonName");

                    b.Property<string>("PersonTitle");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("BranchContactTypeId");

                    b.HasIndex("BranchId");

                    b.ToTable("TWS_BranchContact");
                });

            modelBuilder.Entity("Xyzies.TWC.Public.Data.Entities.BranchContactType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BranchContactTypeID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TWC_BranchContactType");
                });

            modelBuilder.Entity("Xyzies.TWC.Public.Data.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountManager");

                    b.Property<string>("ActivityStatus");

                    b.Property<string>("Address");

                    b.Property<int?>("Agentid");

                    b.Property<DateTime?>("ApprovedDate");

                    b.Property<string>("BankAccountNumber");

                    b.Property<bool?>("BankInfoGiven");

                    b.Property<string>("BankName");

                    b.Property<string>("BankNumber");

                    b.Property<string>("BusinessDescription");

                    b.Property<int?>("BusinessSource");

                    b.Property<string>("CallerId");

                    b.Property<string>("CellNumber");

                    b.Property<string>("City");

                    b.Property<int>("CompanyID");

                    b.Property<Guid?>("CompanyKey");

                    b.Property<string>("CompanyName");

                    b.Property<int?>("CompanyStatusChangedBy");

                    b.Property<DateTime?>("CompanyStatusChangedOn");

                    b.Property<Guid?>("CompanyStatusKey");

                    b.Property<byte?>("CompanyType");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CrmCompanyId");

                    b.Property<int?>("CustomerDemographicId");

                    b.Property<string>("Email");

                    b.Property<string>("Fax");

                    b.Property<string>("FedId");

                    b.Property<string>("FirstName");

                    b.Property<string>("GeoLat");

                    b.Property<string>("GeoLon");

                    b.Property<bool?>("IsAgreement");

                    b.Property<bool?>("IsCallCenter");

                    b.Property<bool?>("IsMarketPlace");

                    b.Property<bool?>("IsOwnerPassBackground");

                    b.Property<bool?>("IsSellsLifelineWireless");

                    b.Property<bool?>("IsSpectrum");

                    b.Property<bool?>("IsWebsite");

                    b.Property<string>("LastName");

                    b.Property<string>("LegalName");

                    b.Property<int?>("LocationTypeId");

                    b.Property<string>("MarketPlaceName");

                    b.Property<string>("MarketStrategy");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<bool?>("NoSyncInfusion");

                    b.Property<int?>("NumberofStores");

                    b.Property<int?>("ParentCompanyId");

                    b.Property<int?>("PaymentMode");

                    b.Property<string>("Phone");

                    b.Property<string>("PhysicalName");

                    b.Property<string>("PrimaryContactName");

                    b.Property<string>("PrimaryContactTitle");

                    b.Property<int?>("ReferralUserId");

                    b.Property<string>("RetailerGoogleAccount");

                    b.Property<string>("RetailerGooglePassword");

                    b.Property<Guid?>("RetailerGroupKey");

                    b.Property<string>("SocialMediaAccount");

                    b.Property<string>("State");

                    b.Property<string>("StateEstablished");

                    b.Property<int?>("Status");

                    b.Property<int?>("StoreID");

                    b.Property<int?>("StoreLocationCount");

                    b.Property<string>("StorePhoneNumber");

                    b.Property<Guid?>("TeamKey");

                    b.Property<int?>("TypeOfCompany");

                    b.Property<string>("WebsiteList");

                    b.Property<string>("XyziesId");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("TWC_Companies");
                });

            modelBuilder.Entity("Xyzies.TWC.Public.Data.Entities.Branch", b =>
                {
                    b.HasOne("Xyzies.TWC.Public.Data.Entities.Company", "Company")
                        .WithMany("Branches")
                        .HasForeignKey("CompanyID");
                });

            modelBuilder.Entity("Xyzies.TWC.Public.Data.Entities.BranchContact", b =>
                {
                    b.HasOne("Xyzies.TWC.Public.Data.Entities.BranchContactType", "BranchContactType")
                        .WithMany("BranchContacts")
                        .HasForeignKey("BranchContactTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Xyzies.TWC.Public.Data.Entities.Branch", "Branch")
                        .WithMany("BranchContacts")
                        .HasForeignKey("BranchId");
                });
#pragma warning restore 612, 618
        }
    }
}
