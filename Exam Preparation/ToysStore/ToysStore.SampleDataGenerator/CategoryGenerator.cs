namespace ToysStore.SampleDataGenerator
{
    using System;

    using ToysStore.Data;

    internal class CategoryGenerator : DataGenerator, IDataGenerator
    {
        public CategoryGenerator(IRandomDataGenerator randomGenerator, ToysStoreEntities dbContex, int count)
            :base(randomGenerator, dbContex, count)
        {
        }

        public override void Generate()
        {
            Console.WriteLine("Adding categories...");
            for (int i = 0; i < this.Count; i++)
            {
                var category = new Category
                {
                    Name = this.Random.GetRandomStringWithRandomLength(5, 20)
                };

                this.Database.Categories.Add(category);

                if (i % 100 == 0)
                {
                    Console.WriteLine(".");
                    this.Database.SaveChanges();
                }
            }
            Console.WriteLine();
            Console.WriteLine("Categories added...");
        }
    }
}
