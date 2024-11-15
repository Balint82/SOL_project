using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sol_server_api.Migrations
{
    /// <inheritdoc />
    public partial class compartmentAutogenereatedID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$qfDsSu4l7H2T3GxJ1uzbV.5EEYbSvZeO7dwwcVfZhrBoTu91B1JBe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$B3rtKMLIGSVUruUC6sIuzurWF7BFTDDOxOvnFQnUAR79dNdnOoBcq");
        }
    }
}
