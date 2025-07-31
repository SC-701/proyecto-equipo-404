-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE EliminarPlatillo
	-- Add the parameters for the stored procedure here
	@IdPlatillo INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   BEGIN TRANSACTION
		DELETE FROM [dbo].[Platillo]
			WHERE idPlatillo = @IdPlatillo;

		SELECT @IdPlatillo;

		COMMIT TRANSACTION;
		
END