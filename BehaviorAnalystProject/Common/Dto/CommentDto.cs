using Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class CommentDto
    {
        public int ID { get; set; }

        public string Comments { get; set; }
        public string AccessPermission { get; set; }
        public DateTime Date { get; set; }
        public int ChildCode { get; set; }
    }
}
