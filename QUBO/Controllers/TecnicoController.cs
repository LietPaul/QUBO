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
    public class TecnicoController : Controller
    {
        private readonly QuboDbContext _context;

        public TecnicoController(QuboDbContext context)
        {
            _context = context;
        }
        
        // GET: Tecnico
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tecnicos.ToListAsync());
        }

        // GET: Tecnico/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnico = await _context.Tecnicos
                .FirstOrDefaultAsync(m => m.IdTecnico == id);
            if (tecnico == null)
            {
                return NotFound();
            }

            return View(tecnico);
        }

        // GET: Tecnico/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tecnico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTecnico,Dni,Nombre")] Tecnico tecnico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tecnico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tecnico);
        }

        // GET: Tecnico/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnico = await _context.Tecnicos.FindAsync(id);
            if (tecnico == null)
            {
                return NotFound();
            }
            return View(tecnico);
        }

        // POST: Tecnico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdTecnico,Dni,Nombre")] Tecnico tecnico)
        {
            if (id != tecnico.IdTecnico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tecnico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TecnicoExists(tecnico.IdTecnico))
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
            return View(tecnico);
        }

        // GET: Tecnico/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnico = await _context.Tecnicos
                .FirstOrDefaultAsync(m => m.IdTecnico == id);
            if (tecnico == null)
            {
                return NotFound();
            }

            return View(tecnico);
        }

        // POST: Tecnico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tecnico = await _context.Tecnicos.FindAsync(id);
            if (tecnico != null)
            {
                _context.Tecnicos.Remove(tecnico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TecnicoExists(long id)
        {
            return _context.Tecnicos.Any(e => e.IdTecnico == id);
        }
    }
}
