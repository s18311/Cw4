using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.DTOs.Requests
{
    public class PromoteStudentRequest
    {
        [Required(ErrorMessage = "Musisz podac semestr ktory chcesz awansowac")]
        public int Semester { get; set; }

        [Required(ErrorMessage = "Musisz podac nazwe studiów")]
        [MaxLength(40)]
        public string StudiesName { get; set; }
    }
}
