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
        public int? ChildCode { get; set; }
        [ForeignKey("ChildCode")]
        public Child Child { get; set; }
        public int? AnalystCode { get; set; }
        [ForeignKey("AnalystCode")]
        public Analyst Analyst { get; set; }

        public int FormCategoryCode { get; set; }
        [ForeignKey("FormCategoryCode")]
        public FormCategory FormCategory { get; set; }
    }
}
