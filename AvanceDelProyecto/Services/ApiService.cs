using AvanceDelProyecto.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using AvanceDelProyecto.Configurations;
using AvanceDelProyecto.Models;
using System.Drawing;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace AvanceDelProyecto.Services
{
    public class ApiService : IApiService
    {
        // URL base de la API con la que se interactúa.

        private readonly string _baseUrl; //Se separa porque se hace mas facil modificarse si se cambia la ip o el dominio

        // Cliente HTTP utilizado para hacer las peticiones a la API.
        private readonly HttpClient _httpClient;

        // Constructor: inicializa el URL base y el cliente HTTP.
        public ApiService(IOptions<ApiSettings> apiSettings, HttpClient httpClient) //Constructor
        {
            _baseUrl = apiSettings.Value.BaseUrl; //Para llamar
            _httpClient = httpClient;
        }

        public async Task<List<User>> getUsuario()
        {

            var response = await _httpClient.GetAsync($"{_baseUrl}User"); //var (ya no se recomienda usarlo muchp, mejor poner el tipo de objeto)
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<List<User>>(content);
                return usuario;
            }
            return null;
        }

        public async Task<User> getUsuario(int IdUsuario)
        {
          

            var response = await _httpClient.GetAsync($"{_baseUrl}User/{IdUsuario}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<User>(content);
                return usuario;
            }
            return null;
        }

        public async Task<bool> addUsuario(User usuario)
        {
            var jsonString = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}User", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> updateUsuario(int IdUsuario, User usuario)
        {

            var jsonString = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}User/{IdUsuario}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> deleteUsuario(int IdUsuario)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}User/{IdUsuario}");

            return response.IsSuccessStatusCode;
        }


        //METODOS DE MEDICO
        public async Task<List<Medico>> getMedico()
        {

            var response = await _httpClient.GetAsync($"{_baseUrl}Medico"); //var (ya no se recomienda usarlo muchp, mejor poner el tipo de objeto)
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var medico = JsonConvert.DeserializeObject<List<Medico>>(content);
                return medico;
            }
            return null;
        }

        public async Task<Medico> getMedico(int IdMedico)
        {

            var response = await _httpClient.GetAsync($"{_baseUrl}Medico/{IdMedico}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var medico = JsonConvert.DeserializeObject<Medico>(content);
                return medico;
            }
            return null;
        }

        public async Task<bool> addMedico(Medico medico)
        {
            var jsonString = JsonConvert.SerializeObject(medico);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}Medico", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> updateMedico(int IdMedico, Medico medico)
        {

            var jsonString = JsonConvert.SerializeObject(medico);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}Medico/{IdMedico}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> deleteMedico(int IdMedico)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}Medico/{IdMedico}");

            return response.IsSuccessStatusCode;
        }


        //METODOAS DE CITA

        public async Task<List<Cita>> getCita()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}Cita"); //var (ya no se recomienda usarlo muchp, mejor poner el tipo de objeto)
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var cita = JsonConvert.DeserializeObject<List<Cita>>(content);
                return cita;
            }
            return null;
        }

        public async Task<Cita> getCita(int IdCita)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}Cita/{IdCita}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var cita = JsonConvert.DeserializeObject<Cita>(content);
                return cita;
            }
            return null;
        }

        public async Task<bool> addCita(Cita cita)
        {
            var jsonString = JsonConvert.SerializeObject(cita);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}Cita", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> updateCita(int IdCita, Cita cita)
        {

            var jsonString = JsonConvert.SerializeObject(cita);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}Cita/{IdCita}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> deleteCita(int IdCita)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}Cita/ {IdCita}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> IniciarSesion(InicioSesion inicioSesion)
        {
            var usuarios = await getUsuario(); // Asegúrate de que este método esté obteniendo los usuarios correctamente

            // Verificar si existe un usuario con las credenciales proporcionadas
            var usuario = usuarios.FirstOrDefault(u => u.Correo == inicioSesion.Correo && u.Clave == inicioSesion.Clave);

            // Devolver true si se encuentra un usuario que coincida con las credenciales, de lo contrario, devolver false
            return usuario != null;
        }


   

    }
}
