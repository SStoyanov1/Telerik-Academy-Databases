namespace ToysStore.SampleDataGenerator
{
    using System;
    using System.Collections.Generic;
    using ToysStore.Data;

    internal class ManufacturerDataGenerator : DataGenerator, IDataGenerator
    {
        public ManufacturerDataGenerator(IRandomDataGenerator randomGenerator, ToysStoreEntities dbContex, int count)
            : base(randomGenerator, dbContex, count)
        {
        }

        public override void Generate()
        {
            var manufacturersToBeAdded = new HashSet<string>();

            Console.WriteLine("Adding manufacturers...");
            while (manufacturersToBeAdded.Count != this.Count)
            {
                manufacturersToBeAdded.Add(this.Random.GetRandomStringWithRandomLength(5, 20));
            }

            int index = 0;
            foreach (var manufacturerName in manufacturersToBeAdded)
            {
                var manufacturer = new Manufacturer
                {
                    Name = manufacturerName,
                    Country = this.Random.GetRandomStringWithRandomLength(5, 20)
                };

                if (index % 100 == 0)
                {
                    Console.WriteLine(".");
                    this.Database.SaveChanges();
                }
                this.Database.Manufacturers.Add(manufacturer);
                index++;
            }
            Console.WriteLine();
            Console.WriteLine("Manufacturers added.");
        }
    }
}
