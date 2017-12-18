using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class FilterLaboratoriesViewModel
    {
        public int? SelectedNumberLaboratory { get; set; }
        public string SelectedIdEquipment { get; set; }

        public FilterLaboratoriesViewModel(int? numberlaboratory, string nameequipment)
        {
            this.SelectedNumberLaboratory = numberlaboratory;
            this.SelectedIdEquipment = nameequipment;
        }
    }
}
