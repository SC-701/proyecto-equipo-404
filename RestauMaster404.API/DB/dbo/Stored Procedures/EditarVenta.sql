-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE EditarVenta
	-- Add the parameters for the stored procedure here
	@IdVenta INT,
	@Fecha DATE,
	@Hora TIME(7),
	@Total FLOAT(53)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   BEGIN TRANSACTION
		UPDATE [dbo].[Venta]
			SET 
				fecha = @Fecha,
				hora = @Hora,
				total = @Total
			WHERE idVenta = @IdVenta;

		SELECT @IdVenta;

		COMMIT TRANSACTION;
		
END