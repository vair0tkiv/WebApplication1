using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class FilterMaterialsViewModel
    {
        public string SelectedNameMaterial { get; set; }
        public double? SelectedMassMaterial { get; set; }
        public string SelectedUnits { get; set; }
        public string SelectedProperty { get; set; }

        public FilterMaterialsViewModel(string namematerial, double? massmaterial, string units, string property)
        {
            this.SelectedNameMaterial = namematerial;
            this.SelectedMassMaterial = massmaterial;
            this.SelectedUnits = units;
            this.SelectedProperty = property;
        }
    }
}
