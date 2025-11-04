using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataAndInheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Cars_CarId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Clients_ClientId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "VIN",
                table: "Rentals");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerDay",
                table: "Rentals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentDate",
                table: "Rentals",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "TaxNumber",
                table: "Clients",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Clients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ClientType",
                table: "Clients",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "DiscountRate",
                table: "Clients",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MembershipDate",
                table: "Clients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VIN",
                table: "Cars",
                type: "nvarchar(17)",
                maxLength: 17,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Cars_VIN",
                table: "Cars",
                column: "VIN");

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Brand", "IsRented", "Model", "VIN", "Year" },
                values: new object[,]
                {
                    { 1, "Mazda", false, "CX-5", "7TDBE32K123456789", 2021 },
                    { 2, "Audi", false, "Q7", "8BAJU71030BL12345", 2017 }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Age", "ClientType", "DiscountRate", "MembershipDate", "Name", "Surname", "TaxNumber" },
                values: new object[] { 100, 29, "Premium", 0.14999999999999999, new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ірина", "Ковальук", "1234567890" });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Client_Age",
                table: "Clients",
                sql: "Age >= 18");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Car_Year",
                table: "Cars",
                sql: "Year <= 2025");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Cars_CarId",
                table: "Rentals",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Clients_ClientId",
                table: "Rentals",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Cars_CarId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Clients_ClientId",
                table: "Rentals");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Client_Age",
                table: "Clients");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Cars_VIN",
                table: "Cars");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Car_Year",
                table: "Cars");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 100);

            migrationBuilder.DropColumn(
                name: "PricePerDay",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "RentDate",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "ClientType",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "DiscountRate",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "MembershipDate",
                table: "Clients");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Rentals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "VIN",
                table: "Rentals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "TaxNumber",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "VIN",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(17)",
                oldMaxLength: 17);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Cars_CarId",
                table: "Rentals",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Clients_ClientId",
                table: "Rentals",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
