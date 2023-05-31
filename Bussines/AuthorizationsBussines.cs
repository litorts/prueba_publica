using crecer_backend.DataSource;
using crecer_backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace crecer_backend.Bussines
{
    public class AuthorizationsBussines
    {
        public static IEnumerable<Authorization> get_list(User usr)
        {
            IEnumerable<Authorization> list = new List<Authorization>();
            try
            {
                var autorizations = AuthorizationsDataSource.get_list(usr.rut);
                list = from cliente in usr.compañias
                       from programa in usr.programas
                       let estado = autorizations.Any(d => d.compañia_id == cliente.id && d.programa_id == programa.id) ? true : false
                       select new Authorization
                       {
                           compañia_id = cliente.id,
                           programa_id = programa.id,
                           status = estado,
                           compañia = cliente.nombre,
                           programa = programa.nombre,
                       };
            }
            catch (Exception ex)
            {

            }
            return list;
        }
    }
}
