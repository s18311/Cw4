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
            using (SqlConnection con = new SqlConnection("Data Source=db-mssql; Initial Catalog=s18311; Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                con.Open();
                com.Connection = con;
                com.CommandText = " select Student.FirstName, Student.LastName, Student.BirthDate, Studies.Name, Enrollment.Semester " +
                        " from student INNER JOIN Enrollment ON Student.IdEnrollment = Enrollment.IdEnrollment INNER JOIN Studies ON " +
                        "Enrollment.IdStudy = Studies.IdStudy WHERE Student.IndexNumber=" + id + ";";
                SqlDataReader dr = com.ExecuteReader();
                Student st = new Student();

                while (dr.Read())
                {

                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.StudiesName = dr["Name"].ToString();
                    st.SemestrNumber = dr["Semester"].ToString();

                }

                con.Dispose();
                string tmp = JsonSerializer.Serialize(st);
                return Ok(tmp);
            }

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