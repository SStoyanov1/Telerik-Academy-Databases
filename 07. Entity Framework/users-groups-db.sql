USE [master]
GO
/****** Object:  Database [UsersGroups]    Script Date: 8/29/2014 4:38:15 PM ******/
CREATE DATABASE [UsersGroups] ON  PRIMARY 
( NAME = N'UsersGroups', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\UsersGroups.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'UsersGroups_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\UsersGroups_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [UsersGroups] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UsersGroups].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UsersGroups] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UsersGroups] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UsersGroups] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UsersGroups] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UsersGroups] SET ARITHABORT OFF 
GO
ALTER DATABASE [UsersGroups] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [UsersGroups] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UsersGroups] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UsersGroups] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UsersGroups] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UsersGroups] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UsersGroups] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UsersGroups] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UsersGroups] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UsersGroups] SET  DISABLE_BROKER 
GO
ALTER DATABASE [UsersGroups] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UsersGroups] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UsersGroups] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UsersGroups] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UsersGroups] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UsersGroups] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [UsersGroups] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UsersGroups] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [UsersGroups] SET  MULTI_USER 
GO
ALTER DATABASE [UsersGroups] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UsersGroups] SET DB_CHAINING OFF 
GO
USE [UsersGroups]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 8/29/2014 4:38:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[GroupId] [int] NOT NULL,
	[GroupName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/29/2014 4:38:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([GroupId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Groups]
GO
USE [master]
GO
ALTER DATABASE [UsersGroups] SET  READ_WRITE 
GO
