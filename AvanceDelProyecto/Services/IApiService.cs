using AvanceDelProyecto.Models;

namespace AvanceDelProyecto.Services
{
    public interface IApiService
    {
        Task<List<User>> getUsuario();
        Task<User> getUsuario(int IdUsuario);
        Task<bool> addUsuario(User usuario);
        Task<bool> updateUsuario(int IdUsuario, User usuario);
        Task<bool> deleteUsuario(int IdUsuario);

        Task<List<Medico>> getMedico();
        Task<Medico> getMedico(int IdMedico);
        Task<bool> addMedico(Medico medico);
        Task<bool> updateMedico(int IdMedico, Medico medico);
        Task<bool> deleteMedico(int IdMedico);

        Task<List<Cita>> getCita();
        Task<Cita> getCita(int IdCita);
        Task<bool> addCita(Cita cita);
        Task<bool> updateCita(int IdCita, Cita cita);
        Task<bool> deleteCita(int IdCita);

        Task<bool> IniciarSesion(InicioSesion inicioSesion);


    }
}
