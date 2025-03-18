using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Documents
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FileUrl { get; set; }

        // Foreign Key for FormCategory
        public int? FormCategoryId { get; set; }
        [ForeignKey("FormCategoryId")]
        public FormCategory FormCategory { get; set; }
    }
}
