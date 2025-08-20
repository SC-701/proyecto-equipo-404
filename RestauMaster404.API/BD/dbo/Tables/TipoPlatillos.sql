CREATE TABLE [dbo].[TipoPlatillos] (
    [Id]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Nombre] VARCHAR (50)     NOT NULL,
    CONSTRAINT [PK_TipoPlatillos] PRIMARY KEY CLUSTERED ([Id] ASC)
);

