﻿namespace MyEcommerce.Migrations.Application
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_address_for_orders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Address");
        }
    }
}
