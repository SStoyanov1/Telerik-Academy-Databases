//10. Create a stored procedures in the Northwind database for finding the total incomes for given supplier name and period (start date, end date). Implement a C# method that calls the stored procedure and returns the retuned record set.

namespace _10.GetIncomeBySupplierAndPeriod
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using _01.NorthwindDbContex;

    class GetIncomeBySupplierAndPeriod
    {
        static void Main()
        {
            NorthwindEntities northwindDbContex = new NorthwindEntities();

            using (northwindDbContex)
            {
                northwindDbContex.Database.ExecuteSqlCommand("CREATE PROCEDURE usp_GetIncomeByGivenCompany " +
                "@companyName nvarchar(60), @startDate date, @endDate date AS " +
                "SELECT s.CompanyName, SUM(od.UnitPrice * od.Quantity) AS Income " +
                "FROM Suppliers s " +
                "JOIN Products p " +
                "ON s.SupplierID = p.SupplierID " +
                "JOIN [Order Details] od " +
                "ON p.ProductID = od.ProductID " +
                "JOIN Orders o " +
                "ON od.OrderID = o.OrderID " +
                "where o.ShippedDate > @startDate AND o.ShippedDate < @endDate " +
                "AND s.CompanyName = @companyName " +
                "GROUP BY s.CompanyName");

                string companyName = "Exotic Liquids";
                DateTime startDate = new DateTime(1990, 1, 1);
                DateTime endDate = new DateTime(1999, 1, 1);

                var col = northwindDbContex.usp_GetIncomeByGivenCompany(companyName, startDate, endDate).First();
                Console.WriteLine("Company name: {0} -> Income: {1}", col.CompanyName, col.Income);
            }
        }
    }
}
