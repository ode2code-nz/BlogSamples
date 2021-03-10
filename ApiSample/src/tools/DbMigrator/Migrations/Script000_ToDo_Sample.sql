CREATE TABLE [dbo].[ToDoItems] (
    [Id]          INT         IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (200) NOT NULL,
    [Description] NVARCHAR (400) NOT NULL,
    [Email]       NVARCHAR (250) NULL,
    [IsDone]      BIT            NOT NULL,
    CONSTRAINT [PK_ToDoItems] PRIMARY KEY CLUSTERED ([Id] ASC)
);

