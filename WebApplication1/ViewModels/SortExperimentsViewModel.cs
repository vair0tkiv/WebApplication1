using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class SortExperimentsViewModel
    {
        public ExperimentsSortState IdExperimentAscSort { get; private set; }
        public ExperimentsSortState IdSampleAscSort { get; private set; }
        public ExperimentsSortState DatesAscSort { get; private set; }
        public ExperimentsSortState StartTimeAscSort { get; private set; }
        public ExperimentsSortState EndTimeAscSort { get; private set; }
        public ExperimentsSortState SupposeMassAscSort { get; private set; }
        public ExperimentsSortState ReceiveMassAscSort { get; private set; }
        public ExperimentsSortState IdLaboratoryAscSort { get; private set; }
        public ExperimentsSortState IdWorkerAscSort { get; private set; }
        public ExperimentsSortState Current { get; private set; }

        public SortExperimentsViewModel(ExperimentsSortState sortOrder)
        {
            IdExperimentAscSort = sortOrder == ExperimentsSortState.IdExperimentAsc ? ExperimentsSortState.IdExperimentDesc : ExperimentsSortState.IdExperimentAsc;
            IdSampleAscSort = sortOrder == ExperimentsSortState.IdSampleAsc ? ExperimentsSortState.IdSampleDesc : ExperimentsSortState.IdSampleAsc;
            DatesAscSort = sortOrder == ExperimentsSortState.DatesAsc ? ExperimentsSortState.DatesDesc : ExperimentsSortState.DatesAsc;
            StartTimeAscSort = sortOrder == ExperimentsSortState.StartTimeAsc ? ExperimentsSortState.StartTimeDesc : ExperimentsSortState.StartTimeAsc;
            EndTimeAscSort = sortOrder == ExperimentsSortState.EndTimeAsc ? ExperimentsSortState.EndTimeDesc : ExperimentsSortState.EndTimeAsc;
            SupposeMassAscSort = sortOrder == ExperimentsSortState.SupposeMassAsc ? ExperimentsSortState.SupposeMassDesc : ExperimentsSortState.SupposeMassAsc;
            ReceiveMassAscSort = sortOrder == ExperimentsSortState.ReceiveMassAsc ? ExperimentsSortState.ReceiveMassDesc : ExperimentsSortState.ReceiveMassAsc;
            IdLaboratoryAscSort = sortOrder == ExperimentsSortState.IdLaboratoryAsc ? ExperimentsSortState.IdLaboratoryDesc : ExperimentsSortState.IdLaboratoryAsc;
            IdWorkerAscSort = sortOrder == ExperimentsSortState.IdWorkerAsc ? ExperimentsSortState.IdWorkerDesc : ExperimentsSortState.IdWorkerAsc;
            Current = sortOrder;
        }
    }
}
