namespace Day04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_course_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.CourseStudents",
                c => new
                    {
                        Course_CourseId = c.Int(nullable: false),
                        Student_StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Course_CourseId, t.Student_StudentId })
                .ForeignKey("dbo.Courses", t => t.Course_CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_StudentId, cascadeDelete: true)
                .Index(t => t.Course_CourseId)
                .Index(t => t.Student_StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseStudents", "Student_StudentId", "dbo.Students");
            DropForeignKey("dbo.CourseStudents", "Course_CourseId", "dbo.Courses");
            DropIndex("dbo.CourseStudents", new[] { "Student_StudentId" });
            DropIndex("dbo.CourseStudents", new[] { "Course_CourseId" });
            DropTable("dbo.CourseStudents");
            DropTable("dbo.Courses");
        }
    }
}
