USE [DBWayraWasi]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 14/6/2024 19:38:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND type in (N'U'))
DROP TABLE [dbo].[Usuarios]
GO
