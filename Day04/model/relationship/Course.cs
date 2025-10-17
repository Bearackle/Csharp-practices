using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04.model.relationship.OneToOne
{
    public class Course
    {
        [Key]
        public int CourseId { set; get; }
        public string Name { set; get; }
        public virtual ICollection<Student> Students { get; set; }
        public Course()
        {
            Students = new HashSet<Student>();
        }
    }
}
