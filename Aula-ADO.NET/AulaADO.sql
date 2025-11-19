USE [master]
GO
/****** Object:  Database [Teste]    Script Date: 11/17/2025 13:19:19 ******/
CREATE DATABASE [AulaADO]
GO
 
USE [AulaADO]
GO
/****** Object:  Table [dbo].[Enderecos]    Script Date: 11/17/2025 13:19:19 ******/
 
CREATE TABLE [dbo].[Enderecos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[logradouro] [varchar](100) NOT NULL,
	[numero] [int] NULL,
	[complemento] [varchar](100) NULL,
	[bairro] [varchar](100) NOT NULL,
	[cidade] [varchar](100) NOT NULL,
	[estado] [varchar](2) NOT NULL,
	[cep] [varchar](10) NOT NULL,
	[pessoaId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pessoas]    Script Date: 11/17/2025 13:19:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pessoas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](100) NOT NULL,
	[cpf] [varchar](11) NOT NULL,
	[dataNascimento] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[cpf] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Telefones]    Script Date: 11/17/2025 13:19:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Telefones](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ddd] [varchar](2) NOT NULL,
	[numero] [varchar](15) NOT NULL,
	[tipo] [varchar](10) NOT NULL,
	[pessoaId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Enderecos]  WITH CHECK ADD FOREIGN KEY([pessoaId])
REFERENCES [dbo].[Pessoas] ([id])
GO
ALTER TABLE [dbo].[Telefones]  WITH CHECK ADD FOREIGN KEY([pessoaId])
REFERENCES [dbo].[Pessoas] ([id])
GO
USE [master]
GO
ALTER DATABASE [AulaADO] SET  READ_WRITE 
GO
