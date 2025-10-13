using Day04.model;
using Day04.model.relationship.OneToOne;
using System;
using System.Data.Entity;

namespace Day04
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> products { get; set; }
        public DbSet<Brand> brands { get; set; }    
        public DbSet<Student> students { set; get; }
        public DbSet<StudentAddress> addresses { set; get; }
        public DbSet<Grade> grades { set; get; }
        public DbSet<Course> courses { set; get; }
        public AppDbContext() : base("DefaultConnection") {
            this.Database.Log = Console.WriteLine;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Day04.Migrations.Configuration>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                        .Map<FulltimeEmployee>(m => m.Requires("EmployeeType").HasValue("FullTime"))
                        .Map<PartimeEmployee>(m => m.Requires("EmployeeType").HasValue("PartTime"));

            modelBuilder.Entity<Student>()
                .HasMany<Course>(s => s.Courses)
                .WithMany(c => c.Students)
                .Map(cs =>
                {
                    cs.MapLeftKey("StudentRefId");
                    cs.MapRightKey("CourseRefId");
                    cs.ToTable("CourseStudentPivot");
                });
        }
    }
}
