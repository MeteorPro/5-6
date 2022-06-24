using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using PVI_5_6.Models;

namespace PVI_5_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public GamerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select
                            GamerId, GamerEmail, GamerYear
                            from dbo.Gamer";
            DataTable table = new DataTable(); 
            string sqlDataSource = _configuration.GetConnectionString("GamerAppCon");
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Gamer gam)
        {
            string query = @"insert into dbo.Gamer 
                            (GamerEmail, GamerYear)
                            values (@GamerEmail, @GamerYear)";
            DataTable table = new DataTable(); 
            string sqlDataSource = _configuration.GetConnectionString("GamerAppCon");
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@GamerEmail", gam.GamerEmail);
                    myCommand.Parameters.AddWithValue("@GamerYear", gam.GamerYear);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added");
        }

        [HttpPut]
        public JsonResult Put(Gamer gam)
        {
            string query = @"update dbo.Gamer 
                            set GamerEmail =@GamerEmail,
                            GamerYear =@GamerYear
                            where GamerId =@GamerId";
                           
            DataTable table = new DataTable(); 
            string sqlDataSource = _configuration.GetConnectionString("GamerAppCon");
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@GamerId", gam.GamerId);
                    myCommand.Parameters.AddWithValue("@GamerEmail", gam.GamerEmail);
                    myCommand.Parameters.AddWithValue("@GamerYear", gam.GamerYear);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.Gamer 
                            where GamerId =@GamerId";
                           
            DataTable table = new DataTable(); 
            string sqlDataSource = _configuration.GetConnectionString("GamerAppCon");
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@GamerId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted");
        }

    }
}
