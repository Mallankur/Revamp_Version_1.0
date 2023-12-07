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

            var result = await _repository.CreateData_Using_SQL_SP_ConnectorAsync();
            return Ok("Data received and processed");
        }

        
        




    }
}
