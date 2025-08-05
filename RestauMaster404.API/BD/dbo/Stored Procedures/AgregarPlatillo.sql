CREATE PROCEDURE AgregarPlatillo
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
        INSERT INTO Platillos (
            Id,
            IdTipoPlatillo,
            Nombre,
            Precio,
            Stock,
            IdEstado
        )
        VALUES (
            @Id,
            @IdTipoPlatillo,
            @Nombre,
            @Precio,
            @Stock,
            @IdEstado
        );
        SELECT @Id;
    COMMIT TRANSACTION
END