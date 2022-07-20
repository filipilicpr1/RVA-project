using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bus_BusLine_BusLineId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Manufacturer_ManufacturerId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusLineCity_BusLine_BusLinesId",
                table: "BusLineCity");

            migrationBuilder.DropForeignKey(
                name: "FK_BusLineCity_City_CitiesId",
                table: "BusLineCity");

            migrationBuilder.DropForeignKey(
                name: "FK_City_Country_CountryId",
                table: "City");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manufacturer",
                table: "Manufacturer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DropPrimaryKey(
                name: "PK_City",
                table: "City");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusLine",
                table: "BusLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bus",
                table: "Bus");

            migrationBuilder.RenameTable(
                name: "Manufacturer",
                newName: "Manufacturers");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "City",
                newName: "Cities");

            migrationBuilder.RenameTable(
                name: "BusLine",
                newName: "BusLines");

            migrationBuilder.RenameTable(
                name: "Bus",
                newName: "Buses");

            migrationBuilder.RenameIndex(
                name: "IX_Manufacturer_Name",
                table: "Manufacturers",
                newName: "IX_Manufacturers_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Country_Name",
                table: "Countries",
                newName: "IX_Countries_Name");

            migrationBuilder.RenameIndex(
                name: "IX_City_Name",
                table: "Cities",
                newName: "IX_Cities_Name");

            migrationBuilder.RenameIndex(
                name: "IX_City_CountryId",
                table: "Cities",
                newName: "IX_Cities_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_BusLine_Label",
                table: "BusLines",
                newName: "IX_BusLines_Label");

            migrationBuilder.RenameIndex(
                name: "IX_Bus_ManufacturerId",
                table: "Buses",
                newName: "IX_Buses_ManufacturerId");

            migrationBuilder.RenameIndex(
                name: "IX_Bus_Label",
                table: "Buses",
                newName: "IX_Buses_Label");

            migrationBuilder.RenameIndex(
                name: "IX_Bus_BusLineId",
                table: "Buses",
                newName: "IX_Buses_BusLineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manufacturers",
                table: "Manufacturers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusLines",
                table: "BusLines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Buses",
                table: "Buses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_BusLines_BusLineId",
                table: "Buses",
                column: "BusLineId",
                principalTable: "BusLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Manufacturers_ManufacturerId",
                table: "Buses",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusLineCity_BusLines_BusLinesId",
                table: "BusLineCity",
                column: "BusLinesId",
                principalTable: "BusLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusLineCity_Cities_CitiesId",
                table: "BusLineCity",
                column: "CitiesId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_BusLines_BusLineId",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Manufacturers_ManufacturerId",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_BusLineCity_BusLines_BusLinesId",
                table: "BusLineCity");

            migrationBuilder.DropForeignKey(
                name: "FK_BusLineCity_Cities_CitiesId",
                table: "BusLineCity");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manufacturers",
                table: "Manufacturers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusLines",
                table: "BusLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Buses",
                table: "Buses");

            migrationBuilder.RenameTable(
                name: "Manufacturers",
                newName: "Manufacturer");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "City");

            migrationBuilder.RenameTable(
                name: "BusLines",
                newName: "BusLine");

            migrationBuilder.RenameTable(
                name: "Buses",
                newName: "Bus");

            migrationBuilder.RenameIndex(
                name: "IX_Manufacturers_Name",
                table: "Manufacturer",
                newName: "IX_Manufacturer_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Countries_Name",
                table: "Country",
                newName: "IX_Country_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_Name",
                table: "City",
                newName: "IX_City_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_CountryId",
                table: "City",
                newName: "IX_City_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_BusLines_Label",
                table: "BusLine",
                newName: "IX_BusLine_Label");

            migrationBuilder.RenameIndex(
                name: "IX_Buses_ManufacturerId",
                table: "Bus",
                newName: "IX_Bus_ManufacturerId");

            migrationBuilder.RenameIndex(
                name: "IX_Buses_Label",
                table: "Bus",
                newName: "IX_Bus_Label");

            migrationBuilder.RenameIndex(
                name: "IX_Buses_BusLineId",
                table: "Bus",
                newName: "IX_Bus_BusLineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manufacturer",
                table: "Manufacturer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_City",
                table: "City",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusLine",
                table: "BusLine",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bus",
                table: "Bus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_BusLine_BusLineId",
                table: "Bus",
                column: "BusLineId",
                principalTable: "BusLine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Manufacturer_ManufacturerId",
                table: "Bus",
                column: "ManufacturerId",
                principalTable: "Manufacturer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusLineCity_BusLine_BusLinesId",
                table: "BusLineCity",
                column: "BusLinesId",
                principalTable: "BusLine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusLineCity_City_CitiesId",
                table: "BusLineCity",
                column: "CitiesId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_City_Country_CountryId",
                table: "City",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
