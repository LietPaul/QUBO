using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QUBO.Models;
using Microsoft.AspNetCore.Authorization;

namespace QUBO.Controllers
{
    [Authorize]
    public class ParteController : Controller
    {
        private readonly QuboDbContext _context;

        public ParteController(QuboDbContext context)
        {
            _context = context;
        }

        // GET: Parte
        public async Task<IActionResult> Index(long? id)
        {
            var quboDbContext = _context.Partes.Include(p => p.IdCelularNavigation)
                                .Where(p => p.IdCelularNavigation!.IdCelular == id);
            if (id == null)
            {
                quboDbContext = _context.Partes.Include(p => p.IdCelularNavigation);
            }
             
            return View(await quboDbContext.ToListAsync());
        }

        // GET: Parte/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parte = await _context.Partes
                .Include(p => p.IdCelularNavigation)
                .FirstOrDefaultAsync(m => m.IdPartes == id);
            if (parte == null)
            {
                return NotFound();
            }

            return View(parte);
        }
        public IActionResult Create(long? id)
        {
            ViewBag.IdExists = false;
            if (id != null )
            {
                ViewBag.IdExists = true;
                ViewBag.IdCelular = id;
            }
            else
            {
                ViewData["IdCelular"] = new SelectList(_context.Celulars, "IdCelular", "Modelo");
            }
            return View();
        }

        // POST: Parte/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(long? id,[Bind("IdPartes,Nombre,Modelo,PrecioUsd,CodigoProducto,IdCelular")] Parte parte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parte);
                await _context.SaveChangesAsync();
                if (id != null)
                {
                    return RedirectToAction("Create","DetalleArreglo", new { id });
                }
                else { 
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["IdCelular"] = new SelectList(_context.Celulars, "IdCelular", "Modelo", parte.IdCelular);
            return View(parte);
        }

        // GET: Parte/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parte = await _context.Partes.FindAsync(id);
            if (parte == null)
            {
                return NotFound();
            }
            ViewData["IdCelular"] = new SelectList(_context.Celulars, "IdCelular", "Modelo", parte.IdCelular);
            return View(parte);
        }

        // POST: Parte/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdPartes,Nombre,Modelo,PrecioUsd,CodigoProducto,IdCelular")] Parte parte)
        {
            if (id != parte.IdPartes)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParteExists(parte.IdPartes))
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
            ViewData["IdCelular"] = new SelectList(_context.Celulars, "IdCelular", "Modelo", parte.IdCelular);
            return View(parte);
        }

        // GET: Parte/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parte = await _context.Partes
                .Include(p => p.IdCelularNavigation)
                .FirstOrDefaultAsync(m => m.IdPartes == id);
            if (parte == null)
            {
                return NotFound();
            }

            return View(parte);
        }

        // POST: Parte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var parte = await _context.Partes.FindAsync(id);
            if (parte != null)
            {
                _context.Partes.Remove(parte);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParteExists(long id)
        {
            return _context.Partes.Any(e => e.IdPartes == id);
        }
    }
}
