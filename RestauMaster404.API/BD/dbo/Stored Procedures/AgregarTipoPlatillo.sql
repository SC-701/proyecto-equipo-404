CREATE PROCEDURE AgregarTipoPlatillo
    @Id UNIQUEIDENTIFIER,
    @Nombre VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION
        INSERT INTO TipoPlatillos (Id, Nombre)
        VALUES (@Id, @Nombre);
        SELECT @Id;
    COMMIT TRANSACTION
END