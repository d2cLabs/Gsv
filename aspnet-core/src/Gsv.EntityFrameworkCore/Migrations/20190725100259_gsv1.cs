using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gsv.Migrations
{
    public partial class gsv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Capitals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    Cn = table.Column<string>(maxLength: 6, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Contact = table.Column<string>(maxLength: 50, nullable: true),
                    WenxinIds = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capitals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    Cn = table.Column<string>(maxLength: 2, nullable: false),
                    Name = table.Column<string>(maxLength: 8, nullable: false),
                    UnitName = table.Column<string>(maxLength: 8, nullable: false),
                    CurrentPrice = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    Cn = table.Column<string>(maxLength: 6, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Contact = table.Column<string>(maxLength: 50, nullable: true),
                    WenxinIds = table.Column<string>(maxLength: 50, nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    Latitude = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    Cn = table.Column<string>(maxLength: 2, nullable: false),
                    Name = table.Column<string>(maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    Cn = table.Column<string>(maxLength: 6, nullable: false),
                    Name = table.Column<string>(maxLength: 12, nullable: false),
                    Password = table.Column<string>(maxLength: 12, nullable: false),
                    WeixinDeviceId = table.Column<string>(maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CargoTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    PlaceId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    TypeName = table.Column<string>(maxLength: 12, nullable: false),
                    Ratio = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoTypes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CargoTypes_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Objects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    CapitalId = table.Column<int>(nullable: false),
                    PlaceId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    isFixedPrice = table.Column<bool>(nullable: false),
                    FixedPrice = table.Column<float>(nullable: false),
                    Remark = table.Column<string>(maxLength: 50, nullable: false),
                    RiskRatio = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objects_Capitals_CapitalId",
                        column: x => x.CapitalId,
                        principalTable: "Capitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Objects_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Objects_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaceWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    PlaceId = table.Column<int>(nullable: false),
                    WorkerList = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaceWorkers_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaceShelves",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Identifier = table.Column<string>(maxLength: 12, nullable: false),
                    CargoTypeId = table.Column<int>(nullable: false),
                    CurrentInventory = table.Column<float>(maxLength: 50, nullable: false),
                    InventoryLastTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceShelves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaceShelves_CargoTypes_CargoTypeId",
                        column: x => x.CargoTypeId,
                        principalTable: "CargoTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inspects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    CarryoutTime = table.Column<DateTime>(nullable: false),
                    WorkerId = table.Column<int>(nullable: false),
                    ShelfId = table.Column<int>(nullable: false),
                    PlaceShelfId = table.Column<int>(nullable: true),
                    Purity = table.Column<float>(nullable: false),
                    Remark = table.Column<string>(maxLength: 50, nullable: true),
                    Photo = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inspects_PlaceShelves_PlaceShelfId",
                        column: x => x.PlaceShelfId,
                        principalTable: "PlaceShelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inspects_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InStocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    CarryoutTime = table.Column<DateTime>(nullable: false),
                    WorkerId = table.Column<int>(nullable: false),
                    ShelfId = table.Column<int>(nullable: false),
                    PlaceShelfId = table.Column<int>(nullable: true),
                    Quantity = table.Column<float>(nullable: false),
                    SourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InStocks_PlaceShelves_PlaceShelfId",
                        column: x => x.PlaceShelfId,
                        principalTable: "PlaceShelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InStocks_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InStocks_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutStocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    CarryoutTime = table.Column<DateTime>(nullable: false),
                    WorkerId = table.Column<int>(nullable: false),
                    ShelfId = table.Column<int>(nullable: false),
                    PlaceShelfId = table.Column<int>(nullable: true),
                    Quantity = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutStocks_PlaceShelves_PlaceShelfId",
                        column: x => x.PlaceShelfId,
                        principalTable: "PlaceShelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutStocks_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocktakings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    CarryoutTime = table.Column<DateTime>(nullable: false),
                    ShelfId = table.Column<int>(nullable: false),
                    PlaceShelfId = table.Column<int>(nullable: true),
                    Inventory = table.Column<float>(nullable: false),
                    Deviation = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocktakings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocktakings_PlaceShelves_PlaceShelfId",
                        column: x => x.PlaceShelfId,
                        principalTable: "PlaceShelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Capitals_Cn",
                table: "Capitals",
                column: "Cn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CargoTypes_CategoryId",
                table: "CargoTypes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoTypes_PlaceId_CategoryId_TypeName",
                table: "CargoTypes",
                columns: new[] { "PlaceId", "CategoryId", "TypeName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Cn",
                table: "Categories",
                column: "Cn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inspects_PlaceShelfId",
                table: "Inspects",
                column: "PlaceShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspects_WorkerId",
                table: "Inspects",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_InStocks_PlaceShelfId",
                table: "InStocks",
                column: "PlaceShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_InStocks_SourceId",
                table: "InStocks",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_InStocks_WorkerId",
                table: "InStocks",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_CategoryId",
                table: "Objects",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_PlaceId",
                table: "Objects",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_CapitalId_PlaceId_CategoryId",
                table: "Objects",
                columns: new[] { "CapitalId", "PlaceId", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutStocks_PlaceShelfId",
                table: "OutStocks",
                column: "PlaceShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_OutStocks_WorkerId",
                table: "OutStocks",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_Cn",
                table: "Places",
                column: "Cn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlaceShelves_CargoTypeId",
                table: "PlaceShelves",
                column: "CargoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceShelves_Name_CargoTypeId",
                table: "PlaceShelves",
                columns: new[] { "Name", "CargoTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlaceWorkers_PlaceId",
                table: "PlaceWorkers",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Sources_Cn",
                table: "Sources",
                column: "Cn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocktakings_PlaceShelfId",
                table: "Stocktakings",
                column: "PlaceShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_Cn",
                table: "Workers",
                column: "Cn",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inspects");

            migrationBuilder.DropTable(
                name: "InStocks");

            migrationBuilder.DropTable(
                name: "Objects");

            migrationBuilder.DropTable(
                name: "OutStocks");

            migrationBuilder.DropTable(
                name: "PlaceWorkers");

            migrationBuilder.DropTable(
                name: "Stocktakings");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropTable(
                name: "Capitals");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "PlaceShelves");

            migrationBuilder.DropTable(
                name: "CargoTypes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Places");
        }
    }
}
