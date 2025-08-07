CREATE PROCEDURE ObtenerPlatillo
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        P.Id,
        P.IdTipoPlatillo,
        TP.Nombre AS TipoPlatillo,
        P.Nombre,
        P.Precio,
        P.Stock,
        P.IdEstado,
        E.Nombre AS Estado
    FROM Platillos P
    INNER JOIN TipoPlatillos TP ON P.IdTipoPlatillo = TP.Id
    INNER JOIN Estados E ON P.IdEstado = E.Id
    WHERE P.Id = @Id;
END