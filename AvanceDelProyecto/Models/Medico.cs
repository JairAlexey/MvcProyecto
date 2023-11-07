using System.ComponentModel.DataAnnotations;

namespace AvanceDelProyecto.Models
{
    public class Medico
    {
        public int IdMedico { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Nacionalidad { get; set; }

        public string Especialidad { get; set; }
    }
}
