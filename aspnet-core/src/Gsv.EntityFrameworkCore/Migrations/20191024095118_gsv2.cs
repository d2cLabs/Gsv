using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gsv.Migrations
{
    public partial class gsv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Inventory",
                table: "Stocktakings",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "Deviation",
                table: "Stocktakings",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Inventory",
                table: "Shelves",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "OutStocks",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "YellowQuantity",
                table: "Objects",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Objects",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "InStocks",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.CreateTable(
                name: "Allots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    CarryoutDate = table.Column<DateTime>(nullable: false),
                    WorkerId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    FromShelfId = table.Column<int>(nullable: false),
                    ToShelfId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Photo = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Allots_Shelves_FromShelfId",
                        column: x => x.FromShelfId,
                        principalTable: "Shelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Allots_Shelves_ToShelfId",
                        column: x => x.ToShelfId,
                        principalTable: "Shelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Allots_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allots_FromShelfId",
                table: "Allots",
                column: "FromShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_Allots_ToShelfId",
                table: "Allots",
                column: "ToShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_Allots_WorkerId",
                table: "Allots",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Allots_TenantId_CarryoutDate_FromShelfId",
                table: "Allots",
                columns: new[] { "TenantId", "CarryoutDate", "FromShelfId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allots");

            migrationBuilder.AlterColumn<float>(
                name: "Inventory",
                table: "Stocktakings",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "Deviation",
                table: "Stocktakings",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Inventory",
                table: "Shelves",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Quantity",
                table: "OutStocks",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "YellowQuantity",
                table: "Objects",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "Quantity",
                table: "Objects",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "Quantity",
                table: "InStocks",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
