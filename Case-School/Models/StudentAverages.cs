using System;

namespace Case_School.Models
{
    public class StudentAverages
    {
        public string Id { get; set; }
        public decimal Average { get; set; }
        public String SubjectId { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }

    }
}