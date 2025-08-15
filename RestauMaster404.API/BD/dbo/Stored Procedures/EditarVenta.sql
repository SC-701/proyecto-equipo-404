CREATE PROCEDURE EditarVenta
    @Id UNIQUEIDENTIFIER,
    @Fecha DATE,
    @Hora TIME(7),
    @Total FLOAT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION
        UPDATE Ventas
        SET Fecha = @Fecha,
            Hora = @Hora,
            Total = @Total
        WHERE Id = @Id;
        SELECT @Id;
    COMMIT TRANSACTION
END