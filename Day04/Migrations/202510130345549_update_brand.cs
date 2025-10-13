namespace Day04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_brand : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brand",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String()
                }
                ).PrimaryKey(t => t.Id) ;
        }
        
        public override void Down()
        {
            DropTable("dbo.Brands");
        }
    }
}
