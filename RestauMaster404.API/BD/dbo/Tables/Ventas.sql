CREATE TABLE [dbo].[Ventas] (
    [Id]    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Fecha] DATE             NOT NULL,
    [Hora]  TIME (7)         NOT NULL,
    [Total] FLOAT (53)       NOT NULL,
    CONSTRAINT [PK_Ventas] PRIMARY KEY CLUSTERED ([Id] ASC)
);

