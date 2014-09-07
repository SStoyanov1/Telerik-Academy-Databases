--03. Get all toys’ name, price and color from category “boys” 

USE ToysStore
GO

SELECT t.Name, t.Price, t.Color
FROM Toys_Categories tc
	INNER JOIN Toys t
		ON tc.ToyId = t.Id
	INNER JOIN Categories c
		ON tc.CategoryId = c.Id
	WHERE c.Name = 'boys'
