USE [master]
GO
/****** Object:  Database [University]    Script Date: 8/21/2014 9:40:46 AM ******/
CREATE DATABASE [University] ON  PRIMARY 
( NAME = N'University', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\University.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'University_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\University_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [University] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [University].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [University] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [University] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [University] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [University] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [University] SET ARITHABORT OFF 
GO
ALTER DATABASE [University] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [University] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [University] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [University] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [University] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [University] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [University] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [University] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [University] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [University] SET  DISABLE_BROKER 
GO
ALTER DATABASE [University] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [University] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [University] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [University] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [University] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [University] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [University] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [University] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [University] SET  MULTI_USER 
GO
ALTER DATABASE [University] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [University] SET DB_CHAINING OFF 
GO
USE [University]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 8/21/2014 9:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DepartmentId] [int] NOT NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Courses_Professors]    Script Date: 8/21/2014 9:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses_Professors](
	[CourseId] [int] NOT NULL,
	[ProfessorId] [int] NOT NULL,
 CONSTRAINT [PK_Courses_Professors] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC,
	[ProfessorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Department]    Script Date: 8/21/2014 9:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[FacultyId] [int] NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Faculty]    Script Date: 8/21/2014 9:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faculty](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[UniversityId] [int] NOT NULL,
 CONSTRAINT [PK_Faculty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Professor]    Script Date: 8/21/2014 9:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Professor](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CourseId] [int] NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[TitleId] [int] NOT NULL,
 CONSTRAINT [PK_Professor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Professors_Titles]    Script Date: 8/21/2014 9:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Professors_Titles](
	[TitleId] [int] NOT NULL,
	[ProfessorId] [int] NOT NULL,
 CONSTRAINT [PK_Professors_Titles] PRIMARY KEY CLUSTERED 
(
	[TitleId] ASC,
	[ProfessorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Student]    Script Date: 8/21/2014 9:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Students_Courses]    Script Date: 8/21/2014 9:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students_Courses](
	[CourseId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
 CONSTRAINT [PK_Students_Courses] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC,
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Titles]    Script Date: 8/21/2014 9:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Titles](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Titles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[University]    Script Date: 8/21/2014 9:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[University](
	[Id] [int] NOT NULL,
	[Name] [nchar](10) NOT NULL,
 CONSTRAINT [PK_University] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Department]
GO
ALTER TABLE [dbo].[Courses_Professors]  WITH CHECK ADD  CONSTRAINT [FK_Courses_Professors_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
GO
ALTER TABLE [dbo].[Courses_Professors] CHECK CONSTRAINT [FK_Courses_Professors_Course]
GO
ALTER TABLE [dbo].[Courses_Professors]  WITH CHECK ADD  CONSTRAINT [FK_Courses_Professors_Professor] FOREIGN KEY([ProfessorId])
REFERENCES [dbo].[Professor] ([Id])
GO
ALTER TABLE [dbo].[Courses_Professors] CHECK CONSTRAINT [FK_Courses_Professors_Professor]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Faculty] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([Id])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Faculty]
GO
ALTER TABLE [dbo].[Faculty]  WITH CHECK ADD  CONSTRAINT [FK_Faculty_University] FOREIGN KEY([UniversityId])
REFERENCES [dbo].[University] ([Id])
GO
ALTER TABLE [dbo].[Faculty] CHECK CONSTRAINT [FK_Faculty_University]
GO
ALTER TABLE [dbo].[Professor]  WITH CHECK ADD  CONSTRAINT [FK_Professor_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Professor] CHECK CONSTRAINT [FK_Professor_Department]
GO
ALTER TABLE [dbo].[Professors_Titles]  WITH CHECK ADD  CONSTRAINT [FK_Professors_Titles_Professor] FOREIGN KEY([ProfessorId])
REFERENCES [dbo].[Professor] ([Id])
GO
ALTER TABLE [dbo].[Professors_Titles] CHECK CONSTRAINT [FK_Professors_Titles_Professor]
GO
ALTER TABLE [dbo].[Professors_Titles]  WITH CHECK ADD  CONSTRAINT [FK_Professors_Titles_Titles] FOREIGN KEY([TitleId])
REFERENCES [dbo].[Titles] ([Id])
GO
ALTER TABLE [dbo].[Professors_Titles] CHECK CONSTRAINT [FK_Professors_Titles_Titles]
GO
ALTER TABLE [dbo].[Students_Courses]  WITH CHECK ADD  CONSTRAINT [FK_Students_Courses_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
GO
ALTER TABLE [dbo].[Students_Courses] CHECK CONSTRAINT [FK_Students_Courses_Course]
GO
ALTER TABLE [dbo].[Students_Courses]  WITH CHECK ADD  CONSTRAINT [FK_Students_Courses_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[Students_Courses] CHECK CONSTRAINT [FK_Students_Courses_Student]
GO
USE [master]
GO
ALTER DATABASE [University] SET  READ_WRITE 
GO
