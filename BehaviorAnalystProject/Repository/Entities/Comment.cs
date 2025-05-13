using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Comment
    {
        public int ID { get; set; }

        public string Comments { get; set; }
        public string AccessPermission { get; set; }
        public DateTime Date { get; set; }

        // קשר עם ילד (כל הערה משויכת לילד מסוים)
        public int ChildCode { get; set; }
        [ForeignKey("ChildCode")]
        public Child child { get; set; }
    }
}
