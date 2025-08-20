CREATE PROCEDURE EditarTipoPlatillo
    @Id UNIQUEIDENTIFIER,
    @Nombre VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION
        UPDATE TipoPlatillos
        SET Nombre = @Nombre
        WHERE Id = @Id;
        SELECT @Id;
    COMMIT TRANSACTION
END