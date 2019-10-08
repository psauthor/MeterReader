using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeterReaderWeb.Migrations
{
    public partial class UpdateData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Readings_Customers_CustomerId1",
                table: "Readings");

            migrationBuilder.DropIndex(
                name: "IX_Readings_CustomerId1",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Readings");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Readings",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Readings",
                columns: new[] { "Id", "CustomerId", "ReadingDate", "Value" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2019, 9, 24, 23, 38, 57, 515, DateTimeKind.Local).AddTicks(4150), 1458.9000000000001 },
                    { 2, 1, new DateTime(2019, 9, 24, 23, 38, 57, 518, DateTimeKind.Local).AddTicks(8672), 18403.5 },
                    { 3, 2, new DateTime(2019, 9, 24, 23, 38, 57, 518, DateTimeKind.Local).AddTicks(8719), 0.0 },
                    { 4, 2, new DateTime(2019, 9, 24, 23, 38, 57, 518, DateTimeKind.Local).AddTicks(8724), 304.75 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Readings_CustomerId",
                table: "Readings",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Readings_Customers_CustomerId",
                table: "Readings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Readings_Customers_CustomerId",
                table: "Readings");

            migrationBuilder.DropIndex(
                name: "IX_Readings_CustomerId",
                table: "Readings");

            migrationBuilder.DeleteData(
                table: "Readings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Readings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Readings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Readings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Readings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "Readings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Readings_CustomerId1",
                table: "Readings",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Readings_Customers_CustomerId1",
                table: "Readings",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
