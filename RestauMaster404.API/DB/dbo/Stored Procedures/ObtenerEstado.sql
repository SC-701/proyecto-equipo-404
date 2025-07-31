-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ObtenerEstado
	-- Add the parameters for the stored procedure here
	@IdEstado INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		idEstado,
		nombreEstado
	FROM 
		[dbo].[Estado]
	WHERE 
		idEstado = @IdEstado;
		
END