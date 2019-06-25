using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EerCare.Models
{
    public class Provider : InheritedEntity
    {
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        
    }
}
