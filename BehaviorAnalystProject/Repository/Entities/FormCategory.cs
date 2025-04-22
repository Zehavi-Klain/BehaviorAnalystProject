using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class FormCategory
    {
        [Key]
        public int Code { get; set; }
        public string CategoryName { get; set; }
    }
}
