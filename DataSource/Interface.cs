using crecer_backend.Models;
namespace crecer_backend.DataSource
{
    public class Interface
    {
        public interface IInformeDataSource
        {
            IEnumerable<Informe> obtener_informes();
            Informe obtener_informe_por_id(long id);
            void crear_informe(Informe informe);
            void actualizar_informe(Informe informe);
            void eliminar_informe(long id);
        }
        public interface ILoginDataSource
        {
            bool VerifyUserExist(string rut);
            bool VerifyUserAndPassword(Login user);
            Models.User GetUser(string rut);
            IEnumerable<Programa> GetUserPrograms(string rut);
            IEnumerable<Compañia> GetUserCompanies(string rut);
        }
    }
}
