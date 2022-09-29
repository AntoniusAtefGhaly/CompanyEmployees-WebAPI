using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Entities.DataTransferObjects
{
    public class EmployeeForManipulationDto
    {
        [Required(ErrorMessage = "Employee name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Employee age is a required field.")]
        [Range(20, 70, ErrorMessage = "employee age out of rang it should be ")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Potsion age is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Name is 20 characters")]
        public string Position { get; set; }
    }
}
