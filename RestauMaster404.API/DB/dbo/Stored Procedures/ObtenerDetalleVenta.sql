-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ObtenerDetalleVenta
	-- Add the parameters for the stored procedure here
	@IdDetalleVenta INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT
		idDetalleVenta,
		idVenta,
		idPlatillo,
		cantidad,
		precioUnitario,
		subTotal
	FROM
		[dbo].[DetalleVenta]
	WHERE
		idDetalleVenta = @IdDetalleVenta;
		
END