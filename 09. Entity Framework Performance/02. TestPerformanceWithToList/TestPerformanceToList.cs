//02. Using Entity Framework write a query that selects all employees from the Telerik Academy database,
//then invokes ToList(), then selects their addresses, then invokes ToList(), then selects their towns,
//then invokes ToList() and finally checks whether the town is "Sofia". Rewrite the same in more optimized
//way and compare the performance.

namespace _02.TestPerformanceWithToList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics;

    using _01.TestsPerformanceWithInclude;

    public class TestPerformanceToList
    {
        static void Main()
        {
            TelerikAcademyEntities telerikAcademyDbContex = new TelerikAcademyEntities();
            Stopwatch sw = new Stopwatch();

            sw.Start();
            //many queries
            var employees = SelectsAllEmployeesWithToList(telerikAcademyDbContex);
            sw.Stop();
            Console.WriteLine("With 3 .ToList() -> {0}", sw.Elapsed);
            sw.Reset();

            telerikAcademyDbContex = new TelerikAcademyEntities();

            sw.Start();
            //one query
            var employeesOptimized = SelectsAllEmployeesOptimized(telerikAcademyDbContex);
            sw.Stop();
            Console.WriteLine("With 1 .ToList() -> {0}", sw.Elapsed);
        }

        private static List<Town> SelectsAllEmployeesWithToList(TelerikAcademyEntities dbContex)
        {
            var allEmployees = dbContex.Employees.ToList()
                                      .Select(e => e.Address).ToList()
                                      .Select(e => e.Town).ToList()
                                      .Where(e => e.Name == "Sofia").ToList();

            return allEmployees;
        }

        private static List<Town> SelectsAllEmployeesOptimized(TelerikAcademyEntities dbContex)
        {
            var allEmployeesOptimized = dbContex.Employees
                                               .Select(e => e.Address)
                                               .Select(e => e.Town)
                                               .Where(e => e.Name == "Sofia").ToList();

            return allEmployeesOptimized;
        }
    }
}
