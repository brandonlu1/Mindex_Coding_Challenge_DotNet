using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace challenge.Models
{
    public class Compensation
    {
        [Key]
        public String EmployeeId { get; set; } 
        public Employee employee { get; set; }
        public String salary { get; set; }
        public String effectiveDate { get; set; }

    }
}
