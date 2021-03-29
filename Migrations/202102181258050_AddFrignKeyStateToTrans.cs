namespace BankTr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFrignKeyStateToTrans : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Trans", "StateId", "dbo.States", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Trans", "StateId", "dbo.States");
        }
    }
}
