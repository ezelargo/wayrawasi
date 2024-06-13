USE [DBWayraWasi]
GO
/****** Object:  StoredProcedure [dbo].[sp_BuscarCabaniaId]    Script Date: 13/6/2024 12:30:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_BuscarCabaniaId]
    @Id INT
AS
BEGIN
    SELECT * 
    FROM Cabanias 
    WHERE IdCabania = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_BuscarReservaAsignadaACabania]    Script Date: 13/6/2024 12:30:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_BuscarReservaAsignadaACabania]
    @Id INT
AS
BEGIN
    SELECT * 
    FROM Reservaciones 
    WHERE IdCabania = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CrearCabania]    Script Date: 13/6/2024 12:30:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CrearCabania]
    @NombreCabania NVARCHAR(100),
    @Descripcion NVARCHAR(500),
    @Capacidad INT,
    @PrecioNoche DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO Cabanias (NombreCabania, Descripcion, Capacidad, PrecioNoche)
    VALUES (@NombreCabania, @Descripcion, @Capacidad, @PrecioNoche);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_EditarCabania]    Script Date: 13/6/2024 12:30:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EditarCabania]
    @IdCabania INT,
    @NombreCabania NVARCHAR(100),
    @Descripcion NVARCHAR(500),
    @Capacidad INT,
    @PrecioNoche DECIMAL(18, 2)
AS
BEGIN
    UPDATE Cabanias
    SET NombreCabania = @NombreCabania, 
        Descripcion = @Descripcion, 
        Capacidad = @Capacidad, 
        PrecioNoche = @PrecioNoche
    WHERE IdCabania = @IdCabania;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarCabania]    Script Date: 13/6/2024 12:30:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EliminarCabania]
    @IdCabania INT
AS
BEGIN
    DELETE FROM Cabanias
    WHERE IdCabania = @IdCabania;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ListarTodasCabanias]    Script Date: 13/6/2024 12:30:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ListarTodasCabanias]
AS
BEGIN
    SELECT * 
    FROM Cabanias;
END
GO
