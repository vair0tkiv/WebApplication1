using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class MaterialsViewModel
    {
        public IEnumerable<Materials> Materials { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterMaterialsViewModel FilterViewModel { get; set; }
        public SortMaterialsViewModel SortViewModel { get; set; }
    }
}
