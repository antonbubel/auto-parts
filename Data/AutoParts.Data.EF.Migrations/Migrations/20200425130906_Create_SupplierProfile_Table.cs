using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoParts.Data.EF.Migrations.Migrations
{
    public partial class Create_SupplierProfile_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupplierProfiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    OrganizationName = table.Column<string>(maxLength: 50, nullable: false),
                    OrganizationAddress = table.Column<string>(maxLength: 200, nullable: false),
                    OrganizationDescription = table.Column<string>(maxLength: 500, nullable: false),
                    SalesEmail = table.Column<string>(nullable: true),
                    SalesPhoneNumber = table.Column<string>(nullable: true),
                    Website = table.Column<string>(maxLength: 50, nullable: true),
                    Logo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierProfiles_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "68b9eccb-032e-473c-985d-daf856cfed41");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "79bcb999-60ea-4eb8-999d-f143cf8bf9de");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "5a9b8c5e-92ee-4897-a637-de4b6720d0ad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierProfiles");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "dc255920-271e-4af0-aa60-440da92b6520");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "e03a66c6-94e2-42e6-aa93-904d4f9f1d61");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "8b49c19f-1978-4b3a-b66a-b6297b7dedea");
        }
    }
}
