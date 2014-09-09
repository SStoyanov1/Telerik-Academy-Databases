namespace CompanySystem.RandomDataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CompanySystem.Data;

    internal class ReportDataGenerator : DataGenerator, IDataGenerator
    {
        public ReportDataGenerator(IRandomDataGenerator randomGenerator, CompanyEntities dbContex, int count)
            :base(randomGenerator, dbContex, count)
        {
        }

        public override void Generate()
        {
            var employeesIds = this.Database.Employees.Select(d => d.EmployeeId).ToList();

            Console.WriteLine("Adding reports...");
            Console.WriteLine("Takes 6 rows... 250 000 is a lot!");
            for (int i = 0; i < this.Count; i++)
            {
                var report = new Report
                {
                    Time = this.Random.GetRandomDate(DateTime.Today.AddDays(-180), DateTime.Today),
                    EmployeeId = employeesIds[this.Random.GetRandomNumber(0, employeesIds.Count - 1)]
                };

                this.Database.Reports.Add(report);

                if (i % 500 == 0)
                {
                    Console.Write(".");
                    this.Database.SaveChanges();
                }
            }
            Console.WriteLine();
            Console.WriteLine("Reports added.");
        }
    }
}
