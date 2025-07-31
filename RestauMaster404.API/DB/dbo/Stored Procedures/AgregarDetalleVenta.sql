-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE AgregarDetalleVenta
	-- Add the parameters for the stored procedure here
	@IdVenta INT,
	@IdPlatillo INT,
	@Cantidad INT,
	@PrecioUnitario FLOAT(53),
	@SubTotal FLOAT(53)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   BEGIN TRANSACTION
		INSERT INTO [dbo].[DetalleVenta] (
			idVenta,
			idPlatillo,
			cantidad,
			precioUnitario,
			subTotal
		)
		VALUES (
			@IdVenta,
			@IdPlatillo,
			@Cantidad,
			@PrecioUnitario,
			@SubTotal
		);

		SELECT SCOPE_IDENTITY() AS idDetalleVenta;

		COMMIT TRANSACTION;
		
END