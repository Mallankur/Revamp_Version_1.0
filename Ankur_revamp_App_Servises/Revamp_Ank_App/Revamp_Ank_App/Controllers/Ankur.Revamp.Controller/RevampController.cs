using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Core.Configuration;
using Revamp_Ank_App.DomainEntites.Repositores.Entites;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using Revamp_Ank_App.Domain.Repositores.Entites;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Revamp_Ank_App.Controllers.Ankur.Revamp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevampController : ControllerBase
    {
        private readonly ISQLconnecterAnkur _repository;
        //private readonly IConfiguration _config;

        public RevampController(ISQLconnecterAnkur repository)
        {
            _repository = repository;
            //_config = config;
        }


        [HttpPost]
        public async Task<IActionResult> Post( int CycleId)
        {
            // var connectionstring = "server=AVINEXSERVER6;database=CatCheckPro;Integrated Security=false;User ID=CCAdmin;Password=Catalyst1*;Trusted_Connection=No;";
            var connectionstring = "Server=AVINEXSERVER6;Database=CatCheckPro;Integrated Security=false;User ID=CCAdmin;Password=Catalyst1*;Trusted_Connection=No;TrustServerCertificate=true;";
            var result = GetDataFromSQL(connectionstring, CycleId);

            if (result!=null)
            {
                var mongores = await _repository.CreateData_Using_SQL_SP_ConnectorAsync(result);
               
            }
            return Ok("Data received and processed");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetDataFromSQL(string sqlConnectionString, int CycleId)
        {
          

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("CC_Revamp_uspGetUMData", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@CycleId", CycleId);
                sqlCommand.Parameters.AddWithValue("@RDIds", "5893209,5893210");




                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dataTable);

                return jsonData;
            }
        }




    }
}
