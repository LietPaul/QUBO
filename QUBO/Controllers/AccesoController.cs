using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUBO.Models;
using QUBO.ViewModels;
using System.Security.Claims;

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

            var usuarioExistente = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Dni == modelo.DNI);
            if (usuarioExistente != null)
            {
                ViewData["Mensaje"] = "Error: Ya existe un usuario con el mismo DNI.";
                return View();
            }

            Usuario user = new Usuario()
            {
                Nombre = modelo.Nombre!.ToUpper(),
                Apellido = modelo.Apellido!.ToUpper(),
                Dni = modelo.DNI,
                Telefono = modelo.Telefono,
                RolUsuario = modelo.RolUsuario
            };
            user.Contrasenia = _passwordHasher.HashPassword(user, modelo.Contrasenia!);

            await _dbContext.Usuarios.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            if (user.IdUsuario != 0)
            {
                ViewData["Mensaje"] = "Usuario creado.";
            }
            else
            {
                ViewData["Mensaje"] = "Error: No se pudo crear usuario.";
            }

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
                                 .Where(u => u.Dni == modelo.usuario)
                                 .FirstOrDefaultAsync();

            if (findedUser == null)
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }

            var result = _passwordHasher.VerifyHashedPassword(findedUser, findedUser.Contrasenia!, modelo.contrasenia);
            if (result == PasswordVerificationResult.Failed)
            {
                ViewData["Mensaje"] = "Contraseña incorrecta";
                return View();
            }

            // Crear los claims de identidad
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, findedUser.Dni),
                new Claim(ClaimTypes.Role, findedUser.RolUsuario ?? string.Empty)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Para mantener la sesión después de cerrar el navegador si es necesario
            };

            // Iniciar sesión y emitir la cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // Guardar información adicional en la sesión si es necesario
            HttpContext.Session.SetString("RolUsuario", findedUser.RolUsuario ?? string.Empty);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Deslogear y eliminar la cookie de autenticación
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Limpiar todos los datos de la sesión
            HttpContext.Session.Clear();

            // Redirigir a la página de login
            return RedirectToAction("Login", "Acceso");
        }
    }
}
