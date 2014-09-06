//01. Using Entity Framework write a SQL query to select all employees from the Telerik Academy database
//and later print their name, department and town. Try the both variants: with and without .Include(…).
//Compare the number of executed SQL statements and the performance.

namespace _01.TestsPerformanceWithInclude
{
    using System;
    using System.Diagnostics;

    class TestsPerformance
    {
        static void Main()
        {
            TelerikAcademyEntities telerikAcademyDbContex = new TelerikAcademyEntities();

            //many queries
            GetNameDepTown(telerikAcademyDbContex);
            
            telerikAcademyDbContex = new TelerikAcademyEntities();
            
            //one query
            GetNameDepTownWithInclude(telerikAcademyDbContex);
        }

        private static void GetNameDepTown(TelerikAcademyEntities contex)
        {
            
            using (contex)
            {
                foreach (var employee in contex.Employees)
                {
                    Console.WriteLine("| {0} | {1} | {2} |", employee.LastName, employee.Department.Name, employee.Address.Town.Name);
                }
            }
        }

        private static void GetNameDepTownWithInclude(TelerikAcademyEntities contex)
        {
            using (contex)
            {
                foreach (var employee in contex.Employees.Include("Address.Town").Include("Department"))
                {
                    Console.WriteLine("| {0} | {1} | {2} |", employee.LastName, employee.Department.Name, employee.Address.Town.Name);
                }
            }
        }
    }
}
