-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ObtenerTiposPlatillos
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		idTipoPlatillo,
		nombreTipoPlatillos
	FROM 
		[dbo].[TipoPlatillos]
	ORDER BY nombreTipoPlatillos;
		
END