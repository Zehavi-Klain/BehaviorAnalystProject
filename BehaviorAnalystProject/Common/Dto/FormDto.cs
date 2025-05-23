using Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;



namespace Common.Dto
{
    public class FormDto
    {
        public int? Id { get; set; }
        public string CategoryName { get; set; }
        public byte[]? ArrFile { get; set; }
        public IFormFile? FormFile { get; set; }
        public string FileName { get; set; }
        // Foreign Key for FormCategory
        public int? ChildID { get; set; }
        public int? AnalystID { get; set; }
        public int FormCategoryId { get; set; }
    }
}
