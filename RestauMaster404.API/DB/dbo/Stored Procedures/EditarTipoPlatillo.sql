-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE EditarTipoPlatillo
	-- Add the parameters for the stored procedure here
	@IdTipoPlatillo INT,
	@NombreTipoPlatillo VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    BEGIN TRANSACTION
		UPDATE [dbo].[TipoPlatillos]
			SET nombreTipoPlatillos = @NombreTipoPlatillo
			WHERE idTipoPlatillo = @IdTipoPlatillo;

		SELECT @IdTipoPlatillo;

	COMMIT TRANSACTION;
		
END