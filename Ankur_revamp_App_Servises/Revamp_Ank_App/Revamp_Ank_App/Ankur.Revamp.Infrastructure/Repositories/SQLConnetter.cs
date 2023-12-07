using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Revamp_Ank_App.DomainEntites.Repositores.Entites;
using System.Data;

namespace Revamp_Ank_App.Ankur.Revamp.Infrastructure.Repositories
{
    public class SQLConnetter : ISQLconnecterAnkurMall228
    {
        public readonly string _connectionString;

        public SQLConnetter(IOptions<SQLConnectorOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public async Task<string> FetchAllSQLData(string connectionString, string storeProcedureName, int CycleId, string rdids)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(storeProcedureName, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@CycleId", CycleId);
                sqlCommand.Parameters.AddWithValue("@RDIds", rdids);




                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dataTable);

                return jsonData;



            }
        }
    }
}
