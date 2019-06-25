using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EerCare.Models
{
    public class Member : InheritedEntity
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string FirstName { get; set; }

        public string MI { get; set; }

        [Required]
        [DisplayName("Surname")]
        public string LastName { get; set; }

        [Required]
        public string SSN { get; set; }

        [Required]
        [DisplayName("Birth Date")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayName("Date of Decedence")]
        public DateTime? DOD { get; set; }
   
    }
}
