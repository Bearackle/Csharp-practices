namespace Day04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename_BrandId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.products", "Brand_Id");
            RenameColumn("dbo.products", "brand", "BrandId");
        }
        
        public override void Down()
        {

        }
    }
}
