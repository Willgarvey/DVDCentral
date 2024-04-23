﻿/*
Deployment script for WMG.DVDCentral.DB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "WMG.DVDCentral.DB"
:setvar DefaultFilePrefix "WMG.DVDCentral.DB"
:setvar DefaultDataPath "C:\Users\INTEL4400\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\INTEL4400\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating database $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

DROP TABLE IF EXISTS tblCustomer;
DROP TABLE IF EXISTS tblDirector;
DROP TABLE IF EXISTS tblFormat;
DROP TABLE IF EXISTS tblGenre;
DROP TABLE IF EXISTS tblMovie;
DROP TABLE IF EXISTS tblMovieGenre;
DROP TABLE IF EXISTS tblOrder;
DROP TABLE IF EXISTS tblOrderItem;
DROP TABLE IF EXISTS tblRating;
DROP TABLE IF EXISTS tblUser;
GO

GO
PRINT N'Creating Table [dbo].[tblCustomer]...';


GO
CREATE TABLE [dbo].[tblCustomer] (
    [Id]        INT           NOT NULL,
    [FirstName] VARCHAR (50)  NOT NULL,
    [LastName]  VARCHAR (50)  NOT NULL,
    [UserId]    INT           NOT NULL,
    [Address]   VARCHAR (50)  NOT NULL,
    [City]      VARCHAR (25)  NOT NULL,
    [State]     VARCHAR (2)   NOT NULL,
    [Zip]       VARCHAR (12)  NOT NULL,
    [Phone]     VARCHAR (20)  NOT NULL,
    [ImagePath] VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[tblDirector]...';


GO
CREATE TABLE [dbo].[tblDirector] (
    [Id]        INT          NOT NULL,
    [FirstName] VARCHAR (50) NOT NULL,
    [LastName]  VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[tblFormat]...';


GO
CREATE TABLE [dbo].[tblFormat] (
    [Id]          INT          NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[tblGenre]...';


GO
CREATE TABLE [dbo].[tblGenre] (
    [Id]          INT          NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[tblMovie]...';


GO
CREATE TABLE [dbo].[tblMovie] (
    [Id]          INT           NOT NULL,
    [Title]       VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (100) NOT NULL,
    [FormatId]    INT           NOT NULL,
    [DirectorId]  INT           NOT NULL,
    [RatingId]    INT           NOT NULL,
    [Cost]        FLOAT (53)    NOT NULL,
    [InStkQty]    INT           NOT NULL,
    [ImagePath]   VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[tblMovieGenre]...';


GO
CREATE TABLE [dbo].[tblMovieGenre] (
    [Id]      INT NOT NULL,
    [MovieId] INT NOT NULL,
    [GenreId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[tblOrder]...';


GO
CREATE TABLE [dbo].[tblOrder] (
    [Id]         INT      NOT NULL,
    [CustomerId] INT      NOT NULL,
    [OrderDate]  DATETIME NOT NULL,
    [ShipDate]   DATETIME NOT NULL,
    [UserId]     INT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[tblOrderItem]...';


GO
CREATE TABLE [dbo].[tblOrderItem] (
    [Id]       INT        NOT NULL,
    [OrderId]  INT        NOT NULL,
    [Quantity] INT        NOT NULL,
    [MovieId]  INT        NOT NULL,
    [Cost]     FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[tblRating]...';


GO
CREATE TABLE [dbo].[tblRating] (
    [Id]          INT          NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[tblUser]...';


GO
CREATE TABLE [dbo].[tblUser] (
    [Id]        INT          NOT NULL,
    [UserName]  VARCHAR (50) NOT NULL,
    [FirstName] VARCHAR (50) NOT NULL,
    [LastName]  VARCHAR (50) NOT NULL,
    [Password]  VARCHAR (28) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

BEGIN
	INSERT INTO tblCustomer (Id, FirstName, LastName, UserId, Address, City, State, Zip, Phone, ImagePath)
	VALUES
	(1, 'Bart', 'Simpson', 1, '20 Main Street', 'Springfield', 'IL', '22334', '9204442233', '.\path.png'),
	(2, 'Michael', 'Jordan', 2, '2150 South Grace Street', 'Charleston', 'WV', '22335', '9204441456', '.\path.png'),
	(3, 'Marty', 'McFly', 3, '40 Grove Avenue', 'Harriet',  'VA', '22337','9202109803', '.\path.png')
END
BEGIN
	INSERT INTO tblDirector (Id, FirstName, LastName)
	VALUES
	(1, 'Jon', 'Favreau'),
	(2, 'Francis', 'Copala'),
	(3, 'George', 'Lucas')
END
BEGIN
	INSERT INTO tblFormat (Id, Description)
	VALUES
	(1, 'DVD'),
	(2, 'Blu Ray'),
	(3, 'VHS')
END
BEGIN
	INSERT INTO tblGenre (Id, Description)
	VALUES
	(1, 'Adventure'),
	(2, 'Drama'),
	(3, 'Science Fiction')
END
BEGIN
	INSERT INTO tblMovie (Id, Title, Description, FormatId, DirectorId, RatingId, Cost, InStkQty, ImagePath)
	VALUES
	(1,'Star Wars', 'A science fiction coming of age story',1, 3, 2, 7.99, 14, 'starwars.jpg'),
	(2, 'The Godfather', 'Part 1 of an Oscar winning drama', 2, 2, 2, 13.99, 7, 'godfather.jpg'),
	(3, 'Iron Man', 'A super hero origin story', 3, 1, 1, 8.99, 12, 'ironman.jpg')
END
BEGIN
	INSERT INTO tblMovieGenre (Id, MovieId, GenreId)
	VALUES
	(1, 1, 1),
	(2, 2, 2),
	(3, 3, 3)
END
BEGIN
	INSERT INTO tblOrder (Id, CustomerId, OrderDate, ShipDate, UserId)
	VALUES
	(1,1,2020-09-15,2020-09-16,1),
	(2,2,2019-08-08,2019-09-01,2),
	(3,2,2018-07-07,2018-07-12,2)
	END
BEGIN
	INSERT INTO tblOrderItem (Id, OrderId, Quantity, MovieId, Cost)
	VALUES
	(1,1,1,1,7.99),
	(2,2,1,2,13.99),
	(3,3,1,3,8.99)
END
BEGIN
	INSERT INTO tblRating (Id, Description)
	VALUES
	(1, 'PG'),
	(2, 'PG-13'),
	(3, 'R')
END
GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Update complete.';


GO
