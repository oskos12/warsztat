
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/07/2022 17:31:36
-- Generated from EDMX file: C:\Users\Damian\source\repos\Warsztat\Warsztat\Database\Warsztat.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Warsztat];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CarsServices]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServicesSet] DROP CONSTRAINT [FK_CarsServices];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersClients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClientsSet] DROP CONSTRAINT [FK_UsersClients];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersWorkers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkersSet] DROP CONSTRAINT [FK_UsersWorkers];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientsCars]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarsSet] DROP CONSTRAINT [FK_ClientsCars];
GO
IF OBJECT_ID(N'[dbo].[FK_DictionaryUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsersSet] DROP CONSTRAINT [FK_DictionaryUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_DictionaryWorkers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkersSet] DROP CONSTRAINT [FK_DictionaryWorkers];
GO
IF OBJECT_ID(N'[dbo].[FK_DictionaryCarsEngine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarsSet] DROP CONSTRAINT [FK_DictionaryCarsEngine];
GO
IF OBJECT_ID(N'[dbo].[FK_DictionaryServices]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServicesSet] DROP CONSTRAINT [FK_DictionaryServices];
GO
IF OBJECT_ID(N'[dbo].[FK_DictionaryCarsType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarsSet] DROP CONSTRAINT [FK_DictionaryCarsType];
GO
IF OBJECT_ID(N'[dbo].[FK_DictionaryCarsStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarsSet] DROP CONSTRAINT [FK_DictionaryCarsStatus];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientsApplications]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ApplicationsSet] DROP CONSTRAINT [FK_ClientsApplications];
GO
IF OBJECT_ID(N'[dbo].[FK_ServicesWorkers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServicesSet] DROP CONSTRAINT [FK_ServicesWorkers];
GO
IF OBJECT_ID(N'[dbo].[FK_WorkersServicesWorkers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkersServicesSet] DROP CONSTRAINT [FK_WorkersServicesWorkers];
GO
IF OBJECT_ID(N'[dbo].[FK_ServicesWorkersServices]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkersServicesSet] DROP CONSTRAINT [FK_ServicesWorkersServices];
GO
IF OBJECT_ID(N'[dbo].[FK_DictionaryApplications]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ApplicationsSet] DROP CONSTRAINT [FK_DictionaryApplications];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UsersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsersSet];
GO
IF OBJECT_ID(N'[dbo].[CarsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CarsSet];
GO
IF OBJECT_ID(N'[dbo].[ServicesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServicesSet];
GO
IF OBJECT_ID(N'[dbo].[WorkersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkersSet];
GO
IF OBJECT_ID(N'[dbo].[ClientsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientsSet];
GO
IF OBJECT_ID(N'[dbo].[DictionarySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DictionarySet];
GO
IF OBJECT_ID(N'[dbo].[ApplicationsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ApplicationsSet];
GO
IF OBJECT_ID(N'[dbo].[WorkersServicesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkersServicesSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UsersSet'
CREATE TABLE [dbo].[UsersSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [DictionaryType_Id] int  NOT NULL
);
GO

-- Creating table 'CarsSet'
CREATE TABLE [dbo].[CarsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Brand] nvarchar(max)  NOT NULL,
    [Model] nvarchar(max)  NOT NULL,
    [Registration] nvarchar(max)  NOT NULL,
    [ProductionYear] datetime  NULL,
    [Capacity] float  NULL,
    [Active] bit  NOT NULL,
    [Clients_Id] int  NOT NULL,
    [DictionaryEngine_Id] int  NOT NULL,
    [DictionaryType_Id] int  NOT NULL,
    [DictionaryStatus_Id] int  NOT NULL
);
GO

-- Creating table 'ServicesSet'
CREATE TABLE [dbo].[ServicesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Count] int  NULL,
    [Hours] int  NULL,
    [DateAdd] datetime  NOT NULL,
    [DateFinish] datetime  NULL,
    [CostSum] decimal(18,0)  NULL,
    [Description] nvarchar(max)  NULL,
    [Cars_Id] int  NOT NULL,
    [Dictionary_Id] int  NOT NULL,
    [Workers_Id] int  NOT NULL
);
GO

-- Creating table 'WorkersSet'
CREATE TABLE [dbo].[WorkersSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NULL,
    [Post] nvarchar(max)  NULL,
    [Address] nvarchar(max)  NULL,
    [Active] bit  NOT NULL,
    [Users_Id] int  NOT NULL
);
GO

-- Creating table 'ClientsSet'
CREATE TABLE [dbo].[ClientsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NULL,
    [Post] nvarchar(max)  NULL,
    [Address] nvarchar(max)  NULL,
    [Active] bit  NOT NULL,
    [Users_Id] int  NOT NULL
);
GO

-- Creating table 'DictionarySet'
CREATE TABLE [dbo].[DictionarySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Key] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'ApplicationsSet'
CREATE TABLE [dbo].[ApplicationsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Contact] nvarchar(max)  NOT NULL,
    [Clients_Id] int  NULL,
    [DictionaryStatus_Id] int  NOT NULL
);
GO

-- Creating table 'WorkersServicesSet'
CREATE TABLE [dbo].[WorkersServicesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Workers_Id] int  NOT NULL,
    [Services_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UsersSet'
ALTER TABLE [dbo].[UsersSet]
ADD CONSTRAINT [PK_UsersSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CarsSet'
ALTER TABLE [dbo].[CarsSet]
ADD CONSTRAINT [PK_CarsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ServicesSet'
ALTER TABLE [dbo].[ServicesSet]
ADD CONSTRAINT [PK_ServicesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WorkersSet'
ALTER TABLE [dbo].[WorkersSet]
ADD CONSTRAINT [PK_WorkersSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientsSet'
ALTER TABLE [dbo].[ClientsSet]
ADD CONSTRAINT [PK_ClientsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DictionarySet'
ALTER TABLE [dbo].[DictionarySet]
ADD CONSTRAINT [PK_DictionarySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ApplicationsSet'
ALTER TABLE [dbo].[ApplicationsSet]
ADD CONSTRAINT [PK_ApplicationsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WorkersServicesSet'
ALTER TABLE [dbo].[WorkersServicesSet]
ADD CONSTRAINT [PK_WorkersServicesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Cars_Id] in table 'ServicesSet'
ALTER TABLE [dbo].[ServicesSet]
ADD CONSTRAINT [FK_CarsServices]
    FOREIGN KEY ([Cars_Id])
    REFERENCES [dbo].[CarsSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CarsServices'
CREATE INDEX [IX_FK_CarsServices]
ON [dbo].[ServicesSet]
    ([Cars_Id]);
GO

-- Creating foreign key on [Users_Id] in table 'ClientsSet'
ALTER TABLE [dbo].[ClientsSet]
ADD CONSTRAINT [FK_UsersClients]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[UsersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersClients'
CREATE INDEX [IX_FK_UsersClients]
ON [dbo].[ClientsSet]
    ([Users_Id]);
GO

-- Creating foreign key on [Users_Id] in table 'WorkersSet'
ALTER TABLE [dbo].[WorkersSet]
ADD CONSTRAINT [FK_UsersWorkers]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[UsersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersWorkers'
CREATE INDEX [IX_FK_UsersWorkers]
ON [dbo].[WorkersSet]
    ([Users_Id]);
GO

-- Creating foreign key on [Clients_Id] in table 'CarsSet'
ALTER TABLE [dbo].[CarsSet]
ADD CONSTRAINT [FK_ClientsCars]
    FOREIGN KEY ([Clients_Id])
    REFERENCES [dbo].[ClientsSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientsCars'
CREATE INDEX [IX_FK_ClientsCars]
ON [dbo].[CarsSet]
    ([Clients_Id]);
GO

-- Creating foreign key on [DictionaryType_Id] in table 'UsersSet'
ALTER TABLE [dbo].[UsersSet]
ADD CONSTRAINT [FK_DictionaryUsers]
    FOREIGN KEY ([DictionaryType_Id])
    REFERENCES [dbo].[DictionarySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DictionaryUsers'
CREATE INDEX [IX_FK_DictionaryUsers]
ON [dbo].[UsersSet]
    ([DictionaryType_Id]);
GO

-- Creating foreign key on [DictionaryEngine_Id] in table 'CarsSet'
ALTER TABLE [dbo].[CarsSet]
ADD CONSTRAINT [FK_DictionaryCarsEngine]
    FOREIGN KEY ([DictionaryEngine_Id])
    REFERENCES [dbo].[DictionarySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DictionaryCarsEngine'
CREATE INDEX [IX_FK_DictionaryCarsEngine]
ON [dbo].[CarsSet]
    ([DictionaryEngine_Id]);
GO

-- Creating foreign key on [Dictionary_Id] in table 'ServicesSet'
ALTER TABLE [dbo].[ServicesSet]
ADD CONSTRAINT [FK_DictionaryServices]
    FOREIGN KEY ([Dictionary_Id])
    REFERENCES [dbo].[DictionarySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DictionaryServices'
CREATE INDEX [IX_FK_DictionaryServices]
ON [dbo].[ServicesSet]
    ([Dictionary_Id]);
GO

-- Creating foreign key on [DictionaryType_Id] in table 'CarsSet'
ALTER TABLE [dbo].[CarsSet]
ADD CONSTRAINT [FK_DictionaryCarsType]
    FOREIGN KEY ([DictionaryType_Id])
    REFERENCES [dbo].[DictionarySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DictionaryCarsType'
CREATE INDEX [IX_FK_DictionaryCarsType]
ON [dbo].[CarsSet]
    ([DictionaryType_Id]);
GO

-- Creating foreign key on [DictionaryStatus_Id] in table 'CarsSet'
ALTER TABLE [dbo].[CarsSet]
ADD CONSTRAINT [FK_DictionaryCarsStatus]
    FOREIGN KEY ([DictionaryStatus_Id])
    REFERENCES [dbo].[DictionarySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DictionaryCarsStatus'
CREATE INDEX [IX_FK_DictionaryCarsStatus]
ON [dbo].[CarsSet]
    ([DictionaryStatus_Id]);
GO

-- Creating foreign key on [Clients_Id] in table 'ApplicationsSet'
ALTER TABLE [dbo].[ApplicationsSet]
ADD CONSTRAINT [FK_ClientsApplications]
    FOREIGN KEY ([Clients_Id])
    REFERENCES [dbo].[ClientsSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientsApplications'
CREATE INDEX [IX_FK_ClientsApplications]
ON [dbo].[ApplicationsSet]
    ([Clients_Id]);
GO

-- Creating foreign key on [Workers_Id] in table 'ServicesSet'
ALTER TABLE [dbo].[ServicesSet]
ADD CONSTRAINT [FK_ServicesWorkers]
    FOREIGN KEY ([Workers_Id])
    REFERENCES [dbo].[WorkersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ServicesWorkers'
CREATE INDEX [IX_FK_ServicesWorkers]
ON [dbo].[ServicesSet]
    ([Workers_Id]);
GO

-- Creating foreign key on [Workers_Id] in table 'WorkersServicesSet'
ALTER TABLE [dbo].[WorkersServicesSet]
ADD CONSTRAINT [FK_WorkersServicesWorkers]
    FOREIGN KEY ([Workers_Id])
    REFERENCES [dbo].[WorkersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WorkersServicesWorkers'
CREATE INDEX [IX_FK_WorkersServicesWorkers]
ON [dbo].[WorkersServicesSet]
    ([Workers_Id]);
GO

-- Creating foreign key on [Services_Id] in table 'WorkersServicesSet'
ALTER TABLE [dbo].[WorkersServicesSet]
ADD CONSTRAINT [FK_ServicesWorkersServices]
    FOREIGN KEY ([Services_Id])
    REFERENCES [dbo].[ServicesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ServicesWorkersServices'
CREATE INDEX [IX_FK_ServicesWorkersServices]
ON [dbo].[WorkersServicesSet]
    ([Services_Id]);
GO

-- Creating foreign key on [DictionaryStatus_Id] in table 'ApplicationsSet'
ALTER TABLE [dbo].[ApplicationsSet]
ADD CONSTRAINT [FK_DictionaryApplications]
    FOREIGN KEY ([DictionaryStatus_Id])
    REFERENCES [dbo].[DictionarySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DictionaryApplications'
CREATE INDEX [IX_FK_DictionaryApplications]
ON [dbo].[ApplicationsSet]
    ([DictionaryStatus_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------