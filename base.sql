USE [VentaDirectaYPorCatalogo]
GO
/****** Object:  Table [dbo].[Catalogo]    Script Date: 4/2/2019 12:51:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Catalogo](
	[idCatalogo] [int] NOT NULL,
	[año] [int] NOT NULL,
	[temporada] [varchar](50) NOT NULL,
	[idProducto] [int] NOT NULL,
 CONSTRAINT [PK_Catalogo] PRIMARY KEY CLUSTERED 
(
	[idCatalogo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persona]    Script Date: 4/2/2019 12:51:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persona](
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[idUsuario] [varchar](50) NOT NULL,
	[idPersona] [int] NOT NULL,
 CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED 
(
	[idPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 4/2/2019 12:51:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[idProducto] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[tipoProducto] [varchar](50) NOT NULL,
	[imagen] [varchar](50) NOT NULL,
	[precio] [int] NOT NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductosXCatalogo]    Script Date: 4/2/2019 12:51:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductosXCatalogo](
	[idProducto] [int] NOT NULL,
	[idCatalogo] [int] NOT NULL,
 CONSTRAINT [PK_ProductosXCatalogo] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC,
	[idCatalogo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoDeProducto]    Script Date: 4/2/2019 12:51:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoDeProducto](
	[tipoProducto] [varchar](50) NOT NULL,
	[descripcion] [varchar](255) NOT NULL,
 CONSTRAINT [PK_TipoDeProducto] PRIMARY KEY CLUSTERED 
(
	[tipoProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 4/2/2019 12:51:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[usuario] [varchar](50) NOT NULL,
	[contraseña] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Persona]  WITH CHECK ADD  CONSTRAINT [FK_Persona_Usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuarios] ([usuario])
GO
ALTER TABLE [dbo].[Persona] CHECK CONSTRAINT [FK_Persona_Usuario]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_TipoDeProducto] FOREIGN KEY([tipoProducto])
REFERENCES [dbo].[TipoDeProducto] ([tipoProducto])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_TipoDeProducto]
GO
ALTER TABLE [dbo].[ProductosXCatalogo]  WITH CHECK ADD  CONSTRAINT [FK_ProductosXCatalogo_Catalogo] FOREIGN KEY([idCatalogo])
REFERENCES [dbo].[Catalogo] ([idCatalogo])
GO
ALTER TABLE [dbo].[ProductosXCatalogo] CHECK CONSTRAINT [FK_ProductosXCatalogo_Catalogo]
GO
ALTER TABLE [dbo].[ProductosXCatalogo]  WITH CHECK ADD  CONSTRAINT [FK_ProductosXCatalogo_Producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[Producto] ([idProducto])
GO
ALTER TABLE [dbo].[ProductosXCatalogo] CHECK CONSTRAINT [FK_ProductosXCatalogo_Producto]
GO
