-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE AgregarTipoPlatillo
	-- Add the parameters for the stored procedure here
	@NombreTipoPlatillo VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    BEGIN TRANSACTION
		INSERT INTO [dbo].[TipoPlatillos] (nombreTipoPlatillos)
		VALUES (@NombreTipoPlatillo);

		SELECT SCOPE_IDENTITY() AS idTipoPlatillo;

	COMMIT TRANSACTION;
		
END