-- =================================================================================
-- Procedimiento para llenar las tablas con datos iniciales de ejemplo
-- Primero limpia todas las tablas para asegurar un estado inicial consistente.
-- Los precios y valores de la venta están configurados para ser lógicos en Colones (CRC).
-- =================================================================================
CREATE PROCEDURE [dbo].[sp_LlenarDatosIniciales]
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRAN;

        -- ===================================================
        -- 1. Limpiar las tablas en el orden correcto (por dependencias)
        --    Se usa DELETE FROM en todas las tablas para evitar errores
        --    con las claves foráneas.
        -- ===================================================
        PRINT 'Limpiando tablas...';
        
        -- Eliminar datos de las tablas con relaciones
        DELETE FROM [dbo].[PerfilesxUsuario];
        DELETE FROM [dbo].[DetalleVenta];
        DELETE FROM [dbo].[Platillos];

        -- Eliminar datos de las tablas principales
        DELETE FROM [dbo].[Perfiles];
        DELETE FROM [dbo].[Usuarios];
        DELETE FROM [dbo].[Ventas];
        DELETE FROM [dbo].[Estados];
        DELETE FROM [dbo].[TipoPlatillos];

        -- ===================================================
        -- 2. Declarar variables para almacenar IDs
        -- ===================================================
        DECLARE @idEstadoDisponible UNIQUEIDENTIFIER;
        DECLARE @idEstadoBajoStock UNIQUEIDENTIFIER;
        
        DECLARE @idTipoPlatilloEntrada UNIQUEIDENTIFIER;
        DECLARE @idTipoPlatilloPrincipal UNIQUEIDENTIFIER;
        DECLARE @idTipoPlatilloPostre UNIQUEIDENTIFIER;
        
        DECLARE @idPlatilloPasta UNIQUEIDENTIFIER;
        DECLARE @idPlatilloEnsalada UNIQUEIDENTIFIER;
        DECLARE @idPlatilloTiramisu UNIQUEIDENTIFIER;
        
        DECLARE @idVenta1 UNIQUEIDENTIFIER;
        
        DECLARE @idUsuarioAdmin UNIQUEIDENTIFIER;

        -- ===================================================
        -- 3. Insertar datos en tablas sin dependencias
        -- ===================================================

        -- Tabla Perfiles
        PRINT 'Insertando datos en Perfiles...';
        INSERT INTO [dbo].[Perfiles] ([Id], [Nombre]) VALUES (1, 'Admin');
        INSERT INTO [dbo].[Perfiles] ([Id], [Nombre]) VALUES (2, 'Ventas');
        INSERT INTO [dbo].[Perfiles] ([Id], [Nombre]) VALUES (3, 'Chef');
        
        -- Tabla Estados
        PRINT 'Insertando datos en Estados...';
        SET @idEstadoDisponible = NEWID();
        INSERT INTO [dbo].[Estados] ([Id], [Nombre]) VALUES (@idEstadoDisponible, 'Disponible');
        SET @idEstadoBajoStock = NEWID();
        INSERT INTO [dbo].[Estados] ([Id], [Nombre]) VALUES (@idEstadoBajoStock, 'Bajo Stock');
        
        -- Tabla TipoPlatillos
        PRINT 'Insertando datos en TipoPlatillos...';
        SET @idTipoPlatilloEntrada = NEWID();
        INSERT INTO [dbo].[TipoPlatillos] ([Id], [Nombre]) VALUES (@idTipoPlatilloEntrada, 'Entradas');
        SET @idTipoPlatilloPrincipal = NEWID();
        INSERT INTO [dbo].[TipoPlatillos] ([Id], [Nombre]) VALUES (@idTipoPlatilloPrincipal, 'Platos Principales');
        SET @idTipoPlatilloPostre = NEWID();
        INSERT INTO [dbo].[TipoPlatillos] ([Id], [Nombre]) VALUES (@idTipoPlatilloPostre, 'Postres');
        
        -- Tabla Usuarios
        PRINT 'Insertando datos en Usuarios...';
        SET @idUsuarioAdmin = NEWID();
        INSERT INTO [dbo].[Usuarios] ([Id], [NombreUsuario], [PasswordHash], [CorreoElectronico], [FechaCreacion])
        VALUES (@idUsuarioAdmin, 'admin', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 'admin@ejemplo.com', GETDATE());
        
        -- ===================================================
        -- 4. Insertar datos con dependencias
        -- ===================================================
        
        -- Tabla PerfilesxUsuario (relaciona a nuestro usuario con un perfil)
        PRINT 'Insertando datos en PerfilesxUsuario...';
        INSERT INTO [dbo].[PerfilesxUsuario] ([IdUsuario], [IdPerfil]) VALUES (@idUsuarioAdmin, 1);
        
        -- Tabla Platillos
        PRINT 'Insertando datos en Platillos...';
        SET @idPlatilloPasta = NEWID();
        INSERT INTO [dbo].[Platillos] ([Id], [IdTipoPlatillo], [Nombre], [Precio], [Stock], [IdEstado])
        VALUES (@idPlatilloPasta, @idTipoPlatilloPrincipal, 'Pasta Alfredo', 5500.00, 50, @idEstadoDisponible);
        
        SET @idPlatilloEnsalada = NEWID();
        INSERT INTO [dbo].[Platillos] ([Id], [IdTipoPlatillo], [Nombre], [Precio], [Stock], [IdEstado])
        VALUES (@idPlatilloEnsalada, @idTipoPlatilloEntrada, 'Ensalada Cesar', 4000.00, 20, @idEstadoDisponible);
        
        SET @idPlatilloTiramisu = NEWID();
        INSERT INTO [dbo].[Platillos] ([Id], [IdTipoPlatillo], [Nombre], [Precio], [Stock], [IdEstado])
        VALUES (@idPlatilloTiramisu, @idTipoPlatilloPostre, 'Tiramisú', 3000.00, 10, @idEstadoBajoStock);
        
        -- Tabla Ventas
        PRINT 'Insertando datos en Ventas...';
        SET @idVenta1 = NEWID();
        -- Total calculado de 2 pastas (11,000) y 1 ensalada (4,000) = 15,000
        INSERT INTO [dbo].[Ventas] ([Id], [Fecha], [Hora], [Total])
        VALUES (@idVenta1, GETDATE(), GETDATE(), 15000.00); 

        -- Tabla DetalleVenta
        PRINT 'Insertando datos en DetalleVenta...';
        INSERT INTO [dbo].[DetalleVenta] ([IdVenta], [IdPlatillo], [Cantidad], [PrecioUnitario], [SubTotal])
        VALUES (@idVenta1, @idPlatilloPasta, 2, 5500.00, 11000.00);
        INSERT INTO [dbo].[DetalleVenta] ([IdVenta], [IdPlatillo], [Cantidad], [PrecioUnitario], [SubTotal])
        VALUES (@idVenta1, @idPlatilloEnsalada, 1, 4000.00, 4000.00);
        
        -- ===================================================
        -- 4. Confirmar la transacción
        -- ===================================================
        COMMIT TRAN;
        PRINT 'Datos de ejemplo insertados exitosamente.';
    
    END TRY
    BEGIN CATCH
        -- En caso de error, deshacer la transacción
        ROLLBACK TRAN;
        PRINT 'Ocurrió un error. La transacción ha sido revertida.';
        -- Puedes agregar un mensaje de error detallado si es necesario
        -- SELECT ERROR_MESSAGE();
    END CATCH
END;