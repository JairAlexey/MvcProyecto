using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace AvanceDelProyecto.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Correo { get; set; }

        public string Clave { get; set; }

        public bool EsAdministrador { get; set; } // Indica si el usuario es un administrador

        public int UserId { get; set; } // ID de usuario

        public string Role { get; set; } // Rol del usuario

    }
}
