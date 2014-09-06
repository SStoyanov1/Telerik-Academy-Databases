namespace ATMEngine.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CardAccounts",
                c => new
                    {
                        CardId = c.Int(nullable: false, identity: true),
                        CardNumber = c.String(),
                        CardPin = c.String(),
                        CardCash = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CardId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CardAccounts");
        }
    }
}
