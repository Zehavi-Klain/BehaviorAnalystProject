using Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class AnalystDto
    {
        [Key]
        public string Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public virtual ICollection<ChildDto> Children { get; set; }
        public virtual ICollection<FormDto> Forms { get; set; }
    }
}
