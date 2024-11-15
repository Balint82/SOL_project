using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sol_server_api.Migrations
{
    /// <inheritdoc />
    public partial class GETCoworkerRolePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$5QnhghU49LcDvwn5DF9W1u66B47nSLG5cTWk9hvTFN2tvZy5.aiDq");

            migrationBuilder.InsertData(
                table: "SzerepJogosultsag",
                columns: new[] { "PermissionID", "RoleID" },
                values: new object[] { 18, 4 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SzerepJogosultsag",
                keyColumns: new[] { "PermissionID", "RoleID" },
                keyValues: new object[] { 18, 4 });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$Bc2/ESEVyw/mooqfg/zn1ehiaHBeZN876Co0z5jL0J4WhO.TEif8G");
        }
    }
}
