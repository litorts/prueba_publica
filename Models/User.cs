using crecer_backend.Bussines;

namespace crecer_backend.Models
{
    public class User
    {
        public String rut { get; set; }
        public String nombre { get; set; }
        public int? profesion { get; set; }
        public int nivel { get; set; }
        public IEnumerable<Authorization> permisos { get; set; }
        public bool aut { get; set; }
        public string token { get; set; }
        public IEnumerable<Programa> programas { get; set; }
        public IEnumerable<Compañia> compañias { get; set; }
        //public int error { get; set; }
        public User(String rut, String nombre, int? profesion, String nivel)
        {
            this.rut = rut;
            this.nombre = nombre;
            if (profesion == null) this.profesion = null;
            else this.profesion = int.Parse(profesion.ToString());
            this.nivel = int.Parse(nivel);
            this.permisos = AuthorizationsBussines.get_list(this);
            this.aut = false;
        }
        public User() { }
        public void hydrate()
        {
            token = LoginBussines.generate_jwt(rut);
            programas = LoginBussines.hydrate_programs(this);
            compañias = LoginBussines.hydrate_cias(this);
            permisos = AuthorizationsBussines.get_list(this);
        }
        public void hydrate(string jwtoken)
        {
            token = jwtoken;
            programas = LoginBussines.hydrate_programs(this);
            compañias = LoginBussines.hydrate_cias(this);
            permisos = AuthorizationsBussines.get_list(this);
        }
    }
}
