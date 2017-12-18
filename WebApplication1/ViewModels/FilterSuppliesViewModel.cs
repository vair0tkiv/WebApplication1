using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class FilterSuppliesViewModel
    {
        public string SelectedDateSupple { get; set; }
        public string SelectedStartDate { get; set; }
        public string SelectedEndDate { get; set; }
        public string SelectedNameMaterial { get; set; }
        public double? SelectedMassMaterial { get; set; }
        public decimal? SelectedPrice { get; set; }
        public string SelectedIdProvider { get; set; }

        public FilterSuppliesViewModel(string datesupple, string startdate, string enddate, string namematerial, double? massmaterial, decimal? price, string surname)
        {
            this.SelectedDateSupple = datesupple;
            this.SelectedStartDate = startdate;
            this.SelectedEndDate = enddate;
            this.SelectedNameMaterial = namematerial;
            this.SelectedMassMaterial = massmaterial;
            this.SelectedPrice = price;
            this.SelectedIdProvider = surname;
        }
    }
}
