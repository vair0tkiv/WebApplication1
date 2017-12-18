using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class SamplesViewModel
    {
        public IEnumerable<Samples> Samples { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterSamplesViewModel FilterViewModel { get; set; }
        public SortSamplesViewModel SortViewModel { get; set; }
    }
}
