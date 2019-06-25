using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EerCare.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public List<LineItem> InvoiceItems { get; set; } = new List<LineItem>();
        public DateTime InvoiceDate { get; set; }
        public DateTime SettledDate { get; set; }
    }
}
