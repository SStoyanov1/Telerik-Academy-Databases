namespace ATMEngine.Data
{
    using System.Data.Entity;

    using ATMEngine.Model;
    using ATMEngine.Data.Migrations;

    public class ATMEngineDbContex : DbContext
    {
        public ATMEngineDbContex()
            :base("ATMEngineConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ATMEngineDbContex, Configuration>());
        }

        public IDbSet<CardAccount> CardAccounts { get; set; }
    }
}
