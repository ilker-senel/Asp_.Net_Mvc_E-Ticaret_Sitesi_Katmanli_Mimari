namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sales", "ProductId");
            AddForeignKey("dbo.Sales", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            DropColumn("dbo.Sales", "UrunId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "UrunId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Sales", "ProductId", "dbo.Products");
            DropIndex("dbo.Sales", new[] { "ProductId" });
            DropColumn("dbo.Sales", "ProductId");
        }
    }
}
