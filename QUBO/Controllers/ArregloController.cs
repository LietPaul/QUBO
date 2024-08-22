using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QUBO.Models;

namespace QUBO.Controllers
{
    public class ArregloController : Controller
    {
        private readonly QuboDbContext _context;

        public ArregloController(QuboDbContext context)
        {
            _context = context;
        }

        // GET: Arreglo
        public async Task<IActionResult> Index(string? estado, bool? sinRep, string? searchString)
        {
            // Consulta base
            IQueryable<Arreglo> query = _context.Arreglos
                .Include(a => a.IdCelularNavigation)
                .Include(a => a.IdClienteNavigation)
                .Include(a => a.IdUsuarioNavigation);

            // Filtrar por estado
            if (!string.IsNullOrEmpty(estado) && estado != "Todos")
            {
                query = query.Where(a => a.Estado == estado);
            }

            // Filtrar por checkbox
            if (sinRep != true)
            {
                if (estado != "Sin reparacion")
                {
                    query = query.Where(a => a.Estado != "Sin reparacion");
                }
            }

            // Filtro por t�rmino de b�squeda
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(a =>
                    a.IdClienteNavigation!.Dni!.Contains(searchString) ||
                    a.IdCelularNavigation!.CodigoProducto!.Contains(searchString) ||
                    a.IdCelularNavigation!.Modelo!.Contains(searchString) ||
                    a.IdCelularNavigation!.Marca!.Contains(searchString) ||
                    a.IdUsuarioNavigation!.Nombre!.Contains(searchString) ||
                    a.IdUsuarioNavigation!.Apellido!.Contains(searchString) ||
                    a.Problema!.Contains(searchString));
            }

            // Ordenar por fecha de ingreso m�s reciente a m�s antigua
            query = query.OrderByDescending(a => a.FechaIng);

            var arreglos = await query.ToListAsync();
            return View(arreglos);
        }


        // GET: Arreglo/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arreglo = await _context.Arreglos
                .Include(a => a.IdCelularNavigation)
                .Include(a => a.IdClienteNavigation)
                .Include(a => a.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdArreglo == id);
            if (arreglo == null)
            {
                return NotFound();
            }

            return View(arreglo);
        }

        // GET: Arreglo/Create
        public IActionResult Create()
        {
            // Filtrar los usuarios con el rol 'TECNICO'
            var tecnicos = _context.Usuarios
                .Where(u => u.RolUsuario == "TECNICO")
                .Select(u => new
                {
                    IdUsuario = u.IdUsuario,
                    NombreCompleto = u.Nombre + " " + u.Apellido
                })
                .ToList();

            ViewData["IdCelular"] = new SelectList(_context.Celulars, "IdCelular", "IdCelular");
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            ViewData["IdUsuario"] = new SelectList(tecnicos, "IdUsuario", "NombreCompleto");
            return View();
        }


        // POST: Arreglo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdArreglo,IdCliente,IdCelular,Problema,FechaIng,FechaEnt,Senia,Total,IdUsuario,Estado")] Arreglo arreglo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(arreglo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCelular"] = new SelectList(_context.Celulars, "IdCelular", "IdCelular", arreglo.IdCelular);
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", arreglo.IdCliente);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", arreglo.IdUsuario);
            return View(arreglo);
        }

        // GET: Arreglo/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arreglo = await _context.Arreglos.FindAsync(id);
            if (arreglo == null)
            {
                return NotFound();
            }

            // Cargar los valores de ClienteDni y CelularCodigo VER SI SE PUEDE HACER CON RAZOR
            var cliente = await _context.Clientes.FindAsync(arreglo.IdCliente);
            var celular = await _context.Celulars.FindAsync(arreglo.IdCelular);

            ViewData["ClienteDni"] = cliente?.Dni ?? string.Empty;
            ViewData["CelularCodigo"] = celular?.CodigoProducto ?? string.Empty;

            // Filtrar los usuarios con el rol 'TECNICO'
            var tecnicos = _context.Usuarios
                .Where(u => u.RolUsuario == "TECNICO")
                .Select(u => new
                {
                    IdUsuario = u.IdUsuario,
                    NombreCompleto = u.Nombre + " " + u.Apellido
                })
                .ToList();
            
            ViewData["IdCelular"] = new SelectList(_context.Celulars, "IdCelular", "IdCelular", arreglo.IdCelular);
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", arreglo.IdCliente);
            ViewData["IdUsuario"] = new SelectList(tecnicos, "IdUsuario", "NombreCompleto", arreglo.IdUsuario);
            return View(arreglo);
        }

        // POST: Arreglo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdArreglo,IdCliente,IdCelular,Problema,FechaIng,FechaEnt,Senia,Total,IdUsuario,Estado")] Arreglo arreglo)
        {
            if (id != arreglo.IdArreglo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arreglo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArregloExists(arreglo.IdArreglo))
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
            ViewData["IdCelular"] = new SelectList(_context.Celulars, "IdCelular", "IdCelular", arreglo.IdCelular);
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", arreglo.IdCliente);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", arreglo.IdUsuario);
            return View(arreglo);
        }

        // GET: Arreglo/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arreglo = await _context.Arreglos
                .Include(a => a.IdCelularNavigation)
                .Include(a => a.IdClienteNavigation)
                .Include(a => a.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdArreglo == id);
            if (arreglo == null)
            {
                return NotFound();
            }

            return View(arreglo);
        }

        // POST: Arreglo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var arreglo = await _context.Arreglos.FindAsync(id);
            if (arreglo != null)
            {
                _context.Arreglos.Remove(arreglo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArregloExists(long id)
        {
            return _context.Arreglos.Any(e => e.IdArreglo == id);
        }
    }
}
