using System;
using System.Collections.Generic;

namespace WebApplication1
{
    public partial class Samples
    {
        public Samples()
        {
            Experiments = new HashSet<Experiments>();
        }

        public int IdSample { get; set; }
        public string NameSample { get; set; }
        public double? MassSample { get; set; }
        public int? IdMaterial { get; set; }

        public Materials IdMaterialNavigation { get; set; }
        public ICollection<Experiments> Experiments { get; set; }
    }
}
