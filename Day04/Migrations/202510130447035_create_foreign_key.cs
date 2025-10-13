namespace Day04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_foreign_key : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.products", "BrandId", "dbo.Brand");
        }

        public override void Down()
        {
        }
    }
}
