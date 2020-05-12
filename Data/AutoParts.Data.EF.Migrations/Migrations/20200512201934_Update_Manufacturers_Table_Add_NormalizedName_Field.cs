using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoParts.Data.EF.Migrations.Migrations
{
    public partial class Update_Manufacturers_Table_Add_NormalizedName_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "Manufacturers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "11a95066-d95c-4f4b-bb12-768e209f5f49");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "5a210a16-52fe-4084-833d-07ff99a7f959");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "594c196f-efdf-4559-baf4-70fa297deab4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "Manufacturers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "c467c9a7-5e55-4f9c-b82a-e99883eeb9f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "ae7978e9-1a53-4ac5-94da-956bc7515bc0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "da11b095-d49d-490b-89dc-bd0cf7dfcad4");
        }
    }
}
