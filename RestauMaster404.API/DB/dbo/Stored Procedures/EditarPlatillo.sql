-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE EditarPlatillo
	-- Add the parameters for the stored procedure here
	@IdPlatillo INT,
	@IdTipoPlatillo INT,
	@NombrePlatillo VARCHAR(100),
	@Precio FLOAT,
	@Stock INT,
	@IdEstado INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   BEGIN TRANSACTION
		UPDATE [dbo].[Platillo]
			SET 
				idTipoPlatillo = @IdTipoPlatillo,
				nombrePlatillo = @NombrePlatillo,
				precio = @Precio,
				stock = @Stock,
				idEstado = @IdEstado
			WHERE idPlatillo = @IdPlatillo;

		SELECT @IdPlatillo;

		COMMIT TRANSACTION;
		
END