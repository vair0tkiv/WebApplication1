using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class SortSamplesViewModel
    {
        public SamplesSortState NameSampleAscSort { get; private set; }
        public SamplesSortState MassSampleAscSort { get; private set; }
        public SamplesSortState IdMaterialAscSort { get; private set; }
        public SamplesSortState Current { get; private set; }

        public SortSamplesViewModel(SamplesSortState sortOrder)
        {
            NameSampleAscSort = sortOrder == SamplesSortState.NameSampleAsc ? SamplesSortState.NameSampleDesc : SamplesSortState.NameSampleAsc;
            MassSampleAscSort = sortOrder == SamplesSortState.MassSampleAsc ? SamplesSortState.MassSampleDesc : SamplesSortState.MassSampleAsc;
            IdMaterialAscSort = sortOrder == SamplesSortState.IdMaterialAsc ? SamplesSortState.IdMaterialDesc : SamplesSortState.IdMaterialAsc;
            Current = sortOrder;
        }
    }
}
