CREATE PROCEDURE AgregarDetalleVenta
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
        INSERT INTO DetalleVenta (
            Id,
            IdVenta,
            IdPlatillo,
            Cantidad,
            PrecioUnitario,
            SubTotal
        )
        VALUES (
            @Id,
            @IdVenta,
            @IdPlatillo,
            @Cantidad,
            @PrecioUnitario,
            @SubTotal
        );
        SELECT @Id;
    COMMIT TRANSACTION
END