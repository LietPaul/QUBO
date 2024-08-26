using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUBO.Models;
using Microsoft.AspNetCore.Authorization;

namespace QUBO.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly QuboDbContext _context;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        public UsuariosController(QuboDbContext context, IPasswordHasher<Usuario> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Contrasenia,RolUsuario,Apellido,Dni,Telefono")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Verificar si ya existe un usuario con el mismo DNI
                var existingUserByDni = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Dni == usuario.Dni);

                if (existingUserByDni != null)
                {
                    ViewData["Mensaje"] = "Error: Ya existe un usuario con el mismo DNI.";
                    return View();
                }

                // Verificar si ya existe un usuario con la misma contraseña
                var allUsers = await _context.Usuarios.ToListAsync();
                foreach (var user in allUsers)
                {
                    var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Contrasenia!, usuario.Contrasenia!);
                    if (verificationResult == PasswordVerificationResult.Success)
                    {
                        ViewData["Mensaje"] = "Error: Ya existe un usuario con la misma contraseña.";
                        return View();
                    }
                }

                // Hashear la contraseña y guardar el usuario
                usuario.Contrasenia = _passwordHasher.HashPassword(usuario, usuario.Contrasenia!);
                _context.Add(usuario);
                await _context.SaveChangesAsync();

                // Verificar si el usuario fue creado exitosamente
                if (usuario.IdUsuario != 0)
                {
                    ViewData["Mensaje"] = "Usuario creado exitosamente.";
                }
                else
                {
                    ViewData["Mensaje"] = "Error: No se pudo crear el usuario.";
                }
                return View();
            }
            return View();
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdUsuario,Nombre,Contrasenia,RolUsuario,Apellido,Dni,Telefono")] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(long id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
