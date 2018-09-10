using Microsoft.EntityFrameworkCore.Migrations;

namespace ShareMe.Core.Migrations
{
	public partial class NewLife : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "AspNetRoleClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserLogins");

			migrationBuilder.DropTable(
				name: "AspNetUserRoles");

			migrationBuilder.DropTable(
				name: "AspNetUserTokens");

			migrationBuilder.DropTable(
				name: "Photos");

			migrationBuilder.DropTable(
				name: "AspNetRoles");

			migrationBuilder.DropTable(
				name: "AspNetUsers");
		}
	}
}
