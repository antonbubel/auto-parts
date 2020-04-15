using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoParts.Data.EF.Migrations.Migrations
{
    public partial class Add_Supplier_And_Administrator_User_Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "f3b180ce-b451-41fb-b4df-69311a4d89b4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 2L, "fbd38e45-5f0d-41e4-9e4b-0cbffac4c7ca", "Supplier", "SUPPLIER" },
                    { 3L, "54320f9c-6456-439c-bb39-3f6b78048e7f", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "Supplier" },
                    { 3, "Administrator" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "ebd4c608-0785-4cc2-8eba-8a851d2cc83d");
        }
    }
}
