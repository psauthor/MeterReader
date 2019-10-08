using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeterReaderWeb.Migrations
{
    public partial class UpdateLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeterId",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "Reading",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "ReadingTime",
                table: "Readings");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReadingDate",
                table: "Readings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "Readings",
                nullable: false,
                defaultValue: 0.0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Readings_Customers_CustomerId1",
                table: "Readings");

            migrationBuilder.DropIndex(
                name: "IX_Readings_CustomerId1",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "ReadingDate",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Readings");

            migrationBuilder.AddColumn<string>(
                name: "MeterId",
                table: "Readings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Reading",
                table: "Readings",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReadingTime",
                table: "Readings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
