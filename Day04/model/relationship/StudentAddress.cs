using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04.model.relationship.OneToOne
{
    public class StudentAddress
    {
        [ForeignKey("Student")]
        public int StudentAddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 {  get; set; }
        public string City {  get; set; }
        public string State { get; set; }
        public virtual Student Student { set; get; }
    }
}
