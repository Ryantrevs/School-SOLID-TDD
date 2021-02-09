using System;

namespace Case_School.Models
{
    public class StudentGrade
    {
        public string Id { get; set; }
        public decimal Average { get; set; }
        public String SubjectId { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }

        public StudentGrade()
        {

        }
        public StudentGrade(string id, decimal average, Student student, Subject subject)
        {
            Id = id;
            Average = average;
            Student = student;
            Subject = subject;
        }
    }
}