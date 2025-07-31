CREATE TABLE [dbo].[Venta] (
    [idVenta] INT        IDENTITY (1, 1) NOT NULL,
    [fecha]   DATE       NOT NULL,
    [hora]    TIME (7)   NOT NULL,
    [total]   FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([idVenta] ASC)
);

