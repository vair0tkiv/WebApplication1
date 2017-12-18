﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class LaboratoriesViewModel
    {
        public IEnumerable<Laboratories> Laboratories { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterLaboratoriesViewModel FilterViewModel { get; set; }
        public SortLaboratoriesViewModel SortViewModel { get; set; }
    }
}