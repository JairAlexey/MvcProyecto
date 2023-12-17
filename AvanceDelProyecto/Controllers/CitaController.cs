using AvanceDelProyecto.Models;
using AvanceDelProyecto.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AvanceDelProyecto.Controllers
{
    public class CitaController : Controller
    {
        // Dependencia para comunicarse con la API.
        private readonly IApiService _apiService;

        public CitaController(IApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: MedicoController  
        public async Task<IActionResult> Index()
        {
            try
            {
                var citas = await _apiService.getCita();

                // Obtener nombres de médicos y correos de usuarios
                foreach (var cita in citas)
                {
                    var medico = await _apiService.getMedico(cita.IdMedico);
                    var usuario = await _apiService.getUsuario(cita.IdUsuario);

                    if (medico != null && usuario != null)
                    {
                        cita.NombreMedico = medico.Nombre;
                        cita.CorreoUsuario = usuario.Correo;
                    }
                }

                return View(citas);
            }
            catch (Exception e)
            {
                return View(new List<Cita>());
            }
        }


        // GET: UsuarioController/Details/5 //ListarDatos
        public async Task<ActionResult> Details(int IdCita)
        {
            var cita = await _apiService.getCita(IdCita);

            if (cita != null)
            {
                // Obtener el nombre del médico y el correo del usuario
                var medico = await _apiService.getMedico(cita.IdMedico);
                var usuario = await _apiService.getUsuario(cita.IdUsuario);

                if (medico != null && usuario != null)
                {
                    cita.NombreMedico = medico.Nombre;
                    cita.CorreoUsuario = usuario.Correo;
                }

                return View(cita);
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Create()
        {
            await SetMedicosAndUsuariosInViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cita cita)
        {
            var result = await _apiService.addCita(cita);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "El médico ya tiene una cita a la misma hora.");
            await SetMedicosAndUsuariosInViewBag();
            return View(cita);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int IdCita)
        {
            await SetMedicosAndUsuariosInViewBag();
            var cita = await _apiService.getCita(IdCita);
            if (cita != null)
            {
                return View(cita);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Cita cita)
        {
            var result = await _apiService.updateCita(cita.IdCita, cita);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al editar la cita. Verifica los datos y vuelve a intentarlo.");
            await SetMedicosAndUsuariosInViewBag();
            return View(cita);
        }




        private async Task SetMedicosAndUsuariosInViewBag()
        {
            var medicos = await _apiService.getMedico();
            var usuarios = await _apiService.getUsuario();

            // Modificar la siguiente línea para incluir el nombre del médico y el correo del usuario
            ViewBag.Medicos = new SelectList(medicos.Select(m => new { m.IdMedico, m.Nombre }), "IdMedico", "Nombre");
            ViewBag.Usuarios = new SelectList(usuarios.Select(u => new { u.IdUsuario, u.Correo }), "IdUsuario", "Correo");
        }




        // GET: ProductoController/Delete/5 //Eliminar
        public ActionResult Delete(int IdCita)
        {
            var pEliminar = _apiService.deleteCita(IdCita);
            return RedirectToAction(nameof(Index));

        }
    }
}
