using Cw4.DTOs;
using Cw4.DTOs.Requests;
using Cw4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.DAL
{
    public interface IEnrollmentDbService
    {
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
        public bool PromoteStudents(PromoteStudentRequest request);


    }
}
