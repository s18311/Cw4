using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw4.DAL;
using Cw4.DTOs;
using Cw4.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Cw4.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        private IEnrollmentDbService _service;
        public EnrollmentsController(IEnrollmentDbService service)
        {
            _service = service;
        }

        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            var response = _service.EnrollStudent(request);

            if(response == null)
            {
                return BadRequest();
            }
            return Ok(response);
            //return Ok(201);
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudent(PromoteStudentRequest request)
        {
            var reposne = _service.PromoteStudents(request);
            if (!reposne)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}