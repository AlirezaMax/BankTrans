namespace BankTr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStateTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 3),
                        Name = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Trans", "StateId");
            //AddForeignKey("dbo.Trans", "StateId", "dbo.States", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Trans", "StateId", "dbo.States");
            DropIndex("dbo.Trans", new[] { "StateId" });
            DropTable("dbo.States");
        }
    }
}
