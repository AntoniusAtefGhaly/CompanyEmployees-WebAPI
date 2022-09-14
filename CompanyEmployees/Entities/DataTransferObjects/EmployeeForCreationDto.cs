using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class EmployeeForCreationDto
    { 
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
    }
}
