using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xyzies.SSO.Identity.Data.Migrations
{
    public partial class Add_User_Migrations_History : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserMigrationHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMigrationHistory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMigrationHistory");
        }
    }
}
