using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xyzies.SSO.Identity.Data.Migrations
{
    public partial class IdentityStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Scope = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TWC_Role",
                columns: table => new
                {
                    RoleKey = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<int>(nullable: true),
                    RoleName = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsCustom = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TWC_Role", x => x.RoleKey);
                });

            migrationBuilder.CreateTable(
                name: "TWC_Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    SalesPersonID = table.Column<int>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: true),
                    is_Agreement = table.Column<bool>(nullable: true),
                    imagename = table.Column<string>(nullable: true),
                    UserRefID = table.Column<int>(nullable: true),
                    XyziesId = table.Column<string>(nullable: true),
                    ManagedBy = table.Column<int>(nullable: false),
                    deleted = table.Column<bool>(nullable: true),
                    UserGuid = table.Column<Guid>(nullable: true),
                    IPAddressRestriction = table.Column<string>(nullable: true),
                    PasswordExpiryOn = table.Column<DateTime>(nullable: false),
                    IsPhoneVerified = table.Column<bool>(nullable: true),
                    IsIdentityUploaded = table.Column<bool>(nullable: true),
                    IsEmailVerified = table.Column<bool>(nullable: true),
                    StatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TWC_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "PermissionToPolicy",
                columns: table => new
                {
                    PermissionId = table.Column<Guid>(nullable: false),
                    PolicyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionToPolicy", x => new { x.PermissionId, x.PolicyId });
                    table.ForeignKey(
                        name: "FK_PermissionToPolicy_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionToPolicy_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PolicyToRole",
                columns: table => new
                {
                    PolicyId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyToRole", x => new { x.PolicyId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_PolicyToRole_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolicyToRole_TWC_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TWC_Role",
                        principalColumn: "RoleKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionToPolicy_PolicyId",
                table: "PermissionToPolicy",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyToRole_RoleId",
                table: "PolicyToRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionToPolicy");

            migrationBuilder.DropTable(
                name: "PolicyToRole");

            migrationBuilder.DropTable(
                name: "TWC_Users");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "TWC_Role");
        }
    }
}
