
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/19/2022 21:44:41
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
    ALTER TABLE [dbo].[UsersSet] DROP CONSTRAINT [FK_UsersClients];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersWorkers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsersSet] DROP CONSTRAINT [FK_UsersWorkers];
GO
IF OBJECT_ID(N'[dbo].[FK_WorkersServices]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServicesSet] DROP CONSTRAINT [FK_WorkersServices];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientsCars]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarsSet] DROP CONSTRAINT [FK_ClientsCars];
GO
IF OBJECT_ID(N'[dbo].[FK_WorkersServiceHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceHistorySet] DROP CONSTRAINT [FK_WorkersServiceHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_CarsServiceHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceHistorySet] DROP CONSTRAINT [FK_CarsServiceHistory];
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
IF OBJECT_ID(N'[dbo].[FK_DictionaryServiceHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceHistorySet] DROP CONSTRAINT [FK_DictionaryServiceHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_DictionaryCarsType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarsSet] DROP CONSTRAINT [FK_DictionaryCarsType];
GO
IF OBJECT_ID(N'[dbo].[FK_DictionaryCarsStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarsSet] DROP CONSTRAINT [FK_DictionaryCarsStatus];
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
IF OBJECT_ID(N'[dbo].[ServiceHistorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServiceHistorySet];
GO
IF OBJECT_ID(N'[dbo].[DictionarySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DictionarySet];
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
    [Clients_Id] int  NOT NULL,
    [Workers_Id] int  NOT NULL,
    [Dictionary_Id] int  NOT NULL
);
GO

-- Creating table 'CarsSet'
CREATE TABLE [dbo].[CarsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Brand] nvarchar(max)  NOT NULL,
    [Model] nvarchar(max)  NOT NULL,
    [Registration] nvarchar(max)  NOT NULL,
    [ProductionYear] datetime  NULL,
    [Capacity] decimal(18,0)  NULL,
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
    [Cars_Id] int  NOT NULL,
    [Workers_Id] int  NOT NULL,
    [Dictionary_Id] int  NOT NULL
);
GO

-- Creating table 'WorkersSet'
CREATE TABLE [dbo].[WorkersSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Post] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Image] varbinary(max)  NULL,
    [Dictionary_Id] int  NOT NULL
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
    [Image] varbinary(max)  NULL
);
GO

-- Creating table 'ServiceHistorySet'
CREATE TABLE [dbo].[ServiceHistorySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Count] int  NULL,
    [Hours] int  NULL,
    [DateAdd] datetime  NOT NULL,
    [DateFinish] datetime  NOT NULL,
    [Workers_Id] int  NOT NULL,
    [Cars_Id] int  NOT NULL,
    [Dictionary_Id] int  NOT NULL
);
GO

-- Creating table 'DictionarySet'
CREATE TABLE [dbo].[DictionarySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NULL
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

-- Creating primary key on [Id] in table 'ServiceHistorySet'
ALTER TABLE [dbo].[ServiceHistorySet]
ADD CONSTRAINT [PK_ServiceHistorySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DictionarySet'
ALTER TABLE [dbo].[DictionarySet]
ADD CONSTRAINT [PK_DictionarySet]
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

-- Creating foreign key on [Clients_Id] in table 'UsersSet'
ALTER TABLE [dbo].[UsersSet]
ADD CONSTRAINT [FK_UsersClients]
    FOREIGN KEY ([Clients_Id])
    REFERENCES [dbo].[ClientsSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersClients'
CREATE INDEX [IX_FK_UsersClients]
ON [dbo].[UsersSet]
    ([Clients_Id]);
GO

-- Creating foreign key on [Workers_Id] in table 'UsersSet'
ALTER TABLE [dbo].[UsersSet]
ADD CONSTRAINT [FK_UsersWorkers]
    FOREIGN KEY ([Workers_Id])
    REFERENCES [dbo].[WorkersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersWorkers'
CREATE INDEX [IX_FK_UsersWorkers]
ON [dbo].[UsersSet]
    ([Workers_Id]);
GO

-- Creating foreign key on [Workers_Id] in table 'ServicesSet'
ALTER TABLE [dbo].[ServicesSet]
ADD CONSTRAINT [FK_WorkersServices]
    FOREIGN KEY ([Workers_Id])
    REFERENCES [dbo].[WorkersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WorkersServices'
CREATE INDEX [IX_FK_WorkersServices]
ON [dbo].[ServicesSet]
    ([Workers_Id]);
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

-- Creating foreign key on [Workers_Id] in table 'ServiceHistorySet'
ALTER TABLE [dbo].[ServiceHistorySet]
ADD CONSTRAINT [FK_WorkersServiceHistory]
    FOREIGN KEY ([Workers_Id])
    REFERENCES [dbo].[WorkersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WorkersServiceHistory'
CREATE INDEX [IX_FK_WorkersServiceHistory]
ON [dbo].[ServiceHistorySet]
    ([Workers_Id]);
GO

-- Creating foreign key on [Cars_Id] in table 'ServiceHistorySet'
ALTER TABLE [dbo].[ServiceHistorySet]
ADD CONSTRAINT [FK_CarsServiceHistory]
    FOREIGN KEY ([Cars_Id])
    REFERENCES [dbo].[CarsSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CarsServiceHistory'
CREATE INDEX [IX_FK_CarsServiceHistory]
ON [dbo].[ServiceHistorySet]
    ([Cars_Id]);
GO

-- Creating foreign key on [Dictionary_Id] in table 'UsersSet'
ALTER TABLE [dbo].[UsersSet]
ADD CONSTRAINT [FK_DictionaryUsers]
    FOREIGN KEY ([Dictionary_Id])
    REFERENCES [dbo].[DictionarySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DictionaryUsers'
CREATE INDEX [IX_FK_DictionaryUsers]
ON [dbo].[UsersSet]
    ([Dictionary_Id]);
GO

-- Creating foreign key on [Dictionary_Id] in table 'WorkersSet'
ALTER TABLE [dbo].[WorkersSet]
ADD CONSTRAINT [FK_DictionaryWorkers]
    FOREIGN KEY ([Dictionary_Id])
    REFERENCES [dbo].[DictionarySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DictionaryWorkers'
CREATE INDEX [IX_FK_DictionaryWorkers]
ON [dbo].[WorkersSet]
    ([Dictionary_Id]);
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

-- Creating foreign key on [Dictionary_Id] in table 'ServiceHistorySet'
ALTER TABLE [dbo].[ServiceHistorySet]
ADD CONSTRAINT [FK_DictionaryServiceHistory]
    FOREIGN KEY ([Dictionary_Id])
    REFERENCES [dbo].[DictionarySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DictionaryServiceHistory'
CREATE INDEX [IX_FK_DictionaryServiceHistory]
ON [dbo].[ServiceHistorySet]
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------