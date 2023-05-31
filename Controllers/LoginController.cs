using crecer_backend.Bussines;
using crecer_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace crecer_backend.Controllers.old
{
    [Route("api/login")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost()]
        public ActionResult<string> login([FromBody]Login login)
        {
            if (login.rut != null)//que el objeto enviado es valido
            {
                //Funcionario funcionario = login.logear();
                (User user, int error) = LoginBussines.login(login);
                if (error == 0)//login correcto
                {
                    user.aut = true;
                    return Ok(JsonConvert.SerializeObject(user));
                }
                if (error != 0)//hay error
                {
                    login.rut = "";//limpiamos los datos
                    login.pwd = "";
                    login.err = error;//asignamos el error
                }
                if (error == 1 || error == 2 || error == 3) return Ok(JsonConvert.SerializeObject(login));
                else return BadRequest();
                //if(login.err==1) return  mal rut
                //if(login.err==2) return  mal pwd
                //if(login.err==3) return  mal conn
            }
            else return BadRequest();
        }
        [HttpPost("get_user")]
        public ActionResult<string> get_user()
        {
            string? jwt = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (jwt == null)
                return Unauthorized();
            var is_valid = LoginBussines.validate_jwt(jwt);
            if ( is_valid == true )
            {
                var user = LoginBussines.get_user_from_token(jwt);
                user.hydrate(jwt);
                var json = JsonConvert.SerializeObject(user);
                return Ok(json);
            }
            return BadRequest();
        }
    }
}
