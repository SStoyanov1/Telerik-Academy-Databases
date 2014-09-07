namespace ToysStore.SampleDataGenerator
{
    using System;

    using ToysStore.Data;

    internal abstract class DataGenerator: IDataGenerator
    {
        private IRandomDataGenerator randomDataGenerator;
        private ToysStoreEntities dbContex;
        private int count;

        public DataGenerator(IRandomDataGenerator randomGenerator, ToysStoreEntities dbContex, int count)
        {
            this.randomDataGenerator = randomGenerator;
            this.dbContex = dbContex;
            this.count = count;
        }

        protected IRandomDataGenerator Random
        {
            get
            {
                return this.randomDataGenerator;
            }
        }

        protected ToysStoreEntities Database
        {
            get
            {
                return this.dbContex;
            }
        }

        protected int Count
        {
            get
            {
                return this.count;
            }
        }

        public abstract void Generate();    
    }
}
