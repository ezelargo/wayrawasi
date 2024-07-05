USE [DBWayraWasi]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_CrearCabania]
    @NombreCabania NVARCHAR(100),
    @Descripcion NVARCHAR(500),
    @Capacidad INT,
    @PrecioNoche DECIMAL(18, 2),
    @CheckIn TIME,
    @CheckOut TIME
AS
BEGIN
    INSERT INTO Cabanias (NombreCabania, Descripcion, Capacidad, PrecioNoche, CheckIn, CheckOut)
    VALUES (@NombreCabania, @Descripcion, @Capacidad, @PrecioNoche, @CheckIn, @CheckOut);
END
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_EditarCabania]
    @IdCabania INT,
    @NombreCabania NVARCHAR(100),
    @Descripcion NVARCHAR(500),
    @Capacidad INT,
    @PrecioNoche DECIMAL(18, 2),
    @CheckIn TIME,
    @CheckOut TIME
AS
BEGIN
    UPDATE Cabanias
    SET NombreCabania = @NombreCabania,
        Descripcion = @Descripcion,
        Capacidad = @Capacidad,
        PrecioNoche = @PrecioNoche,
        CheckIn = @CheckIn,
        CheckOut = @CheckOut
    WHERE IdCabania = @IdCabania;
END
GO

