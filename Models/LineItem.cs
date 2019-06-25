using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EerCare.Models
{
    public class LineItem
    {
        public int LineItemId { get; set; }
        public int InvoiceId { get; set; }
        public double ProcedureCode { get; set; }
        public string ProcedureDesc { get; set; }
        public double AmtBilled { get; set; }
        public double AmtSettled { get; set; }
    }
}
