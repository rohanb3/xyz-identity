﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Xyzies.SSO.Identity.Data;

namespace Xyzies.SSO.Identity.Data.Migrations
{
    [DbContext(typeof(IdentityDataContext))]
    [Migration("20190402181414_AddCityAndState")]
    partial class AddCityAndState
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Xyzies.SSO.Identity.Data.Entity.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<Guid?>("StateId");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("Xyzies.SSO.Identity.Data.Entity.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Scope")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e37245d9-8e7b-4746-8fba-d09a5fb2a212"),
                            IsActive = true,
                            Scope = "xyzies.reviews.templates.read"
                        },
                        new
                        {
                            Id = new Guid("5e30e0c8-3afb-4561-ae8f-5062306977b3"),
                            IsActive = true,
                            Scope = "xyzies.reviews.templates.write"
                        },
                        new
                        {
                            Id = new Guid("65a5611f-2ef0-45c2-9ecc-527742c17665"),
                            IsActive = true,
                            Scope = "xyzies.reviews.templates.update"
                        },
                        new
                        {
                            Id = new Guid("c9ab1dfc-a0f2-48e2-941e-b6df62b20c72"),
                            IsActive = true,
                            Scope = "xyzies.reviews.templates.delete"
                        },
                        new
                        {
                            Id = new Guid("34ffa4f7-b2ea-4426-9669-3e597d049bac"),
                            IsActive = true,
                            Scope = "xyzies.reviews.reviews.read"
                        },
                        new
                        {
                            Id = new Guid("c49d83e5-175d-4aee-9cec-16855260757e"),
                            IsActive = true,
                            Scope = "xyzies.reviews.reviews.write"
                        },
                        new
                        {
                            Id = new Guid("ed192391-351c-4168-88ec-b88c152f1179"),
                            IsActive = true,
                            Scope = "xyzies.reviews.reviews.update"
                        },
                        new
                        {
                            Id = new Guid("240e7915-610b-4321-a03b-b5a8b405a734"),
                            IsActive = true,
                            Scope = "xyzies.reviews.reviews.delete"
                        },
                        new
                        {
                            Id = new Guid("e77d1bbb-6cb4-4ef4-8e98-cf0bfcc6ffc8"),
                            IsActive = true,
                            Scope = "xyzies.authorization.reviews.admin"
                        },
                        new
                        {
                            Id = new Guid("5a690a45-0338-46e2-8ac4-24e6ad9838a4"),
                            IsActive = true,
                            Scope = "xyzies.authorization.reviews.mobile"
                        },
                        new
                        {
                            Id = new Guid("f6428b23-0faa-4591-9223-773ef698122b"),
                            IsActive = true,
                            Scope = "xyzies.authorization.vsp.operator"
                        },
                        new
                        {
                            Id = new Guid("024fd825-381b-4088-b654-bddc1e3b7280"),
                            IsActive = true,
                            Scope = "xyzies.authorization.vsp.mobile"
                        });
                });

            modelBuilder.Entity("Xyzies.SSO.Identity.Data.Entity.Policy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Policies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7e77ec96-9573-4730-a378-4fb5a0ec1d6f"),
                            Name = "TemplatesFull"
                        },
                        new
                        {
                            Id = new Guid("b02154e1-f148-4d66-9111-248edb38f3a7"),
                            Name = "ReviewsFull"
                        },
                        new
                        {
                            Id = new Guid("38c8d1bc-1c84-427b-b7a2-8fdfb246a6a5"),
                            Name = "ReviewsAdminLogin"
                        },
                        new
                        {
                            Id = new Guid("c517a418-c135-4393-8807-6c4580de4b7d"),
                            Name = "ReviewsMobileLogin"
                        },
                        new
                        {
                            Id = new Guid("ecd44d23-a05b-4250-896d-ab1bf4d23da2"),
                            Name = "VspOperatorLogin"
                        },
                        new
                        {
                            Id = new Guid("24d3d136-f8bc-4968-9a33-4388e8b2abae"),
                            Name = "VspMobileLogin"
                        });
                });

            modelBuilder.Entity("Xyzies.SSO.Identity.Data.Entity.Relationship.PermissionToPolicy", b =>
                {
                    b.Property<Guid>("Relation1Id")
                        .HasColumnName("PermissionId");

                    b.Property<Guid>("Relation2Id")
                        .HasColumnName("PolicyId");

                    b.HasKey("Relation1Id", "Relation2Id");

                    b.HasIndex("Relation2Id");

                    b.ToTable("PermissionToPolicy");
                });

            modelBuilder.Entity("Xyzies.SSO.Identity.Data.Entity.Relationship.PolicyToRole", b =>
                {
                    b.Property<Guid>("Relation1Id")
                        .HasColumnName("PolicyId");

                    b.Property<Guid>("Relation2Id")
                        .HasColumnName("RoleId");

                    b.HasKey("Relation1Id", "Relation2Id");

                    b.HasIndex("Relation2Id");

                    b.ToTable("PolicyToRole");
                });

            modelBuilder.Entity("Xyzies.SSO.Identity.Data.Entity.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RoleKey");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsCustom");

                    b.Property<int?>("RoleId");

                    b.Property<string>("RoleName");

                    b.HasKey("Id");

                    b.ToTable("TWC_Role");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c6c48945-0660-470d-82fe-f19c5459b053"),
                            CreatedOn = new DateTime(2019, 4, 2, 21, 14, 13, 731, DateTimeKind.Local).AddTicks(6082),
                            IsCustom = false,
                            RoleId = 1,
                            RoleName = "SuperAdmin"
                        },
                        new
                        {
                            Id = new Guid("9a6b2b65-7e39-4604-a3a4-f5328c8625e3"),
                            CreatedOn = new DateTime(2019, 4, 2, 21, 14, 13, 732, DateTimeKind.Local).AddTicks(9529),
                            IsCustom = false,
                            RoleId = 2,
                            RoleName = "RetailerAdmin"
                        },
                        new
                        {
                            Id = new Guid("97a9a28e-b17d-4681-8f5b-6e0c777bbea3"),
                            CreatedOn = new DateTime(2019, 4, 2, 21, 14, 13, 732, DateTimeKind.Local).AddTicks(9543),
                            IsCustom = false,
                            RoleId = 3,
                            RoleName = "SalesRep"
                        },
                        new
                        {
                            Id = new Guid("bc190516-2994-4c2c-bbfb-fa5ef0e02fbe"),
                            CreatedOn = new DateTime(2019, 4, 2, 21, 14, 13, 732, DateTimeKind.Local).AddTicks(9547),
                            IsCustom = false,
                            RoleId = 4,
                            RoleName = "Operator"
                        });
                });

            modelBuilder.Entity("Xyzies.SSO.Identity.Data.Entity.State", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("ShortName");

                    b.HasKey("Id");

                    b.ToTable("State");
                });

            modelBuilder.Entity("Xyzies.SSO.Identity.Data.Entity.City", b =>
                {
                    b.HasOne("Xyzies.SSO.Identity.Data.Entity.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("Xyzies.SSO.Identity.Data.Entity.Relationship.PermissionToPolicy", b =>
                {
                    b.HasOne("Xyzies.SSO.Identity.Data.Entity.Permission", "Entity1")
                        .WithMany()
                        .HasForeignKey("Relation1Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Xyzies.SSO.Identity.Data.Entity.Policy", "Entity2")
                        .WithMany("RelationToPermission")
                        .HasForeignKey("Relation2Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Xyzies.SSO.Identity.Data.Entity.Relationship.PolicyToRole", b =>
                {
                    b.HasOne("Xyzies.SSO.Identity.Data.Entity.Policy", "Entity1")
                        .WithMany("RelationToRole")
                        .HasForeignKey("Relation1Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Xyzies.SSO.Identity.Data.Entity.Role", "Entity2")
                        .WithMany("RelationToPolicy")
                        .HasForeignKey("Relation2Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}