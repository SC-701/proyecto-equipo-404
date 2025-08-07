CREATE PROCEDURE ObtenerTiposPlatillos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Nombre
    FROM TipoPlatillos
    ORDER BY Nombre;
END