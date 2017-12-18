using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class FilterEquipmentsViewModel
    {
        public string SelectedNameEquipment { get; set; }
        public string SelectedDateRelease { get; set; }

        public FilterEquipmentsViewModel(string nameequipment, string daterelease)
        {
            this.SelectedNameEquipment = nameequipment;
            this.SelectedDateRelease = daterelease;
        }
    }
}
