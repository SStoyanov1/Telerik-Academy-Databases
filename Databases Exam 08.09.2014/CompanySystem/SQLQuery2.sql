--2.	Get all departments and how many employees there are in each one.
--Sort the result by the number of employees in descending order.

Use Company
go

SELECT d.Name AS [Department Name], COUNT(e.FirstName) AS [Employees]
	FROM Employees e
		JOIN Departments d
			ON e.DepartmentId = d.DepartmentId
GROUP BY d.Name
ORDER BY COUNT(e.FirstName) DESC