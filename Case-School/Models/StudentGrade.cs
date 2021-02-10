using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Case_School.Models
{
    public class StudentGrade
    {
        public string Id { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Average { get; set; }
        public String SubjectId { get; set; }
        public int ProofNumber { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }

        public StudentGrade()
        {

        }
        public StudentGrade(string id, decimal average, int proofNumber, Student student, Subject subject)
        {
            Id = id;
            Average = average;
            ProofNumber = proofNumber;
            Student = student;
            Subject = subject;
        }
        public List<StudentGrade> GenerateGrade(IEnumerable<Student> students, IEnumerable<Subject> subjects)
        {
            List<StudentGrade> studentGrades = new List<StudentGrade>();
            Random random = new Random();
            foreach(var subject in subjects)
            {
                foreach(var student in students)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        decimal average = (decimal)(random.NextDouble() * random.Next(0, 10));
                        studentGrades.Add(new StudentGrade(Guid.NewGuid().ToString(), average, i+1, student, subject));
                    }
                }
            }
            return studentGrades;
        }
    }
}