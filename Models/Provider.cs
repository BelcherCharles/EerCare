using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EerCare.Models
{
    public class Provider : InheritedEntity
    {
        public int ProviderId { get; set; }
        [Required]
        [DisplayName("Name")]
        public string ProviderName { get; set; }
        //[Required]
        [DisplayName("Field")]
        public string ProviderType { get; set; }
        
    }
}
