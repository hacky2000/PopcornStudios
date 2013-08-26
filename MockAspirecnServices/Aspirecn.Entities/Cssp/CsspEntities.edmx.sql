
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/23/2013 11:58:43
-- Generated from EDMX file: C:\Users\LibreK\SkyDrive\Coding\PopcornStudios\PopcornStudios\MockAspirecnServices\Aspirecn.Entities\Cssp\CsspEntities.edmx
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

IF OBJECT_ID(N'[dbo].[ServiceAccessReqEntities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServiceAccessReqEntities];
GO
IF OBJECT_ID(N'[dbo].[ParamNameEntities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ParamNameEntities];
GO
IF OBJECT_ID(N'[dbo].[ServiceAccessRespEntities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServiceAccessRespEntities];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ServiceAccessReqEntities'
CREATE TABLE [dbo].[ServiceAccessReqEntities] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Head_Send_Address_DeviceType] nvarchar(max)  NOT NULL,
    [Head_Send_Address_DeviceID] nvarchar(max)  NOT NULL,
    [Head_Dest_Address_DeviceType] nvarchar(max)  NOT NULL,
    [Head_Dest_Address_DeviceID] nvarchar(max)  NOT NULL,
    [Head_Version] nvarchar(max)  NOT NULL,
    [Head_MsgType] nvarchar(max)  NOT NULL,
    [Body_Request_SPID] nvarchar(max)  NOT NULL,
    [Body_Request_SPServiceID] nvarchar(max)  NOT NULL,
    [Body_Request_ChannelID] nvarchar(max)  NOT NULL,
    [Body_Request_ContentID] nvarchar(max)  NOT NULL,
    [Body_Request_MSISDN] nvarchar(max)  NOT NULL,
    [Body_Request_FeeMSISDN] nvarchar(max)  NOT NULL,
    [Body_Request_Params_Pager_BeginIndex] nvarchar(max)  NOT NULL,
    [Body_Request_Params_Pager_EndIndex] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ParamNameEntities'
CREATE TABLE [dbo].[ParamNameEntities] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ParamName] nvarchar(max)  NOT NULL,
    [CompareOp] nvarchar(max)  NOT NULL,
    [ParamValue] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ServiceAccessRespEntities'
CREATE TABLE [dbo].[ServiceAccessRespEntities] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Head_Send_Address_DeviceType] nvarchar(max)  NOT NULL,
    [Head_Send_Address_DeviceID] nvarchar(max)  NOT NULL,
    [Head_Dest_Address_DeviceType] nvarchar(max)  NOT NULL,
    [Head_Dest_Address_DeviceID] nvarchar(max)  NOT NULL,
    [Head_TransactionID] nvarchar(max)  NOT NULL,
    [Head_Version] nvarchar(max)  NOT NULL,
    [Body_RetCode] nvarchar(max)  NOT NULL,
    [Body_SPID] nvarchar(max)  NOT NULL,
    [Body_SPServiceID] nvarchar(max)  NOT NULL,
    [Body_Price] nvarchar(max)  NOT NULL,
    [Body_OrderID] nvarchar(max)  NOT NULL,
    [Body_PushID] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'ServiceAccessReqEntities'
ALTER TABLE [dbo].[ServiceAccessReqEntities]
ADD CONSTRAINT [PK_ServiceAccessReqEntities]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ParamNameEntities'
ALTER TABLE [dbo].[ParamNameEntities]
ADD CONSTRAINT [PK_ParamNameEntities]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ServiceAccessRespEntities'
ALTER TABLE [dbo].[ServiceAccessRespEntities]
ADD CONSTRAINT [PK_ServiceAccessRespEntities]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------