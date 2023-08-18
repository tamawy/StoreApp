USE [master]
GO
/****** Object:  Database [StoreDB]    Script Date: 2023-08-18 6:53:27 PM ******/
CREATE DATABASE [StoreDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StoreDB', FILENAME = N'F:\Task\Store\DB\StoreDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'StoreDB_log', FILENAME = N'F:\Task\Store\DB\StoreDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [StoreDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StoreDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StoreDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StoreDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StoreDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StoreDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StoreDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [StoreDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [StoreDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StoreDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StoreDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StoreDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StoreDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StoreDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StoreDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StoreDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StoreDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [StoreDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StoreDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StoreDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StoreDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StoreDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StoreDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StoreDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StoreDB] SET RECOVERY FULL 
GO
ALTER DATABASE [StoreDB] SET  MULTI_USER 
GO
ALTER DATABASE [StoreDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StoreDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StoreDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StoreDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [StoreDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [StoreDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'StoreDB', N'ON'
GO
ALTER DATABASE [StoreDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [StoreDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [StoreDB]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2023-08-18 6:53:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[count] [bigint] NOT NULL,
	[SpaceFK] [bigint] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Spaces]    Script Date: 2023-08-18 6:53:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spaces](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[StoreFK] [bigint] NOT NULL,
 CONSTRAINT [PK_Space] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stores]    Script Date: 2023-08-18 6:53:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stores](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[IsMain] [bit] NOT NULL,
	[IsInvoiceDirect] [bit] NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [count], [SpaceFK]) VALUES (11, N'PC', 5, 49)
INSERT [dbo].[Products] ([Id], [Name], [count], [SpaceFK]) VALUES (12, N'Camera', 10, 49)
INSERT [dbo].[Products] ([Id], [Name], [count], [SpaceFK]) VALUES (13, N'Laptop', 2, 49)
INSERT [dbo].[Products] ([Id], [Name], [count], [SpaceFK]) VALUES (14, N'Iphone', 100, 49)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Spaces] ON 

INSERT [dbo].[Spaces] ([Id], [Name], [StoreFK]) VALUES (49, N'Default Space', 21)
INSERT [dbo].[Spaces] ([Id], [Name], [StoreFK]) VALUES (50, N'Default Space', 22)
INSERT [dbo].[Spaces] ([Id], [Name], [StoreFK]) VALUES (52, N'Default Space', 24)
INSERT [dbo].[Spaces] ([Id], [Name], [StoreFK]) VALUES (53, N'First Space', 21)
INSERT [dbo].[Spaces] ([Id], [Name], [StoreFK]) VALUES (54, N'Second Space', 21)
SET IDENTITY_INSERT [dbo].[Spaces] OFF
GO
SET IDENTITY_INSERT [dbo].[Stores] ON 

INSERT [dbo].[Stores] ([Id], [Name], [IsMain], [IsInvoiceDirect], [Address]) VALUES (21, N'First Store', 1, 0, N'Alex, Egypt')
INSERT [dbo].[Stores] ([Id], [Name], [IsMain], [IsInvoiceDirect], [Address]) VALUES (22, N'Second Store', 1, 1, N'Cairo, Egypt')
INSERT [dbo].[Stores] ([Id], [Name], [IsMain], [IsInvoiceDirect], [Address]) VALUES (24, N'Third Store', 0, 0, N'Mansoura, Egypt')
SET IDENTITY_INSERT [dbo].[Stores] OFF
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Spaces] FOREIGN KEY([SpaceFK])
REFERENCES [dbo].[Spaces] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Spaces]
GO
ALTER TABLE [dbo].[Spaces]  WITH CHECK ADD  CONSTRAINT [FK_Spaces_Stores] FOREIGN KEY([StoreFK])
REFERENCES [dbo].[Stores] ([Id])
GO
ALTER TABLE [dbo].[Spaces] CHECK CONSTRAINT [FK_Spaces_Stores]
GO
USE [master]
GO
ALTER DATABASE [StoreDB] SET  READ_WRITE 
GO
