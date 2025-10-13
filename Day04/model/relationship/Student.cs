using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04.model.relationship.OneToOne
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }   
        public virtual StudentAddress Address { get; set; }
        public virtual Grade Grade { get; set; }   
        public virtual ICollection<Course> Courses { get; set; }
        public Student()
        {
            Courses = new HashSet<Course>();
        }
    }
}
