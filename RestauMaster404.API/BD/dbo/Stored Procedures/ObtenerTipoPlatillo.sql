CREATE PROCEDURE ObtenerTipoPlatillo
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Nombre
    FROM TipoPlatillos
    WHERE Id = @Id;
END