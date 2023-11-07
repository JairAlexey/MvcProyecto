using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AvanceDelProyecto.Models;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Security.Cryptography;
using AvanceDelProyecto.Services;

namespace AvanceDelProyecto.Controllers
{
    public class AccesoController : Controller
    {

        // Dependencia para comunicarse con la API.
        private readonly IApiService _apiService;

        // Constructor que inyecta el servicio de la API.
        public AccesoController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public ActionResult Login()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        // GET: UsuarioController  
        public async Task<IActionResult> Index()
        {
            try
            {
                var usaurio = await _apiService.getUsuario();
                return View(usaurio);
            }
            catch (Exception e)
            {
                return View(new List<User>());
            }
        }

        // GET: UsuarioController/Details/5 //ListarDatos
        public async Task<ActionResult> Details(int IdUsuario)
        {
            var usuario = await _apiService.getUsuario(IdUsuario);
            if (usuario != null)
            {
                return View(usuario);
            }
            return RedirectToAction("Index");
        }

        // GET: UsuarioController/Create 
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create //Crear Nuevo
        [HttpPost]
        public async Task<IActionResult> Create(User usuario)
        {
            var result = await _apiService.addUsuario(usuario);
            if (result)
            {
                return RedirectToAction(nameof(Index));//Para los errores sirve el namaof (muestra los errores)
            }
            return View(usuario);
        }

        // GET: UsuarioController/Edit/5
        public async Task<IActionResult> Edit(int IdUsuario)
        {
            var usuario = await _apiService.getUsuario(IdUsuario);
            if (usuario != null)
            {
                return View(usuario);
            }
            return RedirectToAction("Index");
        }

        // POST: UsuarioController/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(User usuario)
        {
            var pAEditar = await _apiService.updateUsuario(usuario.IdUsuario, usuario);
            if (pAEditar != null)
            {
                return RedirectToAction(nameof(Index)); //Nameof se utiliza para obtener el nombre de una variable, tipo o miembro de una clase como una cadena en tiempo de compilación. 
            }
            return View(pAEditar);
        }


        // GET: ProductoController/Delete/5 //Eliminar
        public ActionResult Delete(int IdUsuario)
        {
            var pEliminar = _apiService.deleteUsuario(IdUsuario);
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Login(InicioSesion inicioSesion)
        {
            try
            {
                var result = await _apiService.IniciarSesion(inicioSesion);

                if (result)
                {
                    // Si el inicio de sesión es exitoso, redirige al usuario a la página de inicio
                    return RedirectToAction("Index", "Home"); // Reemplaza con la ruta de tu página de inicio
                }
                else
                {
                    // Si el inicio de sesión falla, muestra un mensaje de error en la vista
                    TempData["ErrorMessage"] = "Credenciales inválidas. Intente nuevamente.";
                    return View(inicioSesion);
                }
            }
            catch (Exception e)
            {
                // Maneja la excepción según sea necesario
                TempData["ErrorMessage"] = "Error en el servidor. Intente nuevamente más tarde.";
                return View(inicioSesion);
            }
        }


    }

}
