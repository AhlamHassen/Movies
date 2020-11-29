using System;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace ActorClass
{
    public class Actor
    {
        [JsonProperty("ActorNo")]
        public int ActorNo { get; set; }

        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("GivenName")]
        public string GivenName { get; set; }

        [JsonProperty("SurName")]
        public string SurName { get; set; }

        public Actor(){
            this.ActorNo = 0;
            this.FullName = "";
            this.GivenName = "";
            this.SurName = "";
        }

        public Actor(int num, string fname, string gname, string sname){
            this.ActorNo = num;
            this.FullName = fname;
            this.GivenName = gname;
            this.SurName = sname;
        }

        public string setFullName(int actnum){
            string connectionString = @"Data Source=rpsdp.ctvssf2oqpbl.us-east-1.rds.amazonaws.com;
            Initial Catalog=Movies;User ID=admin; Password=kereneritrea";

            SqlConnection con = new SqlConnection(connectionString);

            string queryString = "SELECT GivenName, Surname FROM ACTOR WHERE ActorNo = @ActorNo";
            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@ActorNo", (int)actnum);

            var fullname = "";
            con.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    fullname = reader[0].ToString() + " " + reader[1].ToString();
                }
            }

            return fullname;
        }

        
    }
}
