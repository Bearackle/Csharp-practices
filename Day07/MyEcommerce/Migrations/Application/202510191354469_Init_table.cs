namespace MyEcommerce.Migrations.Application
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.CartId);
            
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        CartItemId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Product_ProductId = c.Int(),
                        Cart_CartId = c.Int(),
                    })
                .PrimaryKey(t => t.CartItemId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .ForeignKey("dbo.Carts", t => t.Cart_CartId)
                .Index(t => t.Product_ProductId)
                .Index(t => t.Cart_CartId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        CategoryId = c.Int(nullable: false),
                        Price = c.Long(nullable: false),
                        ProductStock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProductAttributes",
                c => new
                    {
                        ProductAttributeId = c.Int(nullable: false, identity: true),
                        AttributeName = c.String(),
                        AttributeNumericValue = c.Long(),
                        AttributeCharacterValue = c.String(),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductAttributeId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Long(nullable: false, identity: true),
                        UserId = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemId = c.Long(nullable: false, identity: true),
                        ProductPrice = c.Long(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Product_ProductId = c.Int(),
                        Order_OrderId = c.Long(),
                    })
                .PrimaryKey(t => t.OrderItemId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .Index(t => t.Product_ProductId)
                .Index(t => t.Order_OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.CartItems", "Cart_CartId", "dbo.Carts");
            DropForeignKey("dbo.CartItems", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.ProductAttributes", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.OrderItems", new[] { "Order_OrderId" });
            DropIndex("dbo.OrderItems", new[] { "Product_ProductId" });
            DropIndex("dbo.ProductImages", new[] { "Product_ProductId" });
            DropIndex("dbo.ProductAttributes", new[] { "Product_ProductId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.CartItems", new[] { "Cart_CartId" });
            DropIndex("dbo.CartItems", new[] { "Product_ProductId" });
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.ProductImages");
            DropTable("dbo.Categories");
            DropTable("dbo.ProductAttributes");
            DropTable("dbo.Products");
            DropTable("dbo.CartItems");
            DropTable("dbo.Carts");
        }
    }
}
