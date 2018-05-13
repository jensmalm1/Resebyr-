using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Data.Migrations
{
    public partial class Registration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Customers_CustomerId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Travels_TravelId",
                table: "Participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participants",
                table: "Participants");

            migrationBuilder.RenameTable(
                name: "Participants",
                newName: "Registrations");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_TravelId",
                table: "Registrations",
                newName: "IX_Registrations_TravelId");

            migrationBuilder.AddColumn<double>(
                name: "LastDebtAmount",
                table: "Customers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDebtDate",
                table: "Customers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations",
                columns: new[] { "CustomerId", "TravelId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Customers_CustomerId",
                table: "Registrations",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Travels_TravelId",
                table: "Registrations",
                column: "TravelId",
                principalTable: "Travels",
                principalColumn: "TravelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Customers_CustomerId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Travels_TravelId",
                table: "Registrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "LastDebtAmount",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LastDebtDate",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Registrations",
                newName: "Participants");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_TravelId",
                table: "Participants",
                newName: "IX_Participants_TravelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participants",
                table: "Participants",
                columns: new[] { "CustomerId", "TravelId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Customers_CustomerId",
                table: "Participants",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Travels_TravelId",
                table: "Participants",
                column: "TravelId",
                principalTable: "Travels",
                principalColumn: "TravelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
