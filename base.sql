USE [master]
GO
/****** Object:  Database [VentaDirectaYPorCatalogo]    Script Date: 4/2/2019 12:41:10 ******/
CREATE DATABASE [VentaDirectaYPorCatalogo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VentaDirectaYPorCatalogo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\VentaDirectaYPorCatalogo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VentaDirectaYPorCatalogo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\VentaDirectaYPorCatalogo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VentaDirectaYPorCatalogo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET ARITHABORT OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET  MULTI_USER 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET QUERY_STORE = OFF
GO
USE [VentaDirectaYPorCatalogo]
GO
/****** Object:  Table [dbo].[Catalogo]    Script Date: 4/2/2019 12:41:10 ******/
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
/****** Object:  Table [dbo].[Persona]    Script Date: 4/2/2019 12:41:10 ******/
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
/****** Object:  Table [dbo].[Producto]    Script Date: 4/2/2019 12:41:10 ******/
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
/****** Object:  Table [dbo].[ProductosXCatalogo]    Script Date: 4/2/2019 12:41:10 ******/
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
/****** Object:  Table [dbo].[TipoDeProducto]    Script Date: 4/2/2019 12:41:10 ******/
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
/****** Object:  Table [dbo].[Usuarios]    Script Date: 4/2/2019 12:41:10 ******/
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
USE [master]
GO
ALTER DATABASE [VentaDirectaYPorCatalogo] SET  READ_WRITE 
GO
