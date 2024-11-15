using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sol_server_api.Migrations
{
    /// <inheritdoc />
    public partial class AddProcessDatas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_folyamat_projekt_FKProjectID",
                table: "folyamat");

            migrationBuilder.DropIndex(
                name: "IX_folyamat_FKProjectID",
                table: "folyamat");

            migrationBuilder.DropColumn(
                name: "ProcessID",
                table: "projekt");

            migrationBuilder.DropColumn(
                name: "ProjectPackageID",
                table: "projekt");

            migrationBuilder.AlterColumn<int>(
                name: "FKProjectID",
                table: "folyamat",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProjectID",
                table: "folyamat",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$5AxyGTtaHDmEbsQZnTyM9ebWmeXgFGyIu1MVPxQN4mffh0lGDcelS");

            migrationBuilder.InsertData(
                table: "folyamat",
                columns: new[] { "ProcessID", "Desc", "FKProjectID", "ProcessName", "ProjectID" },
                values: new object[,]
                {
                    { 1, "A projekt létrehozásra került, de még nem történt meg a helyszíni felmérés.", null, "New", null },
                    { 2, "A helyszíni felmérés folyamatban van, a terv még nem került véglegesítésre.", null, "Draft", null },
                    { 3, "A helyszíni felmérés megtörtént, de az árkalkulációt nem lehetett befejezni, mert volt olyan alkatrész, amely nincs a raktárban, így az ára nem ismert.", null, "Wait", null },
                    { 4, "Árkalkuláció elkészült, a projekt a megvalósításra várakozik.", null, "Scheduled", null },
                    { 5, "A projekt megvalósítása megkezdődött, amelynek első lépése az alkatrészek raktárból való kivételezése", null, "InProgress", null },
                    { 6, "A projekt sikeresen megvalósult.", null, "Completed", null },
                    { 7, "A projekt megvalósítása nem sikerült.", null, "Failed", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_folyamat_FKProjectID",
                table: "folyamat",
                column: "FKProjectID",
                unique: true,
                filter: "[FKProjectID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_folyamat_ProjectID",
                table: "folyamat",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_folyamat_projekt_FKProjectID",
                table: "folyamat",
                column: "FKProjectID",
                principalTable: "projekt",
                principalColumn: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_folyamat_projekt_ProjectID",
                table: "folyamat",
                column: "ProjectID",
                principalTable: "projekt",
                principalColumn: "ProjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_folyamat_projekt_FKProjectID",
                table: "folyamat");

            migrationBuilder.DropForeignKey(
                name: "FK_folyamat_projekt_ProjectID",
                table: "folyamat");

            migrationBuilder.DropIndex(
                name: "IX_folyamat_FKProjectID",
                table: "folyamat");

            migrationBuilder.DropIndex(
                name: "IX_folyamat_ProjectID",
                table: "folyamat");

            migrationBuilder.DeleteData(
                table: "folyamat",
                keyColumn: "ProcessID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "folyamat",
                keyColumn: "ProcessID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "folyamat",
                keyColumn: "ProcessID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "folyamat",
                keyColumn: "ProcessID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "folyamat",
                keyColumn: "ProcessID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "folyamat",
                keyColumn: "ProcessID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "folyamat",
                keyColumn: "ProcessID",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "folyamat");

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

            migrationBuilder.AlterColumn<int>(
                name: "FKProjectID",
                table: "folyamat",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$NYHqZb6G5q8VRyDo6.Scs.we5UfLO85sS8.vf/32S/w0xB/yL85yu");

            migrationBuilder.CreateIndex(
                name: "IX_folyamat_FKProjectID",
                table: "folyamat",
                column: "FKProjectID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_folyamat_projekt_FKProjectID",
                table: "folyamat",
                column: "FKProjectID",
                principalTable: "projekt",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
