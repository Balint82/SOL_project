using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sol_server_api.Migrations
{
    /// <inheritdoc />
    public partial class PackageAndComponentsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_alkatresz_projekt_csomag_FKPackageID",
                table: "alkatresz");

            migrationBuilder.DropIndex(
                name: "IX_alkatresz_FKPackageID",
                table: "alkatresz");

            migrationBuilder.DropColumn(
                name: "RealPiece",
                table: "projekt_csomag");

            migrationBuilder.DropColumn(
                name: "RequiredPiece",
                table: "projekt_csomag");

            migrationBuilder.CreateTable(
                name: "projektcsomag_alkatrész",
                columns: table => new
                {
                    PackageComponentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredPiece = table.Column<int>(type: "int", nullable: false),
                    RealPiece = table.Column<int>(type: "int", nullable: false),
                    ProjectPackageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projektcsomag_alkatrész", x => x.PackageComponentID);
                    table.ForeignKey(
                        name: "FK_projektcsomag_alkatrész_projekt_csomag_ProjectPackageID",
                        column: x => x.ProjectPackageID,
                        principalTable: "projekt_csomag",
                        principalColumn: "ProjectPackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$B3rtKMLIGSVUruUC6sIuzurWF7BFTDDOxOvnFQnUAR79dNdnOoBcq");

            migrationBuilder.CreateIndex(
                name: "IX_projektcsomag_alkatrész_ProjectPackageID",
                table: "projektcsomag_alkatrész",
                column: "ProjectPackageID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "projektcsomag_alkatrész");

            migrationBuilder.AddColumn<int>(
                name: "RealPiece",
                table: "projekt_csomag",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequiredPiece",
                table: "projekt_csomag",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$DHO10G6lgbKMx5diewKBrePhP4lY9AU6Y5QMZHI4s6R7XWGKNHsmu");

            migrationBuilder.CreateIndex(
                name: "IX_alkatresz_FKPackageID",
                table: "alkatresz",
                column: "FKPackageID");

            migrationBuilder.AddForeignKey(
                name: "FK_alkatresz_projekt_csomag_FKPackageID",
                table: "alkatresz",
                column: "FKPackageID",
                principalTable: "projekt_csomag",
                principalColumn: "ProjectPackageID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
