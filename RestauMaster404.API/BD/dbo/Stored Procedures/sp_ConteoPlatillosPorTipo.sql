CREATE   PROCEDURE dbo.sp_ConteoPlatillosPorTipo
    @SoloDisponibles BIT = 0  -- 1 = contar solo stock > 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        tp.Id      AS TipoId,
        tp.Nombre  AS TipoNombre,
        COUNT(p.Id) AS CantidadPlatillos
    FROM dbo.TipoPlatillos tp
    LEFT JOIN dbo.Platillos p
        ON p.IdTipoPlatillo = tp.Id
        AND (@SoloDisponibles = 0 OR ISNULL(p.Stock,0) > 0)
    GROUP BY tp.Id, tp.Nombre
    ORDER BY tp.Nombre;
END;