using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Child
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Form> ChildForms { get; set; } 
        public virtual ICollection<Document> ChildDocument { get; set; } 

        // Foreign Key for Analyst
        
        public int AnalystId { get; set; }
        [ForeignKey("AnalystId")]
        public Analyst Analyst { get; set; }
    }
}
