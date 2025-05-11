using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Child
    {
        [Key]
        public int Code { get; set; }
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
        public bool IsActive { get; set; } = true;//האם משתמש פעיל או לא
        public virtual ICollection<Form>? ChildForms { get; set; } 
        public virtual ICollection<LessonSummary>? ChildLessonsSumery { get; set; } 
        public virtual ICollection<Comment>? ChildComments { get; set; } 

        // Foreign Key for Analyst
        
        public int AnalystId { get; set; }
        [ForeignKey("AnalystId")]
        public Analyst Analyst { get; set; }
    }
}
