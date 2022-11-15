USE [El_Sabroso_DB]
GO

/****** Object:  Table [dbo].[CATEGORIAS_PROD]    Script Date: 11/5/2022 6:08:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CATEGORIAS_PROD](
	[id_categoria] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_CATEGORIAS_PROD] PRIMARY KEY CLUSTERED 
(
	[id_categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



USE [El_Sabroso_DB]
GO

/****** Object:  Table [dbo].[USUARIOS]    Script Date: 11/5/2022 6:09:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[USUARIOS](
	[Id_usuario] [int] NOT NULL,
	[Id_permiso] [int] NULL,
	[usuario] [varchar](50) NULL,
	[password] [varchar](20) NULL,
	[email] [varchar](50) NULL,
	[activo] [bit] NULL,
 CONSTRAINT [PK_USUARIOS] PRIMARY KEY CLUSTERED 
(
	[Id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



USE [El_Sabroso_DB]
GO

/****** Object:  Table [dbo].[PROVEEDORES]    Script Date: 11/5/2022 6:10:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PROVEEDORES](
	[Id_proveedor] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](250) NULL,
	[apellido] [varchar](250) NULL,
	[email] [varchar](250) NULL,
	[telefono] [varchar](250) NULL,
	[direccion] [varchar](250) NULL,
	[ciudad] [varchar](250) NULL,
	[fecha_alta] [date] NULL,
	[activo] [bit] NULL,
 CONSTRAINT [PK_PROVEEDORES] PRIMARY KEY CLUSTERED 
(
	[Id_proveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [El_Sabroso_DB]
GO

/****** Object:  Table [dbo].[PRODUCTOS]    Script Date: 11/5/2022 6:10:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PRODUCTOS](
	[Id_producto] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](250) NULL,
	[descripcion] [varchar](250) NULL,
	[precio] [float] NULL,
	[id_proveedor] [int] NULL,
	[activo] [varchar](1) NULL,
	[id_categoria] [int] NULL,
 CONSTRAINT [PK_PRODUCTOS] PRIMARY KEY CLUSTERED 
(
	[Id_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PRODUCTOS]  WITH CHECK ADD FOREIGN KEY([id_proveedor])
REFERENCES [dbo].[PROVEEDORES] ([Id_proveedor])
GO

ALTER TABLE [dbo].[PRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCTOS_CATEGORIAS] FOREIGN KEY([id_categoria])
REFERENCES [dbo].[CATEGORIAS_PROD] ([id_categoria])
GO

ALTER TABLE [dbo].[PRODUCTOS] CHECK CONSTRAINT [FK_PRODUCTOS_CATEGORIAS]
GO



USE [El_Sabroso_DB]
GO

/****** Object:  Table [dbo].[VENTAS]    Script Date: 11/5/2022 6:11:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VENTAS](
	[Id_venta] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [date] NULL,
	[cantidad] [varchar](50) NULL,
	[monto] [varchar](20) NULL,
	[nombre_cliente] [varchar](50) NULL,
	[apellido_cliente] [varchar](50) NULL,
	[email_cliente] [varchar](50) NULL,
	[telefono_cliente] [varchar](50) NULL,
	[id_producto] [int] NULL,
	[id_usuario] [int] NULL,
	[id_stock] [int] NULL,
 CONSTRAINT [PK_VENTAS] PRIMARY KEY CLUSTERED 
(
	[Id_venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



USE [El_Sabroso_DB]
GO

/****** Object:  Table [dbo].[DETALLE_VENTAS]    Script Date: 11/5/2022 6:11:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DETALLE_VENTAS](
	[detalle_nro] [int] IDENTITY(1,1) NOT NULL,
	[Id_venta] [int] NULL,
	[cantidad] [varchar](50) NULL,
	[Id_proveedor] [int] NULL,
	[id_categoria] [int] NULL,
	[Id_producto] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[detalle_nro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DETALLE_VENTAS]  WITH CHECK ADD FOREIGN KEY([id_categoria])
REFERENCES [dbo].[CATEGORIAS_PROD] ([id_categoria])
GO


ALTER TABLE [dbo].[DETALLE_VENTAS]  WITH CHECK ADD FOREIGN KEY([Id_producto])
REFERENCES [dbo].[PRODUCTOS] ([Id_producto])
GO

ALTER TABLE [dbo].[DETALLE_VENTAS]  WITH CHECK ADD FOREIGN KEY([Id_proveedor])
REFERENCES [dbo].[PROVEEDORES] ([Id_proveedor])
GO

ALTER TABLE [dbo].[DETALLE_VENTAS]  WITH CHECK ADD FOREIGN KEY([Id_venta])
REFERENCES [dbo].[VENTAS] ([Id_venta])
GO






USE [El_Sabroso_DB]
GO

/****** Object:  Table [dbo].[PERMISOS]    Script Date: 11/5/2022 6:11:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PERMISOS](
	[Id_permiso] [int] NOT NULL,
	[nombre] [varchar](50) NULL,
	[descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_PERMISOS] PRIMARY KEY CLUSTERED 
(
	[Id_permiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[STOCK](
	[Id_producto] [int] NOT NULL,
	[stock] [int] NOT NULL,

	)
	ALTER TABLE [dbo].[STOCK]  WITH CHECK ADD FOREIGN KEY([id_producto])
REFERENCES [dbo].[PRODUCTOS] ([Id_producto])
GO


GO



