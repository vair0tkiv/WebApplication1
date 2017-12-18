using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public partial class Workers
    {
        public Workers()
        {
            Experiments = new HashSet<Experiments>();
        }

        public int IdWorker { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Adress { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }

        public ICollection<Experiments> Experiments { get; set; }
    }
}
