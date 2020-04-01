using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Cw4.DAL;
using Cw4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cw4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            return Ok(_dbService.GetStudents());

        }

        [HttpGet("{id}")]
        public IActionResult GetStudentSemester(string id)
        {
            Student st = _dbService.getStudent(id);
            string tmp = JsonSerializer.Serialize(st);
            return Ok(tmp);
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            return Ok(student);
        }


        [HttpPut("{id}")]
        public IActionResult PutStudent(int id)
        {
            return Ok("Dodawanie zakończone");
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie Zakończone");
        }
    }
}