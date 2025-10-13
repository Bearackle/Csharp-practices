namespace Day04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_pending_change : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CourseStudents", newName: "CourseStudentPivot");
            RenameColumn(table: "dbo.CourseStudentPivot", name: "Course_CourseId", newName: "CourseRefId");
            RenameColumn(table: "dbo.CourseStudentPivot", name: "Student_StudentId", newName: "StudentRefId");
            RenameIndex(table: "dbo.CourseStudentPivot", name: "IX_Student_StudentId", newName: "IX_StudentRefId");
            RenameIndex(table: "dbo.CourseStudentPivot", name: "IX_Course_CourseId", newName: "IX_CourseRefId");
            DropPrimaryKey("dbo.CourseStudentPivot");
            AddPrimaryKey("dbo.CourseStudentPivot", new[] { "StudentRefId", "CourseRefId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CourseStudentPivot");
            AddPrimaryKey("dbo.CourseStudentPivot", new[] { "Course_CourseId", "Student_StudentId" });
            RenameIndex(table: "dbo.CourseStudentPivot", name: "IX_CourseRefId", newName: "IX_Course_CourseId");
            RenameIndex(table: "dbo.CourseStudentPivot", name: "IX_StudentRefId", newName: "IX_Student_StudentId");
            RenameColumn(table: "dbo.CourseStudentPivot", name: "StudentRefId", newName: "Student_StudentId");
            RenameColumn(table: "dbo.CourseStudentPivot", name: "CourseRefId", newName: "Course_CourseId");
            RenameTable(name: "dbo.CourseStudentPivot", newName: "CourseStudents");
        }
    }
}
