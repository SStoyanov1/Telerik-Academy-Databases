--2.	Get all manufacturers’ name and how many toys they have in
--the age range of 5 to 10 years, inclusive
USE ToysStore
go

SELECT Manufacturers.Name,
(SELECT COUNT(*)
	FROM Toys
	INNER JOIN AgeRanges
		ON Toys.AgeRangeId = AgeRanges.Id
		WHERE AgeRanges.MinimumAge >= 5 AND AgeRanges.MaximumAge <= 10 AND Manufacturers.id = toys.ManufacturerId) AS [Count]
FROM Manufacturers