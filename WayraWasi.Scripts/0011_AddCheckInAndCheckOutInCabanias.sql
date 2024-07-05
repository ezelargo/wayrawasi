USE [DBWayraWasi]
GO

/****** Object:  Table [dbo].[Cabanias]    Script Date: 4/7/2024 14:06:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cabanias]') AND type in (N'U'))
DROP TABLE [dbo].[Cabanias]
GO

/****** Object:  Table [dbo].[Cabanias]    Script Date: 4/7/2024 14:06:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cabanias](
	[IdCabania] [int] IDENTITY(1,1) NOT NULL,
	[NombreCabania] [nvarchar](100) NOT NULL,
	[Descripcion] [nvarchar](100) NOT NULL,
	[Capacidad] [int] NOT NULL,
	[PrecioNoche] [float] NOT NULL,
	[Disponibilidad] [varchar](50) NULL,
	[CheckIn] [time](7) NULL,
	[CheckOut] [time](7) NULL,
 CONSTRAINT [PK_Cabanias] PRIMARY KEY CLUSTERED 
(
	[IdCabania] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


