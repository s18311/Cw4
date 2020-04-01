using Cw4.DTOs;
using Cw4.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.DAL
{
    public class EnrollmentDbService : IEnrollmentDbService
    {
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            var response = new EnrollStudentResponse();

            using (var con = new SqlConnection("Data Source=db-mssql; Initial Catalog=s18311; Integrated Security=True; MultipleActiveResultSets = True; "))
            using (var com = new SqlCommand())
            {

                con.Open();
                var tran = con.BeginTransaction();
                com.Connection = con;
                com.Transaction = tran;
                try
                  {
                com.CommandText = "select IdStudy from studies where name=@name";
                com.Parameters.AddWithValue("name", request.StudiesName);

                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    dr.Close();
                    tran.Rollback();
                    return null;
                }

                int idStudies = (int)dr["IdStudy"]; 
                dr.Close();



                
                com.CommandText = "select IdEnrollment, Semester, StartDate from enrollment where StartDate = (select max(StartDate) from Enrollment where Semester=1) AND IdStudy =@idstudies;";
                com.Parameters.AddWithValue("idstudies", idStudies);

                var dr1 = com.ExecuteReader();
                if (!dr1.Read())
                {
                    dr1.Close();
                    tran.Rollback();
                    return null;
                }

                int idEnrollment = (int)dr1["IdEnrollment"];
                int semesterOfEnroll = (int)dr1["Semester"];
                DateTime startAt = (DateTime)dr1["StartDate"];
                dr1.Close();


                com.CommandText = "insert into student(IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) values (@Index, @Fname, @Lname, @Birthdate, @Idenrollment)";
                com.Parameters.AddWithValue("Index", request.IndexNumber);
                com.Parameters.AddWithValue("Fname", request.FirstName);
                com.Parameters.AddWithValue("Lname", request.LastName);
                com.Parameters.AddWithValue("Birthdate", request.BirthDate);
                com.Parameters.AddWithValue("Idenrollment", idEnrollment);

                com.ExecuteNonQuery();

               
                tran.Commit();


                response.IndexNumber = request.IndexNumber;
                response.LastName = request.LastName;
                response.Semester = semesterOfEnroll;
                response.StartDate = startAt;

                return response;

                  }
                catch (SqlException e)
                 {

                  tran.Rollback();
                 Console.WriteLine(e.Message.ToString());
                }

            }
            
            return null;
        }

        public bool PromoteStudents(PromoteStudentRequest request)
        {
            using (var con = new SqlConnection("Data Source=db-mssql; Initial Catalog=s18311; Integrated Security=True; MultipleActiveResultSets = True; "))
            using (var com = new SqlCommand())
            {
                
                    con.Open();
                    var tran = con.BeginTransaction();
                    com.Connection = con;
                    com.Transaction = tran;
                // try
                //{
                
                    com.CommandText = "select Enrollment.IdEnrollment, Studies.IdStudy from Enrollment inner join Studies on Enrollment.IdStudy=Studies.IdStudy and Enrollment.Semester=@reqsemester and Studies.Name=@reqname;";
                    com.Parameters.AddWithValue("reqsemester", request.Semester);
                    com.Parameters.AddWithValue("reqname", request.StudiesName);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        tran.Rollback();
                        return false;
                    }

                    dr.Close();
                    if(request.StudiesName == null || request.Semester == null){
                        return true;
                    }
                    
                    com.Parameters.Clear();
                    
                    com.CommandText = "PromoteStudents";
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Studies", request.StudiesName);
                    com.Parameters.AddWithValue("@Semester", request.Semester);

                    com.ExecuteNonQuery();


                    tran.Commit();
                /* }
               catch (SqlException e)
                {

                    tran.Rollback();
                    Console.WriteLine(e.Message.ToString());
                    return false;
                }
                */
                return true;
            }
        }
    }
}
