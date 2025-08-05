CREATE TABLE [dbo].[Platillos] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdTipoPlatillo] UNIQUEIDENTIFIER NOT NULL,
    [Nombre]         VARCHAR (100)    NOT NULL,
    [Precio]         FLOAT (53)       NOT NULL,
    [Stock]          INT              NOT NULL,
    [IdEstado]       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Platillos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Platillos_Estado] FOREIGN KEY ([IdEstado]) REFERENCES [dbo].[Estados] ([Id]),
    CONSTRAINT [FK_Platillos_Tipo] FOREIGN KEY ([IdTipoPlatillo]) REFERENCES [dbo].[TipoPlatillos] ([Id])
);

