using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
     public class FilterProvidersViewModel
        {
            public string SelectedSurname { get; set; }
            public string SelectedFirstname { get; set; }
            public string SelectedAdress { get; set; }
            public string SelectedBirthday { get; set; }

        public FilterProvidersViewModel(string surname, string firstname, string adress, string birthday)
            {
                this.SelectedSurname = surname;
                this.SelectedFirstname = firstname;
                this.SelectedAdress = adress;
                this.SelectedBirthday = birthday;
            }
        }
}
