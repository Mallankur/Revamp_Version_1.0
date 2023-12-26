using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class SPReturnData
    {

        string sqlConnectionString = "server=AVINEXSERVER6;database=CatCheckPro;Integrated Security=false;User ID=CCAdmin;Password=Catalyst1*;Trusted_Connection=No;";


        string storeProcedureName = "uspGetCycleRDEIDs";
        public static string ReturnSPRdied(string sqlConnectionString, int CycleId, string storeProcedureName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                sqlConnection.Open();

                var sqlCommand = new SqlCommand(storeProcedureName, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@CycleId", CycleId);

                var adapter = new SqlDataAdapter(sqlCommand);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                var jsonresult = Newtonsoft.Json.JsonConvert.SerializeObject(dataTable);
                return jsonresult;

            }



        }
    }

}
