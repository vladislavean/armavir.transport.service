using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace armavir.transport.dal.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Company = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MaxCount = table.Column<int>(type: "integer", nullable: false),
                    ShortRoute = table.Column<double>(type: "numeric(10,2)", nullable: false),
                    LongRoute = table.Column<double>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportStops",
                columns: table => new
                {
                    Direction = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TransportId = table.Column<Guid>(type: "uuid", nullable: false),
                    StopId = table.Column<Guid>(type: "uuid", nullable: false),
                    StopOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportStops", x => new { x.TransportId, x.StopId, x.Direction });
                    table.ForeignKey(
                        name: "FK_TransportStops_Stops_StopId",
                        column: x => x.StopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportStops_Transports_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportStops_StopId",
                table: "TransportStops",
                column: "StopId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportStops_StopId_Direction",
                table: "TransportStops",
                columns: new[] { "StopId", "Direction" });

            migrationBuilder.CreateIndex(
                name: "IX_TransportStops_TransportId",
                table: "TransportStops",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportStops_TransportId_Direction",
                table: "TransportStops",
                columns: new[] { "TransportId", "Direction" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportStops");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "Transports");
        }
    }
}
