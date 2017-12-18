using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class ProvidersViewModel
    {
        public IEnumerable<Providers> Providers { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortProvidersViewModel SortViewModel { get; set; }
        public FilterProvidersViewModel FilterViewModel { get; set; }
    }
}
