-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE AgregarVenta
	-- Add the parameters for the stored procedure here
	@Fecha DATE,
	@Hora TIME(7),
	@Total FLOAT(53)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   BEGIN TRANSACTION
		INSERT INTO [dbo].[Venta] (fecha, hora, total)
		VALUES (@Fecha, @Hora, @Total);

		SELECT SCOPE_IDENTITY() AS idVenta;

		COMMIT TRANSACTION;
		
END