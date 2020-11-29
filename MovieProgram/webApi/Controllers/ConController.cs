using System;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace webApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConController : ControllerBase
    { 
        public string ConString { get; set;}

        public ConController(){
            this.ConString = @"no.database.here.com;Initial Catalog=Is;User ID=Wally; Password=Where";
        }

        [HttpGet]
        public string Get()
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
    }
}
