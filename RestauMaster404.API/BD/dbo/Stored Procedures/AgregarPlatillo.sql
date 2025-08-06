CREATE PROCEDURE [dbo].[AgregarPlatillo]
    @Id UNIQUEIDENTIFIER,
    @IdTipoPlatillo UNIQUEIDENTIFIER,
    @Nombre VARCHAR(100),
    @Precio FLOAT,
    @Stock INT,
    @IdEstado UNIQUEIDENTIFIER,
	@Imagen VARBINARY(MAX)

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
            IdEstado,
			Imagen
        )
        VALUES (
            @Id,
            @IdTipoPlatillo,
            @Nombre,
            @Precio,
            @Stock,
            @IdEstado,
			@Imagen
        );
        SELECT @Id;
    COMMIT TRANSACTION
END