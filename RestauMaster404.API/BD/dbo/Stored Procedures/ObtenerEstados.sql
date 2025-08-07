CREATE PROCEDURE ObtenerEstados
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Nombre
    FROM Estados
    ORDER BY Nombre;
END