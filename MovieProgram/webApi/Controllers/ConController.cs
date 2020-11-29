using System;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using MovieClass;
using System.Collections.Generic;
using ActorClass;

namespace webApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConController : ControllerBase
    { 
        public string ConString { get; set;}
        public string ConnectionString { get; set;}
        public List<Movie> Movies { get; set;} 
        public List<string> MovieTitles { get; set;} 

        public ConController(){
            this.ConString = @"no.database.here.com;Initial Catalog=Is;User ID=Wally; Password=Where";
            this.ConnectionString = @"Data Source=rpsdp.ctvssf2oqpbl.us-east-1.rds.amazonaws.com;
            Initial Catalog=Movies;User ID=admin; Password=kereneritrea";
            this.Movies = new List<Movie>();
            this.MovieTitles = new List<string>();
        }

        [HttpGet("Exception")]
        public string CatchException()
        { 
          try{
               using (SqlConnection con = new SqlConnection(this.ConString))
                {
                    string queryString = " SELECT * FROM Is.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' GO";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        con.Open();
                        var result = command.ExecuteNonQuery();
                        return result.ToString();
                    }
                }
           }catch (ArgumentException e){
              return "Error => Keyword not supported: 'no.database.here.com;initial catalog'.";
           }

        }


        [HttpGet("GetAllMovies")]
        public List<Movie> GetAllMovies(){
            string connectionString = @"Data Source=rpsdp.ctvssf2oqpbl.us-east-1.rds.amazonaws.com;
            Initial Catalog=Movies;User ID=admin; Password=kereneritrea";

            SqlConnection con = new SqlConnection(connectionString);

            string queryString = "SELECT * FROM MOVIE";
            SqlCommand command = new SqlCommand(queryString, con);

            con.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Movies.Add(
                        new Movie(Convert.ToInt32(reader[0]), reader[1].ToString(), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]))
                    );
                }
            }

            return this.Movies;
        }

        [HttpGet("FindMovie")]
        public List<string> FindMovie(){
            string connectionString = @"Data Source=rpsdp.ctvssf2oqpbl.us-east-1.rds.amazonaws.com;
            Initial Catalog=Movies;User ID=admin; Password=kereneritrea";

            SqlConnection con = new SqlConnection(connectionString);

            string queryString = "SELECT TITLE FROM MOVIE WHERE TITLE LIKE 'THE%';";
            SqlCommand command = new SqlCommand(queryString, con);

            con.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    MovieTitles.Add(
                        reader[0].ToString()
                    );
                }
            }

            return this.MovieTitles;
        }

         [HttpGet("TitleForActorCasted")]
        public List<string> TitleForActorCasted(){
            string connectionString = @"Data Source=rpsdp.ctvssf2oqpbl.us-east-1.rds.amazonaws.com;
            Initial Catalog=Movies;User ID=admin; Password=kereneritrea";

            SqlConnection con = new SqlConnection(connectionString);

            string queryString = @"SELECT TITLE FROM
                                (
                                SELECT C.ACTORNO, C.MOVIENO, M.TITLE
                                FROM  ((CASTING C
                                INNER JOIN ACTOR A 
                                ON C.ACTORNO = A. ACTORNO)
                                INNER JOIN MOVIE M 
                                ON M.MOVIENO = C.MOVIENO)
                                ) S
                                INNER JOIN
                                (
                                SELECT ACTORNO FROM ACTOR WHERE FULLNAME = 'Luke Wilson'
                                ) A
                                ON S.ACTORNO = A.ACTORNO
                                WHERE S.ACTORNO = A.ACTORNO;";
            SqlCommand command = new SqlCommand(queryString, con);

            con.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    MovieTitles.Add(
                        reader[0].ToString()
                    );
                }
            }

            return this.MovieTitles;
        }

        [HttpGet("MoviesTotalRunTime")]
        public int MoviesTotalRunTime(){
            string connectionString = @"Data Source=rpsdp.ctvssf2oqpbl.us-east-1.rds.amazonaws.com;
            Initial Catalog=Movies;User ID=admin; Password=kereneritrea";

            SqlConnection con = new SqlConnection(connectionString);

            string queryString = "SELECT * FROM MOVIE";
            SqlCommand command = new SqlCommand(queryString, con);

            con.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Movies.Add(
                        new Movie(Convert.ToInt32(reader[0]), reader[1].ToString(), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]))
                    );
                }
            }

            var total = 0;
            foreach(Movie m in this.Movies){
                total += m.RunTime;
            }
            
            return total;
        }

        [HttpPost("ChangeRuntime")]
        public string ChangeRuntime(Movie m){
            string connectionString = @"Data Source=rpsdp.ctvssf2oqpbl.us-east-1.rds.amazonaws.com;
            Initial Catalog=Movies;User ID=admin; Password=kereneritrea";

            SqlConnection con = new SqlConnection(connectionString);

            string queryString = "UPDATE MOVIE SET RUNTIME = @Runtime WHERE TITLE = @title";
            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@Runtime", (int)m.RunTime);
            command.Parameters.AddWithValue("@title", m.Title);

            con.Open();
            var result = command.ExecuteNonQuery();

            return "Updated " + result.ToString() + " row";
        }

        [HttpPost("ChangeSurname")]
        public string ChangeSurname(Object o){
            string connectionString = @"Data Source=rpsdp.ctvssf2oqpbl.us-east-1.rds.amazonaws.com;
            Initial Catalog=Movies;User ID=admin; Password=kereneritrea";

            SqlConnection con = new SqlConnection(connectionString);

            string queryString = "UPDATE ACTOR SET FullName = @givenName + ' ' + @newSurname WHERE GivenName = @givenName AND Surname = @surname;";
            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@givenName",  o.Firstvalue);
            command.Parameters.AddWithValue("@surname", o.Secondvalue );
            command.Parameters.AddWithValue("@newSurname", o.Thirdvalue);

            con.Open();
            var result = command.ExecuteNonQuery();

            return "Updated " + result.ToString() + " row";
        }


    }

    public class Object{
        public string Firstvalue { get; set;}
        public string Secondvalue { get; set;}
        public string Thirdvalue { get; set;}

        public Object(){
            this.Firstvalue = "";
            this.Secondvalue = "";
            this.Thirdvalue = "";
        }
    }
}
