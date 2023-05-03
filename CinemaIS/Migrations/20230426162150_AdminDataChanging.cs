using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaIS.Migrations
{
    /// <inheritdoc />
    public partial class AdminDataChanging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                columns: new[] { "Name", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "Администратор", "ADMIN", "AQAAAAIAAYagAAAAELtlWXR1vHVqzylwGuizLOM/NM+WIRrwnR1Es3E/38C+sRu4zX8icPswMtKnYejbhA==", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                columns: new[] { "Name", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "NAMELESS", "ADMIN@EMAIL.COM", "AQAAAAIAAYagAAAAEMhmWl8kCydgNgsUqdfGIYm5ICd/go1YhofCymid6gOSPLCshQH9kTUZdIjfE1ckWw==", "admin@email.com" });
        }
    }
}
