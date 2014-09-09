namespace CompanySystem.RandomDataGenerator
{
    using System;
    using System.Collections.Generic;

    using CompanySystem.Data;

    internal class DepartmentDataGenerator : DataGenerator, IDataGenerator
    {
        private const int DepartmentNameMinLength = 10;
        private const int DepartmentNameMaxLength = 50;

        public DepartmentDataGenerator(IRandomDataGenerator randomGenerator, CompanyEntities dbContex, int count)
            : base(randomGenerator, dbContex, count)
        {
        }

        public override void Generate()
        {
            //Ensuring uniqueness of the departments names
            var departmentsToBeAdded = new HashSet<string>();

            Console.WriteLine("Adding departments...");
            while (departmentsToBeAdded.Count != this.Count)
            {
                departmentsToBeAdded.Add(this.Random.GetRandomStringWithRandomLength(DepartmentNameMinLength, DepartmentNameMaxLength));
            }

            int index = 0;
            foreach (var departmentName in departmentsToBeAdded)
            {
                var department = new Department
                {
                    Name = departmentName
                };

                if (index % 100 == 0)
                {
                    Console.Write(".");
                    this.Database.SaveChanges();
                }
                this.Database.Departments.Add(department);
                index++;
            }
            Console.WriteLine();
            Console.WriteLine("Departments added.");
        }
    }
}
