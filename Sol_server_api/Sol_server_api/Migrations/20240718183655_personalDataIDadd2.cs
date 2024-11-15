using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sol_server_api.Migrations
{
    /// <inheritdoc />
    public partial class personalDataIDadd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_szemelyi_adat_PersonalDataTelNumber",
                table: "Admins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_szemelyi_adat",
                table: "szemelyi_adat");

            migrationBuilder.DropIndex(
                name: "IX_Admins_PersonalDataTelNumber",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "PersonalDataTelNumber",
                table: "Admins");

            migrationBuilder.AlterColumn<string>(
                name: "TelNumber",
                table: "szemelyi_adat",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "PersonalDataID",
                table: "Admins",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_szemelyi_adat",
                table: "szemelyi_adat",
                column: "PersonalDataID");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                columns: new[] { "Password", "PersonalDataID" },
                values: new object[] { "$2a$11$mngWmCHfbsOObMOloQBPNungbP.VgCjc4lOnFHS.l1NeL1Osg4ECa", null });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_PersonalDataID",
                table: "Admins",
                column: "PersonalDataID");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_szemelyi_adat_PersonalDataID",
                table: "Admins",
                column: "PersonalDataID",
                principalTable: "szemelyi_adat",
                principalColumn: "PersonalDataID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_szemelyi_adat_PersonalDataID",
                table: "Admins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_szemelyi_adat",
                table: "szemelyi_adat");

            migrationBuilder.DropIndex(
                name: "IX_Admins_PersonalDataID",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "PersonalDataID",
                table: "Admins");

            migrationBuilder.AlterColumn<string>(
                name: "TelNumber",
                table: "szemelyi_adat",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PersonalDataTelNumber",
                table: "Admins",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_szemelyi_adat",
                table: "szemelyi_adat",
                column: "TelNumber");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                columns: new[] { "Password", "PersonalDataTelNumber" },
                values: new object[] { "$2a$11$AkD2jWq1cFPbBURsZQHo8OOZpAuE/MJus/J/LchwuQimynT7xWJge", null });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_PersonalDataTelNumber",
                table: "Admins",
                column: "PersonalDataTelNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_szemelyi_adat_PersonalDataTelNumber",
                table: "Admins",
                column: "PersonalDataTelNumber",
                principalTable: "szemelyi_adat",
                principalColumn: "TelNumber");
        }
    }
}
