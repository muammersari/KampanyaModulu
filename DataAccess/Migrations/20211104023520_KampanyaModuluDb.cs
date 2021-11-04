using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class KampanyaModuluDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KDV = table.Column<int>(type: "int", nullable: false),
                    Amout = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KDV = table.Column<int>(type: "int", nullable: false),
                    OIV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "Campaings",
                columns: table => new
                {
                    CampaingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaingName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampaingDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaings", x => x.CampaingId);
                    table.ForeignKey(
                        name: "FK_Campaings_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampaingAndProducts",
                columns: table => new
                {
                    CampaingAndProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaingId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaingAndProducts", x => x.CampaingAndProductId);
                    table.ForeignKey(
                        name: "FK_CampaingAndProducts_Campaings_CampaingId",
                        column: x => x.CampaingId,
                        principalTable: "Campaings",
                        principalColumn: "CampaingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaingAndProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampaingAndProducts_CampaingId",
                table: "CampaingAndProducts",
                column: "CampaingId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaingAndProducts_ProductId",
                table: "CampaingAndProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaings_ScheduleId",
                table: "Campaings",
                column: "ScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaingAndProducts");

            migrationBuilder.DropTable(
                name: "Campaings");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Schedules");
        }
    }
}
