using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gsv.Migrations
{
    public partial class gsv : Migration
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
                    Mobile = table.Column<string>(maxLength: 11, nullable: true),
                    PlaceList = table.Column<string>(maxLength: 50, nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
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
                    Quantity = table.Column<float>(nullable: false),
                    YellowQuantity = table.Column<float>(nullable: false),
                    isFixedPrice = table.Column<bool>(nullable: false),
                    FixedPrice = table.Column<float>(nullable: true),
                    Remark = table.Column<string>(maxLength: 50, nullable: true)
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
                name: "Shelves",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    PlaceId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CargoTypeId = table.Column<int>(nullable: false),
                    Inventory = table.Column<float>(nullable: true),
                    NumInToday = table.Column<int>(nullable: false),
                    NumOutToday = table.Column<int>(nullable: false),
                    LastInTime = table.Column<DateTime>(nullable: true),
                    LastOutTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shelves_CargoTypes_CargoTypeId",
                        column: x => x.CargoTypeId,
                        principalTable: "CargoTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shelves_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
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
                    CarryoutDate = table.Column<DateTime>(nullable: false),
                    WorkerId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ShelfId = table.Column<int>(nullable: false),
                    Purity = table.Column<float>(nullable: false),
                    Remark = table.Column<string>(maxLength: 50, nullable: true),
                    Photo = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inspects_Shelves_ShelfId",
                        column: x => x.ShelfId,
                        principalTable: "Shelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    CarryoutDate = table.Column<DateTime>(nullable: false),
                    WorkerId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ShelfId = table.Column<int>(nullable: false),
                    Quantity = table.Column<float>(nullable: false),
                    SourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InStocks_Shelves_ShelfId",
                        column: x => x.ShelfId,
                        principalTable: "Shelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    CarryoutDate = table.Column<DateTime>(nullable: false),
                    WorkerId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ShelfId = table.Column<int>(nullable: false),
                    Quantity = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutStocks_Shelves_ShelfId",
                        column: x => x.ShelfId,
                        principalTable: "Shelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    CarryoutDate = table.Column<DateTime>(nullable: false),
                    WorkerId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ShelfId = table.Column<int>(nullable: false),
                    Inventory = table.Column<float>(nullable: false),
                    Deviation = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocktakings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocktakings_Shelves_ShelfId",
                        column: x => x.ShelfId,
                        principalTable: "Shelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocktakings_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Capitals_TenantId_Cn",
                table: "Capitals",
                columns: new[] { "TenantId", "Cn" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CargoTypes_CategoryId",
                table: "CargoTypes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoTypes_PlaceId",
                table: "CargoTypes",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoTypes_TenantId_PlaceId_CategoryId_TypeName",
                table: "CargoTypes",
                columns: new[] { "TenantId", "PlaceId", "CategoryId", "TypeName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TenantId_Cn",
                table: "Categories",
                columns: new[] { "TenantId", "Cn" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inspects_ShelfId",
                table: "Inspects",
                column: "ShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspects_WorkerId",
                table: "Inspects",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspects_TenantId_CarryoutDate_ShelfId",
                table: "Inspects",
                columns: new[] { "TenantId", "CarryoutDate", "ShelfId" });

            migrationBuilder.CreateIndex(
                name: "IX_InStocks_ShelfId",
                table: "InStocks",
                column: "ShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_InStocks_SourceId",
                table: "InStocks",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_InStocks_WorkerId",
                table: "InStocks",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_InStocks_TenantId_CarryoutDate_ShelfId",
                table: "InStocks",
                columns: new[] { "TenantId", "CarryoutDate", "ShelfId" });

            migrationBuilder.CreateIndex(
                name: "IX_Objects_CapitalId",
                table: "Objects",
                column: "CapitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_CategoryId",
                table: "Objects",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_PlaceId",
                table: "Objects",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_TenantId_CapitalId_PlaceId_CategoryId",
                table: "Objects",
                columns: new[] { "TenantId", "CapitalId", "PlaceId", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutStocks_ShelfId",
                table: "OutStocks",
                column: "ShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_OutStocks_WorkerId",
                table: "OutStocks",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_OutStocks_TenantId_CarryoutDate_ShelfId",
                table: "OutStocks",
                columns: new[] { "TenantId", "CarryoutDate", "ShelfId" });

            migrationBuilder.CreateIndex(
                name: "IX_Places_TenantId_Cn",
                table: "Places",
                columns: new[] { "TenantId", "Cn" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shelves_CargoTypeId",
                table: "Shelves",
                column: "CargoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shelves_PlaceId",
                table: "Shelves",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Shelves_TenantId_Name_CargoTypeId",
                table: "Shelves",
                columns: new[] { "TenantId", "Name", "CargoTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sources_TenantId_Cn",
                table: "Sources",
                columns: new[] { "TenantId", "Cn" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocktakings_ShelfId",
                table: "Stocktakings",
                column: "ShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocktakings_WorkerId",
                table: "Stocktakings",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocktakings_TenantId_CarryoutDate_ShelfId",
                table: "Stocktakings",
                columns: new[] { "TenantId", "CarryoutDate", "ShelfId" });

            migrationBuilder.CreateIndex(
                name: "IX_Workers_TenantId_Cn",
                table: "Workers",
                columns: new[] { "TenantId", "Cn" },
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
                name: "Stocktakings");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropTable(
                name: "Capitals");

            migrationBuilder.DropTable(
                name: "Shelves");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "CargoTypes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Places");
        }
    }
}
