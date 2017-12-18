using System;
using System.Collections.Generic;

namespace WebApplication1
{
    public partial class Laboratories
    {
        public Laboratories()
        {
            Experiments = new HashSet<Experiments>();
        }

        public int IdLaboratory { get; set; }
        public int? NumberLaboratory { get; set; }
        public int? IdEquipment { get; set; }

        public Equipments IdEquipmentNavigation { get; set; }
        public ICollection<Experiments> Experiments { get; set; }
    }
}
