using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EerCare.Models
{
    public class InheritedEntity
    {
        [Required]
        [DisplayName("Address")]
        public string Address1 { get; set; }

        [DisplayName("Unit / PO Box")]
        public string Address2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Zip { get; set; }

        public string Phone { get; set; }

        [DisplayName("Mobile Phone")]
        public string MobilePhone { get; set; }

        public string Email { get; set; }

        [DisplayName("Member Since")]
        [DataType(DataType.Date)]
        public DateTime Since { get; set; }

        [DisplayName("Archival Date")]
        [DataType(DataType.Date)]
        public DateTime? Archived { get; set; }

    }
}
