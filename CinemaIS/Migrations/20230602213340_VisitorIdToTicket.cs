using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaIS.Migrations
{
    /// <inheritdoc />
    public partial class VisitorIdToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_VisitorId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_VisitorId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "VisitorId1",
                table: "Tickets");

            migrationBuilder.AlterColumn<string>(
                name: "VisitorId",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                columns: new[] { "NormalizedUserName", "PasswordHash" },
                values: new object[] { "АДМИНИСТРАТОР", "AQAAAAIAAYagAAAAEET1KMBKLVbvOMcWk87fgmzNx34aSoaXkis9P9ADR3EnpB2at10MsK76W1GeNHyRhA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_VisitorId",
                table: "Tickets",
                column: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_VisitorId",
                table: "Tickets",
                column: "VisitorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_VisitorId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_VisitorId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "VisitorId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitorId1",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                columns: new[] { "NormalizedUserName", "PasswordHash" },
                values: new object[] { "ADMIN", "AQAAAAIAAYagAAAAEPDtsOvW7gLOHVmJMxU6A2Ao9KFujTdZb0csyklJqB8cHtDq/rKWyqXbYuzS4ktwGA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_VisitorId1",
                table: "Tickets",
                column: "VisitorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_VisitorId1",
                table: "Tickets",
                column: "VisitorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
