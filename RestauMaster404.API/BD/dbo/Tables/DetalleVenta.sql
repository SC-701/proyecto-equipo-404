CREATE TABLE [dbo].[DetalleVenta] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdVenta]        UNIQUEIDENTIFIER NOT NULL,
    [IdPlatillo]     UNIQUEIDENTIFIER NOT NULL,
    [Cantidad]       INT              NOT NULL,
    [PrecioUnitario] FLOAT (53)       NOT NULL,
    [SubTotal]       FLOAT (53)       NOT NULL,
    CONSTRAINT [PK_DetalleVenta] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetalleVenta_Platillo] FOREIGN KEY ([IdPlatillo]) REFERENCES [dbo].[Platillos] ([Id]),
    CONSTRAINT [FK_DetalleVenta_Venta] FOREIGN KEY ([IdVenta]) REFERENCES [dbo].[Ventas] ([Id])
);

