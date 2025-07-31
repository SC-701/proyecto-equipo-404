CREATE TABLE [dbo].[TipoPlatillos] (
    [idTipoPlatillo]      INT          IDENTITY (1, 1) NOT NULL,
    [nombreTipoPlatillos] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([idTipoPlatillo] ASC)
);

