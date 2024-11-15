using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sol_server_api.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "jogosultsag",
                columns: table => new
                {
                    PermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jogosultsag", x => x.PermissionID);
                });

            migrationBuilder.CreateTable(
                name: "megrendelo",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_megrendelo", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Szerep",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(42)", maxLength: 42, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Szerep", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "projekt",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    projectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FKCustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projekt", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK_projekt_megrendelo_FKCustomerID",
                        column: x => x.FKCustomerID,
                        principalTable: "megrendelo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "munkatars_fo",
                columns: table => new
                {
                    CoworkerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoworkerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    PLKLoginID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_munkatars_fo", x => x.CoworkerID);
                    table.ForeignKey(
                        name: "FK_munkatars_fo_Szerep_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Szerep",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SzerepJogosultsag",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    PermissionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SzerepJogosultsag", x => new { x.RoleID, x.PermissionID });
                    table.ForeignKey(
                        name: "FK_SzerepJogosultsag_Szerep_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Szerep",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SzerepJogosultsag_jogosultsag_PermissionID",
                        column: x => x.PermissionID,
                        principalTable: "jogosultsag",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "folyamat",
                columns: table => new
                {
                    ProcessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FKProjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_folyamat", x => x.ProcessID);
                    table.ForeignKey(
                        name: "FK_folyamat_projekt_FKProjectID",
                        column: x => x.FKProjectID,
                        principalTable: "projekt",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "projekt_csomag",
                columns: table => new
                {
                    ProjectPackageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequiredComponentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredPiece = table.Column<int>(type: "int", nullable: false),
                    FKProjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projekt_csomag", x => x.ProjectPackageID);
                    table.ForeignKey(
                        name: "FK_projekt_csomag_projekt_FKProjectID",
                        column: x => x.FKProjectID,
                        principalTable: "projekt",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "login",
                columns: table => new
                {
                    LoginID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FKLoginCWID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login", x => x.LoginID);
                    table.ForeignKey(
                        name: "FK_login_munkatars_fo_FKLoginCWID",
                        column: x => x.FKLoginCWID,
                        principalTable: "munkatars_fo",
                        principalColumn: "CoworkerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "szemelyi_adat",
                columns: table => new
                {
                    TelNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoworkerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_szemelyi_adat", x => x.TelNumber);
                    table.ForeignKey(
                        name: "FK_szemelyi_adat_munkatars_fo_CoworkerID",
                        column: x => x.CoworkerID,
                        principalTable: "munkatars_fo",
                        principalColumn: "CoworkerID");
                });

            migrationBuilder.CreateTable(
                name: "alkatresz",
                columns: table => new
                {
                    ComponentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    StockStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FKPackageID = table.Column<int>(type: "int", nullable: true),
                    CompartmentID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alkatresz", x => x.ComponentID);
                    table.ForeignKey(
                        name: "FK_alkatresz_projekt_csomag_FKPackageID",
                        column: x => x.FKPackageID,
                        principalTable: "projekt_csomag",
                        principalColumn: "ProjectPackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    PersonalDataTelNumber = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminID);
                    table.ForeignKey(
                        name: "FK_Admin_Szerep_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Szerep",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Admin_szemelyi_adat_PersonalDataTelNumber",
                        column: x => x.PersonalDataTelNumber,
                        principalTable: "szemelyi_adat",
                        principalColumn: "TelNumber");
                });

            migrationBuilder.CreateTable(
                name: "rekesz",
                columns: table => new
                {
                    CompartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoragedComponentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Row = table.Column<int>(type: "int", nullable: false),
                    Col = table.Column<int>(type: "int", nullable: false),
                    Bracket = table.Column<int>(type: "int", nullable: false),
                    MaximumPiece = table.Column<int>(type: "int", nullable: false),
                    StoragedPiece = table.Column<int>(type: "int", nullable: true),
                    FKComponentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rekesz", x => x.CompartmentID);
                    table.ForeignKey(
                        name: "FK_rekesz_alkatresz_FKComponentID",
                        column: x => x.FKComponentID,
                        principalTable: "alkatresz",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Szerep",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[,]
                {
                    { 1, "Szakember" },
                    { 2, "Raktárvezető" },
                    { 3, "Raktáros" },
                    { 4, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "jogosultsag",
                columns: new[] { "PermissionID", "PermissionName" },
                values: new object[,]
                {
                    { 1, "Új projekt létrehozása" },
                    { 2, "Projektek listázása" },
                    { 3, "Alkatrészek listázása" },
                    { 4, "Alkatrészek projekthez rendelése" },
                    { 5, "Becsült munkavégzési idő rögzítése" },
                    { 6, "Árkalkuláció elkészítése" },
                    { 7, "Projekt lezárása" },
                    { 8, "Új alkatrészek felvétele a rendszerbe" },
                    { 9, "Árak módosítása" },
                    { 10, "Hiányzó alkatrészek listázása" },
                    { 11, "Beérkező anyagok felvétele" },
                    { 12, "Rekeszeknél a maximálisan elhelyezhető darabszám kezelése" },
                    { 13, "Projekt kiválasztása kivételezéshez" },
                    { 14, "Projekthez tartozó alkatrészek listázása" },
                    { 15, "CREATE_COWORKER" },
                    { 16, "UPDATE_COWORKER" },
                    { 17, "DELETE_COWORKER" }
                });

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "AdminID", "AdminName", "Password", "PersonalDataTelNumber", "RoleID" },
                values: new object[] { 1, "admin", "$2a$11$QyJp0r/QSfH47FTpTS0l8OIvj6Yp87U7cvckpKLlAX3m8bU8Ek6Ea", null, 4 });

            migrationBuilder.InsertData(
                table: "SzerepJogosultsag",
                columns: new[] { "PermissionID", "RoleID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 2 },
                    { 9, 2 },
                    { 10, 2 },
                    { 11, 2 },
                    { 12, 2 },
                    { 13, 3 },
                    { 14, 3 },
                    { 15, 4 },
                    { 16, 4 },
                    { 17, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_PersonalDataTelNumber",
                table: "Admin",
                column: "PersonalDataTelNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_RoleID",
                table: "Admin",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_alkatresz_FKPackageID",
                table: "alkatresz",
                column: "FKPackageID");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkerProject_ProjectID",
                table: "CoworkerProject",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_folyamat_FKProjectID",
                table: "folyamat",
                column: "FKProjectID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_login_FKLoginCWID",
                table: "login",
                column: "FKLoginCWID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_login_LoginName",
                table: "login",
                column: "LoginName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_munkatars_fo_RoleID",
                table: "munkatars_fo",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_projekt_FKCustomerID",
                table: "projekt",
                column: "FKCustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_projekt_csomag_FKProjectID",
                table: "projekt_csomag",
                column: "FKProjectID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rekesz_FKComponentID",
                table: "rekesz",
                column: "FKComponentID",
                unique: true,
                filter: "[FKComponentID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_szemelyi_adat_CoworkerID",
                table: "szemelyi_adat",
                column: "CoworkerID",
                unique: true,
                filter: "[CoworkerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SzerepJogosultsag_PermissionID",
                table: "SzerepJogosultsag",
                column: "PermissionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "CoworkerProject");

            migrationBuilder.DropTable(
                name: "folyamat");

            migrationBuilder.DropTable(
                name: "login");

            migrationBuilder.DropTable(
                name: "rekesz");

            migrationBuilder.DropTable(
                name: "SzerepJogosultsag");

            migrationBuilder.DropTable(
                name: "szemelyi_adat");

            migrationBuilder.DropTable(
                name: "alkatresz");

            migrationBuilder.DropTable(
                name: "jogosultsag");

            migrationBuilder.DropTable(
                name: "munkatars_fo");

            migrationBuilder.DropTable(
                name: "projekt_csomag");

            migrationBuilder.DropTable(
                name: "Szerep");

            migrationBuilder.DropTable(
                name: "projekt");

            migrationBuilder.DropTable(
                name: "megrendelo");
        }
    }
}
