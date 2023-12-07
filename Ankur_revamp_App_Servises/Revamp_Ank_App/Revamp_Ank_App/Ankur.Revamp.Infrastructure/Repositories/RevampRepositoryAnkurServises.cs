
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Revamp_Ank_App.Domain.Repositores.Entites;
using Revamp_Ank_App.DomainEntites.Repositores.Entites;
using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using ThirdParty.Json.LitJson;

namespace Revamp_Ank_App.Ankur.Revamp.Infrastructure.Repositories
{
    public class RevampRepositoryAnkurServises : ISQLconnecterAnkur
    {
        public IMongoCollection<RevampMongoDataModel> RevampCollection { get; set; }
        private readonly SQLConnetter _sqlConnetter;
        public RevampRepositoryAnkurServises(IOptions<MongoScoket> connect, SQLConnetter sql)
        {
            _sqlConnetter = sql;    
            MongoClient client = new MongoClient(connect.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(connect.Value.DatabaseName);
            RevampCollection = database.GetCollection<RevampMongoDataModel>(connect.Value.CollectionName);
        }
        public async Task<string> FetchAllSQLData(string storeProcedureName, int CycleId, string rdids)
        {
            return await _sqlConnetter.FetchAllSQLData(_sqlConnetter._connectionString, storeProcedureName, CycleId, rdids);
        }
        public async Task<bool> CreateData_Using_SQL_SP_ConnectorAsync()
        {
            bool IsInseretd = false;
            var res = await FetchAllSQLData("CC_Revamp_uspGetUMData", 3081, "5893209,5893210");
            IEnumerable<RevampMongoDataModel> revamapData = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<RevampMongoDataModel>>(res, new DateTimeToMillisecondConverter());
            if (revamapData.Any())
            {
                RevampCollection.InsertMany(revamapData);
                IsInseretd = true;
            }

            return IsInseretd;



        }








        public Task<RevampMongoDataModel> GetRevamDataByRDIDSAsync(string[] rdids, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<RevampMongoDataModel> GetRevampRawDataAsync()
        {
            throw new NotImplementedException();
        }

        
    }
}
