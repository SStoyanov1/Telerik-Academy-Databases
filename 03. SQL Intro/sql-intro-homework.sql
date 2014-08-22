/*
01. What is SQL? What is DML? What is DDL? Recite the most important SQL commands.

SQL (Structured Query Language) is a special-purpose programming language designed for
managing data held in a relational database management system (RDBMS).

A data definition language or data description language (DDL) is a syntax similar to a
computer programming language for defining data structures, especially database schemas.

Some of The Most Important SQL Commands
SELECT - extracts data from a database
UPDATE - updates data in a database
DELETE - deletes data from a database
INSERT INTO - inserts new data into a database
CREATE DATABASE - creates a new database
ALTER DATABASE - modifies a database
CREATE TABLE - creates a new table
ALTER TABLE - modifies a table
DROP TABLE - deletes a table
CREATE INDEX - creates an index (search key)
DROP INDEX - deletes an index

02. What is Transact-SQL (T-SQL)?

Transact-SQL (T-SQL) is Microsoft's and Sybase's proprietary extension to SQL. SQL,
the acronym for Structured Query Language, is a standardized computer language that
was originally developed by IBM for querying, altering and defining relational databases,
using declarative statements. T-SQL expands on the SQL standard to include procedural
programming, local variables, various support functions for string processing, date
processing, mathematics, etc. and changes to the DELETE and UPDATE statements. These
additional features make Transact-SQL Turing complete.

03. Start SQL Management Studio and connect to the database TelerikAcademy. Examine the major
tables in the "TelerikAcademy" database.

*/

--04. Write a SQL query to find all information about all departments (use "TelerikAcademy" database).

SELECT * FROM [TelerikAcademy].[dbo].[Departments];

--05. Write a SQL query to find all information about all departments (use "TelerikAcademy" database).

SELECT Name FROM [TelerikAcademy].[dbo].[Departments];

--06. Write a SQL query to find the salary of each employee.

SELECT LastName, Salary FROM [TelerikAcademy].[dbo].[Employees];

--07. Write a SQL to find the full name of each employee.

SELECT FirstName + ' ' + LastName AS FullName FROM [TelerikAcademy].[dbo].[Employees];

--08. Write a SQL query to find the email addresses of each employee (by his first and last name).
-- Consider that the mail domain is telerik.com. Emails should look like “John.Doe@telerik.com".
-- The produced column should be named "Full Email Addresses".

SELECT FirstName + '.' + LastName + '@telerik.com' AS [Full Email Addresses]
FROM [TelerikAcademy].[dbo].[Employees];

--09. Write a SQL query to find all different employee salaries.

SELECT DISTINCT Salary
FROM [TelerikAcademy].[dbo].[Employees];

--10. Write a SQL query to find all information about the employees whose job title is “Sales Representative“.

SELECT *
FROM [TelerikAcademy].[dbo].[Employees]
WHERE JobTitle = 'Sales Representative';

--11. Write a SQL query to find the names of all employees whose first name starts with "SA".

SELECT FirstName
FROM [TelerikAcademy].[dbo].[Employees]
WHERE FirstName LIKE 'Sa%';

--12. Write a SQL query to find the names of all employees whose last name contains "ei".

SELECT LastName
FROM [TelerikAcademy].[dbo].[Employees]
WHERE LastName LIKE '%ei%';

--13. Write a SQL query to find the salary of all employees whose salary is in the range [20000…30000].

SELECT Salary
FROM [TelerikAcademy].[dbo].[Employees]
WHERE Salary BETWEEN 20000 AND 30000;

--14. Write a SQL query to find the names of all employees whose salary is 25000, 14000, 12500 or 23600.

SELECT FirstName + ' ' + LastName AS [Full Name], Salary
FROM [TelerikAcademy].[dbo].[Employees]
WHERE Salary IN (25000, 14000, 12500, 23600);

--15. Write a SQL query to find all employees that do not have manager.

SELECT *
FROM [TelerikAcademy].[dbo].[Employees]
WHERE ManagerID IS NULL;

--16. Write a SQL query to find all employees that have salary more than 50000.
--Order them in decreasing order by salary.

SELECT FirstName + ' ' + LastName AS [Full Name], Salary
FROM [TelerikAcademy].[dbo].[Employees]
WHERE Salary > 50000
ORDER BY Salary DESC;

--17. Write a SQL query to find the top 5 best paid employees.

SELECT TOP 5 *
FROM [TelerikAcademy].[dbo].[Employees]
ORDER BY Salary DESC;

--18. Write a SQL query to find all employees along with their address. Use inner join with ON clause.

SELECT e.FirstName + ' ' + e.LastName AS FullName, a.AddressText
FROM [TelerikAcademy].[dbo].[Employees] e
	JOIN [TelerikAcademy].[dbo].[Addresses] a
	ON e.AddressID = a.AddressID;

--19. Write a SQL query to find all employees and their address.
--Use equijoins (conditions in the WHERE clause).

SELECT e.FirstName + ' ' + e.LastName AS FullName, a.AddressText
FROM [TelerikAcademy].[dbo].[Employees] e, [TelerikAcademy].[dbo].[Addresses] a
	WHERE e.AddressID = a.AddressID;

--20. Write a SQL query to find all employees along with their manager.

SELECT e.FirstName + ' ' + e.LastName AS FullName, m.LastName AS Manager
FROM [TelerikAcademy].[dbo].[Employees] e
	JOIN [TelerikAcademy].[dbo].[Employees] m
	ON e.ManagerID = m.EmployeeID;

--21. Write a SQL query to find all employees, along with their manager and their address.
--Join the 3 tables: Employees e, Employees m and Addresses a.

SELECT e.FirstName + ' ' + e.LastName AS FullName, m.LastName AS Manager,
		a.AddressText
FROM  [TelerikAcademy].[dbo].[Employees] e
	JOIN [TelerikAcademy].[dbo].[Employees] m
	ON e.ManagerID = m.EmployeeID
	JOIN [TelerikAcademy].[dbo].[Addresses] a
	ON e.AddressID = a.AddressID;

--22. Write a SQL query to find all departments and all town names as a single list. Use UNION.

SELECT d.Name
FROM  [TelerikAcademy].[dbo].[Departments] d
UNION
SELECT t.Name
FROM [TelerikAcademy].[dbo].[Towns] t;

--23. Write a SQL query to find all the employees and the manager for each of them along with
--the employees that do not have manager. Use right outer join. Rewrite the query to use left outer join.

SELECT e.FirstName + ' ' + e.LastName AS FullName, m.LastName AS Manager
FROM [TelerikAcademy].[dbo].[Employees] e
	RIGHT OUTER JOIN [TelerikAcademy].[dbo].[Employees] m
	ON e.ManagerID = m.EmployeeID;

SELECT e.FirstName + ' ' + e.LastName AS FullName, m.LastName AS Manager
FROM [TelerikAcademy].[dbo].[Employees] e
	LEFT OUTER JOIN [TelerikAcademy].[dbo].[Employees] m
	ON e.ManagerID = m.EmployeeID;

--24. Write a SQL query to find the names of all employees from the departments "Sales" and "Finance"
--whose hire year is between 1995 and 2005.

SELECT e.FirstName + ' ' + e.LastName AS FullName, e.HireDate, d.Name
FROM [TelerikAcademy].[dbo].[Employees] e
	JOIN [TelerikAcademy].[dbo].[Departments] d
	ON e.DepartmentID = d.DepartmentID
	WHERE d.Name IN ('Sales', 'Finance')
	AND YEAR(e.HireDate) BETWEEN 1995 AND 2005;