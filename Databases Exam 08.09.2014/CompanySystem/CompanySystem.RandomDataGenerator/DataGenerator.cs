namespace CompanySystem.RandomDataGenerator
{
    using System;

    using CompanySystem.Data;

    internal abstract class DataGenerator : IDataGenerator
    {
        private IRandomDataGenerator randomDataGenerator;
        private CompanyEntities dbContex;
        private int count;

        public DataGenerator(IRandomDataGenerator randomGenerator, CompanyEntities dbContex, int count)
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

        protected CompanyEntities Database
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
