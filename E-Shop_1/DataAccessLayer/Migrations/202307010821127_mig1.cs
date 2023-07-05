namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "ProductId");
            AddForeignKey("dbo.Carts", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            DropColumn("dbo.Carts", "UrunId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "UrunId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Carts", "ProductId", "dbo.Products");
            DropIndex("dbo.Carts", new[] { "ProductId" });
            DropColumn("dbo.Carts", "ProductId");
        }
    }
}
