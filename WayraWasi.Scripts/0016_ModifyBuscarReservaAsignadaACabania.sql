USE [DBWayraWasi]
GO

/****** Object:  StoredProcedure [dbo].[sp_BuscarReservaAsignadaACabania]    Script Date: 1/8/2024 14:24:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE OR ALTER     PROCEDURE [dbo].[sp_BuscarReservaAsignadaACabania]
    @Id INT
AS
BEGIN
    SELECT TOP 1 *
    FROM Reservaciones
    WHERE IdCabania = @Id AND FechaSalida >= GETDATE();
END
GO


