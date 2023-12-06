namespace Revamp_Ank_App.DomainEntites.Repositores.Entites
{
    public interface ISQLconnecterAnkurMall228
    {
        Task<string>FetchAllSQLData(string sqlConnectionString, string storeProcedureName, int CycleId, string rdids);

    }
}
