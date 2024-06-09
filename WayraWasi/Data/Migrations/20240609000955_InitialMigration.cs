using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WayraWasi.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cabanias",
                columns: table => new
                {
                    IdCabania = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCabania = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    PrecioNoche = table.Column<double>(type: "float", nullable: false),
                    Disponible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabanias", x => x.IdCabania);
                });

            migrationBuilder.CreateTable(
                name: "Reservaciones",
                columns: table => new
                {
                    IdReservacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCliente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaEntrada = table.Column<DateTime>(type: "date", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "date", nullable: false),
                    NumeroPersonas = table.Column<int>(type: "int", nullable: false),
                    IdCabania = table.Column<int>(type: "int", nullable: false),
                    CabaniaIdCabania = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservaciones", x => x.IdReservacion);
                    table.ForeignKey(
                        name: "FK_Reservaciones_Cabanias_CabaniaIdCabania",
                        column: x => x.CabaniaIdCabania,
                        principalTable: "Cabanias",
                        principalColumn: "IdCabania",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservaciones_CabaniaIdCabania",
                table: "Reservaciones",
                column: "CabaniaIdCabania");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservaciones");

            migrationBuilder.DropTable(
                name: "Cabanias");
        }
    }
}
