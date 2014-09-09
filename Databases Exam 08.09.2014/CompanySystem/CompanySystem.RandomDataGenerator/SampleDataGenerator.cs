namespace CompanySystem.RandomDataGenerator
{
    using System;
    using System.Collections.Generic;

    using CompanySystem.Data;

    public class SampleDataGenerator
    {
        private const int NumberOfDepartments = 100;
        private const int NumberOfProjects = 1000;
        private const int NumberOfEmployees = 5000;
        private const int NumberOfReports = 250000;

        private static void Main()
        {
            var random = RandomDataGenerator.Instance;
            var db = new CompanyEntities();
            db.Configuration.AutoDetectChangesEnabled = false;
            
            var listOfGenerators = new List<IDataGenerator>
                {
                    new DepartmentDataGenerator(random, db, NumberOfDepartments),
                    new ProjectDataGenerator(random, db, NumberOfProjects),
                    new EmployeeDataGenerator(random, db, NumberOfEmployees),
                    new EmployeesProjectsDataGenerator(random, db, NumberOfEmployees),
                    new ReportDataGenerator(random, db, NumberOfReports)
                };
            
            foreach (var generator in listOfGenerators)
            {
                generator.Generate();
                db.SaveChanges();
            }
        }
    }
}
