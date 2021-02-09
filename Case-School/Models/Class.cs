using System;
using System.Collections.Generic;

namespace Case_School.Models
{
    public class Class
    {
        public String Id { get; set; }
        public virtual IList<Student> Students { get; set; }

        public Class()
        {

        }
        public Class(string id, List<Student> students)
        {
            Id = id;
            Students = new List<Student>(students);
        }
        
        public Class GenerateClass(List<Student> students)
        {
            Class newClass = new Class(Guid.NewGuid().ToString(), students);
            return newClass;
        }
    }
}