using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class SortWorkersViewModel
    {
        public WorkersSortState SurnameAscSort { get; private set; }
        public WorkersSortState FirstnameAscSort { get; private set; }
        public WorkersSortState AdressAscSort { get; private set; }
        public WorkersSortState BirthdayAscSort { get; private set; }
        public WorkersSortState Current { get; private set; }

        public SortWorkersViewModel(WorkersSortState sortOrder)
        {
            SurnameAscSort = sortOrder == WorkersSortState.SurnameAsc ? WorkersSortState.SurnameDesc : WorkersSortState.SurnameAsc;
            FirstnameAscSort = sortOrder == WorkersSortState.FirstnameAsc ? WorkersSortState.FirstnsmeDesc : WorkersSortState.FirstnameAsc;
            AdressAscSort = sortOrder == WorkersSortState.AdresslAsc ? WorkersSortState.AdresslDesc : WorkersSortState.AdresslAsc;
            BirthdayAscSort = sortOrder == WorkersSortState.BirthdayAsc ? WorkersSortState.BirthdayDesc : WorkersSortState.BirthdayAsc;
            Current = sortOrder;
        }
    }
}
