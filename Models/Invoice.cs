using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EerCare.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        [Required]
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public List<SelectListItem> allProviders { get; set; }
        [Required]
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public List<SelectListItem> allMembers { get; set; }
        [Required]
        [DisplayName("Invoice Date")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime SettledDate { get; set; }
    }
}
