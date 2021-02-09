using System;
using System.Collections.Generic;

namespace Case_School.Models
{
    public class Student
    {
        public virtual IList<StudentGrade> StudentGrades { get; set; }
        public virtual IList<StudentAverages> StudentAverages { get; set; }
        public string Registration { get; set; }
        public bool Accredited { get; set; }
        public virtual Class Class { get; set; }

        public Student(string registration, bool accredited)
        {
            Registration = registration;
            Accredited = accredited;
        }
        public Student()
        {

        }


        public List<Student> GenerateStudent(int quant)
        {
            List<Student> students = new List<Student>();
            for (int i = 0; i < quant; i++)
            {
                Student student = new Student(Guid.NewGuid().ToString(), false);
                students.Add(student);
            }
            return students;
        }
    }
}
