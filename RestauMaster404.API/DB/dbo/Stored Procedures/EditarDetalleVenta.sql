-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE EditarDetalleVenta
	-- Add the parameters for the stored procedure here
	@IdDetalleVenta INT,
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
		UPDATE [dbo].[DetalleVenta]
			SET
				idVenta = @IdVenta,
				idPlatillo = @IdPlatillo,
				cantidad = @Cantidad,
				precioUnitario = @PrecioUnitario,
				subTotal = @SubTotal
			WHERE idDetalleVenta = @IdDetalleVenta;

		SELECT @IdDetalleVenta;

		COMMIT TRANSACTION;
		
END