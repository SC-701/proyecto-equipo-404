
CREATE PROCEDURE EditarPlatillo
    @Id UNIQUEIDENTIFIER,
    @IdTipoPlatillo UNIQUEIDENTIFIER,
    @Nombre VARCHAR(100),
    @Precio FLOAT,
    @Stock INT,
    @IdEstado UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION
        UPDATE Platillos
        SET
            IdTipoPlatillo = @IdTipoPlatillo,
            Nombre = @Nombre,
            Precio = @Precio,
            Stock = @Stock,
            IdEstado = @IdEstado
        WHERE Id = @Id;
        SELECT @Id;
    COMMIT TRANSACTION
END