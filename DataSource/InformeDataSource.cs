using crecer_backend.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using static crecer_backend.DataSource.Interface;

namespace crecer_backend.DataSource
{
    public class InformeDataSource : IInformeDataSource
    {
        private readonly string _connectionString;

        public InformeDataSource(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Informe> obtener_informes()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var informes = connection.Query<Informe>("obtener_informes", commandType: CommandType.StoredProcedure);

                return informes;
            }
        }

        public Informe obtener_informe_por_id(long id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new { id };
                var informe = connection.QueryFirstOrDefault<Informe>("obtener_informe_por_id", parameters, commandType: CommandType.StoredProcedure);

                return informe;
            }
        }

        public void crear_informe(Informe informe)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new
                {
                    nombre = informe.Nombre,
                    descripcion = informe.Descripcion,
                    usuario_creacion = informe.UsuarioCreacion,
                    fecha_creacion = informe.FechaCreacion
                };
                connection.Execute("crear_informe", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void actualizar_informe(Informe informe)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new
                {
                    id = informe.Id,
                    nombre = informe.Nombre,
                    descripcion = informe.Descripcion,
                    usuario_creacion = informe.UsuarioCreacion,
                    fecha_creacion = informe.FechaCreacion

                };
            }
        }

        public void eliminar_informe(long id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new { id };
                connection.Execute("eliminar_informe", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
