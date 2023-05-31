using System;

namespace crecer_backend.Models
{
    public class Informe
    {
        public long Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
