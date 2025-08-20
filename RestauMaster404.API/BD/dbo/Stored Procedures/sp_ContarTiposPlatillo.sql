-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_ContarTiposPlatillo
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT([Id]) AS Total
    FROM [dbo].[TipoPlatillos];
END