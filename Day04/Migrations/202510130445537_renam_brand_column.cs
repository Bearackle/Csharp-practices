namespace Day04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renam_brand_column : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.products", "brand", "BrandId");
        }
        
        public override void Down()
        {
        }
    }
}
