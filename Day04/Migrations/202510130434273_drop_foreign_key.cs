namespace Day04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drop_foreign_key : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.products", "Brand_Id", "dbo.Brand");
            DropIndex("dbo.products", new[] { "Brand_Id" });
        }

        public override void Down()
        {
        }
    }
}
