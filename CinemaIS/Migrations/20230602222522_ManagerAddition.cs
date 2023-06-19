using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaIS.Migrations
{
    /// <inheritdoc />
    public partial class ManagerAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "66666666-b815-455a-8908-8133983c9200", null, "manager", "MANAGER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFmVHFuogHLxR4Zn0OPYrUIgq/dVOzP/BVGrAYZpIr11LYoE0iLeCA2qWVlDYVS1PQ==");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "aaaaaaaa-3d13-433f-bc24-fcf769b6e6e7", 0, "", "manager@email.com", true, false, null, "MANAGER@EMAIL.COM", "МЕНЕДЖЕР", "AQAAAAIAAYagAAAAEIREu8LrMfZzUciLXm1CZpaQ0TxFOCJsfx6rE48kpeKTmzZFw/97hbUVyifhxDEVyg==", null, false, "", false, "Менеджер" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "66666666-b815-455a-8908-8133983c9200", "aaaaaaaa-3d13-433f-bc24-fcf769b6e6e7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "66666666-b815-455a-8908-8133983c9200", "aaaaaaaa-3d13-433f-bc24-fcf769b6e6e7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66666666-b815-455a-8908-8133983c9200");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-3d13-433f-bc24-fcf769b6e6e7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEET1KMBKLVbvOMcWk87fgmzNx34aSoaXkis9P9ADR3EnpB2at10MsK76W1GeNHyRhA==");
        }
    }
}
