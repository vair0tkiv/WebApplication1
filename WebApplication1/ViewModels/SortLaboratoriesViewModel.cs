using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class SortLaboratoriesViewModel
    {
        public LaboratoriesSortState NumberLaboratoryAscSort { get; private set; }
        public LaboratoriesSortState IdEquipmentAscSort { get; private set; }
        public LaboratoriesSortState Current { get; private set; }

        public SortLaboratoriesViewModel(LaboratoriesSortState sortOrder)
        {
            NumberLaboratoryAscSort = sortOrder == LaboratoriesSortState.NumberLaboratoryAsc ? LaboratoriesSortState.NumberLaboratoryDesc : LaboratoriesSortState.NumberLaboratoryAsc;
            IdEquipmentAscSort = sortOrder == LaboratoriesSortState.IdEquipmentAsc ? LaboratoriesSortState.IdEquipmentDesc : LaboratoriesSortState.IdEquipmentAsc;
            Current = sortOrder;
        }
    }
}
