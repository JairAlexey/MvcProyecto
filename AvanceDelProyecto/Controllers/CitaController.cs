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
                var cita = await _apiService.getCita();
                return View(cita);
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

        private async Task SetMedicosAndUsuariosInViewBag()
        {
            var medicos = await _apiService.getMedico();
            var usuarios = await _apiService.getUsuario();
            ViewBag.Medicos = new SelectList(medicos, "IdMedico", "IdMedico");
            ViewBag.Usuarios = new SelectList(usuarios, "IdUsuario", "IdUsuario");
        }



        // GET: ProductoController/Delete/5 //Eliminar
        public ActionResult Delete(int IdCita)
        {
            var pEliminar = _apiService.deleteCita(IdCita);
            return RedirectToAction(nameof(Index));

        }
    }
}
