using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gsv.Migrations
{
    public partial class gsv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "OutStocks");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "InStocks");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Inspects");

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

            migrationBuilder.AddColumn<string>(
                name: "PhotoFile",
                table: "Stocktakings",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Stocktakings",
                maxLength: 50,
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "PhotoFile",
                table: "OutStocks",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "OutStocks",
                maxLength: 50,
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "CameraIps",
                table: "Objects",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "InStocks",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<string>(
                name: "PhotoFile",
                table: "InStocks",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "InStocks",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoFile",
                table: "Inspects",
                maxLength: 100,
                nullable: true);

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
                    PhotoFile = table.Column<string>(maxLength: 100, nullable: true),
                    Remark = table.Column<string>(maxLength: 50, nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.DropColumn(
                name: "PhotoFile",
                table: "Stocktakings");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Stocktakings");

            migrationBuilder.DropColumn(
                name: "PhotoFile",
                table: "OutStocks");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "OutStocks");

            migrationBuilder.DropColumn(
                name: "CameraIps",
                table: "Objects");

            migrationBuilder.DropColumn(
                name: "PhotoFile",
                table: "InStocks");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "InStocks");

            migrationBuilder.DropColumn(
                name: "PhotoFile",
                table: "Inspects");

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

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "OutStocks",
                nullable: true);

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

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "InStocks",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Inspects",
                nullable: true);
        }
    }
}
