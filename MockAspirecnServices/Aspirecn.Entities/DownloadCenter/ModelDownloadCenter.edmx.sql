
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/22/2013 00:30:19
-- Generated from EDMX file: C:\Users\LibreK\SkyDrive\Coding\PopcornStudios\PopcornStudios\MockAspirecnServices\Aspirecn.Entities\DownloadCenter\ModelDownloadCenter.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ASPIRECN];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[DownloadCenterRequestEntities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DownloadCenterRequestEntities];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'DownloadCenterRequestEntities'
CREATE TABLE [dbo].[DownloadCenterRequestEntities] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ContentID] nvarchar(max)  NOT NULL,
    [Msisdn] nvarchar(max)  NOT NULL,
    [PushID] nvarchar(max)  NOT NULL,
    [DestMsisdn] nvarchar(max)  NULL,
    [OnDemandType] nvarchar(max)  NULL,
    [SCode] nvarchar(max)  NULL,
    [Notify] nvarchar(max)  NULL,
    [DeviceId] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'DownloadCenterRequestEntities'
ALTER TABLE [dbo].[DownloadCenterRequestEntities]
ADD CONSTRAINT [PK_DownloadCenterRequestEntities]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------