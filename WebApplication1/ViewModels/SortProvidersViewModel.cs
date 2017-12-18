using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class SortProvidersViewModel
    {
        public ProvidersSortState SurnameAscSort { get; private set; }
        public ProvidersSortState FirstnameAscSort { get; private set; }
        public ProvidersSortState AdressAscSort { get; private set; }
        public ProvidersSortState BirthdayAscSort { get; private set; }
        public ProvidersSortState Current { get; private set; }

        public SortProvidersViewModel(ProvidersSortState sortOrder)
        {
            SurnameAscSort = sortOrder == ProvidersSortState.SurnameAsc ? ProvidersSortState.SurnameDesc : ProvidersSortState.SurnameAsc;
            FirstnameAscSort = sortOrder == ProvidersSortState.FirstnameAsc ? ProvidersSortState.FirstnsmeDesc : ProvidersSortState.FirstnameAsc;
            AdressAscSort= sortOrder == ProvidersSortState.AdressAsc ? ProvidersSortState.AdressDesc : ProvidersSortState.AdressAsc;
            BirthdayAscSort = sortOrder == ProvidersSortState.BirthdayAsc ? ProvidersSortState.BirthdayDesc : ProvidersSortState.BirthdayAsc;
            Current = sortOrder;
        }
    }
}
