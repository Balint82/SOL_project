using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sol_server_api.Migrations
{
    /// <inheritdoc />
    public partial class personalDataIDadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonalDataID",
                table: "szemelyi_adat",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$AkD2jWq1cFPbBURsZQHo8OOZpAuE/MJus/J/LchwuQimynT7xWJge");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalDataID",
                table: "szemelyi_adat");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$McgmV26dG68FgMiIWyqUmun6xKkyLMGqBAq9eHAgQQINrKK62ckz2");
        }
    }
}
