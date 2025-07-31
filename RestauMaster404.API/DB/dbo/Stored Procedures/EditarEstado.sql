-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE EditarEstado 
	-- Add the parameters for the stored procedure here
	@IdEstado INT,
	@NombreEstado VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION

		UPDATE [dbo].[Estado]
			SET nombreEstado = @NombreEstado
			WHERE idEstado = @IdEstado;

		SELECT @IdEstado

	COMMIT TRANSACTION;
		
END