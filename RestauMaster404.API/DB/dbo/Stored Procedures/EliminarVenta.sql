-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[EliminarVenta]
	-- Add the parameters for the stored procedure here
	@IdVenta INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   BEGIN TRANSACTION
		DELETE FROM [dbo].[Venta]
			WHERE idVenta = @IdVenta;


		SELECT @IdVenta;

		COMMIT TRANSACTION;
		
END