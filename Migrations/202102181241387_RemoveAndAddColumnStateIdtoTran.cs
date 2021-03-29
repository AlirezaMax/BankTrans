namespace BankTr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAndAddColumnStateIdtoTran : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trans", "StateId", c => c.Int(nullable: false));
            DropColumn("dbo.Trans", "StateCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trans", "StateCode", c => c.String(maxLength: 3));
            DropColumn("dbo.Trans", "StateId");
        }
    }
}
