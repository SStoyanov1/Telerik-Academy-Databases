--3.	Get each employee’s full name (first name + “ “ + last name),
--project’s name, department’s name, starting and ending date for each
--employee in project. Additionally get the number of all reports, which
--time of reporting is between the start and end date. Sort the results
--first by the employee id, then by the project id. (This query is slow, be patient!)

Use Company
go

SELECT e.FirstName + ' ' + e.LastName AS [Full Name],
		p.Name AS [Project Name],
		d.Name AS [Department Name],
		ep.StartDate,
		ep.EndDate,
		(SELECT COUNT(r.ReportID))
	FROM Employees e
		JOIN EmployeesProjects ep
			ON e.EmployeeId = ep.EmployeeId
		JOIN Projects p
			ON ep.ProjectId = p.ProjectId
		JOIN Departments d
			ON e.DepartmentId = d.DepartmentId
		JOIN Reports r
			ON e.EmployeeId = r.EmployeeId
			GROUP BY e.FirstName + ' ' + e.LastName, p.Name, d.Name, ep.StartDate, ep.EndDate
