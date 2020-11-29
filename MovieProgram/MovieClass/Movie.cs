using System;
using Newtonsoft.Json;
using System.Data.SqlClient;


namespace MovieClass
{
    public class Movie
    {
        [JsonProperty("MovieNo")]
        public int MovieNo { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("RelYear")]
        public int RelYear { get; set; }

        [JsonProperty("RunTime")]
        public int RunTime { get; set; }

        public Movie(){
            this.MovieNo = 0;
            this.Title = "";
            this.RelYear = 0;
            this.RunTime = 0;
        }

        public Movie(int num, string title, int year, int rtime){
            this.MovieNo = num;
            this.Title = title;
            this.RelYear = year;
            this.RunTime = rtime;
        }

        public int numActors(int movieNo){
            string connectionString = @"Data Source=rpsdp.ctvssf2oqpbl.us-east-1.rds.amazonaws.com;
            Initial Catalog=Movies;User ID=admin; Password=kereneritrea";

            SqlConnection con = new SqlConnection(connectionString);

            string queryString = "SELECT COUNT(*) FROM CASTING WHERE MovieNo = @MovieNo";
            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@MovieNo", (int)movieNo);

            var numActors = 0;
            con.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    numActors = (int)reader[0];
                }
            }

            return numActors;
        }

        public int getAge(int movieNo){
            string connectionString = @"Data Source=rpsdp.ctvssf2oqpbl.us-east-1.rds.amazonaws.com;
            Initial Catalog=Movies;User ID=admin; Password=kereneritrea";

            SqlConnection con = new SqlConnection(connectionString);

            string queryString = "SELECT CAST(YEAR(GETDATE()) AS INT) - RELYEAR FROM MOVIE WHERE MOVIENO = @MovieNo";
            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@MovieNo", (int)movieNo);

            var age = 0;
            con.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    age = (int)reader[0];
                }
            }

            return age;
        }

    }
}
