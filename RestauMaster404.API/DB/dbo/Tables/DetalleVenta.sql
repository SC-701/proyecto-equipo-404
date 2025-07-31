CREATE TABLE [dbo].[DetalleVenta] (
    [idDetalleVenta] INT        IDENTITY (1, 1) NOT NULL,
    [idVenta]        INT        NOT NULL,
    [idPlatillo]     INT        NOT NULL,
    [cantidad]       INT        NOT NULL,
    [precioUnitario] FLOAT (53) NOT NULL,
    [subTotal]       FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([idDetalleVenta] ASC)
);

