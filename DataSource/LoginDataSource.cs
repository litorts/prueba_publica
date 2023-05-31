using crecer_backend.Bussines;
using crecer_backend.Models;
using crecer_backend.Services.LogService;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace crecer_backend.DataSource
{
    public class LoginDataSource
    {
        public static bool verify_user_exist(string rut)
        {
            var exist = false;
            try
            {
                using (SqlConnection db = new SqlConnection(FunctionBussines.get_connection_string()))
                {
                    string query = "verify_user_exist";
                    var param = new DynamicParameters();
                    param.Add("rut", rut);
                    exist = db.Query<bool>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                LogService.logError("datasource", "datasource", "verify_user_exist", ex);
                exist = false;
            }
            return exist;
        }
        public static bool verify_user_and_password(Login user)
        {
            var response = false;
            try
            {
                using (SqlConnection db = new SqlConnection(FunctionBussines.get_connection_string()))
                {
                    string query = "verify_user_and_password";
                    var param = new DynamicParameters();
                    param.Add("rut", user.rut);
                    param.Add("password", user.md5_pwd);
                    response = db.Query<bool>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogService.logError("datasource", "datasource", "verify_user_and_password", ex);
                response = false;
            }
            return response;
        }
        public static User get_user(string rut)
        {
            var func = new User();
            try
            {
                using (SqlConnection db = new SqlConnection(FunctionBussines.get_connection_string()))
                {
                    string query = "get_user";
                    var param = new DynamicParameters();
                    param.Add("rut", rut);
                    func = db.Query<User>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogService.logError("datasource", "datasource", "get_user", ex);
                func = new User();
            }
            return func;
        }
        public static IEnumerable<Programa> get_user_prgs(string rut)
        {
            IEnumerable<Programa> programs = new List<Programa>();
            try
            {
                using (SqlConnection db = new SqlConnection(FunctionBussines.get_connection_string()))
                {
                    string query = "get_rutprgs";
                    var param = new DynamicParameters();
                    param.Add("rut", rut);
                    programs = db.Query<Programa>(query, param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                LogService.logError("datasource", "datasource", "get_user_prgs", ex);
                programs = new List<Programa>();
            }
            return programs;
        }
        public static IEnumerable<Compañia> get_user_cias(string rut)
        {
            IEnumerable<Compañia> programs = new List<Compañia>();
            try
            {
                using (SqlConnection db = new SqlConnection(FunctionBussines.get_connection_string()))
                {
                    string query = "get_rutcias";
                    var param = new DynamicParameters();
                    param.Add("rut", rut);
                    programs = db.Query<Compañia>(query, param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                LogService.logError("datasource", "datasource", "get_rutcias", ex);
                programs = new List<Compañia>();
            }
            return programs;
        }
    }
}
