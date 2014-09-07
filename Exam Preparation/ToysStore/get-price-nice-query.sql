use ToysStore
go

select t.Name, t.Price
from Toys t
where t.Type = 'puzzle' and t.Price > 10
order by t.Price

--1.	Get all toys’s name and price having type of “puzzle” and price above $10.00 ordering them by price