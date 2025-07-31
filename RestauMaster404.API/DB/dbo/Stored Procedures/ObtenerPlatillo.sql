-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ObtenerPlatillo
	-- Add the parameters for the stored procedure here
	@IdPlatillo INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT
		idPlatillo,
		idTipoPlatillo,
		nombrePlatillo,
		precio,
		stock,
		idEstado
	FROM
		[dbo].[Platillo]
	WHERE
		idPlatillo = @IdPlatillo;
		
END