using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccoliteAssessment.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccount_User",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "Age", "Email", "FirstName", "LastName", "Phone", "Sex" },
                values: new object[,]
                {
                    { new Guid("29ce09a1-05bf-4b97-a69a-22352272a2c1"), 25, "test@gmail.com", "Srashti", "Gupta", 123456, "F" },
                    { new Guid("d3ed77d6-42ba-4bfc-8a22-94a830f30049"), 25, "test@outlook.com", "Mukesh", "Shukla", 123456, "M" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserAccount",
                columns: new[] { "Id", "Balance", "CreatedAt", "Currency", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("4059a894-ce73-4af0-9c0e-fa2462f2b42a"), 100.0, new DateTime(2023, 10, 18, 20, 58, 9, 2, DateTimeKind.Utc).AddTicks(1901), "USD", null, new Guid("29ce09a1-05bf-4b97-a69a-22352272a2c1") },
                    { new Guid("47a4c632-6cc9-4023-ad62-19b0242a49e7"), 1000.0, new DateTime(2023, 10, 18, 20, 58, 9, 2, DateTimeKind.Utc).AddTicks(1904), "USD", null, new Guid("d3ed77d6-42ba-4bfc-8a22-94a830f30049") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_UserId",
                schema: "dbo",
                table: "UserAccount",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccount",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");
        }
    }
}
