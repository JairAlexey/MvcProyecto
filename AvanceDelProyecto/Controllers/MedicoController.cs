using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AvanceDelProyecto.Models;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Security.Cryptography;
using AvanceDelProyecto.Services;
using Microsoft.EntityFrameworkCore;

namespace AvanceDelProyecto.Controllers
{
    public class MedicoController : Controller
    {

        // Dependencia para comunicarse con la API.
        private readonly IApiService _apiService;

        public MedicoController(IApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: MedicoController  
        public async Task<IActionResult> Index()
        {
            try
            {
                var medico = await _apiService.getMedico();
                return View(medico);
            }
            catch (Exception e)
            {
                return View(new List<Medico>());
            }
        }

        // GET: MedicoController/Details/5 //ListarDatos
        public async Task<ActionResult> Details(int IdMedico)
        {
            var medico = await _apiService.getMedico(IdMedico);
            if (medico != null)
            {
                return View(medico);
            }
            return RedirectToAction("Index");
        }

        // GET: MedicoController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MedicoController/Create //Crear Nuevo
        [HttpPost]
        public async Task<IActionResult> Create(Medico medico)
        {
            var result = await _apiService.addMedico(medico);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        // GET: MedicoController/Edit/5
        public async Task<IActionResult> Edit(int IdMedico)
        {
            var medico = await _apiService.getMedico(IdMedico);
            if (medico != null)
            {
                return View(medico);
            }
            return RedirectToAction("Index");
        }

        // POST: MedicoController/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Medico medico)
        {
            var pAEditar = await _apiService.updateMedico(medico.IdMedico, medico);
            if (pAEditar != null)
            {
                return RedirectToAction(nameof(Index)); //Nameof se utiliza para obtener el nombre de una variable, tipo o miembro de una clase como una cadena en tiempo de compilación. 
            }
            return View(pAEditar);
        }


        // GET: MedicoController/Delete/5 //Eliminar
        public ActionResult Delete(int IdMedico)
        {
            var pEliminar = _apiService.deleteMedico(IdMedico);
            return RedirectToAction(nameof(Index));

        }
    }
}
