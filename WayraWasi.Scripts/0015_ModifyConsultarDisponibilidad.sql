USE [DBWayraWasi]
GO

/****** Object:  StoredProcedure [dbo].[ConsultarDisponibilidadCabanias]    Script Date: 10/7/2024 13:30:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE OR ALTER     PROCEDURE [dbo].[ConsultarDisponibilidadCabanias]
    @FechaEntrada DATE,
    @FechaSalida DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.NombreCabania,
        c.Capacidad,
        c.PrecioNoche,
        CASE WHEN r.IdCabania IS NOT NULL THEN 'Ocupada' ELSE 'Disponible' END AS Disponibilidad
    FROM
        Cabanias c
    LEFT JOIN
        Reservaciones r ON c.IdCabania = r.IdCabania
                        AND ((r.FechaEntrada BETWEEN @FechaEntrada AND @FechaSalida)
                             OR (r.FechaSalida BETWEEN @FechaEntrada AND @FechaSalida))
    GROUP BY
		c.IdCabania,
		c.NombreCabania,
		c.Capacidad,
		c.PrecioNoche,
		r.IdCabania,
        r.NombreCliente,
        r.NumeroPersonas;
END;

GO


