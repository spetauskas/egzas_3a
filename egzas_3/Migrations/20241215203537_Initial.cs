using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace egzas_3.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountPasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AccountPasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AccountRole = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserSurName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserIdentityNumber = table.Column<int>(type: "int", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserResidenceCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserResidenceStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserResidenceHouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserResidenceApartmentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "AccountEmail", "AccountName", "AccountPasswordHash", "AccountPasswordSalt", "AccountRole" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "spetauskas@gmail.com", "admin", new byte[] { 103, 220, 13, 169, 48, 110, 242, 139, 27, 240, 131, 59, 192, 219, 15, 71, 244, 2, 48, 250, 118, 107, 36, 137, 170, 108, 56, 202, 51, 54, 97, 169 }, new byte[] { 128, 118, 206, 137, 56, 12, 138, 193, 181, 92, 61, 196, 49, 76, 88, 218, 82, 6, 9, 168, 21, 241, 123, 113, 97, 22, 141, 136, 54, 234, 226, 27, 242, 89, 125, 98, 222, 156, 14, 6, 53, 99, 69, 84, 251, 27, 151, 119, 74, 200, 21, 57, 124, 61, 247, 0, 99, 138, 13, 220, 251, 84, 124, 97 }, "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "UserCountry", "UserEmail", "UserIdentityNumber", "UserName", "UserResidenceApartmentNumber", "UserResidenceCity", "UserResidenceHouseNumber", "UserResidenceStreet", "UserSurName" },
                values: new object[,]
                {
                    { new Guid("30264caf-92f7-4c9c-8920-be05c6d5537f"), "Australia", "alice.johnson@example.com", 98765, "Alice", null, "Sydney", "789", "George Street", "Johnson" },
                    { new Guid("6a919b04-0087-4835-9834-4e583026e9dd"), "Canada", "jane.smith@example.com", 54321, "Jane", null, "Toronto", "456", "Queen Street", "Smith" },
                    { new Guid("c48afa0d-230f-447b-bf74-dc229ceef591"), "USA", "john.doe@example.com", 12345, "John", null, "New York", "123", "Broadway", "Doe" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
