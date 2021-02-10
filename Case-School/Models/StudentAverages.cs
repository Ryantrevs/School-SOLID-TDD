using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Case_School.Models
{
    public class StudentAverages
    {
        public string Id { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Average { get; set; }
        public String SubjectId { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }

        public StudentAverages()
        {

        }

        public StudentAverages(string id, decimal average, Student student, Subject subject)
        {
            Id = id;
            Average = average;
            Student = student;
            Subject = subject;
        }
    }
}