CREATE PROCEDURE ObtenerVenta
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Fecha, Hora, Total
    FROM Ventas
    WHERE Id = @Id;
END