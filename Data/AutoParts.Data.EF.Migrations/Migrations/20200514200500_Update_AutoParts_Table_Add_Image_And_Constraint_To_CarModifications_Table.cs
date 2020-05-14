using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoParts.Data.EF.Migrations.Migrations
{
    public partial class Update_AutoParts_Table_Add_Image_And_Constraint_To_CarModifications_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CarModificationId",
                table: "AutoParts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AutoParts",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "6e3f97e1-6ecb-47d9-9aeb-339d3f980888");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "c8c5bf9c-4d6c-4d34-8e36-2453a6ef10f5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "6df77bd4-0b8c-4296-8da1-2ef419b02829");

            migrationBuilder.CreateIndex(
                name: "IX_AutoParts_CarModificationId",
                table: "AutoParts",
                column: "CarModificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AutoParts_CarModifications_CarModificationId",
                table: "AutoParts",
                column: "CarModificationId",
                principalTable: "CarModifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutoParts_CarModifications_CarModificationId",
                table: "AutoParts");

            migrationBuilder.DropIndex(
                name: "IX_AutoParts_CarModificationId",
                table: "AutoParts");

            migrationBuilder.DropColumn(
                name: "CarModificationId",
                table: "AutoParts");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "AutoParts");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "6e61442a-462b-4482-afe4-6f3c615eb7b9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "d6b3f5e9-ba52-42d4-8805-bc100663d067");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "a239a165-ca60-4143-be75-8b660ce3eaf5");
        }
    }
}
