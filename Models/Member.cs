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

        [DisplayName("Member Name")]
        public string FullName {
            get
            {
                return String.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        [DisplayName("Alphabetical By Last")]
        public string AlphaMemberByLast
        {
            get
            {
                return String.Format("{0}, {1} {2}", this.LastName, this.FirstName, this.MI);
            }
        }

        [Required]
        public string SSN { get; set; }

        public string DisplaySSN
        {
            get
            {
                return String.Format("{0}-{1}-{2}", this.SSN.Substring(0, 3), this.SSN.Substring(3, 2), this.SSN.Substring(5, 4));
            }
        }

        [Required]
        [DisplayName("Birth Date")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayName("Date of Decedence")]
        public DateTime? DOD { get; set; }
   
    }
}
