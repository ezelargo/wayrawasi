USE [DBWayraWasi]
GO

/****** Object:  StoredProcedure [dbo].[ProximasReservas]    Script Date: 8/7/2024 16:11:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER   PROCEDURE [dbo].[ProximasReservas]
    @Dias INT
AS
BEGIN
    SELECT r.*, c.*
    FROM Reservaciones r
    INNER JOIN Cabanias c ON c.IdCabania = r.IdCabania
    WHERE r.FechaEntrada BETWEEN GETDATE() AND DATEADD(DAY, @Dias, GETDATE())
END
GO


