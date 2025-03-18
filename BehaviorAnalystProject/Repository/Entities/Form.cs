using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Form
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string FileUrl { get; set; }
        public string FileName { get; set; }

        // Foreign Key for FormCategory
        public int FormCategoryId { get; set; }
        [ForeignKey("FormCategoryId")]
        public FormCategory FormCategory { get; set; }
    }
}
