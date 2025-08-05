CREATE PROCEDURE ObtenerDetallesVenta
    @IdVenta UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        DV.Id,
        DV.IdVenta,
        V.Fecha,
        DV.IdPlatillo,
        P.Nombre AS Platillo,
        DV.Cantidad,
        DV.PrecioUnitario,
        DV.SubTotal
    FROM DetalleVenta DV
    INNER JOIN Ventas V ON DV.IdVenta = V.Id
    INNER JOIN Platillos P ON DV.IdPlatillo = P.Id
    WHERE DV.IdVenta = @IdVenta
    ORDER BY DV.Id;
END