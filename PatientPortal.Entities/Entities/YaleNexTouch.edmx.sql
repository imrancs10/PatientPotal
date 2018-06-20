
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/23/2015 10:13:01
-- Generated from EDMX file: E:\Assa Abloy\PatientPortal\Source Code\Working Code\PatientPortal.Entities\Entities\PatientPortal.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PatientPortal];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Answer_Language]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answer] DROP CONSTRAINT [FK_Answer_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Answer_Question]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answer] DROP CONSTRAINT [FK_Answer_Question];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Language]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductImages_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductImages] DROP CONSTRAINT [FK_ProductImages_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_Question_Language]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Question] DROP CONSTRAINT [FK_Question_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Question_QuestionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Question] DROP CONSTRAINT [FK_Question_QuestionType];
GO
IF OBJECT_ID(N'[dbo].[FK_Survey_Language]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Survey] DROP CONSTRAINT [FK_Survey_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_SurveyQuestionAnswer_Answer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SurveyQuestionAnswer] DROP CONSTRAINT [FK_SurveyQuestionAnswer_Answer];
GO
IF OBJECT_ID(N'[dbo].[FK_SurveyQuestionAnswer_Question]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SurveyQuestionAnswer] DROP CONSTRAINT [FK_SurveyQuestionAnswer_Question];
GO
IF OBJECT_ID(N'[dbo].[FK_SurveyQuestionAnswer_Survey]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SurveyQuestionAnswer] DROP CONSTRAINT [FK_SurveyQuestionAnswer_Survey];
GO
IF OBJECT_ID(N'[dbo].[FK_SurveyQuestionAnswer_UserSurvey]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SurveyQuestionAnswer] DROP CONSTRAINT [FK_SurveyQuestionAnswer_UserSurvey];
GO
IF OBJECT_ID(N'[dbo].[FK_SurveyQuestionMap_Answer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SurveyQuestionMap] DROP CONSTRAINT [FK_SurveyQuestionMap_Answer];
GO
IF OBJECT_ID(N'[dbo].[FK_SurveyQuestionMap_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SurveyQuestionMap] DROP CONSTRAINT [FK_SurveyQuestionMap_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_SurveyQuestionMap_Question]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SurveyQuestionMap] DROP CONSTRAINT [FK_SurveyQuestionMap_Question];
GO
IF OBJECT_ID(N'[dbo].[FK_SurveyQuestionMap_Question1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SurveyQuestionMap] DROP CONSTRAINT [FK_SurveyQuestionMap_Question1];
GO
IF OBJECT_ID(N'[dbo].[FK_SurveyQuestionMap_Survey]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SurveyQuestionMap] DROP CONSTRAINT [FK_SurveyQuestionMap_Survey];
GO
IF OBJECT_ID(N'[dbo].[FK_SurveyQuestionMap_SurveyQuestionMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SurveyQuestionMap] DROP CONSTRAINT [FK_SurveyQuestionMap_SurveyQuestionMap];
GO
IF OBJECT_ID(N'[dbo].[FK_User_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDetail_State]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDetail] DROP CONSTRAINT [FK_UserDetail_State];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDetail_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDetail] DROP CONSTRAINT [FK_UserDetail_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserSurvey_Survey]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSurvey] DROP CONSTRAINT [FK_UserSurvey_Survey];
GO
IF OBJECT_ID(N'[dbo].[FK_UserSurvey_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSurvey] DROP CONSTRAINT [FK_UserSurvey_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Answer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Answer];
GO
IF OBJECT_ID(N'[dbo].[Language]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Language];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[ProductImages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductImages];
GO
IF OBJECT_ID(N'[dbo].[Question]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Question];
GO
IF OBJECT_ID(N'[dbo].[QuestionType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionType];
GO
IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[State]', 'U') IS NOT NULL
    DROP TABLE [dbo].[State];
GO
IF OBJECT_ID(N'[dbo].[Survey]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Survey];
GO
IF OBJECT_ID(N'[dbo].[SurveyQuestionAnswer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SurveyQuestionAnswer];
GO
IF OBJECT_ID(N'[dbo].[SurveyQuestionMap]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SurveyQuestionMap];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[UserDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDetail];
GO
IF OBJECT_ID(N'[dbo].[UserSurvey]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSurvey];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Answers'
CREATE TABLE [dbo].[Answers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QuestionId] int  NOT NULL,
    [LanguageId] int  NOT NULL,
    [Title] nvarchar(1000)  NOT NULL,
    [ToolTip] nvarchar(200)  NULL,
    [ImagePath] varchar(200)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- Creating table 'Languages'
CREATE TABLE [dbo].[Languages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [CultureCode] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(200)  NOT NULL,
    [Description] nvarchar(1000)  NULL,
    [LanguageId] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- Creating table 'ProductImages'
CREATE TABLE [dbo].[ProductImages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductId] int  NOT NULL,
    [ImagePath] varchar(200)  NOT NULL,
    [ThumbnailPath] varchar(200)  NOT NULL,
    [IsPrimary] bit  NOT NULL
);
GO

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(1000)  NOT NULL,
    [IsMandatary] bit  NOT NULL,
    [IsActive] bit  NOT NULL,
    [QuestionTypeId] int  NOT NULL,
    [LanguageId] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- Creating table 'QuestionTypes'
CREATE TABLE [dbo].[QuestionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] varchar(50)  NOT NULL
);
GO

-- Creating table 'States'
CREATE TABLE [dbo].[States] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] char(2)  NOT NULL,
    [Name] varchar(100)  NOT NULL
);
GO

-- Creating table 'Surveys'
CREATE TABLE [dbo].[Surveys] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Description] nvarchar(4000)  NULL,
    [Status] int  NOT NULL,
    [LanguageId] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- Creating table 'SurveyQuestionAnswers'
CREATE TABLE [dbo].[SurveyQuestionAnswers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SurveyId] int  NOT NULL,
    [QuestionId] int  NOT NULL,
    [AnswerId] int  NOT NULL,
    [Guid] uniqueidentifier  NOT NULL,
    [UserId] int  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- Creating table 'SurveyQuestionMaps'
CREATE TABLE [dbo].[SurveyQuestionMaps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SurveyId] int  NOT NULL,
    [QuestionId] int  NOT NULL,
    [AnswerId] int  NOT NULL,
    [ProductId] int  NULL,
    [ChildQuestionId] int  NULL,
    [QuestionOrderNumber] int  NOT NULL,
    [SurveyQuestionMapId] int  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] varchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [RoleId] int  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- Creating table 'UserDetails'
CREATE TABLE [dbo].[UserDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [BusinessName] nvarchar(200)  NULL,
    [FirstName] nvarchar(200)  NOT NULL,
    [LastName] nvarchar(200)  NULL,
    [StateId] int  NOT NULL,
    [Zip] nvarchar(50)  NOT NULL,
    [PhoneNumber] nvarchar(50)  NULL,
    [EmailId] nvarchar(50)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- Creating table 'UserSurveys'
CREATE TABLE [dbo].[UserSurveys] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [SurveyId] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [PK_Answers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Languages'
ALTER TABLE [dbo].[Languages]
ADD CONSTRAINT [PK_Languages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductImages'
ALTER TABLE [dbo].[ProductImages]
ADD CONSTRAINT [PK_ProductImages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [PK_Questions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTypes'
ALTER TABLE [dbo].[QuestionTypes]
ADD CONSTRAINT [PK_QuestionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [PK_States]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Surveys'
ALTER TABLE [dbo].[Surveys]
ADD CONSTRAINT [PK_Surveys]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SurveyQuestionAnswers'
ALTER TABLE [dbo].[SurveyQuestionAnswers]
ADD CONSTRAINT [PK_SurveyQuestionAnswers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SurveyQuestionMaps'
ALTER TABLE [dbo].[SurveyQuestionMaps]
ADD CONSTRAINT [PK_SurveyQuestionMaps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [PK_UserDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSurveys'
ALTER TABLE [dbo].[UserSurveys]
ADD CONSTRAINT [PK_UserSurveys]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LanguageId] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK_Answer_Language]
    FOREIGN KEY ([LanguageId])
    REFERENCES [dbo].[Languages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Answer_Language'
CREATE INDEX [IX_FK_Answer_Language]
ON [dbo].[Answers]
    ([LanguageId]);
GO

-- Creating foreign key on [QuestionId] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK_Answer_Question]
    FOREIGN KEY ([QuestionId])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Answer_Question'
CREATE INDEX [IX_FK_Answer_Question]
ON [dbo].[Answers]
    ([QuestionId]);
GO

-- Creating foreign key on [AnswerId] in table 'SurveyQuestionAnswers'
ALTER TABLE [dbo].[SurveyQuestionAnswers]
ADD CONSTRAINT [FK_SurveyQuestionAnswer_Answer]
    FOREIGN KEY ([AnswerId])
    REFERENCES [dbo].[Answers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SurveyQuestionAnswer_Answer'
CREATE INDEX [IX_FK_SurveyQuestionAnswer_Answer]
ON [dbo].[SurveyQuestionAnswers]
    ([AnswerId]);
GO

-- Creating foreign key on [AnswerId] in table 'SurveyQuestionMaps'
ALTER TABLE [dbo].[SurveyQuestionMaps]
ADD CONSTRAINT [FK_SurveyQuestionMap_Answer]
    FOREIGN KEY ([AnswerId])
    REFERENCES [dbo].[Answers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SurveyQuestionMap_Answer'
CREATE INDEX [IX_FK_SurveyQuestionMap_Answer]
ON [dbo].[SurveyQuestionMaps]
    ([AnswerId]);
GO

-- Creating foreign key on [LanguageId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Product_Language]
    FOREIGN KEY ([LanguageId])
    REFERENCES [dbo].[Languages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Language'
CREATE INDEX [IX_FK_Product_Language]
ON [dbo].[Products]
    ([LanguageId]);
GO

-- Creating foreign key on [LanguageId] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK_Question_Language]
    FOREIGN KEY ([LanguageId])
    REFERENCES [dbo].[Languages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Question_Language'
CREATE INDEX [IX_FK_Question_Language]
ON [dbo].[Questions]
    ([LanguageId]);
GO

-- Creating foreign key on [LanguageId] in table 'Surveys'
ALTER TABLE [dbo].[Surveys]
ADD CONSTRAINT [FK_Survey_Language]
    FOREIGN KEY ([LanguageId])
    REFERENCES [dbo].[Languages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Survey_Language'
CREATE INDEX [IX_FK_Survey_Language]
ON [dbo].[Surveys]
    ([LanguageId]);
GO

-- Creating foreign key on [ProductId] in table 'ProductImages'
ALTER TABLE [dbo].[ProductImages]
ADD CONSTRAINT [FK_ProductImages_Product]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductImages_Product'
CREATE INDEX [IX_FK_ProductImages_Product]
ON [dbo].[ProductImages]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'SurveyQuestionMaps'
ALTER TABLE [dbo].[SurveyQuestionMaps]
ADD CONSTRAINT [FK_SurveyQuestionMap_Product]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SurveyQuestionMap_Product'
CREATE INDEX [IX_FK_SurveyQuestionMap_Product]
ON [dbo].[SurveyQuestionMaps]
    ([ProductId]);
GO

-- Creating foreign key on [QuestionTypeId] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK_Question_QuestionType]
    FOREIGN KEY ([QuestionTypeId])
    REFERENCES [dbo].[QuestionTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Question_QuestionType'
CREATE INDEX [IX_FK_Question_QuestionType]
ON [dbo].[Questions]
    ([QuestionTypeId]);
GO

-- Creating foreign key on [QuestionId] in table 'SurveyQuestionAnswers'
ALTER TABLE [dbo].[SurveyQuestionAnswers]
ADD CONSTRAINT [FK_SurveyQuestionAnswer_Question]
    FOREIGN KEY ([QuestionId])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SurveyQuestionAnswer_Question'
CREATE INDEX [IX_FK_SurveyQuestionAnswer_Question]
ON [dbo].[SurveyQuestionAnswers]
    ([QuestionId]);
GO

-- Creating foreign key on [QuestionId] in table 'SurveyQuestionMaps'
ALTER TABLE [dbo].[SurveyQuestionMaps]
ADD CONSTRAINT [FK_SurveyQuestionMap_Question]
    FOREIGN KEY ([QuestionId])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SurveyQuestionMap_Question'
CREATE INDEX [IX_FK_SurveyQuestionMap_Question]
ON [dbo].[SurveyQuestionMaps]
    ([QuestionId]);
GO

-- Creating foreign key on [ChildQuestionId] in table 'SurveyQuestionMaps'
ALTER TABLE [dbo].[SurveyQuestionMaps]
ADD CONSTRAINT [FK_SurveyQuestionMap_Question1]
    FOREIGN KEY ([ChildQuestionId])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SurveyQuestionMap_Question1'
CREATE INDEX [IX_FK_SurveyQuestionMap_Question1]
ON [dbo].[SurveyQuestionMaps]
    ([ChildQuestionId]);
GO

-- Creating foreign key on [RoleId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_User_Role]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_User_Role'
CREATE INDEX [IX_FK_User_Role]
ON [dbo].[Users]
    ([RoleId]);
GO

-- Creating foreign key on [StateId] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [FK_UserDetail_State]
    FOREIGN KEY ([StateId])
    REFERENCES [dbo].[States]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDetail_State'
CREATE INDEX [IX_FK_UserDetail_State]
ON [dbo].[UserDetails]
    ([StateId]);
GO

-- Creating foreign key on [SurveyId] in table 'SurveyQuestionAnswers'
ALTER TABLE [dbo].[SurveyQuestionAnswers]
ADD CONSTRAINT [FK_SurveyQuestionAnswer_Survey]
    FOREIGN KEY ([SurveyId])
    REFERENCES [dbo].[Surveys]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SurveyQuestionAnswer_Survey'
CREATE INDEX [IX_FK_SurveyQuestionAnswer_Survey]
ON [dbo].[SurveyQuestionAnswers]
    ([SurveyId]);
GO

-- Creating foreign key on [SurveyId] in table 'SurveyQuestionMaps'
ALTER TABLE [dbo].[SurveyQuestionMaps]
ADD CONSTRAINT [FK_SurveyQuestionMap_Survey]
    FOREIGN KEY ([SurveyId])
    REFERENCES [dbo].[Surveys]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SurveyQuestionMap_Survey'
CREATE INDEX [IX_FK_SurveyQuestionMap_Survey]
ON [dbo].[SurveyQuestionMaps]
    ([SurveyId]);
GO

-- Creating foreign key on [SurveyId] in table 'UserSurveys'
ALTER TABLE [dbo].[UserSurveys]
ADD CONSTRAINT [FK_UserSurvey_Survey]
    FOREIGN KEY ([SurveyId])
    REFERENCES [dbo].[Surveys]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserSurvey_Survey'
CREATE INDEX [IX_FK_UserSurvey_Survey]
ON [dbo].[UserSurveys]
    ([SurveyId]);
GO

-- Creating foreign key on [UserId] in table 'SurveyQuestionAnswers'
ALTER TABLE [dbo].[SurveyQuestionAnswers]
ADD CONSTRAINT [FK_SurveyQuestionAnswer_UserSurvey]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSurveys]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SurveyQuestionAnswer_UserSurvey'
CREATE INDEX [IX_FK_SurveyQuestionAnswer_UserSurvey]
ON [dbo].[SurveyQuestionAnswers]
    ([UserId]);
GO

-- Creating foreign key on [SurveyQuestionMapId] in table 'SurveyQuestionMaps'
ALTER TABLE [dbo].[SurveyQuestionMaps]
ADD CONSTRAINT [FK_SurveyQuestionMap_SurveyQuestionMap]
    FOREIGN KEY ([SurveyQuestionMapId])
    REFERENCES [dbo].[SurveyQuestionMaps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SurveyQuestionMap_SurveyQuestionMap'
CREATE INDEX [IX_FK_SurveyQuestionMap_SurveyQuestionMap]
ON [dbo].[SurveyQuestionMaps]
    ([SurveyQuestionMapId]);
GO

-- Creating foreign key on [UserId] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [FK_UserDetail_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDetail_User'
CREATE INDEX [IX_FK_UserDetail_User]
ON [dbo].[UserDetails]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'UserSurveys'
ALTER TABLE [dbo].[UserSurveys]
ADD CONSTRAINT [FK_UserSurvey_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserSurvey_User'
CREATE INDEX [IX_FK_UserSurvey_User]
ON [dbo].[UserSurveys]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------