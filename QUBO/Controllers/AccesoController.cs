using Microsoft.AspNetCore.Mvc;
using QUBO.Models;
using QUBO.ViewModels;

namespace QUBO.Controllers
{
    public class AccesoController : Controller
    {
        private readonly QuboDbContext _dbContext;
        public AccesoController(QuboDbContext quboDbContext) 
        {
            _dbContext = quboDbContext;
        }
        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario(UsuarioVM modelo)
        {
            if(modelo.Contrasenia != modelo.ConfimarContrasenia) 
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            Usuario user = new Usuario(){
                NombreUsuario =  modelo.NombreUsuario,
                RolUsuario =  modelo.RolUsuario,
                Contrasenia = modelo.Contrasenia
            };

            await _dbContext.Usuarios.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            if(user.IdUsuario != 0) {return RedirectToAction("Login", "Acceso");}
            ViewData["Mensaje"] = "Error: No se pudo crear usuario.";
            return View();
        }
    }
}
