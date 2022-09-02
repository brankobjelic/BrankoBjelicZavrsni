using Microsoft.EntityFrameworkCore.Migrations;

namespace BrankoBjelicZavrsni.Migrations
{
    public partial class addedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    YearFounded = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstateType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    YearConstructed = table.Column<int>(type: "int", nullable: false),
                    EstatePrice = table.Column<int>(type: "int", nullable: false),
                    AgencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisements_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Agencies",
                columns: new[] { "Id", "Name", "YearFounded" },
                values: new object[] { 1, "Naj nekretnine", 2005 });

            migrationBuilder.InsertData(
                table: "Agencies",
                columns: new[] { "Id", "Name", "YearFounded" },
                values: new object[] { 2, "dupleks nekretnine", 2010 });

            migrationBuilder.InsertData(
                table: "Agencies",
                columns: new[] { "Id", "Name", "YearFounded" },
                values: new object[] { 3, "Fast nekretnine", 2005 });

            migrationBuilder.InsertData(
                table: "Advertisements",
                columns: new[] { "Id", "AgencyId", "EstatePrice", "EstateType", "Name", "YearConstructed" },
                values: new object[,]
                {
                    { 2, 1, 80000, "Stan", "Stan na ekstra lokaciji", 1979 },
                    { 4, 1, 50000, "vikendica", "Povoljna vikendica", 1971 },
                    { 3, 2, 220000, "Dupleks stan", "Moderan dupleks", 2020 },
                    { 1, 3, 110000, "Kuca", "Komforna porodicna kuca", 1987 },
                    { 5, 3, 90000, "Stan", "Stan u sirem centru", 1955 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_AgencyId",
                table: "Advertisements",
                column: "AgencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisements");

            migrationBuilder.DropTable(
                name: "Agencies");
        }
    }
}
