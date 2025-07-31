-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE AgregarPlatillo
	-- Add the parameters for the stored procedure here
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
		INSERT INTO [dbo].[Platillo] (
			idTipoPlatillo,
			nombrePlatillo,
			precio,
			stock,
			idEstado
		)
		VALUES (
			@IdTipoPlatillo,
			@NombrePlatillo,
			@Precio,
			@Stock,
			@IdEstado
		);

		SELECT SCOPE_IDENTITY() AS idPlatillo;

		COMMIT TRANSACTION;
		
END