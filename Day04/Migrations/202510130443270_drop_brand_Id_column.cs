namespace Day04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drop_brand_Id_column : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.products", "Brand_Id");
        }
        
        public override void Down()
        {
        }
    }
}
