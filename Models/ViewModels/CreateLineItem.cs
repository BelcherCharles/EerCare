using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EerCare.Models.ViewModels
{
    public class CreateLineItem
    {
        public int InvoiceId { get; set; }
        
        public LineItem LineItem { get; set; }
    }
}
