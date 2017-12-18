using System;
using System.Collections.Generic;

namespace WebApplication1
{
    public partial class Materials
    {
        public Materials()
        {
            Samples = new HashSet<Samples>();
            Supplies = new HashSet<Supplies>();
        }

        public int IdMaterial { get; set; }
        public string NameMaterial { get; set; }
        public double? MassMaterial { get; set; }
        public string Units { get; set; }
        public string Property { get; set; }

        public ICollection<Samples> Samples { get; set; }
        public ICollection<Supplies> Supplies { get; set; }
    }
}
