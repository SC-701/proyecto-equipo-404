-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE AgregarEstado 
	-- Add the parameters for the stored procedure here
	@NombreEstado VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION

		INSERT INTO [dbo].[Estado] ([nombreEstado])
		VALUES (@NombreEstado);

		SELECT SCOPE_IDENTITY() AS idEstado;

	COMMIT TRANSACTION;
		
END