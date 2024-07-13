USE [DBWayraWasi]
GO

/****** Object:  StoredProcedure [dbo].[ConsultarDisponibilidadCabanias]    Script Date: 13/7/2024 13:53:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE OR ALTER       PROCEDURE [dbo].[ConsultarDisponibilidadCabanias]
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
                        AND (
								(
									r.FechaEntrada <= @FechaSalida
									AND r.FechaSalida >= @FechaEntrada
								)
								OR
								(
									DATEADD(HOUR, DATEPART(HOUR, c.CheckIn), DATEADD(MINUTE, DATEPART(MINUTE, c.CheckIn), r.FechaEntrada)) <= @FechaSalida
									AND DATEADD(HOUR, DATEPART(HOUR, c.CheckOut), DATEADD(MINUTE, DATEPART(MINUTE, c.CheckOut), r.FechaSalida)) >= @FechaEntrada
								)
							)
    GROUP BY
		c.IdCabania,
		c.NombreCabania,
		c.Capacidad,
		c.PrecioNoche,
		c.CheckIn,
		c.CheckOut,
		r.IdCabania,
        r.NombreCliente,
        r.NumeroPersonas;
END;

GO


