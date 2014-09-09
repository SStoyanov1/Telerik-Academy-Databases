--1.	Get the full name (first name + “ “ + last name)  of every employee and
--its salary, for each employee with salary between $100 000 and $150 000, inclusive.
--Sort the results by salary in ascending order.

Use Company
go

SELECT e.FirstName + ' ' + e.LastName AS [Name]
	FROM Employees e
	WHERE e.YearSalary BETWEEN 100000 AND 150000
	ORDER BY e.YearSalary ASC