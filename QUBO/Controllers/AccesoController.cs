using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM modelo) 
        {
            Usuario? finded_user = await _dbContext.Usuarios
                                 .Where(u => u.NombreUsuario == modelo.usuario 
                                 && u.Contrasenia == modelo.contrasenia).FirstOrDefaultAsync();
            if (finded_user == null) 
            {
                ViewData["Mensaje"] = "Usuario o contraseña no encontrados";
                return View();
            }
            return RedirectToAction("Index","Home");
        }
    }
}
