CREATE TABLE [dbo].[Platillo] (
    [idPlatillo]     INT           IDENTITY (1, 1) NOT NULL,
    [idTipoPlatillo] INT           NOT NULL,
    [nombrePlatillo] VARCHAR (100) NOT NULL,
    [precio]         FLOAT (53)    NOT NULL,
    [stock]          INT           NOT NULL,
    [idEstado]       INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([idPlatillo] ASC),
    FOREIGN KEY ([idEstado]) REFERENCES [dbo].[Estado] ([idEstado]),
    FOREIGN KEY ([idTipoPlatillo]) REFERENCES [dbo].[TipoPlatillos] ([idTipoPlatillo])
);

