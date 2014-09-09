namespace CompanySystem.RandomDataGenerator
{
    using System;
    using System.Linq;

    using CompanySystem.Data;

    internal class EmployeeDataGenerator : DataGenerator, IDataGenerator
    {
        private const int EmployeeNameMinLength = 5;
        private const int EmployeeNameMaxLength = 20;
        private const int EmployeeMinSalary = 50000;
        private const int EmployeeMaxSalary = 200000;
        private const double PercentageOfEmployeesWithManager = 0.95;

        public EmployeeDataGenerator(IRandomDataGenerator randomGenerator, CompanyEntities dbContex, int count)
            : base(randomGenerator, dbContex, count)
        {
        }

        public override void Generate()
        {
            var departmentIds = this.Database.Departments.Select(d => d.DepartmentId).ToList();

            Console.WriteLine("Adding employees...");
            for (int i = 0; i < this.Count; i++)
            {
                var employee = new Employee
                {
                    FirstName = this.Random.GetRandomStringWithRandomLength(EmployeeNameMinLength, EmployeeNameMaxLength),
                    LastName = this.Random.GetRandomStringWithRandomLength(EmployeeNameMinLength, EmployeeNameMaxLength),
                    YearSalary = (decimal)this.Random.GetRandomNumber(EmployeeMinSalary, EmployeeMaxSalary),
                    DepartmentId = departmentIds[this.Random.GetRandomNumber(0, departmentIds.Count - 1)]
                };

                if (i % 98 == 0)
                {
                    Console.Write(".");
                    this.Database.SaveChanges();
                }

                this.Database.Employees.Add(employee);
            }
            this.Database.SaveChanges();

            Console.WriteLine();
            Console.WriteLine("Adding managers...");

            //Ensuring no cycles in the management tree
            var employeeIds = this.Database.Employees.Select(e => e.EmployeeId).ToList();
            var employeesWithManagerIds = employeeIds.GetRange(0, (int)(PercentageOfEmployeesWithManager * (double)(employeeIds.Count - 1)));
            var startIndex = employeesWithManagerIds.Count + 2;
            var endIndex = employeeIds.Count - 1;
            var managersIds = employeeIds.GetRange(startIndex, endIndex - startIndex);

            for (int i = 0; i < employeesWithManagerIds.Count - 1; i++)
            {
                var currentEmployee = Database.Employees.Find(employeesWithManagerIds[i]);
                currentEmployee.ManagerId = managersIds[this.Random.GetRandomNumber(0, managersIds.Count - 1)];
                if (i % 100 == 0)
                {
                    this.Database.Configuration.AutoDetectChangesEnabled = true;
                    Console.Write(".");
                    this.Database.SaveChanges();
                    this.Database.Configuration.AutoDetectChangesEnabled = false;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Employees added.");
        }
    }
}
