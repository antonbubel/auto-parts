using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoParts.Data.EF.Migrations.Migrations
{
    public partial class Create_AutoPartsCatalogGroup_AutoPartsCatalogSubGroup_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutoPartsCatalogGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoPartsCatalogGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AutoPartsCatalogSubGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    AutoPartsCatalogGroupId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoPartsCatalogSubGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutoPartsCatalogSubGroups_AutoPartsCatalogGroups_AutoPartsCatalogGroupId",
                        column: x => x.AutoPartsCatalogGroupId,
                        principalTable: "AutoPartsCatalogGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "a4585434-bdee-45ae-bbcb-0d44111bb19c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "4159157a-75b7-4986-8fe9-53188b9e846f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "f100582b-f41e-4930-8e82-7454b64d2b50");

            migrationBuilder.CreateIndex(
                name: "IX_AutoPartsCatalogSubGroups_AutoPartsCatalogGroupId",
                table: "AutoPartsCatalogSubGroups",
                column: "AutoPartsCatalogGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoPartsCatalogSubGroups");

            migrationBuilder.DropTable(
                name: "AutoPartsCatalogGroups");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "f3b180ce-b451-41fb-b4df-69311a4d89b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "fbd38e45-5f0d-41e4-9e4b-0cbffac4c7ca");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "54320f9c-6456-439c-bb39-3f6b78048e7f");
        }
    }
}
