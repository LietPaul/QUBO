using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUBO.Models;
using QUBO.ViewModels;

namespace QUBO.Controllers
{
    public class AccesoController : Controller
    {
        private readonly QuboDbContext _dbContext;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        public AccesoController(QuboDbContext quboDbContext, IPasswordHasher<Usuario> passwordHasher)
        {
            _dbContext = quboDbContext;
            _passwordHasher = passwordHasher;
        }
        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario(UsuarioVM modelo)
        {
            if (modelo.Contrasenia != modelo.ConfimarContrasenia)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            Usuario user = new Usuario()
            {
                NombreUsuario = modelo.NombreUsuario,
                RolUsuario = modelo.RolUsuario
            };
            user.Contrasenia = _passwordHasher.HashPassword(user, modelo.Contrasenia);

            await _dbContext.Usuarios.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            if (user.IdUsuario != 0) { return RedirectToAction("Login", "Acceso"); }
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
            if (string.IsNullOrEmpty(modelo.contrasenia) || string.IsNullOrEmpty(modelo.usuario))
            {
                ViewData["Mensaje"] = "Por favor ingrese usuario y contraseña para acceder.";
                return View();
            }
            Usuario? findedUser = await _dbContext.Usuarios
                                 .Where(u => u.NombreUsuario == modelo.usuario).FirstOrDefaultAsync();
            if (findedUser == null)
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }
            
            var result = _passwordHasher.VerifyHashedPassword(findedUser, findedUser.Contrasenia, modelo.contrasenia);
            if (result == PasswordVerificationResult.Failed)
            {
                ViewData["Mensaje"] = "Contraseña incorrecta";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
