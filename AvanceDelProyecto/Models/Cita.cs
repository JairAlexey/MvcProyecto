namespace AvanceDelProyecto.Models
{
    public class Cita
    {
        public int IdCita { get; set; }

        public string Fecha { get; set; }

        public string Hora { get; set; }

        public string Descripcion { get; set; }

        // ID del Medico
        public int IdMedico { get; set; }

        // ID del Usuario
        public int IdUsuario { get; set; }

        //Nombres
        public string NombreMedico { get; set; }
        public string CorreoUsuario { get; set; }

    }
}
