using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sol_server_api.Migrations
{
    /// <inheritdoc />
    public partial class ProjectCoworkerCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_alkatresz_projekt_csomag_FKPackageID",
                table: "alkatresz");

            migrationBuilder.DropForeignKey(
                name: "FK_rekesz_alkatresz_FKComponentID",
                table: "rekesz");

            migrationBuilder.DropTable(
                name: "CoworkerProject");

            migrationBuilder.RenameColumn(
                name: "projectDate",
                table: "projekt",
                newName: "ProjectDate");

            migrationBuilder.RenameColumn(
                name: "Desc",
                table: "projekt",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "FKCoworkerID",
                table: "projekt",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcessID",
                table: "projekt",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectPackageID",
                table: "projekt",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$NYHqZb6G5q8VRyDo6.Scs.we5UfLO85sS8.vf/32S/w0xB/yL85yu");

            migrationBuilder.CreateIndex(
                name: "IX_projekt_FKCoworkerID",
                table: "projekt",
                column: "FKCoworkerID");

            migrationBuilder.AddForeignKey(
                name: "FK_alkatresz_projekt_csomag_FKPackageID",
                table: "alkatresz",
                column: "FKPackageID",
                principalTable: "projekt_csomag",
                principalColumn: "ProjectPackageID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_projekt_munkatars_fo_FKCoworkerID",
                table: "projekt",
                column: "FKCoworkerID",
                principalTable: "munkatars_fo",
                principalColumn: "CoworkerID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_rekesz_alkatresz_FKComponentID",
                table: "rekesz",
                column: "FKComponentID",
                principalTable: "alkatresz",
                principalColumn: "ComponentID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_alkatresz_projekt_csomag_FKPackageID",
                table: "alkatresz");

            migrationBuilder.DropForeignKey(
                name: "FK_projekt_munkatars_fo_FKCoworkerID",
                table: "projekt");

            migrationBuilder.DropForeignKey(
                name: "FK_rekesz_alkatresz_FKComponentID",
                table: "rekesz");

            migrationBuilder.DropIndex(
                name: "IX_projekt_FKCoworkerID",
                table: "projekt");

            migrationBuilder.DropColumn(
                name: "FKCoworkerID",
                table: "projekt");

            migrationBuilder.DropColumn(
                name: "ProcessID",
                table: "projekt");

            migrationBuilder.DropColumn(
                name: "ProjectPackageID",
                table: "projekt");

            migrationBuilder.RenameColumn(
                name: "ProjectDate",
                table: "projekt",
                newName: "projectDate");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "projekt",
                newName: "Desc");

            migrationBuilder.CreateTable(
                name: "CoworkerProject",
                columns: table => new
                {
                    CoworkersCoworkerID = table.Column<int>(type: "int", nullable: false),
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkerProject", x => new { x.CoworkersCoworkerID, x.ProjectID });
                    table.ForeignKey(
                        name: "FK_CoworkerProject_munkatars_fo_CoworkersCoworkerID",
                        column: x => x.CoworkersCoworkerID,
                        principalTable: "munkatars_fo",
                        principalColumn: "CoworkerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoworkerProject_projekt_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "projekt",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$5QnhghU49LcDvwn5DF9W1u66B47nSLG5cTWk9hvTFN2tvZy5.aiDq");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkerProject_ProjectID",
                table: "CoworkerProject",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_alkatresz_projekt_csomag_FKPackageID",
                table: "alkatresz",
                column: "FKPackageID",
                principalTable: "projekt_csomag",
                principalColumn: "ProjectPackageID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_rekesz_alkatresz_FKComponentID",
                table: "rekesz",
                column: "FKComponentID",
                principalTable: "alkatresz",
                principalColumn: "ComponentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
