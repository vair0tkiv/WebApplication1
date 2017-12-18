using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public partial class Experiments
    {
        public int IdExperiment { get; set; }
        public int? IdSample { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Dates { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public double? SupposeMass { get; set; }
        public double? ReceiveMass { get; set; }
        public int? IdLaboratory { get; set; }
        public int? IdWorker { get; set; }

        public Laboratories IdLaboratoryNavigation { get; set; }
        public Samples IdSampleNavigation { get; set; }
        public Workers IdWorkerNavigation { get; set; }
    }
}
