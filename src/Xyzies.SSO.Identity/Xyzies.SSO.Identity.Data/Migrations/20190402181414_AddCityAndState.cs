using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xyzies.SSO.Identity.Data.Migrations
{
    public partial class AddCityAndState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    StateId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "IsActive", "Scope" },
                values: new object[,]
                {
                    { new Guid("e37245d9-8e7b-4746-8fba-d09a5fb2a212"), true, "xyzies.reviews.templates.read" },
                    { new Guid("024fd825-381b-4088-b654-bddc1e3b7280"), true, "xyzies.authorization.vsp.mobile" },
                    { new Guid("5a690a45-0338-46e2-8ac4-24e6ad9838a4"), true, "xyzies.authorization.reviews.mobile" },
                    { new Guid("e77d1bbb-6cb4-4ef4-8e98-cf0bfcc6ffc8"), true, "xyzies.authorization.reviews.admin" },
                    { new Guid("240e7915-610b-4321-a03b-b5a8b405a734"), true, "xyzies.reviews.reviews.delete" },
                    { new Guid("ed192391-351c-4168-88ec-b88c152f1179"), true, "xyzies.reviews.reviews.update" },
                    { new Guid("f6428b23-0faa-4591-9223-773ef698122b"), true, "xyzies.authorization.vsp.operator" },
                    { new Guid("34ffa4f7-b2ea-4426-9669-3e597d049bac"), true, "xyzies.reviews.reviews.read" },
                    { new Guid("c9ab1dfc-a0f2-48e2-941e-b6df62b20c72"), true, "xyzies.reviews.templates.delete" },
                    { new Guid("65a5611f-2ef0-45c2-9ecc-527742c17665"), true, "xyzies.reviews.templates.update" },
                    { new Guid("5e30e0c8-3afb-4561-ae8f-5062306977b3"), true, "xyzies.reviews.templates.write" },
                    { new Guid("c49d83e5-175d-4aee-9cec-16855260757e"), true, "xyzies.reviews.reviews.write" }
                });

            migrationBuilder.InsertData(
                table: "Policies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7e77ec96-9573-4730-a378-4fb5a0ec1d6f"), "TemplatesFull" },
                    { new Guid("b02154e1-f148-4d66-9111-248edb38f3a7"), "ReviewsFull" },
                    { new Guid("38c8d1bc-1c84-427b-b7a2-8fdfb246a6a5"), "ReviewsAdminLogin" },
                    { new Guid("c517a418-c135-4393-8807-6c4580de4b7d"), "ReviewsMobileLogin" },
                    { new Guid("ecd44d23-a05b-4250-896d-ab1bf4d23da2"), "VspOperatorLogin" },
                    { new Guid("24d3d136-f8bc-4968-9a33-4388e8b2abae"), "VspMobileLogin" }
                });

            migrationBuilder.InsertData(
                table: "TWC_Role",
                columns: new[] { "RoleKey", "CreatedBy", "CreatedOn", "IsCustom", "RoleId", "RoleName" },
                values: new object[,]
                {
                    { new Guid("97a9a28e-b17d-4681-8f5b-6e0c777bbea3"), null, new DateTime(2019, 4, 2, 21, 14, 13, 732, DateTimeKind.Local).AddTicks(9543), false, 3, "SalesRep" },
                    { new Guid("c6c48945-0660-470d-82fe-f19c5459b053"), null, new DateTime(2019, 4, 2, 21, 14, 13, 731, DateTimeKind.Local).AddTicks(6082), false, 1, "SuperAdmin" },
                    { new Guid("9a6b2b65-7e39-4604-a3a4-f5328c8625e3"), null, new DateTime(2019, 4, 2, 21, 14, 13, 732, DateTimeKind.Local).AddTicks(9529), false, 2, "RetailerAdmin" },
                    { new Guid("bc190516-2994-4c2c-bbfb-fa5ef0e02fbe"), null, new DateTime(2019, 4, 2, 21, 14, 13, 732, DateTimeKind.Local).AddTicks(9547), false, 4, "Operator" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_StateId",
                table: "City",
                column: "StateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("024fd825-381b-4088-b654-bddc1e3b7280"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("240e7915-610b-4321-a03b-b5a8b405a734"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("34ffa4f7-b2ea-4426-9669-3e597d049bac"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("5a690a45-0338-46e2-8ac4-24e6ad9838a4"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("5e30e0c8-3afb-4561-ae8f-5062306977b3"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("65a5611f-2ef0-45c2-9ecc-527742c17665"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c49d83e5-175d-4aee-9cec-16855260757e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c9ab1dfc-a0f2-48e2-941e-b6df62b20c72"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e37245d9-8e7b-4746-8fba-d09a5fb2a212"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e77d1bbb-6cb4-4ef4-8e98-cf0bfcc6ffc8"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ed192391-351c-4168-88ec-b88c152f1179"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f6428b23-0faa-4591-9223-773ef698122b"));

            migrationBuilder.DeleteData(
                table: "Policies",
                keyColumn: "Id",
                keyValue: new Guid("24d3d136-f8bc-4968-9a33-4388e8b2abae"));

            migrationBuilder.DeleteData(
                table: "Policies",
                keyColumn: "Id",
                keyValue: new Guid("38c8d1bc-1c84-427b-b7a2-8fdfb246a6a5"));

            migrationBuilder.DeleteData(
                table: "Policies",
                keyColumn: "Id",
                keyValue: new Guid("7e77ec96-9573-4730-a378-4fb5a0ec1d6f"));

            migrationBuilder.DeleteData(
                table: "Policies",
                keyColumn: "Id",
                keyValue: new Guid("b02154e1-f148-4d66-9111-248edb38f3a7"));

            migrationBuilder.DeleteData(
                table: "Policies",
                keyColumn: "Id",
                keyValue: new Guid("c517a418-c135-4393-8807-6c4580de4b7d"));

            migrationBuilder.DeleteData(
                table: "Policies",
                keyColumn: "Id",
                keyValue: new Guid("ecd44d23-a05b-4250-896d-ab1bf4d23da2"));

            migrationBuilder.DeleteData(
                table: "TWC_Role",
                keyColumn: "RoleKey",
                keyValue: new Guid("97a9a28e-b17d-4681-8f5b-6e0c777bbea3"));

            migrationBuilder.DeleteData(
                table: "TWC_Role",
                keyColumn: "RoleKey",
                keyValue: new Guid("9a6b2b65-7e39-4604-a3a4-f5328c8625e3"));

            migrationBuilder.DeleteData(
                table: "TWC_Role",
                keyColumn: "RoleKey",
                keyValue: new Guid("bc190516-2994-4c2c-bbfb-fa5ef0e02fbe"));

            migrationBuilder.DeleteData(
                table: "TWC_Role",
                keyColumn: "RoleKey",
                keyValue: new Guid("c6c48945-0660-470d-82fe-f19c5459b053"));
        }
    }
}
