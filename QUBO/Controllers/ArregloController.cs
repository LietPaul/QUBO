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
        public async Task<IActionResult> Index()
        {
            var quboDbContext = _context.Arreglos.Include(a => a.IdCelularNavigation).Include(a => a.IdClienteNavigation).Include(a => a.IdUsuarioNavigation);
            return View(await quboDbContext.ToListAsync());
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
            ViewData["IdCelular"] = new SelectList(_context.Celulars, "IdCelular", "IdCelular");
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre");
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
            ViewData["IdCelular"] = new SelectList(_context.Celulars, "IdCelular", "IdCelular", arreglo.IdCelular);
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", arreglo.IdCliente);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", arreglo.IdUsuario);
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", arreglo.IdUsuario);
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
