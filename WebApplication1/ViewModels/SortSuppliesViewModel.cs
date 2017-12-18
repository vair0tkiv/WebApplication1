using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class SortSuppliesViewModel
    {
        public SuppliesSortState IdProviderAscSort { get; private set; }
        public SuppliesSortState PriceAscSort { get; private set; }
        public SuppliesSortState DateSuppleAscSort { get; private set; }
        public SuppliesSortState IdMaterialAscSort { get; private set; }
        public SuppliesSortState Current { get; private set; }

        public SortSuppliesViewModel(SuppliesSortState sortOrder)
        { 
            IdProviderAscSort = sortOrder == SuppliesSortState.IdProviderAsc ? SuppliesSortState.IdProviderDesc : SuppliesSortState.IdProviderAsc;
            PriceAscSort = sortOrder == SuppliesSortState.PriceAsc ? SuppliesSortState.PriceDesc : SuppliesSortState.PriceAsc;
            DateSuppleAscSort = sortOrder == SuppliesSortState.DateSuppleAsc ? SuppliesSortState.DateSuppleDesc : SuppliesSortState.DateSuppleAsc;
            IdMaterialAscSort = sortOrder == SuppliesSortState.IdMaterialAsc ? SuppliesSortState.IdMaterialDesc : SuppliesSortState.IdMaterialAsc;
            Current = sortOrder;
        }
    }
}
