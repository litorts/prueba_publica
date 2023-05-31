namespace crecer_backend.Models
{
    public class Login
    {
        /*USO:
            Login login = new Login("19246931-7", "carlitos123.,");
            Funcionario funcionario = login.logear();
        */
        public string rut { get; set; }
        public string pwd { get; set; }
        public string? md5_pwd { get; set; }
        public int? err { get; set; }
        public bool? aut { get; set; }
        //err=1 usuario no existe, 2 contraseña erronea, 3 falla conexion
        // System.Configuration.ConfigurationManager.ConexionStrings["strCx"].ConexionString
        public Login(String rut, String pwd)
        {
            this.rut = rut;
            this.pwd = pwd;
            this.err = 0;
            this.md5_pwd = "";
        }
        public Login(string rut, string pwd, int err, bool aut)
        {
            this.rut = rut;
            this.pwd = pwd;
            this.err = err;
            this.aut = aut;
            this.md5_pwd = "";
        }
        public Login() { }
    }
}
