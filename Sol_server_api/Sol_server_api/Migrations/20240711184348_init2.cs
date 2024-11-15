using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sol_server_api.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PLKLoginID",
                table: "munkatars_fo",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$RZpC9aJ//HhV6qbH7/5JP./sEW9z4evM/1Hz6uUEFqmL4.kvOX1wG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PLKLoginID",
                table: "munkatars_fo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$QyJp0r/QSfH47FTpTS0l8OIvj6Yp87U7cvckpKLlAX3m8bU8Ek6Ea");
        }
    }
}
