using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Data.Migrations
{
    public partial class participant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Travel_Customers_CustomerId",
                table: "Travel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Travel",
                table: "Travel");

            migrationBuilder.DropIndex(
                name: "IX_Travel_CustomerId",
                table: "Travel");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Travel");

            migrationBuilder.RenameTable(
                name: "Travel",
                newName: "Travels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Travels",
                table: "Travels",
                column: "TravelId");

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false),
                    TravelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => new { x.CustomerId, x.TravelId });
                    table.ForeignKey(
                        name: "FK_Participants_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participants_Travels_TravelId",
                        column: x => x.TravelId,
                        principalTable: "Travels",
                        principalColumn: "TravelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_TravelId",
                table: "Participants",
                column: "TravelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Travels",
                table: "Travels");

            migrationBuilder.RenameTable(
                name: "Travels",
                newName: "Travel");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Travel",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Travel",
                table: "Travel",
                column: "TravelId");

            migrationBuilder.CreateIndex(
                name: "IX_Travel_CustomerId",
                table: "Travel",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Travel_Customers_CustomerId",
                table: "Travel",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
