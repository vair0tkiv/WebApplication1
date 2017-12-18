using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class SortEquipmentsViewModel
    {
        public EquipmentsSortState NameEquipmentAscSort { get; private set; }
        public EquipmentsSortState DateReleaseAscSort { get; private set; }
        public EquipmentsSortState Current { get; private set; }

        public SortEquipmentsViewModel(EquipmentsSortState sortOrder)
        {
            NameEquipmentAscSort = sortOrder == EquipmentsSortState.NameEquipmentAsc ? EquipmentsSortState.NameEquipmentDesc : EquipmentsSortState.NameEquipmentAsc;
            DateReleaseAscSort = sortOrder == EquipmentsSortState.DateReleaseAsc ? EquipmentsSortState.DateReleaseDesc : EquipmentsSortState.DateReleaseAsc;
            Current = sortOrder;
        }
    }
}
