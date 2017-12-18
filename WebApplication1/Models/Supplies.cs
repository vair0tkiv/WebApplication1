using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public partial class Supplies
    {
        public int IdSupple { get; set; }
        public int? IdProvider { get; set; }
        public decimal? Price { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateSupple { get; set; }
        public int? IdMaterial { get; set; }

        public Materials IdMaterialNavigation { get; set; }
        public Providers IdProviderNavigation { get; set; }
    }
}
