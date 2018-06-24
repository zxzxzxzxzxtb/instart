layui:http://www.layui.com/demo/button.html

数据库创建表sql:
CREATE TABLE [dbo].[User] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [UserName]   NVARCHAR (50) NOT NULL,
    [Password]   NVARCHAR (50) NOT NULL,
	[NickName]   NVARCHAR (50),
    [Role]       INT           DEFAULT ((0)) NOT NULL,
    [Status]     INT           DEFAULT ((1)) NOT NULL,
    [CreateTime] DATETIME      DEFAULT (getdate()) NOT NULL,
    [ModifyTime] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Article] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [CategoryId]   INT             DEFAULT ((0)) NOT NULL,
    [CategoryName] NVARCHAR (50)   NULL,
    [Title]        NVARCHAR (512)  NOT NULL,
    [SubTitle]     NVARCHAR (512)  NULL,
    [Author]       NVARCHAR (50)   NULL,
    [Source]       NVARCHAR (50)   NULL,
    [Summary]      NVARCHAR (1024) NULL,
    [Content]      TEXT            NOT NULL,
    [Status]       TINYINT         DEFAULT ((1)) NOT NULL,
    [IsReommend]   TINYINT         DEFAULT ((0)) NOT NULL,
    [IsTop]        TINYINT         DEFAULT ((0)) NOT NULL,
    [IsHot]        TINYINT         DEFAULT ((0)) NOT NULL,
    [GroupIndex]   INT             DEFAULT ((0)) NOT NULL,
    [CreateTime]   DATETIME        DEFAULT (getdate()) NOT NULL,
    [ModifyTime]   DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE NONCLUSTERED INDEX [IX_Article_CategoryId]
    ON [dbo].[Article]([CategoryId] ASC);

CREATE TABLE [dbo].[Category] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [ParentId]   INT           DEFAULT ((0)) NOT NULL,
    [Name]       NVARCHAR (50) NOT NULL,
    [Code]       NVARCHAR (50) NULL,
    [GroupIndex] INT           DEFAULT ((0)) NOT NULL,
    [Status]     TINYINT       DEFAULT ((1)) NOT NULL,
    [CreateTime] DATETIME      DEFAULT (getdate()) NOT NULL,
    [ModifyTime] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

