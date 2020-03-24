using Cw4.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.DAL
{
    public class MockDbService : IDbService
    {

        private static IEnumerable<Student> _students;
        static MockDbService()
        {
            _students = new List<Student>();
            using (var con = new SqlConnection("Data Source=db-mssql; Initial Catalog=s18311; Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;

              
                    com.CommandText = "select Student.FirstName, Student.LastName, Student.BirthDate, Studies.Name, Enrollment.Semester " +
                        " from student INNER JOIN Enrollment ON Student.IdEnrollment = Enrollment.IdEnrollment INNER JOIN Studies ON " +
                        "Enrollment.IdStudy = Studies.IdStudy;";

                    con.Open();
                    var dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        var st = new Student();
                        st.FirstName = dr["FirstName"].ToString();
                        st.LastName = dr["LastName"].ToString();
                        st.BirthDate = dr["BirthDate"].ToString();
                        st.StudiesName = dr["Name"].ToString();
                        st.SemestrNumber = dr["Semester"].ToString();

                        _students = _students.Concat(new[] { st });
                    }
                    con.Dispose();
                
            }
           
          
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }

    }
}
