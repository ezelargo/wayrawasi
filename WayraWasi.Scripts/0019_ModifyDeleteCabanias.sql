USE [DBWayraWasi]
GO

/****** Object:  StoredProcedure [dbo].[sp_EliminarCabania]    Script Date: 19/8/2024 16:07:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE OR ALTER     PROCEDURE [dbo].[sp_EliminarCabania]
    @IdCabania INT
AS
BEGIN
    DELETE r FROM Cabanias c 
	INNER JOIN Reservaciones r
	ON c.IdCabania = r.IdCabania
    WHERE c.IdCabania = @IdCabania;

	DELETE FROM Cabanias
	WHERE IdCabania = @IdCabania;
END
GO


