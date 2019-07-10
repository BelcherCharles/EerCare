using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EerCare.Models.ViewModels
{
    public class InvoiceViewModel
    {
        public int InvoiceId { get; set; }

        public Invoice Invoice { get; set; }

        [DisplayName("Provider Name")]
        public List<SelectListItem> allProviders { get; set; }

        [DisplayName("Member Name")]
        public List<SelectListItem> allMembers { get; set; }
    }
}
