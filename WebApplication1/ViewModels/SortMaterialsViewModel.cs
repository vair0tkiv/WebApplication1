using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class SortMaterialsViewModel
    {
        public MaterialsSortState NameMaterialAscSort { get; private set; }
        public MaterialsSortState MassMaterialAscSort { get; private set; }
        public MaterialsSortState UnitssAscSort { get; private set; }
        public MaterialsSortState PropertyAscSort { get; private set; }
        public MaterialsSortState Current { get; private set; }

        public SortMaterialsViewModel(MaterialsSortState sortOrder)
        {
            NameMaterialAscSort = sortOrder == MaterialsSortState.NameMaterialAsc ? MaterialsSortState.NameMaterialDesc : MaterialsSortState.NameMaterialAsc;
            MassMaterialAscSort = sortOrder == MaterialsSortState.MassMaterialAsc ? MaterialsSortState.MassMaterialDesc : MaterialsSortState.MassMaterialAsc;
            UnitssAscSort = sortOrder == MaterialsSortState.UnitsAsc ? MaterialsSortState.UnitsDesc : MaterialsSortState.UnitsAsc;
            PropertyAscSort = sortOrder == MaterialsSortState.PropertyAsc ? MaterialsSortState.PropertyDesc : MaterialsSortState.PropertyAsc;
            Current = sortOrder;
        }
    }
}
