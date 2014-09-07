namespace ToysStore.SampleDataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ToysStore.Data;

    internal class ToyDataGenerator : DataGenerator, IDataGenerator
    {
        public ToyDataGenerator(IRandomDataGenerator randomGenerator, ToysStoreEntities dbContex, int count)
            : base(randomGenerator, dbContex, count)
        {
        }

        public override void Generate()
        {
            var ageRangeIds = this.Database.AgeRanges.Select(a => a.Id).ToList();
            var manufacturersIds = this.Database.Manufacturers.Select(m => m.Id).ToList();
            var categoriesIds = this.Database.Categories.Select(c => c.Id).ToList();

            Console.WriteLine("Adding toys...");
            for (int i = 0; i < this.Count - 1; i++)
            {
                var toy = new Toy
                {
                    Name = this.Random.GetRandomStringWithRandomLength(5, 20),
                    Type = this.Random.GetRandomStringWithRandomLength(5, 20),
                    Price = (decimal)this.Random.GetRandomNumber(5, 500),
                    Color = this.Random.GetRandomNumber(1, 5) == 5 ? null : this.Random.GetRandomStringWithRandomLength(5, 20),
                    ManufacturerId = manufacturersIds[this.Random.GetRandomNumber(0, manufacturersIds.Count - 1)],
                    AgeRangeId = ageRangeIds[this.Random.GetRandomNumber(0, ageRangeIds.Count - 1)]
                };

                var uniqueCategoryIds = new HashSet<int>();
                var categoriesInToy = this.Random.GetRandomNumber(2, Math.Min(10, categoriesIds.Count - 1));

                while (uniqueCategoryIds.Count != categoriesInToy)
                {
                    uniqueCategoryIds.Add(categoriesIds[this.Random.GetRandomNumber(0, categoriesIds.Count - 1)]);
                }

                foreach (var uniqueCategoryId in uniqueCategoryIds)
                {
                    toy.Categories.Add(this.Database.Categories.Find(uniqueCategoryId));
                }

                if (i % 100 == 0)
                {
                    Console.Write(".");
                    this.Database.SaveChanges();
                }

                this.Database.Toys.Add(toy);
            }
            Console.WriteLine();
            Console.WriteLine("Toys added.");
        }
    }
}
