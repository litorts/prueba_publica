namespace crecer_backend.Models
{
    public class Authorization
    {
        public string compañia { get; set; }
        public string programa { get; set; }
        public int compañia_id { get; set; }
        public int programa_id { get; set; }
        public bool status { get; set; }
    }
    public class Programa
    {
        public int id { get; set; }
        public string nombre { get; set; }
    }
    public class Compañia
    {
        public int id { get; set; }
        public string nombre { get; set; }
    }
}
