using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sol_server_api.Migrations
{
    /// <inheritdoc />
    public partial class adminsDbsetAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_Szerep_RoleID",
                table: "Admin");

            migrationBuilder.DropForeignKey(
                name: "FK_Admin_szemelyi_adat_PersonalDataTelNumber",
                table: "Admin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admin",
                table: "Admin");

            migrationBuilder.RenameTable(
                name: "Admin",
                newName: "Admins");

            migrationBuilder.RenameIndex(
                name: "IX_Admin_RoleID",
                table: "Admins",
                newName: "IX_Admins_RoleID");

            migrationBuilder.RenameIndex(
                name: "IX_Admin_PersonalDataTelNumber",
                table: "Admins",
                newName: "IX_Admins_PersonalDataTelNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admins",
                table: "Admins",
                column: "AdminID");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$mYVhT4OZeXMZGr9HjPM6F.eFC.QvoYbT3tiNiQTyZ4Pye0.E5XKVa");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Szerep_RoleID",
                table: "Admins",
                column: "RoleID",
                principalTable: "Szerep",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_szemelyi_adat_PersonalDataTelNumber",
                table: "Admins",
                column: "PersonalDataTelNumber",
                principalTable: "szemelyi_adat",
                principalColumn: "TelNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Szerep_RoleID",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Admins_szemelyi_adat_PersonalDataTelNumber",
                table: "Admins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admins",
                table: "Admins");

            migrationBuilder.RenameTable(
                name: "Admins",
                newName: "Admin");

            migrationBuilder.RenameIndex(
                name: "IX_Admins_RoleID",
                table: "Admin",
                newName: "IX_Admin_RoleID");

            migrationBuilder.RenameIndex(
                name: "IX_Admins_PersonalDataTelNumber",
                table: "Admin",
                newName: "IX_Admin_PersonalDataTelNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admin",
                table: "Admin",
                column: "AdminID");

            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$RZpC9aJ//HhV6qbH7/5JP./sEW9z4evM/1Hz6uUEFqmL4.kvOX1wG");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_Szerep_RoleID",
                table: "Admin",
                column: "RoleID",
                principalTable: "Szerep",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_szemelyi_adat_PersonalDataTelNumber",
                table: "Admin",
                column: "PersonalDataTelNumber",
                principalTable: "szemelyi_adat",
                principalColumn: "TelNumber");
        }
    }
}
