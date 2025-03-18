using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Analyst
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Child> Children { get; set; }
        public virtual ICollection<Form> Forms { get; set; }
    }
}
