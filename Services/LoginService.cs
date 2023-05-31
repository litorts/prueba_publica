using System.Collections.Generic;
using static crecer_backend.DataSource.Interface;

namespace crecer_backend.Services
{
    /*
    public class LoginService : ILoginService
    {
        private readonly ILoginDataSource _loginDataSource;

        public LoginService(ILoginDataSource loginDataSource)
        {
            _loginDataSource = loginDataSource;
        }

        public bool VerifyUserExist(string rut)
        {
            return _loginDataSource.VerifyUserExist(rut);
        }

        public bool VerifyUserAndPassword(LoginDto user)
        {
            return _loginDataSource.VerifyUserAndPassword(user.ToModel());
        }

        public UserDto GetUser(string rut)
        {
            return _loginDataSource.GetUser(rut).ToDto();
        }

        public IEnumerable<ProgramaDto> GetUserPrgs(string rut)
        {
            return _loginDataSource.GetUserPrgs(rut).Select(p => p.ToDto());
        }

        public IEnumerable<CompañiaDto> GetUserCias(string rut)
        {
            return _loginDataSource.GetUserCias(rut).Select(c => c.ToDto());
        }
    }
    */

}
