namespace MyEcommerce.Migrations.Application
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_product_attribute_table_structure : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductAttributes", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.ProductAttributes", new[] { "Product_ProductId" });
            RenameColumn(table: "dbo.ProductAttributes", name: "Product_ProductId", newName: "ProductId");
            AlterColumn("dbo.ProductAttributes", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductAttributes", "ProductId");
            AddForeignKey("dbo.ProductAttributes", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductAttributes", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductAttributes", new[] { "ProductId" });
            AlterColumn("dbo.ProductAttributes", "ProductId", c => c.Int());
            RenameColumn(table: "dbo.ProductAttributes", name: "ProductId", newName: "Product_ProductId");
            CreateIndex("dbo.ProductAttributes", "Product_ProductId");
            AddForeignKey("dbo.ProductAttributes", "Product_ProductId", "dbo.Products", "ProductId");
        }
    }
}
