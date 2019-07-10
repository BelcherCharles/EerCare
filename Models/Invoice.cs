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
        [DisplayName("Invoice Id")]
        public int InvoiceId { get; set; }

        [Required]
        public int ProviderId { get; set; }

        public Provider Provider { get; set; }
        
        [Required]
        public int MemberId { get; set; }

        public Member Member { get; set; }
        
        [Required]
        [DisplayName("Invoice Date")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Settled Date")]
        public DateTime? SettledDate { get; set; }

        [DisplayName("Line Items")]
        public List<LineItem> InvoiceLineItems { get; set; }
    }
}
