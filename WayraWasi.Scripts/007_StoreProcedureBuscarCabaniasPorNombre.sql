USE [DBWayraWasi]
GO
/****** Object:  StoredProcedure [dbo].[sp_BuscarCabaniaPorNombre]    Script Date: 18/6/2024 19:43:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_BuscarCabaniaPorNombre]
    @NombreCabania NVARCHAR(100)
AS
BEGIN

    SELECT * 
    FROM Cabanias
    WHERE NombreCabania = @NombreCabania;
END
GO
