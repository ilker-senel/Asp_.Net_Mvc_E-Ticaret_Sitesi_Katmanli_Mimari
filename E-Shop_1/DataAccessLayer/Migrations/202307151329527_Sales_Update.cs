namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sales_Update : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Sales", "UserId");
            AddForeignKey("dbo.Sales", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "UserId", "dbo.Users");
            DropIndex("dbo.Sales", new[] { "UserId" });
        }
    }
}
