CREATE DATABASE [ReservaEnLinea]
USE [ReservaEnLinea]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 07/06/2021 13:35:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](150) NOT NULL,
	[Descripcion] [nvarchar](250) NULL,
	[Post] [nvarchar](max) NULL,
	[Fecha] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
	[Tipo] [nvarchar](50) NULL,
	[Categoria] [nvarchar](50) NULL,
	[Video] [nvarchar](50) NULL,
	[Imagen] [nvarchar](150) NULL,
	[Consultas] [int] NOT NULL,
	[Tags] [nvarchar](250) NULL,
	[MetaKeys] [nvarchar](250) NULL,
	[UsuarioId] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[LugarId] [int] NULL,
	[ServicioId] [int] NULL,
	[EventoId] [int] NULL,
	[MeGusta] [int] NULL,
	[Comentarios] [int] NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chat]    Script Date: 07/06/2021 13:35:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chat](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NULL,
	[Nombre] [nvarchar](250) NOT NULL,
	[Mensaje] [nvarchar](450) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[De] [nvarchar](50) NOT NULL,
	[Para] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Chat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comentarios]    Script Date: 07/06/2021 13:35:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comentarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Comentario] [nvarchar](250) NOT NULL,
	[Nombre] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Activo] [bit] NOT NULL,
	[LugarId] [int] NULL,
	[EventoId] [int] NULL,
	[BlogId] [int] NULL,
	[Fecha] [datetime] NOT NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_Comentarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Configuracion]    Script Date: 07/06/2021 13:35:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configuracion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LogoPrincipal] [nvarchar](150) NULL,
	[LogoFooter] [nvarchar](150) NULL,
	[Direccion] [nvarchar](250) NULL,
	[Telefono] [nvarchar](50) NULL,
	[Twitter] [nvarchar](350) NULL,
	[Facebook] [nvarchar](350) NULL,
	[PaginaWeb] [nvarchar](350) NULL,
	[Instagram] [nvarchar](350) NULL,
	[Linkedin] [nvarchar](350) NULL,
	[Universidad] [nvarchar](10) NULL,
 CONSTRAINT [PK_Configuracion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cracteristicas]    Script Date: 07/06/2021 13:35:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cracteristicas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Cracteristicas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Eventos]    Script Date: 07/06/2021 13:35:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Eventos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](250) NOT NULL,
	[PalabrasClave] [nvarchar](250) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[Costo] [numeric](18, 2) NOT NULL,
	[Catedraticos] [int] NOT NULL,
	[Estudiantes] [int] NOT NULL,
	[Invitados] [int] NOT NULL,
	[MaximoCatedraticos] [int] NOT NULL,
	[MaximoEstudiantes] [int] NOT NULL,
	[MaximoInvitados] [int] NOT NULL,
	[CostoAdicionalCatedraticos] [int] NOT NULL,
	[CostoAdicionalEstudiantes] [int] NOT NULL,
	[CostoAdicionalInvitados] [int] NOT NULL,
	[TotalEventos] [int] NOT NULL,
	[Calificacion] [decimal](18, 2) NOT NULL,
	[Activo] [bit] NOT NULL,
	[LugarId] [int] NOT NULL,
	[Seccion1] [int] NOT NULL,
	[Seccion2] [int] NOT NULL,
	[Seccion3] [int] NOT NULL,
	[Seccion4] [int] NOT NULL,
 CONSTRAINT [PK_Eventos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Imagenes]    Script Date: 07/06/2021 13:35:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Imagenes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Imagen] [nvarchar](150) NOT NULL,
	[Nombre] [nvarchar](150) NOT NULL,
	[LugarId] [int] NULL,
	[EventoId] [int] NULL,
	[ServicioId] [int] NULL,
	[BlogId] [int] NULL,
	[Activo] [bit] NULL,
	[UsuarioId] [int] NOT NULL,
 CONSTRAINT [PK_Imagenes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImagenesAsociadas]    Script Date: 07/06/2021 13:35:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImagenesAsociadas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImagenId] [int] NOT NULL,
	[LugarId] [int] NULL,
	[EventoId] [int] NULL,
	[ServicioId] [int] NULL,
	[BlogId] [int] NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_ImagenesAsociadas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lugar]    Script Date: 07/06/2021 13:35:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lugar](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Telefono] [nvarchar](150) NULL,
	[Website] [nvarchar](250) NULL,
	[Email] [nvarchar](150) NULL,
	[PalabrasClave] [nvarchar](250) NULL,
	[Nombre] [nvarchar](250) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Direccion] [nvarchar](250) NULL,
	[Ciudad] [nvarchar](250) NULL,
	[CodigoPostal] [nvarchar](5) NULL,
	[Latitud] [numeric](18, 10) NULL,
	[Longitud] [numeric](18, 10) NULL,
	[Facebook] [nvarchar](150) NULL,
	[Twitter] [nvarchar](150) NULL,
	[Pinterest] [nvarchar](150) NULL,
	[LinkedIn] [nvarchar](150) NULL,
	[WhatsApp] [nvarchar](150) NULL,
	[Instagram] [nvarchar](150) NULL,
	[Facturacion] [bit] NULL,
	[UsuarioId] [int] NOT NULL,
	[Calificacion] [decimal](18, 2) NULL,
	[Estatus] [bit] NULL,
	[Departamento] [nvarchar](250) NULL,
	[Fecha] [datetime] NULL,
 CONSTRAINT [PK_Lugar] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LugaresEventosCaracteristicas]    Script Date: 07/06/2021 13:35:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LugaresEventosCaracteristicas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LugarId] [int] NOT NULL,
	[EventoId] [int] NULL,
	[CaracteristicaId] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_LugaresEventosCaracteristicas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReservaDetalle]    Script Date: 07/06/2021 13:35:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservaDetalle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TotalReservas] [int] NOT NULL,
	[Catedraticos] [int] NOT NULL,
	[Estudiantes] [int] NOT NULL,
	[Invitados] [int] NOT NULL,
	[CostoEvento] [numeric](18, 2) NOT NULL,
	[ReservaId] [int] NOT NULL,
	[EventoId] [int] NOT NULL,
 CONSTRAINT [PK_ReservaDetalle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservas]    Script Date: 07/06/2021 13:35:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Folio] [nvarchar](8) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaReservaInicia] [datetime] NOT NULL,
	[FechaReservaFinaliza] [datetime] NOT NULL,
	[CostoTotal] [numeric](18, 2) NOT NULL,
	[Estatus] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Nombres] [nvarchar](150) NOT NULL,
	[Apellidos] [nvarchar](150) NOT NULL,
	[Telefono] [nvarchar](50) NOT NULL,
	[Pais] [nvarchar](50) NOT NULL,
	[Departamento] [nvarchar](150) NOT NULL,
	[Municipio] [nvarchar](150) NOT NULL,
	[Direccion] [nvarchar](250) NOT NULL,
	[CodigoPostal] [nvarchar](5) NOT NULL,
	[RequerimientosEspeciales] [nvarchar](500) NOT NULL,
	[ReferenciaPago] [nvarchar](850) NOT NULL,
	[Tipo] [nvarchar](50) NOT NULL,
	[LugarId] [int] NOT NULL,
 CONSTRAINT [PK_Reservas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiciosExtras]    Script Date: 07/06/2021 13:35:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiciosExtras](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Servicio] [nvarchar](250) NOT NULL,
	[Costo] [numeric](18, 2) NOT NULL,
	[LugarId] [int] NULL,
	[EventoId] [int] NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[UsuarioId] [int] NULL,
	[Activio] [bit] NOT NULL,
 CONSTRAINT [PK_ServiciosExtras] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suscripciones]    Script Date: 07/06/2021 13:35:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suscripciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Suscripciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 07/06/2021 13:35:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](250) NOT NULL,
	[Telefono] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Password] [nvarchar](350) NOT NULL,
	[Estatus] [bit] NOT NULL,
	[UsuarioIdPadre] [int] NULL,
	[Foto] [nvarchar](150) NOT NULL,
	[Puesto] [nvarchar](100) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[ColorTema] [nvarchar](1) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Blog] ADD  CONSTRAINT [DF_Blog_MeGusta]  DEFAULT ((5)) FOR [MeGusta]
GO
ALTER TABLE [dbo].[Blog] ADD  CONSTRAINT [DF_Blog_Comentarios]  DEFAULT ((0)) FOR [Comentarios]
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_Eventos] FOREIGN KEY([EventoId])
REFERENCES [dbo].[Eventos] ([Id])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_Eventos]
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_Lugares] FOREIGN KEY([LugarId])
REFERENCES [dbo].[Lugar] ([Id])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_Lugares]
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_ServiciosExtras] FOREIGN KEY([ServicioId])
REFERENCES [dbo].[ServiciosExtras] ([Id])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_ServiciosExtras]
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_Usuarios] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_Usuarios]
GO
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_Usuarios] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Chat] CHECK CONSTRAINT [FK_Chat_Usuarios]
GO
ALTER TABLE [dbo].[Eventos]  WITH CHECK ADD  CONSTRAINT [FK_Eventos_Eventos] FOREIGN KEY([LugarId])
REFERENCES [dbo].[Lugar] ([Id])
GO
ALTER TABLE [dbo].[Eventos] CHECK CONSTRAINT [FK_Eventos_Eventos]
GO
ALTER TABLE [dbo].[Imagenes]  WITH CHECK ADD  CONSTRAINT [FK_Imagenes_Blog] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blog] ([Id])
GO
ALTER TABLE [dbo].[Imagenes] CHECK CONSTRAINT [FK_Imagenes_Blog]
GO
ALTER TABLE [dbo].[Imagenes]  WITH CHECK ADD  CONSTRAINT [FK_Imagenes_Lugares] FOREIGN KEY([LugarId])
REFERENCES [dbo].[Lugar] ([Id])
GO
ALTER TABLE [dbo].[Imagenes] CHECK CONSTRAINT [FK_Imagenes_Lugares]
GO
ALTER TABLE [dbo].[Imagenes]  WITH CHECK ADD  CONSTRAINT [FK_Imagenes_ServiciosExtras] FOREIGN KEY([ServicioId])
REFERENCES [dbo].[ServiciosExtras] ([Id])
GO
ALTER TABLE [dbo].[Imagenes] CHECK CONSTRAINT [FK_Imagenes_ServiciosExtras]
GO
ALTER TABLE [dbo].[Imagenes]  WITH CHECK ADD  CONSTRAINT [FK_Imagenes_Usuarios] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Imagenes] CHECK CONSTRAINT [FK_Imagenes_Usuarios]
GO
ALTER TABLE [dbo].[Lugar]  WITH CHECK ADD  CONSTRAINT [FK_Lugares_Usuarios] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Lugar] CHECK CONSTRAINT [FK_Lugares_Usuarios]
GO
ALTER TABLE [dbo].[LugaresEventosCaracteristicas]  WITH CHECK ADD  CONSTRAINT [FK_LugaresEventos_Cracteristicas] FOREIGN KEY([CaracteristicaId])
REFERENCES [dbo].[Cracteristicas] ([Id])
GO
ALTER TABLE [dbo].[LugaresEventosCaracteristicas] CHECK CONSTRAINT [FK_LugaresEventos_Cracteristicas]
GO
ALTER TABLE [dbo].[LugaresEventosCaracteristicas]  WITH CHECK ADD  CONSTRAINT [FK_LugaresEventosCaracteristicas_Eventos] FOREIGN KEY([EventoId])
REFERENCES [dbo].[Eventos] ([Id])
GO
ALTER TABLE [dbo].[LugaresEventosCaracteristicas] CHECK CONSTRAINT [FK_LugaresEventosCaracteristicas_Eventos]
GO
ALTER TABLE [dbo].[LugaresEventosCaracteristicas]  WITH CHECK ADD  CONSTRAINT [FK_LugaresEventosCaracteristicas_Lugares] FOREIGN KEY([LugarId])
REFERENCES [dbo].[Lugar] ([Id])
GO
ALTER TABLE [dbo].[LugaresEventosCaracteristicas] CHECK CONSTRAINT [FK_LugaresEventosCaracteristicas_Lugares]
GO
ALTER TABLE [dbo].[ReservaDetalle]  WITH CHECK ADD  CONSTRAINT [FK_ReservaDetalle_Eventos] FOREIGN KEY([EventoId])
REFERENCES [dbo].[Eventos] ([Id])
GO
ALTER TABLE [dbo].[ReservaDetalle] CHECK CONSTRAINT [FK_ReservaDetalle_Eventos]
GO
ALTER TABLE [dbo].[ReservaDetalle]  WITH CHECK ADD  CONSTRAINT [FK_ReservaDetalle_Reservas] FOREIGN KEY([ReservaId])
REFERENCES [dbo].[Reservas] ([Id])
GO
ALTER TABLE [dbo].[ReservaDetalle] CHECK CONSTRAINT [FK_ReservaDetalle_Reservas]
GO
ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [FK_Reservas_Lugares] FOREIGN KEY([LugarId])
REFERENCES [dbo].[Lugar] ([Id])
GO
ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [FK_Reservas_Lugares]
GO
ALTER TABLE [dbo].[ServiciosExtras]  WITH CHECK ADD  CONSTRAINT [FK_ServiciosExtras_Eventos] FOREIGN KEY([EventoId])
REFERENCES [dbo].[Eventos] ([Id])
GO
ALTER TABLE [dbo].[ServiciosExtras] CHECK CONSTRAINT [FK_ServiciosExtras_Eventos]
GO
ALTER TABLE [dbo].[ServiciosExtras]  WITH CHECK ADD  CONSTRAINT [FK_ServiciosExtras_Lugares] FOREIGN KEY([LugarId])
REFERENCES [dbo].[Lugar] ([Id])
GO
ALTER TABLE [dbo].[ServiciosExtras] CHECK CONSTRAINT [FK_ServiciosExtras_Lugares]
GO
ALTER TABLE [dbo].[ServiciosExtras]  WITH CHECK ADD  CONSTRAINT [FK_ServiciosExtras_Usuarios] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[ServiciosExtras] CHECK CONSTRAINT [FK_ServiciosExtras_Usuarios]
GO
