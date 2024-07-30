USE [master]
GO
/****** Object:  Database [PRN221_Project]    Script Date: 7/30/2024 7:52:40 AM ******/
CREATE DATABASE [PRN221_Project]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PRN221_Project', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PRN221_Project.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PRN221_Project_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PRN221_Project_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PRN221_Project] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PRN221_Project].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PRN221_Project] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PRN221_Project] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PRN221_Project] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PRN221_Project] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PRN221_Project] SET ARITHABORT OFF 
GO
ALTER DATABASE [PRN221_Project] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PRN221_Project] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PRN221_Project] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PRN221_Project] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PRN221_Project] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PRN221_Project] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PRN221_Project] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PRN221_Project] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PRN221_Project] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PRN221_Project] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PRN221_Project] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PRN221_Project] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PRN221_Project] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PRN221_Project] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PRN221_Project] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PRN221_Project] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PRN221_Project] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PRN221_Project] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PRN221_Project] SET  MULTI_USER 
GO
ALTER DATABASE [PRN221_Project] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PRN221_Project] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PRN221_Project] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PRN221_Project] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PRN221_Project] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PRN221_Project] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PRN221_Project] SET QUERY_STORE = OFF
GO
USE [PRN221_Project]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 7/30/2024 7:52:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[showtime_id] [int] NULL,
	[seat_id] [int] NULL,
	[customer_name] [varchar](255) NULL,
	[customer_phone] [varchar](20) NULL,
	[booking_date] [datetime] NULL,
	[paymentID] [int] NULL,
	[booking_id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[booking_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 7/30/2024 7:52:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[customer_id] [int] IDENTITY(1,1) NOT NULL,
	[last_name] [varchar](50) NULL,
	[first_name] [varchar](50) NULL,
	[middle_name] [varchar](50) NULL,
	[email] [varchar](100) NULL,
	[phone] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[genre]    Script Date: 7/30/2024 7:52:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[genre](
	[genre_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nchar](20) NOT NULL,
 CONSTRAINT [PK_genre] PRIMARY KEY CLUSTERED 
(
	[genre_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movie]    Script Date: 7/30/2024 7:52:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movie](
	[movie_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](255) NOT NULL,
	[release_date] [date] NOT NULL,
	[duration] [int] NOT NULL,
	[story_line] [text] NULL,
	[poster] [varchar](255) NULL,
	[director_name] [varchar](50) NULL,
	[rating] [varchar](10) NOT NULL,
	[Time] [varchar](5) NULL,
	[Trailer] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[movie_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[movie_genre]    Script Date: 7/30/2024 7:52:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[movie_genre](
	[movie_id] [int] NOT NULL,
	[genre_id] [int] NOT NULL,
 CONSTRAINT [PK_movie_genre] PRIMARY KEY CLUSTERED 
(
	[movie_id] ASC,
	[genre_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 7/30/2024 7:52:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[payment_id] [int] IDENTITY(1,1) NOT NULL,
	[total_amount] [decimal](10, 2) NULL,
	[payment_method] [varchar](50) NULL,
	[payment_date] [datetime] NULL,
	[userid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[payment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 7/30/2024 7:52:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seat]    Script Date: 7/30/2024 7:52:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seat](
	[seat_id] [int] IDENTITY(1,1) NOT NULL,
	[theater_id] [int] NOT NULL,
	[row_num] [varchar](2) NULL,
	[seat_num] [int] NOT NULL,
	[available] [bit] NULL,
	[price] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[seat_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Showtime]    Script Date: 7/30/2024 7:52:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Showtime](
	[showtime_id] [int] IDENTITY(1,1) NOT NULL,
	[movie_id] [int] NOT NULL,
	[theater_id] [int] NOT NULL,
	[start_time] [datetime] NOT NULL,
	[end_time] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[showtime_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Theater]    Script Date: 7/30/2024 7:52:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Theater](
	[theatre_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[location] [varchar](255) NOT NULL,
	[capacity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[theatre_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/30/2024 7:52:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[last_name] [varchar](50) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[password] [varchar](255) NOT NULL,
	[phone] [varchar](20) NOT NULL,
	[address] [varchar](255) NOT NULL,
	[registration_date] [datetime] NOT NULL,
	[RoleID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Seat] ADD  DEFAULT ((1)) FOR [available]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [registration_date]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([seat_id])
REFERENCES [dbo].[Seat] ([seat_id])
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([showtime_id])
REFERENCES [dbo].[Showtime] ([showtime_id])
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Payment] FOREIGN KEY([paymentID])
REFERENCES [dbo].[Payment] ([payment_id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Payment]
GO
ALTER TABLE [dbo].[movie_genre]  WITH CHECK ADD  CONSTRAINT [FK_movie_genre_genre] FOREIGN KEY([movie_id])
REFERENCES [dbo].[genre] ([genre_id])
GO
ALTER TABLE [dbo].[movie_genre] CHECK CONSTRAINT [FK_movie_genre_genre]
GO
ALTER TABLE [dbo].[movie_genre]  WITH CHECK ADD  CONSTRAINT [FK_movie_genre_Movie] FOREIGN KEY([movie_id])
REFERENCES [dbo].[Movie] ([movie_id])
GO
ALTER TABLE [dbo].[movie_genre] CHECK CONSTRAINT [FK_movie_genre_Movie]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_User] FOREIGN KEY([userid])
REFERENCES [dbo].[User] ([user_id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_User]
GO
ALTER TABLE [dbo].[Seat]  WITH CHECK ADD FOREIGN KEY([theater_id])
REFERENCES [dbo].[Theater] ([theatre_id])
GO
ALTER TABLE [dbo].[Showtime]  WITH CHECK ADD FOREIGN KEY([movie_id])
REFERENCES [dbo].[Movie] ([movie_id])
GO
ALTER TABLE [dbo].[Showtime]  WITH CHECK ADD FOREIGN KEY([theater_id])
REFERENCES [dbo].[Theater] ([theatre_id])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [PRN221_Project] SET  READ_WRITE 
GO
