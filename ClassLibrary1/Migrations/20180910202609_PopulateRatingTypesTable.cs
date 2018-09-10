using Microsoft.EntityFrameworkCore.Migrations;

namespace ShareMe.Core.Migrations
{
	public partial class PopulateRatingTypesTable : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("Insert into RatingType Values ('Like'), ('Dislike')");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("remove from RatingType where Name = 'Like' or Name ='Dislike'");
		}
	}
}
