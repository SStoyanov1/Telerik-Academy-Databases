namespace ToysStore.SampleDataGenerator
{
    using System;
    using System.Collections.Generic;

    using ToysStore.Data;

    internal class SampleDataGenerator
    {
        private static void Main()
        {
            var random = RandomDataGenerator.Instance;
            var db = new ToysStoreEntities();
            db.Configuration.AutoDetectChangesEnabled = false;

            var listOfGenerators = new List<IDataGenerator>
                {
                    new CategoryGenerator(random, db, 100),
                    new ManufacturerDataGenerator(random, db, 50),
                    new AgeRangeDataGenerator(random, db, 100),
                    new ToyDataGenerator(random, db, 20000)
                };

            foreach (var generator in listOfGenerators)
            {
                generator.Generate();
                db.SaveChanges();
            }
        }
    }
}
