USE [master]
GO
/****** Object:  Database [qlsx]    Script Date: 01/05/2021 11:57:49 ******/
CREATE DATABASE [qlsx]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'qlsx', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\qlsx.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'qlsx_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\qlsx_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [qlsx] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [qlsx].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [qlsx] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [qlsx] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [qlsx] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [qlsx] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [qlsx] SET ARITHABORT OFF 
GO
ALTER DATABASE [qlsx] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [qlsx] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [qlsx] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [qlsx] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [qlsx] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [qlsx] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [qlsx] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [qlsx] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [qlsx] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [qlsx] SET  DISABLE_BROKER 
GO
ALTER DATABASE [qlsx] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [qlsx] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [qlsx] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [qlsx] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [qlsx] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [qlsx] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [qlsx] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [qlsx] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [qlsx] SET  MULTI_USER 
GO
ALTER DATABASE [qlsx] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [qlsx] SET DB_CHAINING OFF 
GO
ALTER DATABASE [qlsx] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [qlsx] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [qlsx] SET DELAYED_DURABILITY = DISABLED 
GO
USE [qlsx]
GO
/****** Object:  User [IIS APPPOOL\QLSUAXE]    Script Date: 01/05/2021 11:57:49 ******/
CREATE USER [IIS APPPOOL\QLSUAXE] FOR LOGIN [IIS APPPOOL\QLSUAXE] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [IIS APPPOOL\DefaultAppPool]    Script Date: 01/05/2021 11:57:49 ******/
CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [IIS APPPOOL\QLSUAXE]
GO
ALTER ROLE [db_datareader] ADD MEMBER [IIS APPPOOL\QLSUAXE]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [IIS APPPOOL\QLSUAXE]
GO
ALTER ROLE [db_owner] ADD MEMBER [IIS APPPOOL\DefaultAppPool]
GO
/****** Object:  Table [dbo].[Accessories]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accessories](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Quantity] [int] NULL,
	[Unit] [nvarchar](50) NULL,
 CONSTRAINT [PK_Accessories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AccessoryPriceHistory]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessoryPriceHistory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AccessoriesId] [bigint] NOT NULL,
	[Price] [money] NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
 CONSTRAINT [PK_AccessoryPriceHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Account_Roles]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account_Roles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
 CONSTRAINT [PK_Account_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](250) NULL,
	[Token] [varchar](500) NULL,
	[ExpiredToken] [datetime] NULL,
	[Status] [tinyint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[StatusActing] [tinyint] NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Sex] [bit] NULL,
	[Birth] [datetime] NULL,
	[Phone] [varchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[Status] [tinyint] NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[JobPositionId] [bigint] NULL,
	[Name] [nvarchar](250) NULL,
	[Birth] [datetime] NULL,
	[Sex] [bit] NULL,
	[JoinedDate] [datetime] NULL,
	[IdentityId] [bigint] NULL,
	[Phone] [varchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[Note] [nvarchar](500) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ImportReceipt]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImportReceipt](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SupplierId] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
 CONSTRAINT [PK_ImportReceipt] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ImportReceipt_Accessory]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImportReceipt_Accessory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ImportReceiptId] [bigint] NOT NULL,
	[AccesoryId] [bigint] NOT NULL,
	[ImportPrice] [money] NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_ImportReceipt_Accessory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[JobPositions]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobPositions](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Status] [tinyint] NULL,
	[Note] [nvarchar](500) NULL,
 CONSTRAINT [PK_JobPositions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menus]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Menus](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ParentId] [bigint] NULL,
	[Url] [varchar](500) NULL,
	[Note] [nvarchar](500) NULL,
	[Order] [int] NULL,
	[Status] [tinyint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MotorLifts]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MotorLifts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Status] [tinyint] NULL,
	[Note] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_MotorLifts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MotorManufacture]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MotorManufacture](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [tinyint] NULL,
 CONSTRAINT [PK_MotorManufacture] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MotorTypes]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MotorTypes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MotorManufactureID] [bigint] NULL,
	[Name] [nvarchar](50) NULL,
	[Status] [tinyint] NULL,
 CONSTRAINT [PK_MotorTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Permissions](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[MenuId] [bigint] NOT NULL,
	[ActionCode] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Note] [nvarchar](500) NULL,
	[Status] [tinyint] NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServicePriceHistory]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServicePriceHistory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ServiceId] [bigint] NULL,
	[Price] [money] NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
 CONSTRAINT [PK_ServicePriceHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Services]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NULL,
	[Status] [tinyint] NULL,
	[Note] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Supplier](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[Phone] [varchar](50) NULL,
	[Email] [varchar](500) NULL,
	[Website] [nvarchar](500) NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TemporaryBill]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TemporaryBill](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MotorLiftId] [bigint] NULL,
	[CustomerId] [bigint] NULL,
	[MotorTypeId] [bigint] NULL,
	[TimeIn] [datetime] NULL,
	[TimeOut] [datetime] NULL,
	[Note] [nvarchar](500) NULL,
	[Status] [tinyint] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedTime] [datetime] NULL,
	[PrintedBy] [bigint] NULL,
 CONSTRAINT [PK_TemporaryBill] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TemporaryBill_Accesary]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TemporaryBill_Accesary](
	[TemporaryBillId] [bigint] NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AccesaryId] [bigint] NULL,
	[Quantity] [int] NULL,
	[AccesaryPrice] [money] NULL,
 CONSTRAINT [PK_TemporaryBill_Accesary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TemporaryBill_Service]    Script Date: 01/05/2021 11:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TemporaryBill_Service](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TemporaryBillId] [bigint] NULL,
	[ServiceId] [bigint] NULL,
	[ServicePrice] [money] NULL,
 CONSTRAINT [PK_TemporaryBill_Service] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Accessories] ON 

INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (1, N'Bugi loại nhỏ', 25, N'cái')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (2, N'Bô xe số', 5, N'cái')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (3, N'Dây cu loa', 3, N'cái')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (4, N'Dầu Castrol 70', 16, N'chai')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (5, N'Dầu Castrol 115', 24, N'chai')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (6, N'Kim phun xăng', 10, N'cái')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (7, N'Nhông xích xe số loại 1', 29, N'cái')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (8, N'Nhông xích xe số loại 2', 10, N'cái')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (9, N'Xăm xe số loại 1', 20, N'cái')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (10, N'Xăm xe số loại 2', 10, N'cái')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (11, N'Phanh xe số', 20, N'cái')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (12, N'Phanh ABS', 8, N'cái')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (13, N'Nhông sên dĩa Redleo', 5, N'bộ')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (14, N'Lốp xe Camel 14', 5, N'cái')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (15, N'Đĩa thắng', 4, N'cái')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (16, N'Phuộc', 2, N'cặp')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (17, N'Phao báo xăng ', 5, N'cái')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (18, N'Lốp loại 1', 13, N'cái')
INSERT [dbo].[Accessories] ([Id], [Name], [Quantity], [Unit]) VALUES (19, N'Xăm xe', 8, N'cái')
SET IDENTITY_INSERT [dbo].[Accessories] OFF
SET IDENTITY_INSERT [dbo].[AccessoryPriceHistory] ON 

INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (1, 1, 120000.0000, CAST(N'2020-11-17 21:11:28.840' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (2, 2, 300000.0000, CAST(N'2020-11-17 21:11:40.040' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (3, 3, 80000.0000, CAST(N'2020-11-17 21:11:49.827' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (4, 4, 70000.0000, CAST(N'2020-11-17 21:12:09.827' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (5, 5, 115000.0000, CAST(N'2020-11-17 21:12:20.880' AS DateTime), CAST(N'2020-11-22 01:59:57.197' AS DateTime))
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (6, 6, 150000.0000, CAST(N'2020-11-17 21:12:35.833' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (7, 7, 280000.0000, CAST(N'2020-11-17 21:12:49.747' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (8, 8, 280000.0000, CAST(N'2020-11-17 21:12:59.907' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (9, 9, 50000.0000, CAST(N'2020-11-17 21:13:12.217' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (10, 10, 70000.0000, CAST(N'2020-11-17 21:13:24.547' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (11, 11, 150000.0000, CAST(N'2020-11-17 21:13:36.617' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (12, 12, 450000.0000, CAST(N'2020-11-17 21:13:48.677' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (13, 13, 340000.0000, CAST(N'2020-11-17 21:14:02.867' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (14, 14, 120000.0000, CAST(N'2020-11-17 21:14:11.617' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (15, 15, 300000.0000, CAST(N'2020-11-17 21:14:24.177' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (16, 16, 1500000.0000, CAST(N'2020-11-17 21:14:37.547' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (17, 17, 190000.0000, CAST(N'2020-11-17 21:14:47.740' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (18, 18, 0.0000, CAST(N'2020-11-17 21:15:10.003' AS DateTime), CAST(N'2020-11-17 22:23:23.497' AS DateTime))
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (22, 18, 160000.0000, CAST(N'2020-11-17 22:23:23.517' AS DateTime), CAST(N'2020-11-22 01:40:47.063' AS DateTime))
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (23, 19, 500000.0000, CAST(N'2020-11-21 21:45:36.660' AS DateTime), CAST(N'2020-11-30 01:58:49.523' AS DateTime))
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (24, 18, 170000.0000, CAST(N'2020-11-22 01:40:47.080' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (25, 5, 120000.0000, CAST(N'2020-11-22 01:59:57.207' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (26, 5, 120000.0000, CAST(N'2020-11-22 01:59:57.207' AS DateTime), NULL)
INSERT [dbo].[AccessoryPriceHistory] ([Id], [AccessoriesId], [Price], [FromDate], [ToDate]) VALUES (27, 19, 70000.0000, CAST(N'2020-11-30 01:58:49.540' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[AccessoryPriceHistory] OFF
SET IDENTITY_INSERT [dbo].[Account_Roles] ON 

INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (23, 5, 2)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (24, 5, 2)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (25, 5, 2)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (26, 5, 2)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (27, 5, 2)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (28, 5, 2)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (30, 6, 2)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (33, 9, 1)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (52, 26, 1)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (54, 27, 1)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (56, 2, 1)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (61, 30, 2)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (64, 1, 4)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (65, 20, 3)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (66, 31, 2)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (67, 32, 8)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (69, 30, 2)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (70, 33, 1)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (71, 32, 8)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (72, 34, 2)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (73, 35, 2)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (74, 36, 2)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (75, 37, 2)
INSERT [dbo].[Account_Roles] ([Id], [AccountId], [RoleId]) VALUES (76, 38, 2)
SET IDENTITY_INSERT [dbo].[Account_Roles] OFF
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([Id], [EmployeeId], [UserName], [Password], [Token], [ExpiredToken], [Status], [CreatedDate], [UpdatedDate], [StatusActing]) VALUES (1, 1, N'linhvpbv', N'123456', N'Jyu7qGpzdEOLzvnMFT/P9w==', CAST(N'2020-12-16 01:45:32.713' AS DateTime), 1, NULL, CAST(N'2020-10-29 02:16:57.553' AS DateTime), 1)
INSERT [dbo].[Accounts] ([Id], [EmployeeId], [UserName], [Password], [Token], [ExpiredToken], [Status], [CreatedDate], [UpdatedDate], [StatusActing]) VALUES (2, 2, N'hoantb', N'123456', N'cL+gUioZx0ywURR56GkrXA==', CAST(N'2020-12-01 04:03:15.607' AS DateTime), 1, NULL, CAST(N'2020-10-29 02:16:37.590' AS DateTime), 1)
INSERT [dbo].[Accounts] ([Id], [EmployeeId], [UserName], [Password], [Token], [ExpiredToken], [Status], [CreatedDate], [UpdatedDate], [StatusActing]) VALUES (20, 3, N'thuybb', N'123456', N'iUs8eHmTE0aH+4J9ou+zOg==', CAST(N'2020-12-16 01:45:23.320' AS DateTime), 1, NULL, CAST(N'2020-10-29 02:17:05.397' AS DateTime), 1)
INSERT [dbo].[Accounts] ([Id], [EmployeeId], [UserName], [Password], [Token], [ExpiredToken], [Status], [CreatedDate], [UpdatedDate], [StatusActing]) VALUES (30, 12, N'datnt', N'123456', N'XfV7wgceTkWcMPAP1CCRTQ==', CAST(N'2020-11-22 14:00:49.740' AS DateTime), 1, CAST(N'2020-10-29 21:59:59.670' AS DateTime), NULL, 1)
INSERT [dbo].[Accounts] ([Id], [EmployeeId], [UserName], [Password], [Token], [ExpiredToken], [Status], [CreatedDate], [UpdatedDate], [StatusActing]) VALUES (31, 11, N'lamhn', N'123456', N'xPiBtR+7qU2H+1nbkMM/bQ==', CAST(N'2020-11-30 18:11:51.273' AS DateTime), 1, CAST(N'2020-10-30 02:56:48.000' AS DateTime), NULL, 2)
INSERT [dbo].[Accounts] ([Id], [EmployeeId], [UserName], [Password], [Token], [ExpiredToken], [Status], [CreatedDate], [UpdatedDate], [StatusActing]) VALUES (32, 13, N'thuyph', N'123456', N'l8ZWfJ8aYkekAHDSSvitNA==', CAST(N'2020-11-30 18:10:05.830' AS DateTime), 1, CAST(N'2020-10-31 22:23:00.290' AS DateTime), NULL, 1)
INSERT [dbo].[Accounts] ([Id], [EmployeeId], [UserName], [Password], [Token], [ExpiredToken], [Status], [CreatedDate], [UpdatedDate], [StatusActing]) VALUES (34, 16, N'tranhd', N'123456', N'ndNlzEgPx0qu2LMHTKly8g==', CAST(N'2020-11-23 05:37:29.017' AS DateTime), 1, CAST(N'2020-11-21 09:52:20.547' AS DateTime), NULL, 1)
INSERT [dbo].[Accounts] ([Id], [EmployeeId], [UserName], [Password], [Token], [ExpiredToken], [Status], [CreatedDate], [UpdatedDate], [StatusActing]) VALUES (38, 15, N'anhht', N'123456', NULL, NULL, 1, CAST(N'2020-11-22 17:50:52.393' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[Accounts] OFF
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (1, N'Phương Văn Linh', 0, NULL, N'0339032999', N'Ba Đình - Hà Nội', 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (2, N'Nguyễn Thùy Linh', 0, NULL, N'012345678', NULL, 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (3, N'Lê Văn A', 0, NULL, N'0001112222', NULL, 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (4, N'Phương Tiến Nhật', 0, CAST(N'2000-11-12 00:00:00.000' AS DateTime), N'0904331788', NULL, 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (5, N'Nguyễn Hoàng Long', 0, CAST(N'1994-12-01 00:00:00.000' AS DateTime), N'0904331218', NULL, 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (6, N'Hoàng Ngọc Chinh', 0, NULL, NULL, NULL, 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (8, N'Nguyễn Văn Dương', 0, NULL, N'0906879911', NULL, 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (9, N'Nguyễn Hoàng Long', 0, CAST(N'1999-02-22 00:00:00.000' AS DateTime), N'0389115062', N'Hà Nam', 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (10, N'Lê Văn A', 0, CAST(N'2020-11-08 00:00:00.000' AS DateTime), N'0348471010', N'Mê linh, Hà nội', 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (11, N'Nguyễn Hoàng Long', 0, CAST(N'2020-11-08 00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (12, N'Nguyễn Thị Hồng Hạnh', 0, CAST(N'2020-11-08 00:00:00.000' AS DateTime), NULL, N'Mê linh, Hà nội', 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (13, N'Lê Quang Anh', 0, NULL, N'0981234567', NULL, 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (14, N'Đào Tiến Long', 0, NULL, NULL, NULL, 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (15, N'Thắng', 0, NULL, NULL, NULL, 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (16, N'Lê Thị Tươi', 0, NULL, NULL, NULL, 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (17, N'Hoàng Bảo Lộc', 0, NULL, N'09068799122', NULL, 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (18, N'Lê Tuấn Tú', 0, NULL, N'0982936914', NULL, 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (19, N'Hứa Văn Kiên', 0, NULL, N'0988122855', NULL, 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (20, N'Đỗ Thị Hà', 0, NULL, N'0336501878', NULL, 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (21, N'Trịnh Thị Xuân', 0, NULL, N'0906879933', NULL, 1)
INSERT [dbo].[Customers] ([Id], [Name], [Sex], [Birth], [Phone], [Address], [Status]) VALUES (24, N'Nguyễn Ngọc Anh', 0, NULL, N'0901237784', NULL, 1)
SET IDENTITY_INSERT [dbo].[Customers] OFF
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([Id], [JobPositionId], [Name], [Birth], [Sex], [JoinedDate], [IdentityId], [Phone], [Address], [Note]) VALUES (1, 5, N'Phương Văn Linh', CAST(N'1998-01-12 00:00:00.000' AS DateTime), 0, CAST(N'2020-10-01 00:00:00.000' AS DateTime), 339032999, N'0339032999', NULL, N'Đẹp trai')
INSERT [dbo].[Employees] ([Id], [JobPositionId], [Name], [Birth], [Sex], [JoinedDate], [IdentityId], [Phone], [Address], [Note]) VALUES (2, 2, N'Nguyễn Thị Bích Hòa', CAST(N'1998-12-20 00:00:00.000' AS DateTime), 0, CAST(N'2019-01-01 00:00:00.000' AS DateTime), 33044055, N'033044055', NULL, N'Đẹp gái')
INSERT [dbo].[Employees] ([Id], [JobPositionId], [Name], [Birth], [Sex], [JoinedDate], [IdentityId], [Phone], [Address], [Note]) VALUES (3, 3, N'Bùi Bích Thủy', CAST(N'1998-04-01 00:00:00.000' AS DateTime), 0, CAST(N'2020-10-07 00:00:00.000' AS DateTime), 389115072, N'0389115072', N'Hà Nam', N'Test')
INSERT [dbo].[Employees] ([Id], [JobPositionId], [Name], [Birth], [Sex], [JoinedDate], [IdentityId], [Phone], [Address], [Note]) VALUES (11, 1, N'Hoàng Ngọc Lâm', CAST(N'1996-10-13 00:00:00.000' AS DateTime), 0, CAST(N'2020-09-29 00:00:00.000' AS DateTime), 904331728, N'0904331728', N'Đông Anh - Hà Nội', NULL)
INSERT [dbo].[Employees] ([Id], [JobPositionId], [Name], [Birth], [Sex], [JoinedDate], [IdentityId], [Phone], [Address], [Note]) VALUES (12, 1, N'Nguyễn Tiến Đạt', CAST(N'1995-12-04 00:00:00.000' AS DateTime), 0, CAST(N'2020-05-29 00:00:00.000' AS DateTime), 904331728, N'0904331728', NULL, NULL)
INSERT [dbo].[Employees] ([Id], [JobPositionId], [Name], [Birth], [Sex], [JoinedDate], [IdentityId], [Phone], [Address], [Note]) VALUES (13, 4, N'Phạm Hồng Thúy', CAST(N'1997-09-02 00:00:00.000' AS DateTime), 0, CAST(N'2020-10-07 00:00:00.000' AS DateTime), 904331218, N'0904331218', NULL, NULL)
INSERT [dbo].[Employees] ([Id], [JobPositionId], [Name], [Birth], [Sex], [JoinedDate], [IdentityId], [Phone], [Address], [Note]) VALUES (15, 1, N'Hoàng Tuấn Anh', CAST(N'1989-12-22 00:00:00.000' AS DateTime), 0, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Employees] ([Id], [JobPositionId], [Name], [Birth], [Sex], [JoinedDate], [IdentityId], [Phone], [Address], [Note]) VALUES (16, 3, N'Trần Hồng Đức', CAST(N'1997-01-19 00:00:00.000' AS DateTime), 0, CAST(N'2020-11-05 00:00:00.000' AS DateTime), 987654321, N'0987654321', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Employees] OFF
SET IDENTITY_INSERT [dbo].[JobPositions] ON 

INSERT [dbo].[JobPositions] ([Id], [Name], [Status], [Note]) VALUES (1, N'Kỹ thuật viên', 1, N'Kỹ thuật viên')
INSERT [dbo].[JobPositions] ([Id], [Name], [Status], [Note]) VALUES (2, N'Lễ tân', 1, NULL)
INSERT [dbo].[JobPositions] ([Id], [Name], [Status], [Note]) VALUES (3, N'Thu ngân', 1, NULL)
INSERT [dbo].[JobPositions] ([Id], [Name], [Status], [Note]) VALUES (4, N'Nhân viên quản lý phụ tùng', 1, NULL)
INSERT [dbo].[JobPositions] ([Id], [Name], [Status], [Note]) VALUES (5, N'Quản trị viên', 1, N'Quản trị viên')
INSERT [dbo].[JobPositions] ([Id], [Name], [Status], [Note]) VALUES (6, N'hh', 2, N'hh')
INSERT [dbo].[JobPositions] ([Id], [Name], [Status], [Note]) VALUES (7, N'Nhân viên vệ sinh', 2, NULL)
INSERT [dbo].[JobPositions] ([Id], [Name], [Status], [Note]) VALUES (11, N'Nhân viên mua bán', 2, NULL)
SET IDENTITY_INSERT [dbo].[JobPositions] OFF
SET IDENTITY_INSERT [dbo].[Menus] ON 

INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (1, N'Quản lý hệ thống', 0, N'he-thong', NULL, 1, 1, CAST(N'2020-10-14 22:39:39.617' AS DateTime), NULL)
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (2, N'Quản lý nhân viên', 0, N'/nhan-vien/danh-sach', NULL, 2, 1, NULL, CAST(N'2020-10-26 16:02:15.433' AS DateTime))
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (3, N'Quản lý vị trí công việc', 8, N'/vi-tri-cong-viec/danh-sach', NULL, 3, 1, NULL, CAST(N'2020-10-21 21:26:07.840' AS DateTime))
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (5, N'Cập nhật nhóm quyền', 1, N'/nhom-quyen/danh-sach', N'1', 2, 1, CAST(N'2020-10-16 16:51:14.693' AS DateTime), NULL)
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (6, N'Quản lý chức năng', 1, N'/menu/danh-sach', N'1', 2, 1, CAST(N'2020-10-20 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (7, N'Quản lý tài khoản', 1, N'/tai-khoan/danh-sach', N'Quản lý tài khoản', 3, 1, CAST(N'2020-10-20 17:15:30.697' AS DateTime), NULL)
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (8, N'Quản lý danh mục', 0, N'/danh-muc', N'Quản lý danh mục', 2, 1, NULL, CAST(N'2020-10-26 16:01:19.777' AS DateTime))
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (9, N'Quản lý hãng xe', 8, N'/hang-xe/danh-sach', N'Quản lý hãng xe', 1, 1, CAST(N'2020-10-21 00:57:06.213' AS DateTime), NULL)
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (10, N'Quản lý loại xe', 8, N'/loai-xe/danh-sach', N'Quản lý loại xe', 2, 1, NULL, CAST(N'2020-10-21 21:25:56.547' AS DateTime))
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (11, N'Quản lý quầy sửa', 8, N'/quay-sua/danh-sach', N'Quản lý quầy sửa', 4, 1, CAST(N'2020-10-21 21:25:13.933' AS DateTime), NULL)
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (12, N'Quản lý dịch vụ', 8, N'/dich-vu/danh-sach', N'Quản lý dịch vụ', 3, 1, CAST(N'2020-10-21 21:25:41.037' AS DateTime), NULL)
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (13, N'Quản lý nhà cung cấp', 8, N'/nha-cung-cap/danh-sach', N'Quản lý nhà cung cấp', 6, 1, CAST(N'2020-10-21 21:26:53.407' AS DateTime), NULL)
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (14, N'Quản lý khách hàng', 0, N'/khach-hang/danh-sach', N'Quản lý khách hàng', 3, 1, NULL, CAST(N'2020-10-26 16:02:11.797' AS DateTime))
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (16, N'Quản lý phụ tùng', 8, N'/phu-tung/danh-sach', N'Quản lý phụ tùng', 7, 1, CAST(N'2020-10-21 23:56:56.207' AS DateTime), NULL)
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (17, N'Quản lý hóa đơn', 0, N'/hoa-don/danh-sach', N'Quản lý hóa đơn', 6, 1, NULL, CAST(N'2020-11-01 22:40:56.010' AS DateTime))
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (19, N'Khôi phục', 1, N'/khoi-phuc', NULL, 4, 1, CAST(N'2020-10-22 14:51:37.560' AS DateTime), NULL)
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (20, N'Quản lý đơn hàng nhập', 0, N'/don-hang-nhap/danh-sach', NULL, 6, 1, NULL, CAST(N'2020-11-01 00:54:29.747' AS DateTime))
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (21, N'Quản lý BCTK', 0, N'/bao-cao-thong-ke', NULL, 7, 1, CAST(N'2020-10-26 16:03:46.147' AS DateTime), NULL)
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (22, N'BCTK theo Ngày', 21, N'/thong-ke-doanh-thu-ngay/danh-sach', NULL, 1, 1, CAST(N'2020-11-16 03:56:50.760' AS DateTime), NULL)
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (23, N'BCTK theo Tháng', 21, N'/thong-ke-doanh-thu-thang/danh-sach', NULL, 2, 1, CAST(N'2020-11-17 03:39:27.107' AS DateTime), NULL)
INSERT [dbo].[Menus] ([Id], [Name], [ParentId], [Url], [Note], [Order], [Status], [CreatedDate], [UpdatedDate]) VALUES (24, N'BC tồn kho', 21, N'/ton-kho/danh-sach', NULL, 3, 1, NULL, CAST(N'2020-11-25 03:06:06.857' AS DateTime))
SET IDENTITY_INSERT [dbo].[Menus] OFF
SET IDENTITY_INSERT [dbo].[MotorLifts] ON 

INSERT [dbo].[MotorLifts] ([Id], [Name], [Status], [Note], [CreatedDate]) VALUES (1, N'Quầy số 1', 2, N'Quầy số 1', NULL)
INSERT [dbo].[MotorLifts] ([Id], [Name], [Status], [Note], [CreatedDate]) VALUES (2, N'Quầy số 2', 1, N'Quầy số 2', NULL)
INSERT [dbo].[MotorLifts] ([Id], [Name], [Status], [Note], [CreatedDate]) VALUES (3, N'Quầy số 3', 1, N'Quầy số 3', NULL)
INSERT [dbo].[MotorLifts] ([Id], [Name], [Status], [Note], [CreatedDate]) VALUES (4, N'Quầy số 4', 1, N'Quầy số 4', NULL)
INSERT [dbo].[MotorLifts] ([Id], [Name], [Status], [Note], [CreatedDate]) VALUES (9, N'Quầy số 5', 1, NULL, CAST(N'2020-11-01 05:36:37.250' AS DateTime))
INSERT [dbo].[MotorLifts] ([Id], [Name], [Status], [Note], [CreatedDate]) VALUES (10, N'Quầy số 6', 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[MotorLifts] OFF
SET IDENTITY_INSERT [dbo].[MotorManufacture] ON 

INSERT [dbo].[MotorManufacture] ([Id], [Name], [Status]) VALUES (1, N'Honda', 1)
INSERT [dbo].[MotorManufacture] ([Id], [Name], [Status]) VALUES (2, N'Yamaha', 1)
INSERT [dbo].[MotorManufacture] ([Id], [Name], [Status]) VALUES (4, N'Suzuki2', 1)
INSERT [dbo].[MotorManufacture] ([Id], [Name], [Status]) VALUES (5, N'Toyota', 1)
INSERT [dbo].[MotorManufacture] ([Id], [Name], [Status]) VALUES (6, N'Toyota', 1)
SET IDENTITY_INSERT [dbo].[MotorManufacture] OFF
SET IDENTITY_INSERT [dbo].[MotorTypes] ON 

INSERT [dbo].[MotorTypes] ([Id], [MotorManufactureID], [Name], [Status]) VALUES (1, 0, N'AirBlade 2019 ABS', 1)
INSERT [dbo].[MotorTypes] ([Id], [MotorManufactureID], [Name], [Status]) VALUES (2, 1, N'Wave Alpha 2016', 1)
INSERT [dbo].[MotorTypes] ([Id], [MotorManufactureID], [Name], [Status]) VALUES (3, 0, N'Exciter RC 2020', 1)
INSERT [dbo].[MotorTypes] ([Id], [MotorManufactureID], [Name], [Status]) VALUES (4, 0, N'Exciter RC 150 2020', 1)
INSERT [dbo].[MotorTypes] ([Id], [MotorManufactureID], [Name], [Status]) VALUES (12, 0, N'Lead 2021', 1)
INSERT [dbo].[MotorTypes] ([Id], [MotorManufactureID], [Name], [Status]) VALUES (37, 0, N'Wave 2018', 2)
INSERT [dbo].[MotorTypes] ([Id], [MotorManufactureID], [Name], [Status]) VALUES (38, 1, N'Wave Rsx 2016', 1)
INSERT [dbo].[MotorTypes] ([Id], [MotorManufactureID], [Name], [Status]) VALUES (39, 1, N'Lead 2017', 1)
INSERT [dbo].[MotorTypes] ([Id], [MotorManufactureID], [Name], [Status]) VALUES (40, 2, N'Lead 2020', 1)
INSERT [dbo].[MotorTypes] ([Id], [MotorManufactureID], [Name], [Status]) VALUES (41, 1, N'Honda 2020', 1)
INSERT [dbo].[MotorTypes] ([Id], [MotorManufactureID], [Name], [Status]) VALUES (42, 0, N'Lead 2018', 1)
SET IDENTITY_INSERT [dbo].[MotorTypes] OFF
SET IDENTITY_INSERT [dbo].[Permissions] ON 

INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (208, 5, 1, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (209, 5, 5, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (210, 5, 6, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (211, 5, 7, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (212, 5, 2, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (214, 5, 8, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (215, 5, 3, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (216, 5, 9, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (217, 5, 10, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (218, 5, 11, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (219, 5, 12, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (220, 5, 13, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (221, 5, 16, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (222, 5, 14, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (224, 5, 17, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (225, 5, 17, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (226, 5, 17, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (230, 6, 1, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (231, 6, 5, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (232, 6, 6, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (233, 6, 7, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (234, 6, 19, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (235, 6, 2, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (236, 6, 8, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (237, 6, 3, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (238, 6, 9, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (239, 6, 10, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (240, 6, 11, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (241, 6, 12, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (242, 6, 13, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (243, 6, 16, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (244, 6, 14, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (245, 6, 17, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (247, 6, 20, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (248, 6, 20, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (249, 6, 20, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (250, 6, 21, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (251, 7, 1, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (252, 7, 5, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (253, 7, 6, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (254, 7, 7, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (255, 7, 19, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (256, 7, 2, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (257, 7, 8, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (258, 7, 3, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (259, 7, 9, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (260, 7, 10, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (261, 7, 11, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (262, 7, 12, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (263, 7, 13, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (264, 7, 16, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (265, 7, 14, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (266, 7, 17, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (268, 7, 20, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (269, 7, 21, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (270, 7, 21, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (271, 7, 21, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (778, 2, 1, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (779, 2, 5, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (780, 2, 6, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (781, 2, 7, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (782, 2, 19, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (783, 2, 2, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (784, 2, 8, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (785, 2, 3, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (786, 2, 9, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (787, 2, 10, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (788, 2, 11, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (789, 2, 12, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (790, 2, 13, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (791, 2, 16, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (792, 2, 14, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (793, 2, 17, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (796, 2, 20, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (797, 2, 21, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (873, 1, 1, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (874, 1, 5, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (875, 1, 6, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (876, 1, 7, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (877, 1, 19, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (878, 1, 2, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (879, 1, 8, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (880, 1, 3, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (881, 1, 9, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (882, 1, 10, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (883, 1, 11, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (884, 1, 12, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (885, 1, 13, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (886, 1, 16, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (887, 1, 14, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (888, 1, 14, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (889, 1, 14, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (890, 1, 17, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (891, 1, 17, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (892, 1, 17, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (893, 1, 20, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (894, 1, 21, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (977, 4, 1, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (978, 4, 1, N'Update')
GO
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (979, 4, 1, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (980, 4, 5, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (981, 4, 5, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (982, 4, 5, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (983, 4, 6, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (984, 4, 6, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (985, 4, 6, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (986, 4, 7, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (987, 4, 7, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (988, 4, 7, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (989, 4, 19, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (990, 4, 19, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (991, 4, 19, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (992, 4, 2, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (993, 4, 2, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (994, 4, 2, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (995, 4, 8, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (996, 4, 8, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (997, 4, 8, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (998, 4, 3, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (999, 4, 3, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1000, 4, 3, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1001, 4, 9, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1002, 4, 9, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1003, 4, 9, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1004, 4, 10, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1005, 4, 10, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1006, 4, 10, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1007, 4, 11, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1008, 4, 11, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1009, 4, 11, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1010, 4, 12, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1011, 4, 12, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1012, 4, 12, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1013, 4, 13, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1014, 4, 13, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1015, 4, 13, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1016, 4, 16, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1017, 4, 16, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1018, 4, 16, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1019, 4, 14, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1020, 4, 14, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1021, 4, 14, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1022, 4, 17, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1023, 4, 17, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1024, 4, 17, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1025, 4, 20, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1026, 4, 20, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1027, 4, 20, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1028, 4, 21, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1029, 4, 21, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1030, 4, 21, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1031, 4, 22, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1032, 4, 22, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1033, 4, 22, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1034, 4, 23, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1035, 4, 23, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1036, 4, 23, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1065, 3, 1, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1066, 3, 5, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1067, 3, 6, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1068, 3, 7, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1069, 3, 19, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1070, 3, 2, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1071, 3, 8, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1072, 3, 3, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1073, 3, 9, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1074, 3, 10, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1075, 3, 11, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1076, 3, 12, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1077, 3, 13, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1078, 3, 16, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1079, 3, 14, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1080, 3, 17, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1081, 3, 17, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1082, 3, 20, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1083, 3, 21, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1084, 3, 21, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1085, 3, 21, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1086, 3, 22, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1087, 3, 22, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1088, 3, 22, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1089, 3, 23, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1090, 3, 23, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1091, 3, 23, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1092, 4, 1, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1093, 4, 1, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1094, 4, 1, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1095, 4, 5, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1096, 4, 5, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1097, 4, 5, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1098, 4, 6, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1099, 4, 6, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1100, 4, 6, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1101, 4, 7, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1102, 4, 7, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1103, 4, 7, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1104, 4, 2, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1105, 4, 2, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1106, 4, 2, N'Delete')
GO
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1107, 4, 8, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1108, 4, 8, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1109, 4, 8, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1110, 4, 3, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1111, 4, 3, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1112, 4, 3, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1113, 4, 9, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1114, 4, 9, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1115, 4, 9, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1116, 4, 10, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1117, 4, 10, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1118, 4, 10, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1119, 4, 11, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1120, 4, 11, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1121, 4, 11, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1122, 4, 12, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1123, 4, 12, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1124, 4, 12, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1125, 4, 13, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1126, 4, 13, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1127, 4, 13, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1128, 4, 16, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1129, 4, 16, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1130, 4, 16, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1131, 4, 14, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1132, 4, 14, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1133, 4, 14, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1134, 4, 17, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1135, 4, 17, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1136, 4, 17, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1137, 4, 20, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1138, 4, 20, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1139, 4, 20, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1140, 4, 21, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1141, 4, 21, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1142, 4, 21, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1143, 4, 22, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1144, 4, 22, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1145, 4, 22, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1146, 4, 23, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1147, 4, 23, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1148, 4, 23, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1149, 4, 24, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1150, 4, 24, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1151, 4, 24, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1176, 8, 1, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1177, 8, 5, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1178, 8, 6, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1179, 8, 7, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1180, 8, 19, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1181, 8, 2, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1182, 8, 8, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1183, 8, 3, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1184, 8, 9, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1185, 8, 10, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1186, 8, 11, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1187, 8, 12, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1188, 8, 13, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1189, 8, 16, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1190, 8, 14, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1191, 8, 17, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1192, 8, 20, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1193, 8, 20, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1194, 8, 20, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1195, 8, 21, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1196, 8, 21, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1197, 8, 21, N'Delete')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1198, 8, 22, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1199, 8, 23, N'')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1200, 8, 24, N'Create')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1201, 8, 24, N'Update')
INSERT [dbo].[Permissions] ([Id], [RoleId], [MenuId], [ActionCode]) VALUES (1202, 8, 24, N'Delete')
SET IDENTITY_INSERT [dbo].[Permissions] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name], [Note], [Status]) VALUES (1, N'Lễ tân', N'Quản lý nhân viên', 1)
INSERT [dbo].[Roles] ([Id], [Name], [Note], [Status]) VALUES (2, N'Kỹ thuật viên', N'Kỹ thuật viên', 1)
INSERT [dbo].[Roles] ([Id], [Name], [Note], [Status]) VALUES (3, N'Thu ngân', N'Quản lý danh mục', 1)
INSERT [dbo].[Roles] ([Id], [Name], [Note], [Status]) VALUES (4, N'Admin', N'Admin', 1)
INSERT [dbo].[Roles] ([Id], [Name], [Note], [Status]) VALUES (8, N'Quản lý đơn nhập', NULL, 1)
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[ServicePriceHistory] ON 

INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (1, 1, 120000.0000, CAST(N'2020-11-17 21:05:35.833' AS DateTime), NULL)
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (2, 2, 300000.0000, CAST(N'2020-11-17 21:05:52.207' AS DateTime), NULL)
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (3, 3, 30000.0000, CAST(N'2020-11-17 21:06:43.173' AS DateTime), NULL)
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (4, 4, 80000.0000, CAST(N'2020-11-17 21:07:01.640' AS DateTime), NULL)
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (5, 5, 20000.0000, CAST(N'2020-11-17 21:07:12.267' AS DateTime), NULL)
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (6, 6, 60000.0000, CAST(N'2020-11-17 21:07:28.197' AS DateTime), NULL)
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (7, 7, 40000.0000, CAST(N'2020-11-17 21:07:41.097' AS DateTime), NULL)
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (8, 8, 60000.0000, CAST(N'2020-11-17 21:07:57.213' AS DateTime), NULL)
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (9, 9, 50000.0000, CAST(N'2020-11-17 21:08:06.627' AS DateTime), NULL)
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (10, 10, 60000.0000, CAST(N'2020-11-17 21:08:17.557' AS DateTime), NULL)
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (11, 11, 10000.0000, CAST(N'2020-11-17 21:08:29.053' AS DateTime), NULL)
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (12, 12, 40000.0000, CAST(N'2020-11-17 21:09:06.803' AS DateTime), NULL)
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (13, 13, 20000.0000, CAST(N'2020-11-17 21:09:59.227' AS DateTime), NULL)
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (14, 14, 120000.0000, CAST(N'2020-11-17 21:10:32.130' AS DateTime), NULL)
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (15, 15, 210000.0000, CAST(N'2020-11-17 21:10:41.940' AS DateTime), CAST(N'2020-11-17 22:04:15.627' AS DateTime))
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (16, 15, 215000.0000, CAST(N'2020-11-17 22:04:28.587' AS DateTime), CAST(N'2020-11-22 01:41:29.173' AS DateTime))
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (17, 15, 218000.0000, CAST(N'2020-11-22 01:41:29.187' AS DateTime), NULL)
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (18, 17, 30000.0000, CAST(N'2020-11-30 02:00:03.860' AS DateTime), NULL)
INSERT [dbo].[ServicePriceHistory] ([Id], [ServiceId], [Price], [FromDate], [ToDate]) VALUES (19, 18, 2000.0000, CAST(N'2020-11-30 02:00:48.230' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[ServicePriceHistory] OFF
SET IDENTITY_INSERT [dbo].[Services] ON 

INSERT [dbo].[Services] ([Id], [Name], [Status], [Note], [CreatedDate], [UpdatedDate]) VALUES (3, N'Thay dầu xe số                                    ', 1, NULL, CAST(N'2020-11-17 21:06:43.163' AS DateTime), NULL)
INSERT [dbo].[Services] ([Id], [Name], [Status], [Note], [CreatedDate], [UpdatedDate]) VALUES (4, N'Thay đèn pha trước                                ', 1, NULL, CAST(N'2020-11-17 21:07:01.637' AS DateTime), NULL)
INSERT [dbo].[Services] ([Id], [Name], [Status], [Note], [CreatedDate], [UpdatedDate]) VALUES (5, N'Rửa xe                                            ', 1, NULL, CAST(N'2020-11-17 21:07:12.260' AS DateTime), NULL)
INSERT [dbo].[Services] ([Id], [Name], [Status], [Note], [CreatedDate], [UpdatedDate]) VALUES (6, N'Thay bô xe số                                     ', 1, NULL, CAST(N'2020-11-17 21:07:28.193' AS DateTime), NULL)
INSERT [dbo].[Services] ([Id], [Name], [Status], [Note], [CreatedDate], [UpdatedDate]) VALUES (7, N'Thay accqui                                       ', 1, NULL, CAST(N'2020-11-17 21:07:41.090' AS DateTime), NULL)
INSERT [dbo].[Services] ([Id], [Name], [Status], [Note], [CreatedDate], [UpdatedDate]) VALUES (8, N'Vệ sinh kim phun                                  ', 1, NULL, CAST(N'2020-11-17 21:07:57.210' AS DateTime), NULL)
INSERT [dbo].[Services] ([Id], [Name], [Status], [Note], [CreatedDate], [UpdatedDate]) VALUES (9, N'Quấn dây bơm xăng                                 ', 1, NULL, CAST(N'2020-11-17 21:08:06.623' AS DateTime), NULL)
INSERT [dbo].[Services] ([Id], [Name], [Status], [Note], [CreatedDate], [UpdatedDate]) VALUES (10, N'Vệ sinh buồng đốt                                 ', 1, NULL, CAST(N'2020-11-17 21:08:17.553' AS DateTime), NULL)
INSERT [dbo].[Services] ([Id], [Name], [Status], [Note], [CreatedDate], [UpdatedDate]) VALUES (11, N'Thay má phanh sau                                 ', 1, NULL, CAST(N'2020-11-17 21:08:29.047' AS DateTime), NULL)
INSERT [dbo].[Services] ([Id], [Name], [Status], [Note], [CreatedDate], [UpdatedDate]) VALUES (12, N'Thay nhông xích xe số                             ', 1, NULL, CAST(N'2020-11-17 21:09:06.800' AS DateTime), NULL)
INSERT [dbo].[Services] ([Id], [Name], [Status], [Note], [CreatedDate], [UpdatedDate]) VALUES (13, N'Thay đèn phanh sau                                ', 1, NULL, CAST(N'2020-11-17 21:09:59.227' AS DateTime), NULL)
INSERT [dbo].[Services] ([Id], [Name], [Status], [Note], [CreatedDate], [UpdatedDate]) VALUES (14, N'Bảo dưỡng 1                                       ', 1, NULL, CAST(N'2020-11-17 21:10:32.127' AS DateTime), NULL)
INSERT [dbo].[Services] ([Id], [Name], [Status], [Note], [CreatedDate], [UpdatedDate]) VALUES (15, N'Bảo dưỡng 2                                       ', 1, NULL, NULL, CAST(N'2020-11-22 01:41:29.150' AS DateTime))
INSERT [dbo].[Services] ([Id], [Name], [Status], [Note], [CreatedDate], [UpdatedDate]) VALUES (17, N'Thay phanh                                        ', 1, NULL, CAST(N'2020-11-30 02:00:03.840' AS DateTime), NULL)
INSERT [dbo].[Services] ([Id], [Name], [Status], [Note], [CreatedDate], [UpdatedDate]) VALUES (18, N'Bơm xe                                            ', 1, NULL, CAST(N'2020-11-30 02:00:48.220' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Services] OFF
SET IDENTITY_INSERT [dbo].[Supplier] ON 

INSERT [dbo].[Supplier] ([Id], [Name], [Address], [Phone], [Email], [Website]) VALUES (2, N'Hoàng Lâm1', N'86 - Ngọc Khánh - Ba Đình - Hà Nội', N'0904331788', N'hoanglamsp@gmail.com', NULL)
INSERT [dbo].[Supplier] ([Id], [Name], [Address], [Phone], [Email], [Website]) VALUES (5, N'Phụ tùng Lê Anh', N'121 Đội Cấn ', N'19002881', NULL, NULL)
INSERT [dbo].[Supplier] ([Id], [Name], [Address], [Phone], [Email], [Website]) VALUES (6, N'Phụ tùng Ngọc Bảo', N'182 Liễu Giai', N'033922299', NULL, NULL)
INSERT [dbo].[Supplier] ([Id], [Name], [Address], [Phone], [Email], [Website]) VALUES (7, N'CÔNG TY TNHH CÔNG NGHỆ PHÚC LÂN', N'Số 09A7, KP 11, Phường Tân Phong, TP Biên Hoà, Tỉnh Đồng Nai', N'02513999856', N'plc@phuclan.com', N'http://www.plcgo.vn')
INSERT [dbo].[Supplier] ([Id], [Name], [Address], [Phone], [Email], [Website]) VALUES (8, N'PHỤ TÙNG XE MÁY HOÀNG HƯƠNG', N'Thôn Phú Vinh, X. Phú Nghĩa, H. Chương Mỹ, TP Hà Nội (TPHN)', N'02466826451', N'huongjlv7769@gmail.com', N'http://hoanghuonghh.com')
INSERT [dbo].[Supplier] ([Id], [Name], [Address], [Phone], [Email], [Website]) VALUES (9, N'Phụ tùng Kiều Trang', N' 69/88C Đông Lân , Bà Điểm , Hóc Môn , TPHCM', N'0975 974 978', N'phutungkieutrang@gmail.com', N'https://phutungkieutrang.com')
INSERT [dbo].[Supplier] ([Id], [Name], [Address], [Phone], [Email], [Website]) VALUES (10, N'Huân chương sao vàng', N'kkkkk', N'0906879911', N'linhvpbv@gmail.com', N'fb.com')
INSERT [dbo].[Supplier] ([Id], [Name], [Address], [Phone], [Email], [Website]) VALUES (11, N'Hòa Nguyễn', N'Xóm 2 - Nại Châu - Chu Phan_ Mê Linh Hn', N'0348471010', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Supplier] OFF
SET IDENTITY_INSERT [dbo].[TemporaryBill] ON 

INSERT [dbo].[TemporaryBill] ([Id], [MotorLiftId], [CustomerId], [MotorTypeId], [TimeIn], [TimeOut], [Note], [Status], [CreatedBy], [UpdatedBy], [UpdatedTime], [PrintedBy]) VALUES (1, 1, 1, 1, CAST(N'2020-11-30 06:11:24.000' AS DateTime), NULL, NULL, 1, 2, 31, NULL, 0)
INSERT [dbo].[TemporaryBill] ([Id], [MotorLiftId], [CustomerId], [MotorTypeId], [TimeIn], [TimeOut], [Note], [Status], [CreatedBy], [UpdatedBy], [UpdatedTime], [PrintedBy]) VALUES (2, 1, 2, 40, CAST(N'2020-11-30 16:03:19.000' AS DateTime), NULL, NULL, 1, 2, 31, NULL, 0)
SET IDENTITY_INSERT [dbo].[TemporaryBill] OFF
SET IDENTITY_INSERT [dbo].[TemporaryBill_Accesary] ON 

INSERT [dbo].[TemporaryBill_Accesary] ([TemporaryBillId], [Id], [AccesaryId], [Quantity], [AccesaryPrice]) VALUES (2, 2, 2, 1, 300000.0000)
INSERT [dbo].[TemporaryBill_Accesary] ([TemporaryBillId], [Id], [AccesaryId], [Quantity], [AccesaryPrice]) VALUES (2, 3, 3, 1, 80000.0000)
SET IDENTITY_INSERT [dbo].[TemporaryBill_Accesary] OFF
SET IDENTITY_INSERT [dbo].[TemporaryBill_Service] ON 

INSERT [dbo].[TemporaryBill_Service] ([Id], [TemporaryBillId], [ServiceId], [ServicePrice]) VALUES (2, 2, 3, 30000.0000)
INSERT [dbo].[TemporaryBill_Service] ([Id], [TemporaryBillId], [ServiceId], [ServicePrice]) VALUES (3, 1, 3, 30000.0000)
SET IDENTITY_INSERT [dbo].[TemporaryBill_Service] OFF
USE [master]
GO
ALTER DATABASE [qlsx] SET  READ_WRITE 
GO
