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
    public class CelularController : Controller
    {
        private readonly QuboDbContext _context;

        public CelularController(QuboDbContext context)
        {
            _context = context;
        }
        //Get Celular por producto
        [HttpGet]
        public async Task<JsonResult> GetCelularesByCod(string term)
        {
            var celulares = await _context.Celulars
                .Where(c => c.CodigoProducto!.Contains(term))
                .Select(c => new { c.IdCelular, c.CodigoProducto })
                .ToListAsync();

            return Json(celulares);
        }

        //Partial
        public IActionResult CreatePartial()
        {
            return PartialView("_CelularPartial", new Celular());
        }
        // GET: Celular
        public async Task<IActionResult> Index()
        {
            return View(await _context.Celulars.ToListAsync());
        }

        // GET: Celular/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var celular = await _context.Celulars
                .FirstOrDefaultAsync(m => m.IdCelular == id);
            if (celular == null)
            {
                return NotFound();
            }

            return View(celular);
        }

        // GET: Celular/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Celular/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCelular,Modelo,Marca,CodigoProducto,PrecioUsd")] Celular celular)
        {
            if (ModelState.IsValid)
            {
                _context.Add(celular);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(celular);
        }

        // GET: Celular/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var celular = await _context.Celulars.FindAsync(id);
            if (celular == null)
            {
                return NotFound();
            }
            return View(celular);
        }

        // POST: Celular/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdCelular,Modelo,Marca,CodigoProducto,PrecioUsd")] Celular celular)
        {
            if (id != celular.IdCelular)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(celular);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CelularExists(celular.IdCelular))
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
            return View(celular);
        }

        // GET: Celular/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var celular = await _context.Celulars
                .FirstOrDefaultAsync(m => m.IdCelular == id);
            if (celular == null)
            {
                return NotFound();
            }

            return View(celular);
        }

        // POST: Celular/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var celular = await _context.Celulars.FindAsync(id);
            if (celular != null)
            {
                _context.Celulars.Remove(celular);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CelularExists(long id)
        {
            return _context.Celulars.Any(e => e.IdCelular == id);
        }
    }
}
