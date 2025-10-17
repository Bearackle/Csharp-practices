namespace Day04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_product_brand_id : DbMigration
    {
        public override void Up()
        { 
            AddColumn("dbo.products", "Brand_Id", c => c.Int());
            CreateIndex("dbo.products", "Brand_Id");
            AddForeignKey("dbo.products", "Brand_Id", "dbo.Brand", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.products", "Brand_Id", "dbo.Brand");
            DropIndex("dbo.products", new[] { "Brand_Id" });
            DropColumn("dbo.products", "Brand_Id");
            DropTable("dbo.Brand");
        }
    }
}
