USE [master]
GO
/****** Object:  Database [PersonalAccounts]    Script Date: 8/25/2014 4:28:04 PM ******/
CREATE DATABASE [PersonalAccounts] ON  PRIMARY 
( NAME = N'PersonalAccounts', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\PersonalAccounts.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PersonalAccounts_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\PersonalAccounts_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PersonalAccounts] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PersonalAccounts].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PersonalAccounts] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PersonalAccounts] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PersonalAccounts] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PersonalAccounts] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PersonalAccounts] SET ARITHABORT OFF 
GO
ALTER DATABASE [PersonalAccounts] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PersonalAccounts] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PersonalAccounts] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PersonalAccounts] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PersonalAccounts] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PersonalAccounts] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PersonalAccounts] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PersonalAccounts] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PersonalAccounts] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PersonalAccounts] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PersonalAccounts] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PersonalAccounts] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PersonalAccounts] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PersonalAccounts] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PersonalAccounts] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PersonalAccounts] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PersonalAccounts] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PersonalAccounts] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PersonalAccounts] SET  MULTI_USER 
GO
ALTER DATABASE [PersonalAccounts] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PersonalAccounts] SET DB_CHAINING OFF 
GO
USE [PersonalAccounts]
GO
/****** Object:  UserDefinedFunction [dbo].[ufn_CalculatesSumWithInterest]    Script Date: 8/25/2014 4:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--03. Create a function that accepts as parameters – sum, yearly interest rate and
--number of months. It should calculate and return the new sum. Write a SELECT to
--test whether the function works as expected.

CREATE FUNCTION [dbo].[ufn_CalculatesSumWithInterest](
	@initialSum money,
	@annualIntRate money,
	@months int)
	RETURNS money
AS
BEGIN
	DECLARE @sumWithInterest money;
	SET @sumWithInterest = @initialSum * (@annualIntRate / 100) * (@months / 12.0) + @initialSum;
	RETURN @sumWithInterest;
END

GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 8/25/2014 4:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountId] [int] NOT NULL,
	[Balance] [money] NOT NULL,
	[PersonId] [int] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Logs]    Script Date: 8/25/2014 4:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[OldSum] [money] NOT NULL,
	[NewSum] [money] NOT NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Persons]    Script Date: 8/25/2014 4:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[PersonId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[SSN] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[Accounts] ([AccountId], [Balance], [PersonId]) VALUES (1, 302.5000, 1)
INSERT [dbo].[Accounts] ([AccountId], [Balance], [PersonId]) VALUES (2, 1100.0000, 2)
INSERT [dbo].[Accounts] ([AccountId], [Balance], [PersonId]) VALUES (3, 500.0000, 3)
SET IDENTITY_INSERT [dbo].[Logs] ON 

INSERT [dbo].[Logs] ([LogId], [AccountId], [OldSum], [NewSum]) VALUES (1, 2, 1000.0000, 1100.0000)
SET IDENTITY_INSERT [dbo].[Logs] OFF
INSERT [dbo].[Persons] ([PersonId], [FirstName], [LastName], [SSN]) VALUES (1, N'Georgi', N'Georgiev', N'123-456-789')
INSERT [dbo].[Persons] ([PersonId], [FirstName], [LastName], [SSN]) VALUES (2, N'Marin', N'Marinov', N'234-567-890')
INSERT [dbo].[Persons] ([PersonId], [FirstName], [LastName], [SSN]) VALUES (3, N'Galin', N'Grozev', N'345-678-901')
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Persons] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Persons] ([PersonId])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Persons]
GO
ALTER TABLE [dbo].[Logs]  WITH CHECK ADD  CONSTRAINT [FK_Logs_Accounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([AccountId])
GO
ALTER TABLE [dbo].[Logs] CHECK CONSTRAINT [FK_Logs_Accounts]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddInterestForOneMonth]    Script Date: 8/25/2014 4:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--04. Create a stored procedure that uses the function from the previous example to
--give an interest to a person's account for one month. It should take the AccountId
--and the interest rate as parameters.

CREATE PROC [dbo].[usp_AddInterestForOneMonth](
	@accountId int,
	@annualIntRate money)
AS
	DECLARE @sumWithInterest money,
			@currentPersonBalance money,
			@months int = 1;

	SELECT @currentPersonBalance = a.Balance
	FROM Persons p
		JOIN Accounts a
			ON p.PersonId = a.PersonId
	WHERE p.PersonId = @accountId;

	SET @sumWithInterest = dbo.ufn_CalculatesSumWithInterest(
								@currentPersonBalance,
								 @annualIntRate,
								 @months);

	UPDATE Accounts
	SET Balance = @sumWithInterest
	WHERE AccountId = @accountId;

GO
/****** Object:  StoredProcedure [dbo].[usp_DepositMoneyToAccount]    Script Date: 8/25/2014 4:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--03. Create a function that accepts as parameters – sum, yearly interest rate and
--number of months. It should calculate and return the new sum. Write a SELECT to
--test whether the function works as expected.
/*
CREATE FUNCTION ufn_CalculatesSumWithInterest(
	@initialSum money,
	@annualIntRate money,
	@months int)
	RETURNS money
AS
BEGIN
	DECLARE @sumWithInterest money;
	SET @sumWithInterest = @initialSum * (@annualIntRate / 100) * (@months / 12.0) + @initialSum;
	RETURN @sumWithInterest;
END
GO

SELECT dbo.ufn_CalculatesSumWithInterest(10000, 10, 12) AS [Sum With Interest];
SELECT dbo.ufn_CalculatesSumWithInterest(10000, 10, 6) AS [Sum With Interest];
SELECT dbo.ufn_CalculatesSumWithInterest(10000, 10, 3) AS [Sum With Interest];
GO

--04. Create a stored procedure that uses the function from the previous example to
--give an interest to a person's account for one month. It should take the AccountId
--and the interest rate as parameters.

CREATE PROC usp_AddInterestForOneMonth(
	@accountId int,
	@annualIntRate money)
AS
	DECLARE @sumWithInterest money,
			@currentPersonBalance money,
			@months int = 1;

	SELECT @currentPersonBalance = a.Balance
	FROM Persons p
		JOIN Accounts a
			ON p.PersonId = a.PersonId
	WHERE p.PersonId = @accountId;

	SET @sumWithInterest = dbo.ufn_CalculatesSumWithInterest(
								@currentPersonBalance,
								 @annualIntRate,
								 @months);

	UPDATE Accounts
	SET Balance = @sumWithInterest
	WHERE AccountId = @accountId;
GO

DECLARE @personId int = 1,
		@annualIntRate money = 10;

EXEC usp_AddInterestForOneMonth @personId, @annualIntRate;

SELECT *
	FROM Accounts
	WHERE AccountId = @personId;
GO
*/
--05. Add two more stored procedures WithdrawMoney( AccountId, money) and DepositMoney
--(AccountId, money) that operate in transactions.

--CREATE PROC usp_WithdrawMoneyFromAccount(
--			@accountId int,
--			@amount money)
--AS
--	UPDATE Accounts
--	SET Balance = Balance - @amount
--	WHERE AccountId = @accountId;
--GO

CREATE PROC [dbo].[usp_DepositMoneyToAccount](
			@accountId int,
			@amount money)
AS
	UPDATE Accounts
	SET Balance = Balance + @amount
	WHERE AccountId = @accountId;

GO
/****** Object:  StoredProcedure [dbo].[usp_SelectFullNameOfAllPersons]    Script Date: 8/25/2014 4:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--01. Create a database with two tables: Persons(Id(PK), FirstName, LastName, SSN)
--and Accounts(Id(PK), PersonId(FK), Balance). Insert few records for testing. Write
--a stored procedure that selects the full names of all persons.

CREATE PROC [dbo].[usp_SelectFullNameOfAllPersons]
AS
	SELECT FirstName + ' ' + LastName AS [Full Name]
		FROM Persons;

GO
/****** Object:  StoredProcedure [dbo].[usp_SelectPersonsWithHigherBalance]    Script Date: 8/25/2014 4:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--02. Create a stored procedure that accepts a number as a parameter and returns all
--persons who have more money in their accounts than the supplied number.

CREATE PROC [dbo].[usp_SelectPersonsWithHigherBalance](
	@balance money = 0)
AS
	SELECT *
	FROM Persons p
		JOIN Accounts a
			ON p.PersonId = a.PersonId
	WHERE a.Balance > @balance;

GO
/****** Object:  StoredProcedure [dbo].[usp_WithdrawMoneyFromAccount]    Script Date: 8/25/2014 4:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--03. Create a function that accepts as parameters – sum, yearly interest rate and
--number of months. It should calculate and return the new sum. Write a SELECT to
--test whether the function works as expected.
/*
CREATE FUNCTION ufn_CalculatesSumWithInterest(
	@initialSum money,
	@annualIntRate money,
	@months int)
	RETURNS money
AS
BEGIN
	DECLARE @sumWithInterest money;
	SET @sumWithInterest = @initialSum * (@annualIntRate / 100) * (@months / 12.0) + @initialSum;
	RETURN @sumWithInterest;
END
GO

SELECT dbo.ufn_CalculatesSumWithInterest(10000, 10, 12) AS [Sum With Interest];
SELECT dbo.ufn_CalculatesSumWithInterest(10000, 10, 6) AS [Sum With Interest];
SELECT dbo.ufn_CalculatesSumWithInterest(10000, 10, 3) AS [Sum With Interest];
GO

--04. Create a stored procedure that uses the function from the previous example to
--give an interest to a person's account for one month. It should take the AccountId
--and the interest rate as parameters.

CREATE PROC usp_AddInterestForOneMonth(
	@accountId int,
	@annualIntRate money)
AS
	DECLARE @sumWithInterest money,
			@currentPersonBalance money,
			@months int = 1;

	SELECT @currentPersonBalance = a.Balance
	FROM Persons p
		JOIN Accounts a
			ON p.PersonId = a.PersonId
	WHERE p.PersonId = @accountId;

	SET @sumWithInterest = dbo.ufn_CalculatesSumWithInterest(
								@currentPersonBalance,
								 @annualIntRate,
								 @months);

	UPDATE Accounts
	SET Balance = @sumWithInterest
	WHERE AccountId = @accountId;
GO

DECLARE @personId int = 1,
		@annualIntRate money = 10;

EXEC usp_AddInterestForOneMonth @personId, @annualIntRate;

SELECT *
	FROM Accounts
	WHERE AccountId = @personId;
GO
*/
--05. Add two more stored procedures WithdrawMoney( AccountId, money) and DepositMoney
--(AccountId, money) that operate in transactions.

CREATE PROC [dbo].[usp_WithdrawMoneyFromAccount](
			@accountId int,
			@amount money)
AS
	UPDATE Accounts
	SET Balance = Balance - @amount
	WHERE AccountId = @accountId;

GO
USE [master]
GO
ALTER DATABASE [PersonalAccounts] SET  READ_WRITE 
GO
