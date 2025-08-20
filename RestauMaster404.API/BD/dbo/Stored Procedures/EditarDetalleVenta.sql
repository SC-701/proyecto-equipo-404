CREATE PROCEDURE EditarDetalleVenta
    @Id UNIQUEIDENTIFIER,
    @IdVenta UNIQUEIDENTIFIER,
    @IdPlatillo UNIQUEIDENTIFIER,
    @Cantidad INT,
    @PrecioUnitario FLOAT,
    @SubTotal FLOAT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION
        UPDATE DetalleVenta
        SET
            IdVenta = @IdVenta,
            IdPlatillo = @IdPlatillo,
            Cantidad = @Cantidad,
            PrecioUnitario = @PrecioUnitario,
            SubTotal = @SubTotal
        WHERE Id = @Id;
        SELECT @Id;
    COMMIT TRANSACTION
END