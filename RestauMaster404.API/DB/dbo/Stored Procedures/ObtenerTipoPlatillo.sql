-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ObtenerTipoPlatillo
	-- Add the parameters for the stored procedure here
	@IdTipoPlatillo INT
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
	WHERE 
		idTipoPlatillo = @IdTipoPlatillo;
		
END