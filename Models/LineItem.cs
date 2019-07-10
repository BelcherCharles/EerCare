using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EerCare.Models
{
    public class LineItem
    {
        public int LineItemId { get; set; }
        public int InvoiceId { get; set; }
        [Required]
        [DisplayName("Procedure Code")]
        public string ProcedureCode { get; set; }
        [Required]
        [DisplayName("Procedure Description")]
        public string ProcedureDesc { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double AmtBilled { get; set; }
        [DataType(DataType.Currency)]
        public double? AmtSettled { get; set; }
    }
}
