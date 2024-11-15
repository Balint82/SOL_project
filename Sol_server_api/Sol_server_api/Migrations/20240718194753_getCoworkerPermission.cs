using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sol_server_api.Migrations
{
    /// <inheritdoc />
    public partial class getCoworkerPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$Bc2/ESEVyw/mooqfg/zn1ehiaHBeZN876Co0z5jL0J4WhO.TEif8G");

            migrationBuilder.InsertData(
                table: "jogosultsag",
                columns: new[] { "PermissionID", "PermissionName" },
                values: new object[] { 18, "GET_COWORKER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "jogosultsag",
                keyColumn: "PermissionID",
                keyValue: 18);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$mngWmCHfbsOObMOloQBPNungbP.VgCjc4lOnFHS.l1NeL1Osg4ECa");
        }
    }
}
