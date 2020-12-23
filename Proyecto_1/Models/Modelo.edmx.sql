
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/23/2020 17:23:30
-- Generated from EDMX file: C:\Users\Carlos\Source\Repos\Cristopher240598\LlanterasHalcon\Proyecto_1\Models\Modelo.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Llantera];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__compras__id_prov__76969D2E]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[compras] DROP CONSTRAINT [FK__compras__id_prov__76969D2E];
GO
IF OBJECT_ID(N'[dbo].[FK__detallesC__id_co__778AC167]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[detallesCompras] DROP CONSTRAINT [FK__detallesC__id_co__778AC167];
GO
IF OBJECT_ID(N'[dbo].[FK__detallesC__id_ll__787EE5A0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[detallesCompras] DROP CONSTRAINT [FK__detallesC__id_ll__787EE5A0];
GO
IF OBJECT_ID(N'[dbo].[FK__detallesV__id_ll__797309D9]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[detallesVentas] DROP CONSTRAINT [FK__detallesV__id_ll__797309D9];
GO
IF OBJECT_ID(N'[dbo].[FK__detallesV__id_ve__7A672E12]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[detallesVentas] DROP CONSTRAINT [FK__detallesV__id_ve__7A672E12];
GO
IF OBJECT_ID(N'[dbo].[FK__envios__id_paque__160F4887]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[envios] DROP CONSTRAINT [FK__envios__id_paque__160F4887];
GO
IF OBJECT_ID(N'[dbo].[FK__envios__id_venta__17036CC0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[envios] DROP CONSTRAINT [FK__envios__id_venta__17036CC0];
GO
IF OBJECT_ID(N'[dbo].[FK__llantas__id_marc__7F2BE32F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[llantas] DROP CONSTRAINT [FK__llantas__id_marc__7F2BE32F];
GO
IF OBJECT_ID(N'[dbo].[FK__llantas__id_prov__7D439ABD]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[llantas] DROP CONSTRAINT [FK__llantas__id_prov__7D439ABD];
GO
IF OBJECT_ID(N'[dbo].[FK__llantas__id_subc__7E37BEF6]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[llantas] DROP CONSTRAINT [FK__llantas__id_subc__7E37BEF6];
GO
IF OBJECT_ID(N'[dbo].[FK__subcatego__id_ca__75A278F5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[subcategorias] DROP CONSTRAINT [FK__subcatego__id_ca__75A278F5];
GO
IF OBJECT_ID(N'[dbo].[FK__usuarios__id_rol__19DFD96B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[usuarios] DROP CONSTRAINT [FK__usuarios__id_rol__19DFD96B];
GO
IF OBJECT_ID(N'[dbo].[FK__ventas__id_usuar__1AD3FDA4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ventas] DROP CONSTRAINT [FK__ventas__id_usuar__1AD3FDA4];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[categorias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[categorias];
GO
IF OBJECT_ID(N'[dbo].[compras]', 'U') IS NOT NULL
    DROP TABLE [dbo].[compras];
GO
IF OBJECT_ID(N'[dbo].[detallesCompras]', 'U') IS NOT NULL
    DROP TABLE [dbo].[detallesCompras];
GO
IF OBJECT_ID(N'[dbo].[detallesVentas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[detallesVentas];
GO
IF OBJECT_ID(N'[dbo].[envios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[envios];
GO
IF OBJECT_ID(N'[dbo].[llantas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[llantas];
GO
IF OBJECT_ID(N'[dbo].[marcas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[marcas];
GO
IF OBJECT_ID(N'[dbo].[paqueterias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[paqueterias];
GO
IF OBJECT_ID(N'[dbo].[proveedores]', 'U') IS NOT NULL
    DROP TABLE [dbo].[proveedores];
GO
IF OBJECT_ID(N'[dbo].[roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[roles];
GO
IF OBJECT_ID(N'[dbo].[subcategorias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[subcategorias];
GO
IF OBJECT_ID(N'[dbo].[usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[usuarios];
GO
IF OBJECT_ID(N'[dbo].[ventas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ventas];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'categorias'
CREATE TABLE [dbo].[categorias] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(80)  NOT NULL,
    [descripcion] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'compras'
CREATE TABLE [dbo].[compras] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [fecha] datetime  NOT NULL,
    [iva] decimal(9,2)  NOT NULL,
    [subtotal] decimal(9,2)  NOT NULL,
    [total] decimal(9,2)  NOT NULL,
    [id_proveedor] int  NOT NULL
);
GO

-- Creating table 'detallesCompras'
CREATE TABLE [dbo].[detallesCompras] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [cantidad] int  NOT NULL,
    [precioCompra] decimal(9,2)  NOT NULL,
    [precioVenta] decimal(9,2)  NOT NULL,
    [id_compra] int  NOT NULL,
    [id_llanta] int  NOT NULL
);
GO

-- Creating table 'detallesVentas'
CREATE TABLE [dbo].[detallesVentas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [cantidad] int  NOT NULL,
    [precioCompra] decimal(9,2)  NOT NULL,
    [precioVenta] decimal(9,2)  NOT NULL,
    [id_llanta] int  NOT NULL,
    [id_venta] int  NOT NULL
);
GO

-- Creating table 'envios'
CREATE TABLE [dbo].[envios] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [fechaCreacion] datetime  NOT NULL,
    [fechaEnvio] datetime  NULL,
    [estado] nvarchar(80)  NOT NULL,
    [id_paqueteria] int  NOT NULL,
    [id_venta] int  NOT NULL
);
GO

-- Creating table 'llantas'
CREATE TABLE [dbo].[llantas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [modelo] nvarchar(80)  NOT NULL,
    [descripcion] nvarchar(100)  NOT NULL,
    [rin] int  NOT NULL,
    [ancho] int  NOT NULL,
    [perfil] int  NOT NULL,
    [carga] int  NOT NULL,
    [imagen] nvarchar(255)  NOT NULL,
    [stock] int  NOT NULL,
    [existencia] int  NOT NULL,
    [precioVenta] decimal(9,2)  NOT NULL,
    [precioCompra] decimal(9,2)  NOT NULL,
    [ultActualizacion] datetime  NOT NULL,
    [id_proveedor] int  NOT NULL,
    [id_subcategoria] int  NOT NULL,
    [id_marca] int  NOT NULL
);
GO

-- Creating table 'marcas'
CREATE TABLE [dbo].[marcas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(80)  NOT NULL,
    [imagen] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'paqueterias'
CREATE TABLE [dbo].[paqueterias] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(80)  NOT NULL,
    [web] nvarchar(100)  NOT NULL,
    [telefono] nchar(10)  NOT NULL
);
GO

-- Creating table 'proveedores'
CREATE TABLE [dbo].[proveedores] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [razonSocial] nvarchar(80)  NOT NULL,
    [rfc] nvarchar(15)  NOT NULL,
    [direccion] nvarchar(80)  NOT NULL,
    [telefono] nvarchar(10)  NOT NULL,
    [correoElectronico] nvarchar(80)  NOT NULL
);
GO

-- Creating table 'roles'
CREATE TABLE [dbo].[roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(80)  NOT NULL,
    [descripcion] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'subcategorias'
CREATE TABLE [dbo].[subcategorias] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(80)  NOT NULL,
    [descripcion] nvarchar(100)  NOT NULL,
    [id_categoria] int  NOT NULL
);
GO

-- Creating table 'usuarios'
CREATE TABLE [dbo].[usuarios] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(80)  NOT NULL,
    [apellidoPaterno] nvarchar(80)  NOT NULL,
    [apellidoMaterno] nvarchar(80)  NOT NULL,
    [telefono] nvarchar(10)  NOT NULL,
    [correoElectronico] nvarchar(80)  NOT NULL,
    [contrasenia] nvarchar(80)  NULL,
    [estado] nvarchar(80)  NULL,
    [municipio] nvarchar(80)  NULL,
    [colonia] nvarchar(80)  NULL,
    [calle] nvarchar(80)  NULL,
    [numeroCasa] int  NULL,
    [cp] int  NULL,
    [tarjetaCredito] nvarchar(80)  NULL,
    [tipoTarjeta] nvarchar(80)  NULL,
    [anio] int  NULL,
    [mes] int  NULL,
    [cvv] int  NULL,
    [id_rol] int  NOT NULL
);
GO

-- Creating table 'ventas'
CREATE TABLE [dbo].[ventas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [fechaVenta] datetime  NOT NULL,
    [subtotal] decimal(9,2)  NOT NULL,
    [iva] decimal(9,2)  NOT NULL,
    [total] decimal(9,2)  NOT NULL,
    [id_usuario] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'categorias'
ALTER TABLE [dbo].[categorias]
ADD CONSTRAINT [PK_categorias]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'compras'
ALTER TABLE [dbo].[compras]
ADD CONSTRAINT [PK_compras]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'detallesCompras'
ALTER TABLE [dbo].[detallesCompras]
ADD CONSTRAINT [PK_detallesCompras]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'detallesVentas'
ALTER TABLE [dbo].[detallesVentas]
ADD CONSTRAINT [PK_detallesVentas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'envios'
ALTER TABLE [dbo].[envios]
ADD CONSTRAINT [PK_envios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'llantas'
ALTER TABLE [dbo].[llantas]
ADD CONSTRAINT [PK_llantas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'marcas'
ALTER TABLE [dbo].[marcas]
ADD CONSTRAINT [PK_marcas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'paqueterias'
ALTER TABLE [dbo].[paqueterias]
ADD CONSTRAINT [PK_paqueterias]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'proveedores'
ALTER TABLE [dbo].[proveedores]
ADD CONSTRAINT [PK_proveedores]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'roles'
ALTER TABLE [dbo].[roles]
ADD CONSTRAINT [PK_roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'subcategorias'
ALTER TABLE [dbo].[subcategorias]
ADD CONSTRAINT [PK_subcategorias]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'usuarios'
ALTER TABLE [dbo].[usuarios]
ADD CONSTRAINT [PK_usuarios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ventas'
ALTER TABLE [dbo].[ventas]
ADD CONSTRAINT [PK_ventas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [id_categoria] in table 'subcategorias'
ALTER TABLE [dbo].[subcategorias]
ADD CONSTRAINT [FK__subcatego__id_ca__75A278F5]
    FOREIGN KEY ([id_categoria])
    REFERENCES [dbo].[categorias]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__subcatego__id_ca__75A278F5'
CREATE INDEX [IX_FK__subcatego__id_ca__75A278F5]
ON [dbo].[subcategorias]
    ([id_categoria]);
GO

-- Creating foreign key on [id_proveedor] in table 'compras'
ALTER TABLE [dbo].[compras]
ADD CONSTRAINT [FK__compras__id_prov__76969D2E]
    FOREIGN KEY ([id_proveedor])
    REFERENCES [dbo].[proveedores]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__compras__id_prov__76969D2E'
CREATE INDEX [IX_FK__compras__id_prov__76969D2E]
ON [dbo].[compras]
    ([id_proveedor]);
GO

-- Creating foreign key on [id_compra] in table 'detallesCompras'
ALTER TABLE [dbo].[detallesCompras]
ADD CONSTRAINT [FK__detallesC__id_co__778AC167]
    FOREIGN KEY ([id_compra])
    REFERENCES [dbo].[compras]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__detallesC__id_co__778AC167'
CREATE INDEX [IX_FK__detallesC__id_co__778AC167]
ON [dbo].[detallesCompras]
    ([id_compra]);
GO

-- Creating foreign key on [id_llanta] in table 'detallesCompras'
ALTER TABLE [dbo].[detallesCompras]
ADD CONSTRAINT [FK__detallesC__id_ll__787EE5A0]
    FOREIGN KEY ([id_llanta])
    REFERENCES [dbo].[llantas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__detallesC__id_ll__787EE5A0'
CREATE INDEX [IX_FK__detallesC__id_ll__787EE5A0]
ON [dbo].[detallesCompras]
    ([id_llanta]);
GO

-- Creating foreign key on [id_llanta] in table 'detallesVentas'
ALTER TABLE [dbo].[detallesVentas]
ADD CONSTRAINT [FK__detallesV__id_ll__797309D9]
    FOREIGN KEY ([id_llanta])
    REFERENCES [dbo].[llantas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__detallesV__id_ll__797309D9'
CREATE INDEX [IX_FK__detallesV__id_ll__797309D9]
ON [dbo].[detallesVentas]
    ([id_llanta]);
GO

-- Creating foreign key on [id_venta] in table 'detallesVentas'
ALTER TABLE [dbo].[detallesVentas]
ADD CONSTRAINT [FK__detallesV__id_ve__7A672E12]
    FOREIGN KEY ([id_venta])
    REFERENCES [dbo].[ventas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__detallesV__id_ve__7A672E12'
CREATE INDEX [IX_FK__detallesV__id_ve__7A672E12]
ON [dbo].[detallesVentas]
    ([id_venta]);
GO

-- Creating foreign key on [id_paqueteria] in table 'envios'
ALTER TABLE [dbo].[envios]
ADD CONSTRAINT [FK__envios__id_paque__160F4887]
    FOREIGN KEY ([id_paqueteria])
    REFERENCES [dbo].[paqueterias]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__envios__id_paque__160F4887'
CREATE INDEX [IX_FK__envios__id_paque__160F4887]
ON [dbo].[envios]
    ([id_paqueteria]);
GO

-- Creating foreign key on [id_venta] in table 'envios'
ALTER TABLE [dbo].[envios]
ADD CONSTRAINT [FK__envios__id_venta__17036CC0]
    FOREIGN KEY ([id_venta])
    REFERENCES [dbo].[ventas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__envios__id_venta__17036CC0'
CREATE INDEX [IX_FK__envios__id_venta__17036CC0]
ON [dbo].[envios]
    ([id_venta]);
GO

-- Creating foreign key on [id_marca] in table 'llantas'
ALTER TABLE [dbo].[llantas]
ADD CONSTRAINT [FK__llantas__id_marc__7F2BE32F]
    FOREIGN KEY ([id_marca])
    REFERENCES [dbo].[marcas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__llantas__id_marc__7F2BE32F'
CREATE INDEX [IX_FK__llantas__id_marc__7F2BE32F]
ON [dbo].[llantas]
    ([id_marca]);
GO

-- Creating foreign key on [id_proveedor] in table 'llantas'
ALTER TABLE [dbo].[llantas]
ADD CONSTRAINT [FK__llantas__id_prov__7D439ABD]
    FOREIGN KEY ([id_proveedor])
    REFERENCES [dbo].[proveedores]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__llantas__id_prov__7D439ABD'
CREATE INDEX [IX_FK__llantas__id_prov__7D439ABD]
ON [dbo].[llantas]
    ([id_proveedor]);
GO

-- Creating foreign key on [id_subcategoria] in table 'llantas'
ALTER TABLE [dbo].[llantas]
ADD CONSTRAINT [FK__llantas__id_subc__7E37BEF6]
    FOREIGN KEY ([id_subcategoria])
    REFERENCES [dbo].[subcategorias]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__llantas__id_subc__7E37BEF6'
CREATE INDEX [IX_FK__llantas__id_subc__7E37BEF6]
ON [dbo].[llantas]
    ([id_subcategoria]);
GO

-- Creating foreign key on [id_rol] in table 'usuarios'
ALTER TABLE [dbo].[usuarios]
ADD CONSTRAINT [FK__usuarios__id_rol__19DFD96B]
    FOREIGN KEY ([id_rol])
    REFERENCES [dbo].[roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__usuarios__id_rol__19DFD96B'
CREATE INDEX [IX_FK__usuarios__id_rol__19DFD96B]
ON [dbo].[usuarios]
    ([id_rol]);
GO

-- Creating foreign key on [id_usuario] in table 'ventas'
ALTER TABLE [dbo].[ventas]
ADD CONSTRAINT [FK__ventas__id_usuar__1AD3FDA4]
    FOREIGN KEY ([id_usuario])
    REFERENCES [dbo].[usuarios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ventas__id_usuar__1AD3FDA4'
CREATE INDEX [IX_FK__ventas__id_usuar__1AD3FDA4]
ON [dbo].[ventas]
    ([id_usuario]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------