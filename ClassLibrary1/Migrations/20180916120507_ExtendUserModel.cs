using Microsoft.EntityFrameworkCore.Migrations;

namespace ShareMe.Core.Migrations
{
	public partial class ExtendUserModel : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "ProfileImageUrl",
				table: "AspNetUsers",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "Description",
				table: "AspNetUsers",
				nullable: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn("ProfileImageUrl", "AspNetUsers");
			migrationBuilder.DropColumn("Description", "AspNetUsers");
		}
	}
}
