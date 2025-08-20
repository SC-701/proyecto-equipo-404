CREATE TABLE [dbo].[Estados] (
    [Id]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Nombre] VARCHAR (50)     NOT NULL,
    CONSTRAINT [PK_Estados] PRIMARY KEY CLUSTERED ([Id] ASC)
);

