using Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class LessonSummaryDto
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int ChildId { get; set; }
    }
}
