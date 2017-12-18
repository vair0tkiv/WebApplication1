using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class WorkersViewModel
    {
        public IEnumerable<Workers> Workers { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortWorkersViewModel SortViewModel { get; set; }
        public FilterWorkersViewModel FilterViewModel { get; set; }
    }
}
