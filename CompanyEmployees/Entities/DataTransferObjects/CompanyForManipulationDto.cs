using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CompanyForManipulationDto
    {
        [Required(ErrorMessage = "company name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "company address is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the address is 100 characters.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "company country is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the country is 30 characters.")]
        public string Country { get; set; }
    }
}
