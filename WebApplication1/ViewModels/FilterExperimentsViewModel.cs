using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class FilterExperimentsViewModel
    {
        public int SelectedIdExperiment { get; set; }
        public string SelectedIdSample { get; set; }
        public DateTime? SelectedDates { get; set; }
        public TimeSpan? SelectedStartTime { get; set; }
        public TimeSpan? SelectedEndTime { get; set; }
        public double? SelectedSupposeMass { get; set; }
        public double? SelectedReceiveMass { get; set; }
        public int? SelectedIdLaboratory { get; set; }
        public string SelectedIdWorker { get; set; }

        public FilterExperimentsViewModel(int idexperiment ,string namesample, DateTime? dates, TimeSpan? starttime, TimeSpan? endtime, double? supposemass, double? receivemass, int? numberlaboratory, string surname)
        {
            this.SelectedIdExperiment = idexperiment;
            this.SelectedIdSample = namesample;
            this.SelectedDates = dates;
            this.SelectedStartTime = starttime;
            this.SelectedEndTime = endtime;
            this.SelectedSupposeMass = supposemass;
            this.SelectedReceiveMass = receivemass;
            this.SelectedIdLaboratory = numberlaboratory;
            this.SelectedIdWorker = surname;
        }
    }
}
