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
    public class DetalleArregloController : Controller
    {
        private readonly QuboDbContext _context;

        public DetalleArregloController(QuboDbContext context)
        {
            _context = context;
        }

        // GET: DetalleArreglo
        public async Task<IActionResult> Index()
        {
            var quboDbContext = _context.DetalleArreglos.Include(d => d.IdArregloNavigation).Include(d => d.IdParteNavigation);
            return View(await quboDbContext.ToListAsync());
        }

        // GET: DetalleArreglo/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleArreglo = await _context.DetalleArreglos
                .Include(d => d.IdArregloNavigation)
                .Include(d => d.IdParteNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalle == id);
            if (detalleArreglo == null)
            {
                return NotFound();
            }

            return View(detalleArreglo);
        }

        // GET: DetalleArreglo/Create
        public IActionResult Create()
        {
            ViewData["IdArreglo"] = new SelectList(_context.Arreglos, "IdArreglo", "IdArreglo");
            ViewData["IdParte"] = new SelectList(_context.Partes, "IdPartes", "IdPartes");
            return View();
        }

        // POST: DetalleArreglo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalle,IdArreglo,IdParte,Descripcion")] DetalleArreglo detalleArreglo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleArreglo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdArreglo"] = new SelectList(_context.Arreglos, "IdArreglo", "IdArreglo", detalleArreglo.IdArreglo);
            ViewData["IdParte"] = new SelectList(_context.Partes, "IdPartes", "IdPartes", detalleArreglo.IdParte);
            return View(detalleArreglo);
        }

        // GET: DetalleArreglo/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleArreglo = await _context.DetalleArreglos.FindAsync(id);
            if (detalleArreglo == null)
            {
                return NotFound();
            }
            ViewData["IdArreglo"] = new SelectList(_context.Arreglos, "IdArreglo", "IdArreglo", detalleArreglo.IdArreglo);
            ViewData["IdParte"] = new SelectList(_context.Partes, "IdPartes", "IdPartes", detalleArreglo.IdParte);
            return View(detalleArreglo);
        }

        // POST: DetalleArreglo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdDetalle,IdArreglo,IdParte,Descripcion")] DetalleArreglo detalleArreglo)
        {
            if (id != detalleArreglo.IdDetalle)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleArreglo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleArregloExists(detalleArreglo.IdDetalle))
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
            ViewData["IdArreglo"] = new SelectList(_context.Arreglos, "IdArreglo", "IdArreglo", detalleArreglo.IdArreglo);
            ViewData["IdParte"] = new SelectList(_context.Partes, "IdPartes", "IdPartes", detalleArreglo.IdParte);
            return View(detalleArreglo);
        }

        // GET: DetalleArreglo/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleArreglo = await _context.DetalleArreglos
                .Include(d => d.IdArregloNavigation)
                .Include(d => d.IdParteNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalle == id);
            if (detalleArreglo == null)
            {
                return NotFound();
            }

            return View(detalleArreglo);
        }

        // POST: DetalleArreglo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var detalleArreglo = await _context.DetalleArreglos.FindAsync(id);
            if (detalleArreglo != null)
            {
                _context.DetalleArreglos.Remove(detalleArreglo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleArregloExists(long id)
        {
            return _context.DetalleArreglos.Any(e => e.IdDetalle == id);
        }
    }
}
