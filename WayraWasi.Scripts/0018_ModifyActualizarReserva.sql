USE [DBWayraWasi]
GO

/****** Object:  StoredProcedure [dbo].[spActualizarReserva]    Script Date: 19/8/2024 15:47:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER   PROCEDURE [dbo].[spActualizarReserva]
    @IdReservacion INT,
    @Estado NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Reservaciones
    SET Estado = @Estado
    WHERE IdReservacion = @IdReservacion;
END
GO


