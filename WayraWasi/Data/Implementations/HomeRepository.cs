﻿using Dapper;
using System.Data;
using WayraWasi.Models;
using WayraWasi.Data;

namespace WayraWasi.Data.Implementations
{
    public class HomeRepository : IHomeRepository
    {
        private readonly DBDapperContext _conexionDapper;

        public HomeRepository(DBDapperContext conexionDapper)
        {
            _conexionDapper = conexionDapper;
        }

        // Integran las dos fechas
        public async Task<IEnumerable<Cabania>> ListarDisponibilidadPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                // El CASE lo utilizo como un if donde si es NULL esta desocupada(Porque la cabaña no se asigna a ninguna reserva) y si no es NULL entonces esta ocupada
                return await conexionD.QueryAsync<Cabania>("SELECT c.NombreCabania, c.Descripcion, c.Capacidad, c.PrecioNoche, CASE WHEN r.IdCabania IS NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS Disponible FROM Cabanias c LEFT JOIN Reservaciones r ON  c.IdCabania = r.IdCabania AND ((r.FechaEntrada BETWEEN @FechaEntrada AND @FechaSalida) OR (r.FechaSalida BETWEEN @FechaEntrada AND @FechaSalida)) GROUP BY c.IdCabania, c.NombreCabania, c.Descripcion, c.Capacidad, c.PrecioNoche, r.IdCabania;"
                    , new { FechaEntrada = fechaInicio, FechaSalida = fechaFin }); // Esta es la forma por la cual Dapper ingresa valores de busqueda para SQL
            }
        }
    }
}
