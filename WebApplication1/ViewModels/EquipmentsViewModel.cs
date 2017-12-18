using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class EquipmentsViewModel
    {
        public IEnumerable<Equipments> Equipments { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterEquipmentsViewModel FilterViewModel { get; set; }
        public SortEquipmentsViewModel SortViewModel { get; set; }
    }
}
