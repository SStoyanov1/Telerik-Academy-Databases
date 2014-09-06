namespace ATMEngine.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ATMEngine.Data.ATMEngineDbContex>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "ATMEngine.Data.ATMEngineDbContex";
        }

        protected override void Seed(ATMEngine.Data.ATMEngineDbContex context)
        {
        }
    }
}
