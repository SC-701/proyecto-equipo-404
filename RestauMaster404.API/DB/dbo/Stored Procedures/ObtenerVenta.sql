-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ObtenerVenta
	-- Add the parameters for the stored procedure here
	@IdVenta INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT
		idVenta,
		fecha,
		hora,
		total
	FROM
		[dbo].[Venta]
	WHERE
		idVenta = @IdVenta;
		
END