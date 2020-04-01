using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.DTOs
{
    public class EnrollStudentRequest
    {   
        [Required(ErrorMessage ="Musisz podac numer albumu w formacie sXXXXX gdzie X to liczba")]
        [RegularExpression("^s[0-9]+$")]
        public string IndexNumber { get; set; }

        [Required(ErrorMessage = "Musisz podac imie")]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Musisz podac Nazwisko")]
        [MaxLength(60)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Musisz podac Date Urodzenia")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Musisz podac nazwe studiów")]
        [MaxLength(40)]
        public string StudiesName { get; set; }
     }
}
