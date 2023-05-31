using crecer_backend.Bussines;
using crecer_backend.Models;
using crecer_backend.Services.LogService;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace crecer_backend.DataSource
{
    public class AuthorizationsDataSource
    {
        public static IEnumerable<Authorization> get_list(string rut)
        {
            IEnumerable<Authorization> list = new List<Authorization>();
            try
            {
                using (SqlConnection db = new SqlConnection(FunctionBussines.get_connection_string()))
                {
                    string query = "get_authorizations";
                    var param = new DynamicParameters();
                    param.Add("rut", rut);
                    list = db.Query<Authorization>(query, param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                LogService.logError("datasource", "Authorizations", "get_list", ex);
            }
            return list;
        }
    }
}
