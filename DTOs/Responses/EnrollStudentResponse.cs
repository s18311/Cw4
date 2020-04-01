using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.DTOs
{
    public class EnrollStudentResponse
    {
        public string IndexNumber { get; set; }
        public string LastName { get; set; }
        public int Semester { get; set; }
        public DateTime StartDate { get; set; }
        
    }
}
