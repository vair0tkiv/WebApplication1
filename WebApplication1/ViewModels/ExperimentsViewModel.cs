using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class ExperimentsViewModel
    {
        public IEnumerable<Experiments> Experiments { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterExperimentsViewModel FilterViewModel { get; set; }
        public SortExperimentsViewModel SortViewModel { get; set; }
    }
}
