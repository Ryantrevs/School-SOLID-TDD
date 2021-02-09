using System;
using System.Collections.Generic;

namespace Case_School.Models
{
    public class WeightProof
    {
        public String Id { get; set; }
        public decimal Value { get; set; }
        public virtual Subject Subject { get; set; }

        public WeightProof(string id, decimal value)
        {
            Id = id;
            Value = value;
        }
        public WeightProof()
        {

        }

        public List<WeightProof> generateListWeight(List<double> list)
        {
            List<WeightProof> weightProofs = new List<WeightProof>();
            foreach(var item in list)
            {
                weightProofs.Add(new WeightProof(Guid.NewGuid().ToString(), (decimal)item));
            }
            return weightProofs;
        }
    }
}