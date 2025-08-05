CREATE PROCEDURE AgregarVenta
    @Id UNIQUEIDENTIFIER,
    @Fecha DATE,
    @Hora TIME(7),
    @Total FLOAT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION
        INSERT INTO Ventas (Id, Fecha, Hora, Total)
        VALUES (@Id, @Fecha, @Hora, @Total);
        SELECT @Id;
    COMMIT TRANSACTION
END