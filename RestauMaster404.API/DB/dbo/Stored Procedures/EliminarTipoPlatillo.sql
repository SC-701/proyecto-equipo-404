-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE EliminarTipoPlatillo
	-- Add the parameters for the stored procedure here
	@IdTipoPlatillo INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    BEGIN TRANSACTION
		DELETE FROM [dbo].[TipoPlatillos]
			WHERE idTipoPlatillo = @IdTipoPlatillo;

		SELECT @IdTipoPlatillo;

	COMMIT TRANSACTION;
		
END