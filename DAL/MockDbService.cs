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
            using (var com2 = new SqlCommand())
                {
                    com2.Connection = con;


                    com2.CommandText = "select Student.FirstName, Student.LastName, Student.BirthDate, Studies.Name, Enrollment.Semester " +
                        " from student INNER JOIN Enrollment ON Student.IdEnrollment = Enrollment.IdEnrollment INNER JOIN Studies ON " +
                        "Enrollment.IdStudy = Studies.IdStudy;";

                    con.Open();
                    var dr2 = com2.ExecuteReader();
                    while (dr2.Read())
                    {
                        var st = new Student();
                        st.FirstName = dr2["FirstName"].ToString();
                        st.LastName = dr2["LastName"].ToString();
                        st.BirthDate = (DateTime)dr2["BirthDate"];
                        st.StudiesName = dr2["Name"].ToString();
                        st.Semester = (int)dr2["Semester"];

                        _students = _students.Concat(new[] { st });
                    }//while2
                dr2.Close();
                }//com2

            
        }

        public Student GetStudent(string id)
        {

            Student stud = new Student();

            using (var con = new SqlConnection("Data Source=db-mssql; Initial Catalog=s18311; Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
                {
                    con.Open();
                    com.Connection = con;
                    com.CommandText = " select Student.FirstName, Student.LastName, Student.BirthDate, Studies.Name, Enrollment.Semester " +
                            " from student INNER JOIN Enrollment ON Student.IdEnrollment = Enrollment.IdEnrollment INNER JOIN Studies ON " +
                            "Enrollment.IdStudy = Studies.IdStudy WHERE Student.IndexNumber=" + id + ";";
                    SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {

                        stud.FirstName = dr["FirstName"].ToString();
                        stud.LastName = dr["LastName"].ToString();
                        stud.BirthDate = (DateTime)dr["BirthDate"];
                        stud.StudiesName = dr["Name"].ToString();
                        stud.Semester = (int)dr["Semester"];

                    }//while1
                dr.Close();
                }//com1 

                return stud;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }

        public static bool StudentIdAuthorization(string id)
        {

            using (var con = new SqlConnection("Data Source=db-mssql; Initial Catalog=s18311; Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "select * from Student where indexNumber=@id;";
                com.Parameters.AddWithValue("id", id);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    return true;
                }
                dr.Close();
            }//com


            return false;
        } // method body
    }
}
