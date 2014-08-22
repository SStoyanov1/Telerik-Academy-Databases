-- Advanced SQL Homework --

--01. Write a SQL query to find the names and salaries of the employees that take
--the minimal salary in the company. Use a nested SELECT statement.

SELECT FirstName, LastName, Salary
FROM Employees
WHERE Salary =
	(SELECT MIN(Salary) FROM Employees);

--02. Write a SQL query to find the names and salaries of the employees that have
--a salary that is up to 10% higher than the minimal salary for the company.

SELECT FirstName, LastName, Salary
FROM Employees
WHERE Salary <=
	(SELECT MIN(Salary) * 1.1 FROM Employees);

--03. Write a SQL query to find the full name, salary and department of the employees
--that take the minimal salary in their department. Use a nested SELECT statement.

SELECT e.FirstName + ' ' + e.LastName AS Name, e.Salary, d.Name 
FROM Employees e
	JOIN Departments d
		ON e.DepartmentID = d.DepartmentID
WHERE Salary =
	(SELECT MIN(Salary) FROM Employees m
				WHERE m.DepartmentID = e.DepartmentID);

--04. Write a SQL query to find the average salary in the department #1.

SELECT AVG(e.Salary) AS [Average Salary]
FROM Employees e
WHERE e.DepartmentID = 1;

--05. Write a SQL query to find the average salary  in the "Sales" department.

SELECT AVG(e.Salary) AS [Average Salary]
FROM Employees e
	JOIN Departments d
		ON e.DepartmentID = d.DepartmentID
WHERE d.Name = 'Sales';

--06. Write a SQL query to find the number of employees in the "Sales" department.

SELECT COUNT(*) AS Employees
FROM Employees e
	JOIN Departments d
		ON e.DepartmentID = d.DepartmentID
WHERE d.Name = 'Sales';

--07. Write a SQL query to find the number of all employees that have manager.

SELECT COUNT(*) AS Employees
	FROM Employees e
WHERE e.ManagerID IS NOT NULL;

--08. Write a SQL query to find the number of all employees that have no manager.

SELECT COUNT(*) AS Employees
	FROM Employees e
WHERE e.ManagerID IS NULL;

--09. Write a SQL query to find all departments and the average salary for each of them.

SELECT d.Name AS [Department Name], AVG(e.Salary) AS [Average Salary]
	FROM Employees e
		JOIN Departments d
			ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name
ORDER BY [Average Salary];

--10. Write a SQL query to find the count of all employees in each department and for each town.

SELECT COUNT(d.Name) AS Employees, d.Name AS Department, t.Name AS [Town Name]
	FROM Employees e
		JOIN Departments d
			ON e.DepartmentID = d.DepartmentID
		JOIN Addresses a
			ON e.AddressID = a.AddressID
		JOIN Towns t
			ON a.TownID = t.TownID
GROUP BY t.Name, d.Name;

--11. Write a SQL query to find all managers that have exactly 5 employees.
--Display their first name and last name.

SELECT m.FirstName + ' ' + m.LastName AS Manager, COUNT(*) AS Employees
	FROM Employees e
		JOIN Employees m
			ON e.ManagerID = m.EmployeeID
GROUP BY m.FirstName + ' ' + m.LastName
HAVING COUNT(*) = 5;

--12. Write a SQL query to find all employees along with their managers.
--For employees that do not have manager display the value "(no manager)".

SELECT e.FirstName + ' ' + e.LastName AS Employee, ISNULL(m.FirstName + ' ' + m.LastName, 'No manager') AS Manager
	FROM Employees e
		LEFT JOIN Employees m
			ON e.ManagerID = m.EmployeeID

--13. Write a SQL query to find the names of all employees whose last name
--is exactly 5 characters long. Use the built-in LEN(str) function.

SELECT e.FirstName + ' ' + e.LastName AS Employees
	FROM Employees e
	WHERE LEN(e.LastName) = 5;

--14. Write a SQL query to display the current date and time in the following
--format "day.month.year hour:minutes:seconds:milliseconds". Search in  Google
--to find how to format dates in SQL Server.

SELECT CONVERT(VARCHAR(10), GETDATE(), 104) + ' ' + CONVERT(VARCHAR(12), GETDATE(), 114) AS [Time];

/*15. Write a SQL statement to create a table Users. Users should have username,
password, full name and last login time. Choose appropriate data types for the
table fields. Define a primary key column with a primary key constraint. Define
the primary key column as identity to facilitate inserting records. Define unique
constraint to avoid repeating usernames. Define a check constraint to ensure the
password is at least 5 characters long.
*/

CREATE TABLE Users
(
  UserID int IDENTITY,
  UserName nvarchar(30) NOT NULL,
  UserPassword nvarchar(30) NOT NULL,
  FullName nvarchar(40) NOT NULL,
  LastLoginTime datetime,
  CONSTRAINT PK_Users PRIMARY KEY(UserID),
  CONSTRAINT UN_User UNIQUE(Username),
  CONSTRAINT CH_Password Check (LEN(UserPassword) >= 5)
);

INSERT INTO Users(UserName, UserPassword, FullName, LastLoginTime)
VALUES
('Gosho', '123456', 'Gosho Goshev', GETDATE()),
('Misho', '123456', 'Misho Mishev', GETDATE()),
('DIcho', '123456', 'DIcho Dichev', GETDATE());

--16. Write a SQL statement to create a view that displays the users from the Users
--table that have been in the system today. Test if the view works correctly.

GO
SELECT u.UserName, u.FullName, u.LastLoginTime
FROM Users u
WHERE DAY(u.LastLoginTime) = DAY(GETDATE());

--17. Write a SQL statement to create a table Groups. Groups should have unique name
--(use unique constraint). Define primary key and identity column.

CREATE TABLE Groups
(
	GroupId int IDENTITY,
	Name nvarchar(30) NOT NULL,
	CONSTRAINT PK_Group PRIMARY KEY(GroupId),
	CONSTRAINT UN_Name UNIQUE(Name)
)

--18. Write a SQL statement to add a column GroupID to the table Users. Fill some data
--in this new column and as well in the Groups table. Write a SQL statement to add a
--foreign key constraint between tables Users and Groups tables.

ALTER TABLE Users
	ADD GroupId int
ALTER TABLE Users
	ADD CONSTRAINT FK_Users_Group
	FOREIGN KEY (GroupId)
	REFERENCES Groups(GroupId)

--19. Write SQL statements to insert several records in the Users and Groups tables.

INSERT INTO Users(UserName, UserPassword, FullName, LastLoginTime)
VALUES
('Berbo', '123456', 'Berbo Berbatov', GETDATE()),
('Tedo', '123456', 'Tedo Tedov', GETDATE()),
('Dendo', '123456', 'Dendo Dendov', GETDATE());

INSERT INTO Groups(Name)
VALUES
('Footballers'),
('Basketball players'),
('Baseball players');

--20. Write SQL statements to update some of the records in the Users and Groups tables.

UPDATE Users
SET UserName = 'Doncho'
WHERE UserId = 2
UPDATE Groups
SET Name = 'Chess players'
WHERE GroupId = 1;

--21. Write SQL statements to delete some of the records from the Users and Groups tables.

DELETE FROM Users
WHERE UserName ='Dendo'
DELETE FROM Groups
WHERE Name ='Baseball players';

--22. Write SQL statements to insert in the Users table the names of all employees from the
--Employees table. Combine the first and last names as a full name. For username use the
--first letter of the first name + the last name (in lowercase). Use the same for the password,
--and NULL for last login time.

INSERT INTO Users(UserName, UserPassword, FullName, LastLoginTime)
SELECT LOWER(e.FirstName + e.LastName),
	   LOWER(e.FirstName + e.LastName),
	   e.FirstName + ' ' + e.LastName,
	   NULL
FROM Employees e;

--23. Write a SQL statement that changes the password to NULL for all users that have not been
--in the system since 10.03.2010.

UPDATE Users
 SET UserPassword = NULL
 WHERE LastLoginTime < CONVERT(VARCHAR(10), GETDATE(), 111);

--24. Write a SQL statement that deletes all users without passwords (NULL password).

DELETE FROM Users
WHERE UserPassword = NULL

--25. Write a SQL query to display the average employee salary by department and job title.

SELECT d.Name AS [Department], e.JobTitle AS [Job Title], AVG(Salary) AS [Average Salary]
FROM Employees e
	JOIN Departments d
		ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name, e.JobTitle;

--26. Write a SQL query to display the minimal employee salary by department and job title
--along with the name of some of the employees that take it.

SELECT MIN(e.FirstName + ' ' + e.LastName) AS [Full Name], d.Name AS [Department],
		e.JobTitle AS [Job Title], MIN(Salary) AS [Minimal Salary]
FROM Employees e
	JOIN Departments d
		ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name, e.JobTitle;

--27. Write a SQL query to display the town where maximal number of employees work.

SELECT TOP 1 t.Name
FROM Employees e
	JOIN Addresses a
		ON e.AddressID = a.AddressID
	JOIN Towns t
		ON a.TownID = t.TownID
GROUP BY t.TownID, t.Name
ORDER BY COUNT(*) DESC;

--28. Write a SQL query to display the number of managers from each town.

SELECT COUNT(m.EmployeeID) AS Managers, t.Name AS Town
FROM Employees e
	JOIN Employees m
		ON e.ManagerID = m.EmployeeID
	JOIN Addresses a
		ON e.AddressID = a.AddressID
	JOIN Towns t
		ON a.TownID = t.TownID
GROUP BY t.Name

/* 29. Write a SQL to create table WorkHours to store work reports for each employee (employee
id, date, task, hours, comments). Don't forget to define  identity, primary key and appropriate
foreign key. 
Issue few SQL statements to insert, update and delete of some data in the table.
Define a table WorkHoursLogs to track all changes in the WorkHours table with triggers.
For each change keep the old record data, the new record data and the command (insert / update / delete).
*/

CREATE TABLE WorkHours (
  ReportID int IDENTITY PRIMARY KEY,
  EmployeeID INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeId) ,
  FromDate datetime,
  Task nvarchar(100),
  HoursUsed int ,
  Comments varchar(200),
)
GO

CREATE TABLE WorkHoursLogs (
  ReportID int, 
  OldEmployeeID INT ,
  OldFromDate datetime,
  OldTask nvarchar(100),
  OldHoursUsed int ,
  OldComments varchar(200),
  NewEmployeeID INT ,
  NewFromDate datetime,
  NewTask nvarchar(100),
  NewHoursUsed int ,
  NewComments varchar(200),
  Command varchar(20)
)
GO

create trigger tr_workHoursUpdate on WorkHours
after UPDATE
as
BEGIN
insert into WorkHoursLogs ( ReportID,OldEmployeeID, OldFromDate,OldTask,OldHoursUsed,OldComments, NewEmployeeID,NewFromDate,NewTask,NewHoursUsed,NewComments,Command) 
select d.ReportID, d.EmployeeID, d.FromDate,d.Task,d.HoursUsed,d.Comments,
w.EmployeeID,w.FromDate,w.Task,w.HoursUsed,w.Comments,'Update'
from deleted d JOIN
WorkHours w 
ON 
d.ReportID = w.ReportID
END
go

create trigger tr_workHoursInsert on WorkHours
after INSERT
as
BEGIN
insert into WorkHoursLogs ( ReportID,OldEmployeeID, OldFromDate,OldTask,OldHoursUsed,OldComments, NewEmployeeID,NewFromDate,NewTask,NewHoursUsed,NewComments,Command) 
select w.ReportID,NULL,NULL,NULL,NULL,NULL,i.EmployeeID,i.FromDate,i.Task,i.HoursUsed,i.Comments,'Insert'
from WorkHours w JOIN 
inserted i
ON w.ReportID=i.ReportID
END
go

create trigger tr_workHoursDelete on WorkHours
after DELETE
as
BEGIN
insert into WorkHoursLogs ( ReportID,OldEmployeeID, OldFromDate,OldTask,OldHoursUsed,OldComments, NewEmployeeID,NewFromDate,NewTask,NewHoursUsed,NewComments,Command) 
select ReportID, EmployeeID, FromDate,Task,HoursUsed,Comments,
NULL,NULL,NULL,NULL,NULL,'Delete'
from deleted
END
go

INSERT INTO WorkHours
VALUES (1,12/12/2012,'Test',8,'VERYGOOD' )

UPDATE WorkHours
SET Comments ='GOOD'
where EmployeeID=1

DELETE FROM WorkHours
WHERE Comments='VERYGOOD'

--30. Start a database transaction, delete all employees from the 'Sales' department along
--with all dependent records from the pother tables. At the end rollback the transaction.
/*
BEGIN TRAN
	DELETE FROM Employees 
	WHERE DepartmentId IN (SELECT DepartmentId FROM Departments WHERE Name = 'Sales')

	ROLLBACK TRAN
GO
*/
--31. Start a database transaction and drop the table EmployeesProjects. Now how you could
--restore back the lost table data?

GO
 
BEGIN TRAN
 
DROP TABLE EmployeesProjects
 
ROLLBACK TRAN

--32. Find how to use temporary tables in SQL Server. Using temporary tables backup all records
--from EmployeesProjects and restore them back after dropping and re-creating the table.

BEGIN TRAN 
SELECT * INTO #TemporaryEmployeesProject
FROM EmployeesProjects

DROP TABLE EmployeesProjects

SELECT * INTO EmployeesProjects
FROM #TemporaryEmployeesProject

DROP TABLE #TemporaryEmployeesProject
GO
ROLLBACK TRAN

