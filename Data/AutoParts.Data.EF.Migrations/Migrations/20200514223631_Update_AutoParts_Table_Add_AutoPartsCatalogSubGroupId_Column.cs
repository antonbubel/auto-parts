using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoParts.Data.EF.Migrations.Migrations
{
    public partial class Update_AutoParts_Table_Add_AutoPartsCatalogSubGroupId_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutoParts_CarModifications_CarModificationId",
                table: "AutoParts");

            migrationBuilder.AddColumn<long>(
                name: "AutoPartsCatalogSubGroupId",
                table: "AutoParts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "f87f62cf-dbe4-4a22-9cf5-1742f237fd67");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "1e1c4c6b-4513-4790-ae5b-0e43bd7854b5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "067cc823-c9f1-4876-ad96-75b65edc08da");

            migrationBuilder.CreateIndex(
                name: "IX_AutoParts_AutoPartsCatalogSubGroupId",
                table: "AutoParts",
                column: "AutoPartsCatalogSubGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AutoParts_AutoPartsCatalogSubGroups_AutoPartsCatalogSubGroupId",
                table: "AutoParts",
                column: "AutoPartsCatalogSubGroupId",
                principalTable: "AutoPartsCatalogSubGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AutoParts_CarModifications_CarModificationId",
                table: "AutoParts",
                column: "CarModificationId",
                principalTable: "CarModifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutoParts_AutoPartsCatalogSubGroups_AutoPartsCatalogSubGroupId",
                table: "AutoParts");

            migrationBuilder.DropForeignKey(
                name: "FK_AutoParts_CarModifications_CarModificationId",
                table: "AutoParts");

            migrationBuilder.DropIndex(
                name: "IX_AutoParts_AutoPartsCatalogSubGroupId",
                table: "AutoParts");

            migrationBuilder.DropColumn(
                name: "AutoPartsCatalogSubGroupId",
                table: "AutoParts");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "016e000f-c55d-49a4-b11f-3453379a1edb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "9d5b61d1-4575-44c2-b18d-cbd786fb9e67");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "bf8ccf37-fe80-4f05-a536-059ce609b518");

            migrationBuilder.AddForeignKey(
                name: "FK_AutoParts_CarModifications_CarModificationId",
                table: "AutoParts",
                column: "CarModificationId",
                principalTable: "CarModifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
