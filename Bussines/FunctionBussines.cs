using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace crecer_backend.Bussines
{
    public class FunctionBussines
    {
        #region appsetting configurations
        public static string get_appsettings_value(string key)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);//creamos la variable que lee la configuracion
            IConfigurationRoot configuration = builder.Build();//creamos la variable que contiene las configuraciones
            return configuration[key];
        }
        public static string get_connection_string()
        {
            return get_appsettings_value("db:connection_string");
        }
        public static string get_jwt_secret()
        {
            return get_appsettings_value("jwt:secret_key");
        }
        public static string get_jwt_issuer()
        {
            return get_appsettings_value("jwt:issuer");
        }
        public static string get_jwt_audience()
        {
            return get_appsettings_value("jwt:audience");
        }
        #endregion
        #region jwtoken
        public static string generate_jwtoken(string rut)
        {
            var issuer = get_jwt_issuer();
            var audience = get_jwt_audience();
            var secretKey = get_jwt_secret();
            var expires = DateTime.UtcNow.AddDays(7);

            var claims = new[]
            {
            new Claim("rut", rut),
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        public static string get_rut_from_jwtoken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(get_jwt_secret());
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);
                var claim = claimsPrincipal.FindFirst("rut");
                return claim?.Value == null? "" : claim.Value;
            }
            catch
            {
                return "";
            }
        }
        #endregion
    }
}
