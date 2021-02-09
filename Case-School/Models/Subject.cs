using System;
using System.Collections.Generic;

namespace Case_School.Models
{
    public class Subject
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public virtual List<WeightProof> WeightProof { get; set; }
        public virtual StudentGrade StudentGrade { get; set; }
        public virtual StudentAverages StudentAverage { get; set; }
       

        public Subject(string id, string name, List<WeightProof> weightProof)
        {
            Id = id;
            Name = name;
            WeightProof = weightProof;
        }
        public Subject()
        {

        }
    }
}