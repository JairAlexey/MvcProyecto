using AvanceDelProyecto.Models;
using AvanceDelProyecto.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvanceDelProyecto.Controllers
{
    public class RoleController : Controller
    {
        private readonly IApiService _apiService;

        public RoleController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var roles = await _apiService.getRole();
                return View(roles);
            }
            catch (Exception e)
            {
                return View(new List<Role>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Role role)
        {
            var result = await _apiService.addRole(role);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }


        // GET: RoleController/Edit/5
        public async Task<IActionResult> Edit(int RoleId)
        {
            var role = await _apiService.getRole(RoleId);
            if (role != null)
            {
                return View(role);
            }
            return RedirectToAction("Index");
        }

        // POST: RoleController/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Role role)
        {
            var pAEditar = await _apiService.updateRole(role.RoleId, role);
            if (pAEditar != null)
            {
                return RedirectToAction(nameof(Index)); //Nameof se utiliza para obtener el nombre de una variable, tipo o miembro de una clase como una cadena en tiempo de compilación. 
            }
            return View(pAEditar);
        }


        // GET: RoleController/Delete/5 //Eliminar
        public ActionResult Delete(int RoleId)
        {
            var pEliminar = _apiService.deleteRole(RoleId);
            return RedirectToAction(nameof(Index));

        }
    }
}
