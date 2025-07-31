-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ObtenerDetallesVentas
	-- Add the parameters for the stored procedure here

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
	ORDER BY
		idVenta, idDetalleVenta;
		
END