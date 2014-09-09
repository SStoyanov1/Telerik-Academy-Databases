namespace CompanySystem.RandomDataGenerator
{
    using System;

    using CompanySystem.Data;

    internal class ProjectDataGenerator : DataGenerator, IDataGenerator
    {
        private const int ProjectNameMinLength = 5;
        private const int ProjectNameMaxLength = 50;

        public ProjectDataGenerator(IRandomDataGenerator randomGenerator, CompanyEntities dbContex, int count)
            :base(randomGenerator, dbContex, count)
        {
        }

        public override void Generate()
        {
            Console.WriteLine("Adding projects...");
            for (int i = 0; i < this.Count; i++)
            {
                var project = new Project
                {
                    Name = this.Random.GetRandomStringWithRandomLength(ProjectNameMinLength, ProjectNameMaxLength)
                };

                this.Database.Projects.Add(project);

                if (i % 100 == 0)
                {
                    Console.Write(".");
                    this.Database.SaveChanges();
                }
            }
            Console.WriteLine();
            Console.WriteLine("Projects added.");
        }
    }
}
