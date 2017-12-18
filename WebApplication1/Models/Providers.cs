using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public partial class Providers
    {
        public Providers()
        {
            Supplies = new HashSet<Supplies>();
        }

        public int IdProvider { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Adress { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }

        public ICollection<Supplies> Supplies { get; set; }
    }
}
