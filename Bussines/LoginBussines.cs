using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using crecer_backend.DataSource;
using crecer_backend.Models;
using crecer_backend.Services.LogService;
using Microsoft.IdentityModel.Tokens;

namespace crecer_backend.Bussines
{
    public class LoginBussines
    {//el codigo comentado lo creo chatGPT, debido a los alcances del proyecto lo considere innecesario
        //private readonly string _secretKey;

        public LoginBussines(
            //string secretKey
            )
        {
            //_secretKey = secretKey;
        }

        public static string generate_jwt(string rut)
        {   
            return FunctionBussines.generate_jwtoken(rut);
        }
        public static bool validate_jwt(string jwt)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                string _secret_key = "iWs3Db9^-Hgjp1sI~K)_SeuvLFW_(2,X";//se genero con avast para mas seguridad
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret_key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken validatedToken;
                var claimsPrincipal = tokenHandler.ValidateToken(jwt, validationParameters, out validatedToken);
                return true;
            }
            catch (Exception ex)
            {
                LogService.logWarning("bussines", "login", "validate_jwt", ex.Message);
                return false;
            }
        }
        public static User get_user_from_token(string jwt)
        {
            var user = new User();
            var rut = FunctionBussines.get_rut_from_jwtoken(jwt);
            user = LoginDataSource.get_user(rut);
            return user;
        }
        public static (User, int) login(Login user)
        {
            User funcionario = new User();
            int error = 0;
            try
            {
                var rut_exist = LoginDataSource.verify_user_exist(user.rut);
                if (rut_exist)
                {
                    user = hydrate_password(user);
                    var user_correct = LoginDataSource.verify_user_and_password(user);
                    if (user_correct)//si es que el usuario y la contrasena son correctos
                    {
                        funcionario = LoginDataSource.get_user(user.rut);
                        funcionario.hydrate();
                        LogService.logInfo("bussines", "login", "login", $"login succesfull... rut {user.rut}, token: {funcionario.token}");
                    }
                    else//contrasena incorrecta
                    {
                        error = 2;//asignamos el error correspondiente
                        LogService.logWarning("bussines", "login", "login", $"try to connect with bad password... rut {user.rut}, password: {user.pwd}");
                    }
                }
                else//rut no existe como funcionario
                {
                    error = 1;
                    LogService.logWarning("bussines", "login", "login", $"try to connect with a not existed rut {user.rut}");
                }
            }
            catch (Exception ex)
            {
                error = 3;
                LogService.logWarning("bussines", "login", "login", $"error in login rut {user.rut}; password{user.pwd}; message {ex.Message}");
            }
            return (funcionario, error);
        }
        public static Login hydrate_password(Login user)
        {
            using (var md5 = MD5.Create())
            {
                var datos = Encoding.UTF8.GetBytes(user.pwd);
                var hashbytes = md5.ComputeHash(datos);
                var hash = BitConverter.ToString(hashbytes).Replace("-", string.Empty);
                user.md5_pwd = hash;
            }
            return user;
        }
        public static IEnumerable<Programa> hydrate_programs(User func)
        {
            return LoginDataSource.get_user_prgs(func.rut);
        }
        public static IEnumerable<Compañia> hydrate_cias(User func)
        {
            return LoginDataSource.get_user_cias(func.rut);
        }
    }
}
