USE [DBWayraWasi]
GO
/****** Object:  StoredProcedure [dbo].[sp_BuscarCabaniaPorID]    Script Date: 12/6/2024 20:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_BuscarCabaniaPorID]
    @Id INT
AS
BEGIN
    SELECT * 
    FROM Cabanias
    WHERE IdCabania = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_BuscarDisponibilidadCabania]    Script Date: 12/6/2024 20:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_BuscarDisponibilidadCabania]
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
/****** Object:  StoredProcedure [dbo].[sp_BuscarReservaId]    Script Date: 12/6/2024 20:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_BuscarReservaId]
    @Id INT
AS
BEGIN
    SELECT * 
    FROM Reservaciones
    WHERE IdReservacion = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CrearReserva]    Script Date: 12/6/2024 20:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CrearReserva]
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
/****** Object:  StoredProcedure [dbo].[sp_EditarReserva]    Script Date: 12/6/2024 20:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EditarReserva]
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
/****** Object:  StoredProcedure [dbo].[sp_EliminarReserva]    Script Date: 12/6/2024 20:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EliminarReserva]
    @IdReservacion INT
AS
BEGIN
    DELETE FROM Reservaciones
    WHERE IdReservacion = @IdReservacion;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GenerarReservaPorFecha]    Script Date: 12/6/2024 20:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GenerarReservaPorFecha]
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
/****** Object:  StoredProcedure [dbo].[sp_ListarCabanias]    Script Date: 12/6/2024 20:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ListarCabanias]
AS
BEGIN
    SELECT * 
    FROM Cabanias;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ListarReservas]    Script Date: 12/6/2024 20:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ListarReservas]
AS
BEGIN
    SELECT r.*, c.*
    FROM Reservaciones r
    INNER JOIN Cabanias c ON c.IdCabania = r.IdCabania;
END
GO
