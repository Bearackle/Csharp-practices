namespace Day04.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Day04.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "Day04.ProductContext";
        }

        protected override void Seed(Day04.AppDbContext context)
        {
            //var grade = context.grades.Add(new model.relationship.OneToOne.Grade()
            //{
            //    GradeName = "Lop 7"
            //});
            //var student = context.students.Add(new model.relationship.OneToOne.Student()
            //{
            //    Name = "trandinh",
            //    Grade = grade,
            //});
            //context.addresses.Add(new model.relationship.OneToOne.StudentAddress()
            //{
            //    StudentAddressId = student.StudentId ,
            //    Address1 = "truong cong dinh 138 22",
            //    Address2 = "truong chinh 22",
            //    City = "hcm",
            //    State = "hcm",
            //});
            //var course = context.courses.Add(new model.relationship.OneToOne.Course()
            //{
            //    Name = "csdl phan tan"
            //});
            //context.students.Find(4).Courses.Add(course);

        }
    }
}
