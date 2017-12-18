using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public partial class Equipments
    {
        public Equipments()
        {
            Laboratories = new HashSet<Laboratories>();
        }

        public int IdEquipment { get; set; }
        public string NameEquipment { get; set; }
        [DataType (DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateRelease { get; set; }

        public ICollection<Laboratories> Laboratories { get; set; }
    }
}
