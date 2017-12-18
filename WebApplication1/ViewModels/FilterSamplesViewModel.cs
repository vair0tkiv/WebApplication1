using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class FilterSamplesViewModel
    {
        public string SelectedMaterial { get; set; }
        public double? SelectedMass { get; set; }
        public string SelectedProperty { get; set; }

        public FilterSamplesViewModel(string material, double? mass, string property)
        {
            this.SelectedMass = mass;
            this.SelectedMaterial = material;
            this.SelectedProperty = property;
        }
    }
}
