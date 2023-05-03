using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaIS.Migrations
{
    /// <inheritdoc />
    public partial class VisitorNameAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                columns: new[] { "Name", "PasswordHash" },
                values: new object[] { "NAMELESS", "AQAAAAIAAYagAAAAEMhmWl8kCydgNgsUqdfGIYm5ICd/go1YhofCymid6gOSPLCshQH9kTUZdIjfE1ckWw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENitKxoeCpzfvwoPhIHrxLANP1XPJkrlLGba0VyTyQ/1nEDTVT8dyg3ezmCxBp+hMA==");
        }
    }
}
