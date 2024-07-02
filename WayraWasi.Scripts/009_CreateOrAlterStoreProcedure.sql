USE [DBWayraWasi]
GO

/****** Object:  StoredProcedure [dbo].[spActualizarReserva]    Script Date: 1/7/2024 16:57:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[spActualizarReserva]
    @Id INT,
    @Estado NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Reservas
    SET Estado = @Estado
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ListarTodasCabanias]    Script Date: 1/7/2024 16:57:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_ListarTodasCabanias]
AS
BEGIN
    SELECT * 
    FROM Cabanias;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ListarReservas]    Script Date: 1/7/2024 16:57:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_ListarReservas]
AS
BEGIN
    SELECT r.*, c.*
    FROM Reservaciones r
    INNER JOIN Cabanias c ON c.IdCabania = r.IdCabania;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ListarCabanias]    Script Date: 1/7/2024 16:57:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_ListarCabanias]
AS
BEGIN
    SELECT * 
    FROM Cabanias;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GenerarReservaPorFecha]    Script Date: 1/7/2024 16:57:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_GenerarReservaPorFecha]
    @FechaEntrada DATETIME,
    @FechaSalida DATETIME
AS
BEGIN
    SELECT *
    FROM Reservaciones r
    INNER JOIN Cabanias c ON c.IdCabania = r.IdCabania
    WHERE (r.FechaEntrada BETWEEN @FechaEntrada AND @FechaSalida)
       OR (r.FechaSalida BETWEEN @FechaEntrada AND @FechaSalida);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarReserva]    Script Date: 1/7/2024 16:57:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_EliminarReserva]
    @IdReservacion INT
AS
BEGIN
    DELETE FROM Reservaciones
    WHERE IdReservacion = @IdReservacion;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarCabania]    Script Date: 1/7/2024 16:57:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER   PROCEDURE [dbo].[sp_EliminarCabania]
    @IdCabania INT
AS
BEGIN
    DELETE FROM Cabanias
    WHERE IdCabania = @IdCabania;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_EditarReserva]    Script Date: 1/7/2024 16:57:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_EditarReserva]
    @IdReservacion INT,
    @NombreCliente NVARCHAR(255),
    @FechaEntrada DATETIME,
    @FechaSalida DATETIME,
    @NumeroPersonas INT,
    @IdCabania INT,
    @Estado NVARCHAR(50)
AS
BEGIN
    UPDATE Reservaciones
    SET 
        NombreCliente = @NombreCliente,
        FechaEntrada = @FechaEntrada,
        FechaSalida = @FechaSalida,
        NumeroPersonas = @NumeroPersonas,
        IdCabania = @IdCabania,
        CabaniaIdCabania = @IdCabania,
        Estado = @Estado
    WHERE IdReservacion = @IdReservacion;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_EditarCabania]    Script Date: 1/7/2024 16:57:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_EditarCabania]
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
/****** Object:  StoredProcedure [dbo].[sp_CrearReserva]    Script Date: 1/7/2024 16:57:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_CrearReserva]
    @NombreCliente NVARCHAR(255),
    @FechaEntrada DATETIME,
    @FechaSalida DATETIME,
    @NumeroPersonas INT,
    @IdCabania INT
AS
BEGIN
    INSERT INTO Reservaciones 
    (NombreCliente, FechaEntrada, FechaSalida, NumeroPersonas, IdCabania, CabaniaIdCabania, Estado) 
    VALUES 
    (@NombreCliente, @FechaEntrada, @FechaSalida, @NumeroPersonas, @IdCabania, @IdCabania, 'Reservado');
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CrearCabania]    Script Date: 1/7/2024 16:56:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_CrearCabania]
    @NombreCabania NVARCHAR(100),
    @Descripcion NVARCHAR(500),
    @Capacidad INT,
    @PrecioNoche DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO Cabanias (NombreCabania, Descripcion, Capacidad, PrecioNoche,Disponibilidad)
    VALUES (@NombreCabania, @Descripcion, @Capacidad, @PrecioNoche,NULL);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_BuscarReservaId]    Script Date: 1/7/2024 16:56:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_BuscarReservaId]
    @Id INT
AS
BEGIN
    SELECT * 
    FROM Reservaciones
    WHERE IdReservacion = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_BuscarReservaAsignadaACabania]    Script Date: 1/7/2024 16:56:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER   PROCEDURE [dbo].[sp_BuscarReservaAsignadaACabania]
    @Id INT
AS
BEGIN
    SELECT * 
    FROM Reservaciones
    WHERE IdCabania = @Id AND FechaSalida >= GETDATE();
END
GO
/****** Object:  StoredProcedure [dbo].[sp_BuscarDisponibilidadCabania]    Script Date: 1/7/2024 16:56:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_BuscarDisponibilidadCabania]
    @FechaEntrada DATETIME,
    @FechaSalida DATETIME,
    @IdCabania INT,
    @IdReserva INT
AS
BEGIN
    SELECT *
    FROM Cabanias c
    INNER JOIN Reservaciones r ON r.IdCabania = c.IdCabania
    WHERE r.IdCabania = @IdCabania
      AND r.IdReservacion != @IdReserva
      AND (
          (@FechaEntrada < r.FechaSalida AND @FechaSalida > r.FechaEntrada)
      );
END

GO
/****** Object:  StoredProcedure [dbo].[sp_BuscarCabaniaPorNombre]    Script Date: 1/7/2024 16:55:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER   PROCEDURE [dbo].[sp_BuscarCabaniaPorNombre]
    @NombreCabania NVARCHAR(100),
	@Id INT
AS
BEGIN

    SELECT * 
    FROM Cabanias
    WHERE NombreCabania = @NombreCabania AND IdCabania <> @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_BuscarCabaniaPorID]    Script Date: 1/7/2024 16:53:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_BuscarCabaniaPorID]
    @Id INT
AS
BEGIN
    SELECT * 
    FROM Cabanias
    WHERE IdCabania = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_BuscarCabaniaId]    Script Date: 1/7/2024 16:52:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_BuscarCabaniaId]
    @Id INT
AS
BEGIN
    SELECT * 
    FROM Cabanias 
    WHERE IdCabania = @Id;
END
GO