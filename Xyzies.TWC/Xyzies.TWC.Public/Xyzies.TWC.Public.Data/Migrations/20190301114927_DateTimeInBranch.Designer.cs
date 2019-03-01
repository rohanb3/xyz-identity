﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Xyzies.TWC.Public.Data;

namespace Xyzies.TWC.Public.Data.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20190301114927_DateTimeInBranch")]
    partial class DateTimeInBranch
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("AddressLine1")
                        .HasMaxLength(50);

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(50);

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<int>("CompanyId");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasComputedColumnSql("GETUTCDATE()");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<string>("Fax")
                        .HasMaxLength(50);

                    b.Property<string>("GeoLat")
                        .HasMaxLength(50);

                    b.Property<string>("GeoLng")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDisabled")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate")
                        .ValueGeneratedOnUpdate()
                        .HasComputedColumnSql("GETUTCDATE()");

                    b.Property<string>("Phone")
                        .HasMaxLength(50);

                    b.Property<string>("State")
                        .HasMaxLength(50);

                    b.Property<int>("Status");

                    b.Property<int?>("UserId");

                    b.Property<string>("ZipCode")
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasName("BranchID");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("TWC_Branches");
                });

            modelBuilder.Entity("Xyzies.TWC.Public.Data.Entities.BranchContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BranchContactID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BranchContactTypeId");

                    b.Property<int>("BranchPrimaryContactId");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasComputedColumnSql("GETUTCDATE()");

                    b.Property<DateTime?>("ModifiedDate")
                        .ValueGeneratedOnUpdate()
                        .HasComputedColumnSql("GETUTCDATE()");

                    b.Property<string>("PersonLastName")
                        .HasMaxLength(50);

                    b.Property<string>("PersonName")
                        .HasMaxLength(50);

                    b.Property<string>("PersonTitle")
                        .HasMaxLength(100);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id")
                        .HasName("BranchContactID");

                    b.HasIndex("BranchContactTypeId");

                    b.HasIndex("BranchPrimaryContactId");

                    b.ToTable("TWC_BranchContacts");
                });

            modelBuilder.Entity("Xyzies.TWC.Public.Data.Entities.BranchContactType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BranchContactTypeID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasName("BranchContactTypeID");

                    b.ToTable("TWC_BranchContactTypes");
                });

            modelBuilder.Entity("Xyzies.TWC.Public.Data.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CompanyID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<Guid?>("CompanyKey");

                    b.Property<string>("CompanyName");

                    b.Property<int?>("CompanyStatusChangedBy");

                    b.Property<DateTime?>("CompanyStatusChangedOn");

                    b.Property<Guid?>("CompanyStatusKey");

                    b.Property<byte?>("CompanyType");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasComputedColumnSql("GETUTCDATE()");

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

                    b.Property<DateTime?>("ModifiedDate")
                        .ValueGeneratedOnUpdate()
                        .HasComputedColumnSql("GETUTCDATE()");

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

                    b.Property<int?>("UserId");

                    b.Property<string>("WebsiteList");

                    b.Property<string>("XyziesId");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id")
                        .HasName("CompanyID");

                    b.HasIndex("UserId");

                    b.ToTable("TWC_Companies");
                });

            modelBuilder.Entity("Xyzies.TWC.Public.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Active");

                    b.Property<string>("Address")
                        .HasMaxLength(50);

                    b.Property<string>("AuthKey");

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<int?>("CompanyID");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasComputedColumnSql("GETUTCDATE()");

                    b.Property<bool?>("Deleted");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<string>("IPAddressRestriction");

                    b.Property<string>("Imagename");

                    b.Property<string>("InfusionSoftId");

                    b.Property<bool?>("IsEmailVerified");

                    b.Property<bool?>("IsIdentityUploaded");

                    b.Property<bool?>("IsPhoneVerified");

                    b.Property<string>("IsRegisteredUser");

                    b.Property<bool?>("IsUserPictureUploaded");

                    b.Property<bool?>("Is_Agreement");

                    b.Property<string>("LastName");

                    b.Property<string>("LoginIpAddress");

                    b.Property<int?>("ManagedBy");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate")
                        .ValueGeneratedOnUpdate()
                        .HasComputedColumnSql("GETUTCDATE()");

                    b.Property<string>("Name")
                        .HasMaxLength(250);

                    b.Property<string>("Password")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("PasswordExpiryOn");

                    b.Property<string>("Phone")
                        .HasMaxLength(50);

                    b.Property<string>("PhotoID");

                    b.Property<string>("Role")
                        .HasMaxLength(50);

                    b.Property<int?>("SalesPersonID");

                    b.Property<string>("SocialMediaAccount");

                    b.Property<string>("State")
                        .HasMaxLength(50);

                    b.Property<int?>("StatusId");

                    b.Property<Guid?>("UserGuid");

                    b.Property<int?>("UserRefID");

                    b.Property<int?>("UserStatusChangedBy");

                    b.Property<DateTime?>("UserStatusChangedOn");

                    b.Property<Guid?>("UserStatusKey");

                    b.Property<string>("XyziesId");

                    b.Property<string>("ZipCode")
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasName("UserID");

                    b.ToTable("TWC_Users");
                });

            modelBuilder.Entity("Xyzies.TWC.Public.Data.Entities.Branch", b =>
                {
                    b.HasOne("Xyzies.TWC.Public.Data.Entities.Company", "Company")
                        .WithMany("Branches")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Xyzies.TWC.Public.Data.Entities.User")
                        .WithMany("Branches")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Xyzies.TWC.Public.Data.Entities.BranchContact", b =>
                {
                    b.HasOne("Xyzies.TWC.Public.Data.Entities.BranchContactType", "BranchContactType")
                        .WithMany("BranchContacts")
                        .HasForeignKey("BranchContactTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Xyzies.TWC.Public.Data.Entities.Branch", "BranchPrimaryContact")
                        .WithMany("BranchContacts")
                        .HasForeignKey("BranchPrimaryContactId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Xyzies.TWC.Public.Data.Entities.Company", b =>
                {
                    b.HasOne("Xyzies.TWC.Public.Data.Entities.User")
                        .WithMany("Companies")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
