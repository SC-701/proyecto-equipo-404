CREATE PROCEDURE ObtenerVentas
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Fecha, Hora, Total
    FROM Ventas
    ORDER BY Fecha DESC, Hora DESC;
END