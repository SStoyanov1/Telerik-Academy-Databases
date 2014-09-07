namespace ToysStore.SampleDataGenerator
{
    using System;
    using System.Collections.Generic;

    using ToysStore.Data;

    internal class AgeRangeDataGenerator : DataGenerator, IDataGenerator
    {
        public AgeRangeDataGenerator(IRandomDataGenerator randomGenerator, ToysStoreEntities dbContex, int count)
            : base(randomGenerator, dbContex, count)
        {
        }

        public override void Generate()
        {
            Console.WriteLine("Adding age ranges...");

            int count = 0;
            for (int i = 0; i < this.Count / 5; i++)
            {
                for (int j = i + 1; j < i + 6; j++)
                {
                    var ageRange = new AgeRanx
                    {
                        MinimumAge = i,
                        MaximumAge = j
                    };

                    this.Database.AgeRanges.Add(ageRange);
                    count++;

                    if (count % 100 == 0)
                    {
                        this.Database.SaveChanges();
                    }

                    if (count == this.Count)
                    {
                        Console.WriteLine("Age ranges added.");
                        return;
                    }
                }
            }
        }
    }
}
