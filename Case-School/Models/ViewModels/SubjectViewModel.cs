using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Case_School.Models.ViewModels
{
    public class SubjectViewModel
    {
        public List<String> Names { get; set; } = new List<String>();
        public List<double> Weights { get; set; } = new List<double>();

        public SubjectViewModel(List<string> names, List<double> weights)
        {
            Names = names;
            Weights = weights;
        }
        public SubjectViewModel()
        {

        }
    }
}
