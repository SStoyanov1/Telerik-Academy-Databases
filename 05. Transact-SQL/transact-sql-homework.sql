USE PersonalAccounts
GO

--01. Create a database with two tables: Persons(Id(PK), FirstName, LastName, SSN)
--and Accounts(Id(PK), PersonId(FK), Balance). Insert few records for testing. Write
--a stored procedure that selects the full names of all persons.

--CREATE PROC usp_SelectFullNameOfAllPersons
--AS
--	SELECT FirstName + ' ' + LastName AS [Full Name]
--		FROM Persons;
--GO

EXEC usp_SelectFullNameOfAllPersons;
GO
--02. Create a stored procedure that accepts a number as a parameter and returns all
--persons who have more money in their accounts than the supplied number.

--CREATE PROC usp_SelectPersonsWithHigherBalance(
--	@balance money = 0)
--AS
--	SELECT *
--	FROM Persons p
--		JOIN Accounts a
--			ON p.PersonId = a.PersonId
--	WHERE a.Balance > @balance;
--GO

EXEC usp_SelectPersonsWithHigherBalance 400
GO

--03. Create a function that accepts as parameters – sum, yearly interest rate and
--number of months. It should calculate and return the new sum. Write a SELECT to
--test whether the function works as expected.

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

--05. Add two more stored procedures WithdrawMoney( AccountId, money) and DepositMoney
--(AccountId, money) that operate in transactions.

CREATE PROC usp_WithdrawMoneyFromAccount(
			@accountId int,
			@amount money)
AS
	UPDATE Accounts
	SET Balance = Balance - @amount
	WHERE AccountId = @accountId;
GO

CREATE PROC usp_DepositMoneyToAccount(
			@accountId int,
			@amount money)
AS
	UPDATE Accounts
	SET Balance = Balance + @amount
	WHERE AccountId = @accountId;
GO

DECLARE @personId int = 1,
		@amount money = 50;

EXEC usp_WithdrawMoneyFromAccount @personId, @amount;
GO

DECLARE @personId int = 1,
		@amount money = 100;

EXEC usp_DepositMoneyToAccount @personId, @amount;

SELECT *
	FROM Accounts
	WHERE AccountId = @personId;
GO

--06. Create another table – Logs(LogID, AccountID, OldSum, NewSum). Add a trigger to
--the Accounts table that enters a new entry into the Logs table every time the sum on
--an account changes.

CREATE TRIGGER tr_BalanceLog ON Accounts FOR UPDATE
AS
	INSERT INTO Logs(AccountId, OldSum, NewSum)
	SELECT d.AccountId, d.Balance, i.Balance
	FROM deleted AS d
		JOIN inserted AS i
		ON d.AccountId = i.AccountId;
GO

DECLARE @personId int = 2,
		@amount money = 100;

EXEC usp_DepositMoneyToAccount @personId, @amount;

SELECT * FROM Logs;
GO
--07. Define a function in the database TelerikAcademy that returns all Employee's names
--(first or middle or last name) and all town's names that are comprised of given set of
--letters. Example 'oistmiahf' will return 'Sofia', 'Smith', … but not 'Rob' and 'Guy'.

USE TelerikAcademy;
GO
CREATE FUNCTION usp_IsComposed(
				@name nvarchar(50),
				@characters nvarchar(50))
	RETURNS bit
AS
BEGIN
	DECLARE @index int = 1,
			@foundIndex int,
			@currentCharacter nvarchar(1),
			@counter int,
			@result bit;
	DECLARE @usedLetters table(LetterIndex int, Letter nvarchar(1));
	SET @characters = LOWER(@characters);
		WHILE(@index <= LEN(@name))
		BEGIN
			SET @currentCharacter = LOWER(SUBSTRING(@name, @index, 1));
			SET @foundIndex = CHARINDEX(@currentCharacter, @characters);
			IF (@foundIndex = 0)
			BEGIN
				SET @result = 0;
				BREAK;
			END
			ELSE
			BEGIN
				IF(EXISTS(SELECT * FROM @usedLetters WHERE LetterIndex = @foundIndex))
				BEGIN
					SELECT TOP 1 @foundIndex = LetterIndex
					FROM @usedLetters
					WHERE Letter = @currentCharacter
					ORDER BY Letter DESC;
					SET @foundIndex = CHARINDEX(@currentCharacter, @characters, @foundIndex + 1);
						IF (@foundIndex = 0)
						BEGIN
							SET @result = 0;
						BREAK;
					END
				END
			INSERT INTO @usedLetters
			VALUES (@foundIndex, @currentCharacter);
		END
		SET @index = @index + 1;
	END
	SELECT @counter = COUNT(*) FROM @usedLetters;
	IF(@counter = LEN(@name))
	BEGIN
		SET @result = 1;
	END
	ELSE
	BEGIN
		SET @result = 0;
	END
RETURN @result;
END
GO

USE TelerikAcademy;
GO

CREATE FUNCTION ufn_GetComposedNames (@characters nvarchar(50))
RETURNS TABLE
	AS
	RETURN (
	(SELECT 'First Name: ' + e.FirstName AS Name
	FROM Employees as e
		WHERE 1 = (SELECT dbo.usp_IsComposed(e.FirstName, @characters)))
	UNION
	(SELECT 'Middle Name: ' + e.MiddleName AS Name
		FROM Employees As e
	WHERE 1 = (SELECT dbo.usp_IsComposed(e.MiddleName, @characters)))
	UNION
	(SELECT 'Last Name: ' + e.LastName AS Name
		FROM Employees AS e
	WHERE 1 = (SELECT dbo.usp_IsComposed(e.LastName, @characters)))
	UNION
	(SELECT 'Town Name: ' + t.Name AS Name
		FROM Towns AS t
	WHERE 1 = (SELECT dbo.usp_IsComposed(t.Name, @characters)))
);
GO

SELECT *
FROM dbo.ufn_GetComposedNames('oistmiahf');
GO
SELECT *
FROM dbo.ufn_GetComposedNames('RoBERto');
GO

SELECT *
FROM dbo.ufn_GetComposedNames('Kharatishvili');
GO

--08. Using database cursor write a T-SQL script that scans all employees and their addresses
--and prints all pairs of employees that live in the same town.

USE TelerikAcademy;

DECLARE lineCursor CURSOR READ_ONLY FOR
	SELECT e.FirstName, e.LastName, t1.Name,
	m.FirstName, m.LastName
	FROM Employees e
			INNER JOIN Addresses a1
			ON a1.AddressID = e.AddressID
			INNER JOIN Towns t1
			ON t1.TownID = a1.TownID,
		Employees m
			INNER JOIN Addresses a2
			ON a2.AddressID = m.AddressID
			INNER JOIN Towns t2
			ON t2.TownID = a2.TownID 
		WHERE t1.TownID = t2.TownID AND e.EmployeeID <> m.EmployeeID
		ORDER BY t1.Name, e.FirstName, m.FirstName;

OPEN lineCursor
DECLARE @firstName1 nvarchar(50),
		@lastName1 nvarchar(50),
		@town nvarchar(50),
		@firstName2 nvarchar(50),
		@lastName2 nvarchar(50);
DECLARE @resultTable table(
			FirstEmployee nvarchar(100),
			Town nvarchar(500),
			SecondEmployee nvarchar(100));
FETCH NEXT FROM lineCursor
INTO @firstName1, @lastName1, @town, @firstName2, @lastName2
WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO @resultTable
	VALUES (@firstName1 + ' ' + @lastName1, @town, @firstName2 + ' ' + @lastName2);
	FETCH NEXT FROM lineCursor INTO @firstName1, @lastName1, @town, @firstName2, @lastName2
END
CLOSE lineCursor

DEALLOCATE lineCursor
SELECT * FROM @resultTable;
GO

--10. Define a .NET aggregate function StrConcat that takes as input a sequence of strings
--and return a single string that consists of the input strings separated by ','. For example
--the following SQL statement should return a single string:
--SELECT StrConcat(FirstName + ' ' + LastName)
--FROM Employees

USE [TelerikAcademy]
GO

IF OBJECT_ID('dbo.StrConcat') IS NOT NULL DROP Aggregate StrConcat 
GO 

IF EXISTS (SELECT * FROM sys.assemblies WHERE name = 'concat_assembly') 
       DROP assembly concat_assembly; 
GO      

DECLARE @path nvarchar(256)
-- You must change this path to the folder where the .dll with the CLR function is.
SET @path = 'C:\Users\Stoyan\Documents\Visual Studio 2013\Projects\Homeworks Databases\05. Transact-SQL'

CREATE Assembly concat_assembly 
   AUTHORIZATION dbo 
   FROM @path+'\StrConcat.dll' 
   WITH PERMISSION_SET = SAFE; 
GO 

CREATE AGGREGATE dbo.StrConcat ( 

    @Value NVARCHAR(MAX),
	@Delimiter NVARCHAR(4000) 

) RETURNS NVARCHAR(MAX) 
EXTERNAL Name concat_assembly.Concat; 
GO

-- Enable execution of CLR code 
sp_configure 'clr enabled',1
GO
RECONFIGURE
GO
--sp_configure 'clr enabled'  -- make sure it took
--GO

SELECT [dbo].StrConcat(FirstName + ' ' + LastName, ', ') as Names
FROM Employees