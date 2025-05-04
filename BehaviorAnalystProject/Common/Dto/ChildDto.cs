using Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class ChildDto
    {
        [Key]
        public string Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int LessonNumber { get; set; }
        public string ChildsDisability { get; set; }//לקות הילד
        public string FamilyPosition { get; set; }//מיקום במשפחה
        public string EducationalInstitution { get; set; }//מוסד לימודים
        public virtual ICollection<FormDto> ChildForms { get; set; }
        public virtual ICollection<LessonSummary> ChildLessonsSumery { get; set; }
        public virtual ICollection<Comment> ChildComments { get; set; }

        // Foreign Key for Analyst

        public string AnalystId { get; set; }
        [ForeignKey("AnalystId")]
        public Analyst Analyst { get; set; }
    }
}
