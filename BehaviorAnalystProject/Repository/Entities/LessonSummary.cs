using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class LessonSummary
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }
        public string Text { get; set; }

        // קשר עם ילד (סיכום שיעור שייך לילד מסוים)
        public int ChildId { get; set; }
        [ForeignKey("ChildId")]
        public Child child { get; set; }
    }
}
