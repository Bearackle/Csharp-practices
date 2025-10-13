namespace Day04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_grade_table_and_in_student : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        GradeId = c.Int(nullable: false, identity: true),
                        GradeName = c.String(),
                    })
                .PrimaryKey(t => t.GradeId);
            
            AddColumn("dbo.Students", "Grade_GradeId", c => c.Int());
            CreateIndex("dbo.Students", "Grade_GradeId");
            AddForeignKey("dbo.Students", "Grade_GradeId", "dbo.Grades", "GradeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Grade_GradeId", "dbo.Grades");
            DropIndex("dbo.Students", new[] { "Grade_GradeId" });
            DropColumn("dbo.Students", "Grade_GradeId");
            DropTable("dbo.Grades");
        }
    }
}
