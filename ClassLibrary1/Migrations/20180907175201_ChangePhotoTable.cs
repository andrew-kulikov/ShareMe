using Microsoft.EntityFrameworkCore.Migrations;

namespace ShareMe.Core.Migrations
{
	public partial class ChangePhotoTable : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Photos_ApplicationUser_UserId",
				table: "Photos");

			migrationBuilder.AlterColumn<string>(
				name: "UserId",
				table: "Photos",
				nullable: false,
				oldClrType: typeof(string),
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "Url",
				table: "Photos",
				nullable: false,
				oldClrType: typeof(string),
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "Description",
				table: "Photos",
				maxLength: 1000,
				nullable: true,
				oldClrType: typeof(string),
				oldNullable: true);

			migrationBuilder.AddForeignKey(
				name: "FK_Photos_AspNetUsers_UserId",
				table: "Photos",
				column: "UserId",
				principalTable: "AspNetUsers",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Photos_AspNetUsers_UserId",
				table: "Photos");

			migrationBuilder.AlterColumn<string>(
				name: "UserId",
				table: "Photos",
				nullable: true,
				oldClrType: typeof(string));

			migrationBuilder.AlterColumn<string>(
				name: "Url",
				table: "Photos",
				nullable: true,
				oldClrType: typeof(string));

			migrationBuilder.AlterColumn<string>(
				name: "Description",
				table: "Photos",
				nullable: true,
				oldClrType: typeof(string),
				oldMaxLength: 1000,
				oldNullable: true);

			migrationBuilder.AddForeignKey(
				name: "FK_Photos_AspNetUsers_UserId",
				table: "Photos",
				column: "UserId",
				principalTable: "AspNetUsers",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}
	}
}
