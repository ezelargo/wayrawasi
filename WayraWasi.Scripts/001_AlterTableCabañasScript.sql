/*
   miércoles, 12 de junio de 202414:45:33
   User: 
   Server: DESKTOP-5JTFLM3\SQLEXPRESS
   Database: DBWayraWasi
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Cabanias
	DROP COLUMN Disponible
GO
ALTER TABLE dbo.Cabanias SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
