namespace CompanySystem.RandomDataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CompanySystem.Data;

    internal class EmployeesProjectsDataGenerator : DataGenerator, IDataGenerator
    {
        public EmployeesProjectsDataGenerator(IRandomDataGenerator randomGenerator, CompanyEntities dbContex, int count)
            : base(randomGenerator, dbContex, count)
        {
        }

        public override void Generate()
        {
            var employeesIds = this.Database.Employees.Select(e => e.EmployeeId).ToList();
            var projectsIds = this.Database.Projects.Select(p => p.ProjectId).ToList();

            Console.WriteLine("Adding projects to each employees...");
            Console.WriteLine("Takes just one row and a half...");
            for (int i = 0; i < this.Count; i++)
            {
                var currentEmployee = this.Database.Employees.Find(i + 1);

                var uniqueProjectsIds = new HashSet<int>();
                var projectsOfEmployee = this.Random.GetRandomNumber(2, 20);

                while (uniqueProjectsIds.Count != projectsOfEmployee)
                {
                    uniqueProjectsIds.Add(projectsIds[this.Random.GetRandomNumber(0, projectsIds.Count - 1)]);
                }

                foreach (var uniqueProjectId in uniqueProjectsIds)
                {
                    currentEmployee.EmployeesProjects.Add(
                        new EmployeesProject
                        {
                            ProjectId = uniqueProjectId,
                            StartDate = this.Random.GetRandomDate(DateTime.Today.AddDays(-180), DateTime.Today),
                            EndDate = this.Random.GetRandomDate(DateTime.Today, DateTime.Today.AddDays(+180))
                        });
                }

                if (i % 50 == 0)
                {
                    this.Database.Configuration.AutoDetectChangesEnabled = true;
                    Console.Write(".");
                    this.Database.SaveChanges();
                    this.Database.Configuration.AutoDetectChangesEnabled = false;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Projects added.");
        }
    }
}
