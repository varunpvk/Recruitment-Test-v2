using Microsoft.EntityFrameworkCore.Migrations;

namespace JG.FinTech.Models.Migrations
{
    public partial class GiftAidMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonorDetails",
                columns: table => new
                {
                    DonorID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    DonationAmount = table.Column<double>(nullable: false),
                    GiftAid = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorDetails", x => x.DonorID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonorDetails");
        }
    }
}
